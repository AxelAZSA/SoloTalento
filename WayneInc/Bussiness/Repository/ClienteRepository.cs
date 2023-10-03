using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using WayneInc.Data;
using WayneInc.Entitys;

namespace WayneInc.Bussiness.Repository
{
    public class ClienteRepository : IClienteRepository
    {
        private readonly DbWContext _context;

        public ClienteRepository(DbWContext dbWContext)
        {
            _context = dbWContext;
        }
        public async Task<int> Create(Cliente cliente)
        {
            await _context.Clientes.AddAsync(cliente);
            await Save();
            return cliente.id;
        }

        public async Task<int> Delete(int id)
        {
            var cliente = await _context.Clientes.FindAsync(id);

            if (cliente == null)
                return 0;

            _context.Entry(cliente).State = EntityState.Deleted;

            try
            {
                await Save();
                return cliente.id;
            }
            catch (DbUpdateConcurrencyException)
            {
                return 0;
            }
        }

        public async Task<Cliente> GetById(int id)
        {
            return await _context.Clientes.FindAsync(id);
        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<Cliente> Update(Cliente cliente)
        {
            _context.Entry(cliente).State = EntityState.Modified;

            try
            {
                await Save();
                return cliente;
            }
            catch (DbUpdateConcurrencyException)
            {
                return null;
            }
        }
    }
}
