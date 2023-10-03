using System.Collections.Generic;
using System.Threading.Tasks;
using WayneInc.Entitys;

namespace WayneInc.Bussiness.Repository
{
    public interface IArticuloClienteRepository
    {
        Task<ArticuloCliente> Create(ArticuloCliente articulo);
        Task<List<ArticuloCliente>> GetAll();
        Task<ArticuloCliente> GetById(int id);
        Task<ArticuloCliente> Update(ArticuloCliente articulo);
        Task<int> Delete(int id);
        Task Save();
    }
}
