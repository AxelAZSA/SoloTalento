using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WayneInc.Data;
using WayneInc.Entitys;

namespace WayneInc.Bussiness.Repository
{
    public class ArticuloTiendaRepository : IArticuloTiendaRepository
    {
        private readonly DbWContext _context;

        public ArticuloTiendaRepository(DbWContext context)
        {
            _context = context;
        }

        public async Task<ArticuloTienda> Create(ArticuloTienda articulo)
        {
            await _context.ArticulosTienda.AddAsync(articulo);
            await Save();
            return articulo;
        }

        public async Task DeleteArticuloByCantidad(int idArticulo,int cantidad)
        {
            var lastN = await _context.ArticulosTienda.Where(a=>a.idArticulo==idArticulo).Take(cantidad).ToListAsync();

            _context.ArticulosTienda.RemoveRange(lastN);
        }

        public async Task<List<ArticuloTienda>> GetAll()
        {
            return await _context.ArticulosTienda.ToListAsync();
        }

        public async Task<List<ArticuloTienda>> GetByTienda(int idTienda)
        {
            return await _context.ArticulosTienda.Where(t => t.idTienda == idTienda).ToListAsync();
        }

        public  async Task<int> GetStock(int idArticulo)
        {
            var articulo = await _context.ArticulosTienda.Where(a=>a.idArticulo == idArticulo).ToListAsync();

            return articulo.Count;
        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<ArticuloTienda> Update(ArticuloTienda articulo)
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
