using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WayneInc.Entitys.Response;
using WayneInc.Entitys;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using WayneInc.Bussiness.TokenService;
using WayneInc.Bussiness.Repository;
using Microsoft.AspNetCore.Identity;
using WayneInc.Bussiness.AuthenticationService;
using WayneInc.Bussiness.Service;
using WayneInc.Entitys.Tokens;

namespace WayneInc.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SesionController : ControllerBase
    {
        private const string role = "Cliente";
        private readonly ISesionRepository _SesionRepository;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IRefreshTokenRepository _refreshTokenRepository;
        private readonly RefreshTokenValidator _refreshTokenValidator;
        private readonly Authenticator _authenticator;

        public SesionController(ISesionRepository SesionRepository, IPasswordHasher passwordHasher, IRefreshTokenRepository refreshTokenRepository, Authenticator authenticator, RefreshTokenValidator refreshTokenValidator)
        {
            _SesionRepository = SesionRepository;
            _passwordHasher = passwordHasher;
            _authenticator = authenticator;
            _refreshTokenValidator = refreshTokenValidator;
            _refreshTokenRepository = refreshTokenRepository;
        }

        [AllowAnonymous]
        [HttpGet("{idCliente}")]
        public async Task<IActionResult> GetByCliente(int idCliente)
        {
            return Ok(await _SesionRepository.GetByCliente(idCliente));
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] Sesion sesion)
        {
            if (!ModelState.IsValid)
            {
                return BadRequestModelState();
            }

            var existingUser = await _SesionRepository.GetByCorreo(sesion.correo);

            if (existingUser != null)
            {
                return Conflict(new ErrorResponse("Ya hay una cuenta con ese correo"));
            }


            Login login = new Login()
            {
                correo = sesion.correo,
                password = sesion.password
            };

            sesion.password = _passwordHasher.Hash(sesion.password);

            await _SesionRepository.Create(sesion);

            return await Login(login);
        }


        [AllowAnonymous]
        [HttpPost("edit/{id}")]
        public async Task<IActionResult> Put(int id, Sesion sesion)
        {
            if (id!=sesion.id)
            {
                return BadRequest();
            }

            await _SesionRepository.Update(sesion);

            return Ok("Actualizado con exito");
        }


        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] Login login)
        {
            if (!ModelState.IsValid)
            {
                return BadRequestModelState();
            }

            var existingUser = await _SesionRepository.GetByCorreo(login.correo);

            if (existingUser == null)
            {
                return Unauthorized();
            }

            return Ok(await _authenticator.Authentication(existingUser));
        }

        [Authorize]
        [HttpDelete("logout")]
        public async Task<IActionResult> Logout()
        {
            string rawUserId = HttpContext.User.FindFirstValue("id");
            if (!int.TryParse(rawUserId, out int userId))
            {
                return Unauthorized();
            }
            await _refreshTokenRepository.DeleteAll(userId, role);
            return NoContent();
        }

        [HttpPost("refresh")]
        public async Task<IActionResult> Refresh([FromBody] RefreshRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequestModelState();
            }
            bool validateToken = _refreshTokenValidator.Validate(request.RefreshToken);
            if (!validateToken)
            {
                return BadRequest(new ErrorResponse("Invalid Refresh Token"));
            }
            RefreshToken refreshTokenDTO = await _refreshTokenRepository.GetByToken(request.RefreshToken, role);
            if (refreshTokenDTO == null)
            {
                return NotFound(new ErrorResponse("Invalid refresh token"));
            }
            await _refreshTokenRepository.DeleteRefreshToken(refreshTokenDTO.id);
            Sesion sesion = await _SesionRepository.GetById(refreshTokenDTO.idSesion);
            if (sesion == null)
            {
                return NotFound(new ErrorResponse("User doesn´t exist"));
            }

            return Ok(await _authenticator.Authentication(sesion));
        }

        private IActionResult BadRequestModelState()
        {
            IEnumerable<string> errors = ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage));
            return BadRequest(new ErrorResponse(errors));
        }
    }
}
