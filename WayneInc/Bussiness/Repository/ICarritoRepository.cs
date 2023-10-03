using System.Threading.Tasks;
using WayneInc.Entitys;

namespace WayneInc.Bussiness.Repository
{
    public interface ICarritoRepository
    {
        Task<Carrito> Create(int idCliente, int total);
        Task<Carrito> GetById(int id);
        Task<Carrito> GetByIdCliente(int idCliente);
        Task<Carrito> Update(Carrito carrito);
        Task<int> Delete(int id);
        Task Save();
    }
}
