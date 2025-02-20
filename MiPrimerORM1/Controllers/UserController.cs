using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MiPrimerORM1.Models;

namespace MiPrimerORM1.Controllers
{
    public class UserController
    {
        private readonly EmpresadbContext _context;

        public UserController(EmpresadbContext context)
        {
            _context = context;
        }

        public async Task<List<Usuario>> GetAllUsersAsync()
        {
            return await _context.Usuarios.ToListAsync();
        }

        public async Task<Usuario> GetUserByIdAsync(int id)
        {
            return await _context.Usuarios.FindAsync(id);
        }

        public async Task AddUserAsync(Usuario usuario)
        {
            _context.Add(usuario);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateUserAsync(Usuario usuario)
        {
            _context.Entry(usuario).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteUserAsync(int id)
        {
            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario != null)
            {
                _context.Usuarios.Remove(usuario);
                await _context.SaveChangesAsync();
            }
        }
    }
}
