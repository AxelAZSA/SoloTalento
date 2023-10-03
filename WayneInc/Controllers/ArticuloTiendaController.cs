using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WayneInc.Bussiness.Repository;
using WayneInc.Entitys.Response;
using WayneInc.Entitys;
using WayneInc.Bussiness.Service;

namespace WayneInc.Controllers
{
    [Route("api/Inventario")]
    [ApiController]
    public class ArticuloTiendaController : ControllerBase
    {

        private readonly ProcesoInventario _procesoInventario;

        public ArticuloTiendaController(ProcesoInventario procesoInventario)
        {
            _procesoInventario = procesoInventario;
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> GetAll() 
        { 
            return Ok(await _procesoInventario.GetAll());
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("{idTienda}")]
        public async Task<IActionResult> GetByTienda(int idTienda)
        {
            return Ok(await _procesoInventario.GetByTienda(idTienda));
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Post(RequestStock request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequestModelState();
            }

            await _procesoInventario.declararStock(request.idArticulo, request.idTienda, request.stock);

            return Ok("Inventario Actualizado");
        }

        public class RequestStock
        {
           public int idArticulo { get; set; }
            public int idTienda { get; set; }
            public int stock { get; set; }
        }

        private IActionResult BadRequestModelState()
        {
            IEnumerable<string> errors = ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage));
            return BadRequest(new ErrorResponse(errors));
        }
    }
}
