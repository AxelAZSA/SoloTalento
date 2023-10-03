using System.Collections.Generic;
using System.Threading.Tasks;
using WayneInc.Entitys;

namespace WayneInc.Bussiness.Repository
{
    public interface IClienteRepository
    {
        Task<int> Create(Cliente cliente);
        Task<Cliente> GetById(int id);
        Task<Cliente> Update(Cliente Cliente);
        Task<int> Delete(int id);
        Task Save();
    }
}
