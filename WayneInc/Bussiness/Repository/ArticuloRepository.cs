using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using WayneInc.Data;
using WayneInc.Entitys;

namespace WayneInc.Bussiness.Repository
{
    public class ArticuloRepository : IArticuloRepository
    {
        private readonly DbWContext _context;

        public ArticuloRepository(DbWContext dbWContext)
        {
            _context = dbWContext;
        }

        public async Task<int> Create(Articulo articulo)
        {
            await _context.Articulos.AddAsync(articulo);
            await Save();
            return articulo.id;
        }

        public async Task<int> Delete(int id)
        {
            var articulo = await _context.Articulos.FindAsync(id);

            if (articulo == null)
                return 0;

            _context.Entry(articulo).State = EntityState.Deleted;

            try
            {
                await Save();
                return articulo.id;
            }
            catch (DbUpdateConcurrencyException)
            {
                return 0;
            }
        }

        public async Task<List<Articulo>> GetAll()
        {
            return await _context.Articulos.ToListAsync();
        }

        public async Task<Articulo> GetById(int id)
        {
            return await _context.Articulos.FindAsync(id);
        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<Articulo> Update(Articulo articulo)
        {
            _context.Entry(articulo).State = EntityState.Modified;

            try
            {
                await Save();
                return articulo;
            }
            catch (DbUpdateConcurrencyException)
            {
                return null;
            }
        }
    }
}
