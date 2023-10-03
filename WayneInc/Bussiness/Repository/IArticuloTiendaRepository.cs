using System.Collections.Generic;
using System.Threading.Tasks;
using WayneInc.Entitys;

namespace WayneInc.Bussiness.Repository
{
    public interface IArticuloTiendaRepository
    {
        Task<ArticuloTienda> Create(ArticuloTienda articulo);
        Task<List<ArticuloTienda>> GetAll();
        Task<List<ArticuloTienda>> GetByTienda(int idTienda);
        Task<int> GetStock(int idArticulo);
        Task<ArticuloTienda> Update(ArticuloTienda articulo);
        Task DeleteArticuloByCantidad(int idArticulo,int cantidad);
        Task Save();
    }
}
