using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using WayneInc.Data;
using WayneInc.Entitys;

namespace WayneInc.Bussiness.Repository
{
    public class ArticuloClienteRepository : IArticuloClienteRepository
    {
        private readonly DbWContext _context;

        public ArticuloClienteRepository(DbWContext dbWContext)
        {
            _context = dbWContext;
        }

        public async Task<ArticuloCliente> Create(ArticuloCliente articulo)
        {
            await _context.ArticulosCliente.AddAsync(articulo);
            await Save();
            return articulo;
        }

        public async Task<int> Delete(int id)
        {
            var articulo = await _context.ArticulosCliente.FindAsync(id);

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

        public async Task<List<ArticuloCliente>> GetAll()
        {
            return await _context.ArticulosCliente.ToListAsync();
        }

        public async Task<ArticuloCliente> GetById(int id)
        {
            return await _context.ArticulosCliente.FindAsync(id);
        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<ArticuloCliente> Update(ArticuloCliente articulo)
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
