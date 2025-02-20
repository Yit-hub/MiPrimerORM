using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MiPrimerORM1.Models;

namespace MiPrimerORM1.Controllers
{
    public class OrderController
    {
        private readonly EmpresadbContext _context;

        public OrderController(EmpresadbContext context)
        {
            _context = context;
        }

        public async Task<List<Pedido>> GetAllOrdersAsync()
        {
            return await _context.Pedidos.Include(p => p.Cliente).ToListAsync();
        }

        public async Task<Pedido> GetOrderByIdAsync(int id)
        {
            return await _context.Pedidos.Include(p => p.Cliente).FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task AddOrderAsync(Pedido pedido)
        {
            _context.Add(pedido);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateOrderAsync(Pedido pedido)
        {
            _context.Entry(pedido).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteOrderAsync(int id)
        {
            var pedido = await _context.Pedidos.FindAsync(id);
            if (pedido != null)
            {
                _context.Pedidos.Remove(pedido);
                await _context.SaveChangesAsync();
            }
        }
    }
}
