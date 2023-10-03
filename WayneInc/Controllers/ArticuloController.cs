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
using System.IO;
using System.Collections;
using System;
using WayneInc.Entitys.DTO;
using System.Text;

namespace WayneInc.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArticuloController : ControllerBase
    {
        private readonly IArticuloRepository _ArticuloRepository;

        public ArticuloController(IArticuloRepository ArticuloRepository)
        {
            _ArticuloRepository = ArticuloRepository;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            return Ok(await _ArticuloRepository.GetById(id));
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            List<ArticuloDTO> dtos = new List<ArticuloDTO>();
            var articulos = await _ArticuloRepository.GetAll();

            foreach(var articulo in articulos)
            {

                string s = Convert.ToBase64String(articulo.imagen);
                ArticuloDTO dto = new ArticuloDTO()
                {
                    idArticulo = articulo.id,
                    descripcion = articulo.descripcion,
                    codigo = articulo.codigo,
                    stock = articulo.stock,
                    precio = articulo.precio,
                    imagen = s

                };

                dtos.Add(dto);
            }


            return Ok(dtos);
        }


        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Post(ArticuloDTO temp)
        {
            if (!ModelState.IsValid)
            {
                return BadRequestModelState();
            }
             
            Articulo articulo = new Articulo()
                {
                    descripcion = temp.descripcion,
                    codigo = temp.codigo,
                    stock = temp.stock,
                    precio = temp.precio,
                    imagen = Encoding.ASCII.GetBytes(temp.imagen)

            };
                await _ArticuloRepository.Create(articulo);
            
            return Ok();
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, Articulo Articulo)
        {
            if (id != Articulo.id)
            {
                return BadRequest();
            }

            await _ArticuloRepository.Update(Articulo);

            return Ok();
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (await _ArticuloRepository.Delete(id) == 0)
                return BadRequest("El Articulo no existe");
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
