using MiPrimerORM1.Controllers;
using MiPrimerORM1.Clases;
using MiPrimerORM1.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MiPrimerORM1.Controllers
{
    public class EmpresadbController<TEntity> where TEntity : class, new()
    {
        private readonly EmpresadbContexto<TEntity, EmpresadbContext> _context;

        public EmpresadbController(EmpresadbContexto<TEntity, EmpresadbContext> context)
        {
            _context = context;
        }

        // Método para obtener todos los registros
        public List<TEntity> GetAll()
        {
            return _context.Set<TEntity>().ToList();
        }

        // Método para obtener un registro por ID (suponiendo que hay una propiedad 'Id')
        public TEntity GetById(int id)
        {
            return _context.Set<TEntity>().Find(id);
        }

        // Método para agregar un nuevo registro
        public void Add(TEntity entity)
        {
            _context.Add(entity);
        }

        // Método para actualizar un registro
        public void Update(TEntity entity)
        {
            _context.Update(entity);
        }

        // Método para eliminar un registro
        public void Remove(TEntity entity)
        {
            _context.Remove(entity);
        }
    }
}
