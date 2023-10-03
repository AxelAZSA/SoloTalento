using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using WayneInc.Data;
using WayneInc.Entitys;

namespace WayneInc.Bussiness.Repository
{
    public class SesionRepository : ISesionRepository
    {

        private readonly DbWContext _context;

        public SesionRepository(DbWContext dbWContext)
        {
            _context = dbWContext;
        }
        public async Task<Sesion> Create(Sesion sesion)
        {
            await _context.Sesion.AddAsync(sesion);
            await Save();
            return sesion;
        }

        public async Task<int> Delete(int id)
        {
            var sesion = await _context.Sesion.FindAsync(id);

            if (sesion == null)
                return 0;

            _context.Entry(sesion).State = EntityState.Deleted;

            try
            {
                await Save();
                return sesion.id;
            }
            catch (DbUpdateConcurrencyException)
            {
                return 0;
            }
        }

        public async Task<Sesion> GetByCliente(int idCliente)
        {
            return await _context.Sesion.FirstOrDefaultAsync(s => s.idCliente == idCliente);
        }
        public async Task<Sesion> GetByCorreo(string correo)
        {
            return await _context.Sesion.FirstOrDefaultAsync(s=>s.correo==correo);
        }

        public async Task<Sesion> GetById(int id)
        {
            return await _context.Sesion.FindAsync(id);
        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<Sesion> Update(Sesion sesion)
        {
            _context.Entry(sesion).State = EntityState.Modified;

            try
            {
                await Save();
                return sesion;
            }
            catch (DbUpdateConcurrencyException)
            {
                return null;
            }
        }
    }
}
