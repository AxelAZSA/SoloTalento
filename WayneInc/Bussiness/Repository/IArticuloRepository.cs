using System.Collections.Generic;
using System.Threading.Tasks;
using WayneInc.Entitys;

namespace WayneInc.Bussiness.Repository
{
    public interface IArticuloRepository
    {
        Task<int> Create(Articulo articulo);
        Task<List<Articulo>> GetAll();
        Task<Articulo> GetById(int id);
        Task<Articulo> Update(Articulo articulo);
        Task<int> Delete(int id);
        Task Save();
    }
}
