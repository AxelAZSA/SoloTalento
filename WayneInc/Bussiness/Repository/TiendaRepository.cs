using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using WayneInc.Data;
using WayneInc.Entitys;

namespace WayneInc.Bussiness.Repository
{
    public class TiendaRepository : ITiendaRepository
    {

        private readonly DbWContext _context;

        public TiendaRepository(DbWContext dbWContext)
        {
            _context = dbWContext;
        }

        public async Task<Tienda> Create(Tienda tienda)
        {
            await _context.Tiendas.AddAsync(tienda);
            await Save();
            return tienda;
        }

        public async Task<int> Delete(int id)
        {
            var tienda = await _context.Tiendas.FindAsync(id);

            if (tienda == null)
                return 0;

            _context.Entry(tienda).State = EntityState.Deleted;

            try
            {
                await Save();
                return tienda.id;
            }
            catch (DbUpdateConcurrencyException)
            {
                return 0;
            }
        }

        public async Task<List<Tienda>> GetAll()
        {
            return await _context.Tiendas.ToListAsync(); ;
        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<Tienda> Update(Tienda tienda)
        {
            _context.Entry(tienda).State = EntityState.Modified;

            try
            {
                await Save();
                return tienda;
            }
            catch (DbUpdateConcurrencyException)
            {
                return null;
            }
        }
    }
}
