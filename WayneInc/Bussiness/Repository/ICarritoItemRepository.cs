using System.Collections.Generic;
using System.Threading.Tasks;
using WayneInc.Entitys;

namespace WayneInc.Bussiness.Repository
{
    public interface ICarritoItemRepository
    {
        Task<int> Create(CarritoItem item);
        Task<CarritoItem> GetByIdArticulo(int idArticulo, int idCarrito);
        Task<List<CarritoItem>> GetByIdCarrito(int idCarrito);
        Task Update(CarritoItem item);
        Task<decimal> Delete(int id);
        Task DeleteByCarrito(int idCarrito);
        Task Save();
    }
}
