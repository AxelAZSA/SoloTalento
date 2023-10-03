using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WayneInc.Data;
using WayneInc.Entitys;

namespace WayneInc.Bussiness.Repository
{
    public class CarritoItemRepository : ICarritoItemRepository
    {
        private readonly DbWContext _context;
        public CarritoItemRepository(DbWContext context)
        {
            _context = context;
        }
     
        public async Task<int> Create(CarritoItem item)
        {
            await _context.CarritoItems.AddAsync(item);
            await Save();
            return item.id;
        }

        public async Task<decimal> Delete(int id)
        {
            var item = await _context.CarritoItems.FindAsync(id);

            if (item == null)
                return 0;

            _context.Entry(item).State = EntityState.Deleted;

            try
            {
                await Save();
                return item.subtotal;
            }
            catch (DbUpdateConcurrencyException)
            {
                return 0;
            }
        }

        public async Task DeleteByCarrito(int idCarrito)
        {
            var items = await GetByIdCarrito(idCarrito);
            _context.CarritoItems.RemoveRange(items);
        }

        public async Task<CarritoItem> GetByIdArticulo(int idArticulo, int idCarrito)
        {
            return await _context.CarritoItems.FirstOrDefaultAsync(c=>c.idCarrito==idCarrito&&c.idArticulo==idArticulo);
        }

        public async Task<List<CarritoItem>> GetByIdCarrito(int idCarrito)
        {
            return await _context.CarritoItems.Where(x => x.idCarrito == idCarrito).ToListAsync();
        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }

        public async Task Update(CarritoItem item)
        {
            _context.Entry(item).State = EntityState.Modified;

            try
            {
                await Save();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                throw ex;
            }
        }
    }
}
