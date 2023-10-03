using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using WayneInc.Data;
using WayneInc.Entitys;

namespace WayneInc.Bussiness.Repository
{
    public class CarritoRepository : ICarritoRepository
    {
        private readonly DbWContext _context;
        public CarritoRepository(DbWContext context) 
        {
            _context = context;
        }
        public async Task<Carrito> Create(int idCliente, int total)
        {
            Carrito carrito = new Carrito()
            {
                idCliente = idCliente,
                total = total
            };
            await _context.Carritos.AddAsync(carrito);
            await Save();
            return carrito;
        }

        public async Task<int> Delete(int id)
        {
            var carrito = await _context.Carritos.FindAsync(id);

            if (carrito == null)
                return 0;

            _context.Entry(carrito).State = EntityState.Deleted;

            try
            {
                await Save();
                return carrito.id;
            }
            catch (DbUpdateConcurrencyException)
            {
                return 0;
            }
        }

        public async Task<Carrito> GetById(int id)
        {
            return await _context.Carritos.FindAsync(id);
        }

        public async Task<Carrito> GetByIdCliente(int idCliente)
        {
            return await _context.Carritos.FirstOrDefaultAsync(c=>c.idCliente==idCliente);
        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<Carrito> Update(Carrito carrito)
        {
            _context.Entry(carrito).State = EntityState.Modified;

            try
            {
                await Save();
                return carrito;
            }
            catch (DbUpdateConcurrencyException)
            {
                return null;
            }
        }
    }
}
