using System.Collections.Generic;
using System.Threading.Tasks;
using WayneInc.Entitys;

namespace WayneInc.Bussiness.Repository
{
    public interface ITiendaRepository
    {
        Task<Tienda> Create(Tienda tienda);
        Task<List<Tienda>> GetAll();
        Task<Tienda> Update(Tienda tienda);
        Task<int> Delete(int id);
        Task Save();
    }
}
