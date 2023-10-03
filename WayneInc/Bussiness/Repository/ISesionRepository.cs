using System.Threading.Tasks;
using WayneInc.Entitys;

namespace WayneInc.Bussiness.Repository
{
    public interface ISesionRepository
    {
        Task<Sesion> GetByCliente(int idCliente);
        Task<Sesion> Create(Sesion sesion);
        Task<Sesion> GetByCorreo(string correo);
        Task<Sesion> GetById(int id);
        Task<Sesion> Update(Sesion sesion);
        Task<int> Delete(int id);
        Task Save();
    }
}
