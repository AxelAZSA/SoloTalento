using System.Threading.Tasks;
using WayneInc.Entitys;

namespace WayneInc.Bussiness.Repository
{
    public interface IAdminRepository
    {
        Task<Admin> Create(Admin admin);
        Task<Admin> GetByCorreo(string correo);
        Task<Admin> GetById(int id);
        Task<Admin> Update(Admin sesion);
        Task<int> Delete(int id);
        Task Save();
    }
}
