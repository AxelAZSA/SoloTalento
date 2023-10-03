using System.Threading.Tasks;
using System;
using WayneInc.Bussiness.Repository;
using WayneInc.Entitys;
using System.Collections.Generic;
using WayneInc.Entitys.DTO;

namespace WayneInc.Bussiness.Service
{
    public class ProcesoCompra
    {
        private readonly ProcesoInventario _procesoInventario;
        private readonly IArticuloRepository _ArticuloRepository;
        private readonly IArticuloClienteRepository _ArticuloClienteRepository;
        private readonly ICarritoItemRepository _CarritoItemRepository;
        private readonly ICarritoRepository _CarritoRepository;

        public ProcesoCompra(ProcesoInventario procesoInventario,IArticuloRepository articuloRepository, IArticuloClienteRepository articuloClienteRepository, ICarritoItemRepository CarritoItemRepository, ICarritoRepository CarritoRepository)
        {
            _procesoInventario = procesoInventario;
            _ArticuloRepository = articuloRepository;
            _ArticuloClienteRepository = articuloClienteRepository;
            _CarritoItemRepository = CarritoItemRepository;
            _CarritoRepository = CarritoRepository;
        }

        //Metodo ue agrega articulos al carrito
        public async Task AddItem(int idArticulo, int idCliente)
        {
            var carrito = await getCarritoByCliente(idCliente);

            var articulo = await _ArticuloRepository.GetById(idArticulo);

            var itemExist = await _CarritoItemRepository.GetByIdArticulo(idArticulo, carrito.id);

            if (itemExist==null)
            {

                CarritoItem item = new CarritoItem()
                {
                    idArticulo = idArticulo,
                    idCarrito = carrito.id,
                    cantidad = 1,
                    subtotal = articulo.precio
                };
                await _CarritoItemRepository.Create(item);
            }
            else
            {

                itemExist.cantidad += 1;
                itemExist.subtotal += articulo.precio;
                await _CarritoItemRepository.Update(itemExist);

            }
            carrito.total = carrito.total + articulo.precio;
            await _CarritoRepository.Update(carrito);
        }

        //Metodo que elimina articulos del carrito
        public async Task RemoveItem(int idCarritoItem, int idCarrito)
        {
            var subtotalRest = await _CarritoItemRepository.Delete(idCarritoItem);

            await editTotal(idCarrito, subtotalRest);
        }

        //Metodo que actualiza el total del carrito
        public async Task editTotal(int idCarrito, decimal subtotal)
        {

            var carrito = await _CarritoRepository.GetById(idCarrito);

            carrito.total = carrito.total - subtotal;

            await _CarritoRepository.Update(carrito);

        }
        public async Task resetTotal(int idCarrito)
        {

            var carrito = await _CarritoRepository.GetById(idCarrito);

            carrito.total = 0;

            await _CarritoRepository.Update(carrito);

        }

        //Obtiene el carrito del cliente
        public async Task<Carrito> getCarritoByCliente(int idCliente)
        {
            var carrito = await _CarritoRepository.GetByIdCliente(idCliente);
            if (carrito != null)
            {
                return carrito;
            }
            else
            {
                carrito = await _CarritoRepository.Create(idCliente, 0);
                return carrito;
            }
        }

        public async Task<List<CompraDTO>> obtenerCompras()
        {
            List<ArticuloCliente> articulosC = await _ArticuloClienteRepository.GetAll();
            List<CompraDTO> compras = new List<CompraDTO>();

            foreach (var articuloC in articulosC)
            {
                var articulo = await _ArticuloRepository.GetById(articuloC.idArticulo);
                string s = Convert.ToBase64String(articulo.imagen);
                CompraDTO compra = new CompraDTO()
                {
                    idCliente = articuloC.idCliente,
                    articuloDescripcion = articulo.descripcion,
                    fecha = articuloC.fecha,
                    imagen = s
                };
                compras.Add(compra);
            }
            return compras;
        }
        public async Task<CarritoDTO> getItems(int idCliente)
        {
            var carrito = await getCarritoByCliente(idCliente);
            var items = await _CarritoItemRepository.GetByIdCarrito(carrito.id);
            List<ItemDto> dtos = new List<ItemDto>();
            foreach (var item in items)
            {
                var articulo = await _ArticuloRepository.GetById(item.idArticulo);
                string s = Convert.ToBase64String(articulo.imagen);
                ItemDto itemDto = new ItemDto()
                {
                    idCarritoItem = item.id,
                    subtotal = item.subtotal,
                    cantidad = item.cantidad,
                    imagen = s,
                    descripcion = articulo.descripcion,
                    precioU = articulo.precio,
                    idArticulo = articulo.id
                };
                dtos.Add(itemDto);
            }
            CarritoDTO carritoDTO = new CarritoDTO()
            {
                idCarrito = carrito.id,
                total = carrito.total,
                items =dtos
            };

            return carritoDTO;

        }
        //Metodo que registra la compra
        public async Task ComprarArticulos(int idCarrito, int idCliente)
        {
            var items = await _CarritoItemRepository.GetByIdCarrito(idCarrito);
            foreach (var item in items)
            {
                int i = 0;
                ArticuloCliente articulo = new ArticuloCliente()
                {
                    idArticulo = item.idArticulo,
                    idCliente = idCliente,
                    fecha = DateTime.UtcNow
                };

                for (i = 0; i < item.cantidad; i++)
                {
                    await _ArticuloClienteRepository.Create(articulo);
                }


                await _procesoInventario.procesarCompra(item.idArticulo,item.cantidad);

            }

            await _CarritoItemRepository.DeleteByCarrito(idCarrito);
            await resetTotal(idCarrito);
        }

       
    }
}
