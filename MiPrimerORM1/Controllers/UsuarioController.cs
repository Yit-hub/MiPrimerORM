using System;
using System.Collections.Generic;
using System.Linq;
using MiPrimerORM1.Models;

namespace MiPrimerORM1.Controllers
{
    public class UsuarioController
    {
        private readonly EmpresadbContext _context;

        public UsuarioController(EmpresadbContext context)
        {
            _context = context;
        }

        public List<Usuario> ListarUsuarios()
        {
            return _context.Usuarios.ToList();
        }

        public void AgregarUsuario(Usuario usuario)
        {
            _context.Usuarios.Add(usuario);
            _context.SaveChanges();
        }

        public void EliminarUsuario(int id)
        {
            var usuario = _context.Usuarios.Find(id);
            if (usuario != null)
            {
                _context.Usuarios.Remove(usuario);
                _context.SaveChanges();
            }
        }

        public void ActualizarUsuario(Usuario usuario)
        {
            _context.Usuarios.Update(usuario);
            _context.SaveChanges();
        }
    }
}
