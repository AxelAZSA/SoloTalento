using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using WayneInc.Bussiness.Repository;
using WayneInc.Entitys.Response;
using WayneInc.Entitys;
using System.Data;
using WayneInc.Bussiness.Service;

namespace WayneInc.Controllers
{
    [Route("api/Compras")]
    [ApiController]
    public class ArticuloClienteController : ControllerBase
    {

        private readonly IArticuloClienteRepository _ArticuloClienteRepository;
        private readonly ProcesoCompra _procesoCompra;

        public ArticuloClienteController(IArticuloClienteRepository ArticuloClienteRepository, ProcesoCompra procesoCompra)
        {
            _ArticuloClienteRepository = ArticuloClienteRepository;
            _procesoCompra = procesoCompra;
        }

        [Authorize(Roles = "Cliente")]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _procesoCompra.obtenerCompras());
        }

        [Authorize(Roles = "Cliente")]
        [HttpGet("Carrito")]
        public async Task<IActionResult> getCarrito()
        {
            string rawUserId = HttpContext.User.FindFirstValue("idCliente");
            if (!int.TryParse(rawUserId, out int clienteId))
            {
                return Unauthorized();
            }

            return Ok(await _procesoCompra.getItems(clienteId));
        }

        [Authorize(Roles = "Cliente")]
        [HttpPost("Carrito/agregar/{idArticulo}")]
        public async Task<IActionResult> Post(int idArticulo)
        {
            string rawUserId = HttpContext.User.FindFirstValue("idCliente");
            if (!int.TryParse(rawUserId, out int clienteId))
            {
                return Unauthorized();
            }
            await _procesoCompra.AddItem(idArticulo, clienteId);

            return Ok("Agregado con exito");
        }

        [Authorize(Roles = "Cliente")]
        [HttpPost("Carrito/remover")]
        public async Task<IActionResult> delete(requestRemove request)
        {

            await _procesoCompra.RemoveItem(request.idCarritoItem,request.idCarrito);

            return Ok("Removido con exito");
        }

        public class requestRemove
        {
            public int idCarritoItem { get; set; }
            public int idCarrito { get; set; }
        }

        [Authorize(Roles = "Cliente")]
        [HttpPost("Comprar/{idCarrito}")]
        public async Task<IActionResult> Compra(int idCarrito)
        {
            string rawUserId = HttpContext.User.FindFirstValue("idCliente");
            if (!int.TryParse(rawUserId, out int clienteId))
            {
                return Unauthorized();
            }
            await _procesoCompra.ComprarArticulos(idCarrito,clienteId);

            return Ok("Cmmprado");
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, ArticuloCliente articulo)
        {
            if (id != articulo.id)
            {
                return BadRequest();
            }

            await _ArticuloClienteRepository.Update(articulo);

            return Ok();
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (await _ArticuloClienteRepository.Delete(id) == 0)
                return BadRequest("El articulo no existe");
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
