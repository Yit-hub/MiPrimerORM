using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MiPrimerORM1.Models;

namespace MiPrimerORM1.Controllers
{
    public class ProductController
    {
        private readonly EmpresadbContext _context;

        public ProductController(EmpresadbContext context)
        {
            _context = context;
        }

        public async Task<List<Producto>> GetAllProductsAsync()
        {
            return await _context.Productos.ToListAsync();
        }

        public async Task<Producto> GetProductByIdAsync(int id)
        {
            return await _context.Productos.FindAsync(id);
        }

        public async Task AddProductAsync(Producto producto)
        {
            _context.Add(producto);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateProductAsync(Producto producto)
        {
            _context.Entry(producto).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteProductAsync(int id)
        {
            var producto = await _context.Productos.FindAsync(id);
            if (producto != null)
            {
                _context.Productos.Remove(producto);
                await _context.SaveChangesAsync();
            }
        }
    }
}
