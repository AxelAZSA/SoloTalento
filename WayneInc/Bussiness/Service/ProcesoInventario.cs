using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WayneInc.Bussiness.Repository;
using WayneInc.Entitys;
using WayneInc.Entitys.DTO;

namespace WayneInc.Bussiness.Service
{

    //Servicio para las operaciones de inventario
    public class ProcesoInventario
    {
        private readonly IArticuloRepository _ArticuloRepository;
        private readonly IArticuloTiendaRepository _ArticuloTiendaRepository;

        public ProcesoInventario(IArticuloRepository articuloRepository, IArticuloTiendaRepository articuloTiendaRepository)
        {
            _ArticuloRepository = articuloRepository;
            _ArticuloTiendaRepository = articuloTiendaRepository;
        }

        //Obtiene todos los articulos de inventario
        public async Task<List<stockDTO>> GetAll()
        {
            var articulosT = await _ArticuloTiendaRepository.GetAll();
            return await obtenerStock(articulosT);
        }

        public async Task<List<stockDTO>> obtenerStock(List<ArticuloTienda> articulosT)
        {
            List<stockDTO> stocks = new List<stockDTO>();
            foreach(var articuloT in articulosT)
            {
                var articulo = await _ArticuloRepository.GetById(articuloT.idArticulo);
                string s = Convert.ToBase64String(articulo.imagen);
                stockDTO stock = new stockDTO()
                {
                    idTienda = articuloT.idTienda,
                    articuloDescripcion = articulo.descripcion,
                    articuloCode = articulo.codigo,
                    fecha = articuloT.fecha,
                    imagen = s
                };
                stocks.Add(stock);
            }
            return stocks;
        }

        //Filtra los articulos por tienda
        public async Task<List<stockDTO>> GetByTienda(int idTienda)
        {
            var articulosT = await _ArticuloTiendaRepository.GetByTienda(idTienda);
            return await obtenerStock(articulosT);
        }

        //Sube la cantidad de articulos con las que se cuenta
        public async Task declararStock(int idArticulo, int idTienda, int stock)
        {
            for(int i = 0; i<stock; i++)
            {
                ArticuloTienda articulo = new ArticuloTienda()
                {
                    idArticulo = idArticulo,
                    idTienda = idTienda,
                    fecha = DateTime.UtcNow
                };

                await _ArticuloTiendaRepository.Create(articulo);
            }
            await ActualizarStock(idArticulo);
        }

        public async Task procesarCompra(int idArticulo,int cantidad)
        {
            var articulo = await _ArticuloRepository.GetById(idArticulo);

            articulo.stock = articulo.stock - cantidad;

            await _ArticuloTiendaRepository.DeleteArticuloByCantidad(idArticulo, cantidad);

            await ActualizarStock(idArticulo);

        }

        //Actualiza la cantidad en existencia
        public async Task ActualizarStock(int idArticulo)
        {
            var articulo = await _ArticuloRepository.GetById(idArticulo);
        
            int stock = await _ArticuloTiendaRepository.GetStock(articulo.id);

            articulo.stock = stock;

            await _ArticuloRepository.Update(articulo);
        }

    }
}
