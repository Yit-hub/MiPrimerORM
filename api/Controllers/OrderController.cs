using Microsoft.AspNetCore.Mvc;
using MiPrimerORM1.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly EmpresadbContext _context;

        public OrderController(EmpresadbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Pedido>>> GetAllOrders()
        {
            return await _context.Pedidos.Include(p => p.Cliente).ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult<Pedido>> AddOrder(Pedido pedido)
        {
            _context.Pedidos.Add(pedido);
            await _context.SaveChangesAsync();
            return CreatedAtRoute("GetOrder", new { id = pedido.Id }, pedido);
        }

        // GET: api/Order/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Pedido>> GetOrder(int id)
        {
            var order = await _context.Pedidos.Include(o => o.Cliente).FirstOrDefaultAsync(o => o.Id == id);

            if (order == null)
            {
                return NotFound();
            }

            return order;
        }

        // PUT: api/Order/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateOrder(int id, Pedido pedido)
        {
            if (id != pedido.Id)
            {
                return BadRequest();
            }

            _context.Entry(pedido).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Pedidos.Any(e => e.Id == id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // DELETE: api/Order/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            var order = await _context.Pedidos.FindAsync(id);
            if (order == null)
            {
                return NotFound();
            }

            _context.Pedidos.Remove(order);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}