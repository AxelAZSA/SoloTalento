using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using WayneInc.Bussiness.Repository;
using WayneInc.Entitys;
using WayneInc.Entitys.Response;

namespace WayneInc.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClienteController : ControllerBase
    {

        private readonly IClienteRepository _ClienteRepository;

        public ClienteController(IClienteRepository ClienteRepository)
        {
            _ClienteRepository = ClienteRepository;
        }

        [Authorize(Roles = "Cliente")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            return Ok(await _ClienteRepository.GetById(id));
        }
        [Authorize(Roles = "Cliente")]
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            string rawUserId = HttpContext.User.FindFirstValue("idCliente");
            if (!int.TryParse(rawUserId, out int clienteId))
            {
                return Unauthorized();
            }
            return Ok(await _ClienteRepository.GetById(clienteId));
        }
        [HttpPost]
            public async Task<IActionResult> Post(Cliente cliente)
            {
                if (!ModelState.IsValid)
                {
                    return BadRequestModelState();
                }

                int id  = await _ClienteRepository.Create(cliente);

                return Ok(id);
            }

        [Authorize(Roles = "Cliente")]
        [HttpPut("{id}")]
            public async Task<IActionResult> Put(int id, Cliente cliente)
            {
                if (id != cliente.id)
                {
                    return BadRequest();
                }

                await _ClienteRepository.Update(cliente);

                return Ok();
            }

        [Authorize]
        [HttpDelete("{id}")]
            public async Task<IActionResult> Delete(int id)
            {
                if (await _ClienteRepository.Delete(id) == 0)
                    return BadRequest("El cliente no existe");
                else
                    return Ok("Registro eliminado con éxito");
            }

            private IActionResult BadRequestModelState()
            {
                IEnumerable<string> errors = ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage));
                return BadRequest(new ErrorResponse(errors));
            }
        }
    }

