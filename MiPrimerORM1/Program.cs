using System;
using System.Linq;
using MiPrimerORM1.Models;
using Microsoft.EntityFrameworkCore;
using MiPrimerORM1.Clases;
using MiPrimerORM1.Controllers;

class Program
{
    static void Main()
    {
        var optionsBuilder = new DbContextOptionsBuilder<EmpresadbContext>();
        optionsBuilder.UseMySql("server=localhost;database=empresadb;user=root;password=",
            new MySqlServerVersion(new Version(8, 0, 25)));

        using (var context = new EmpresadbContext(optionsBuilder.Options))
        {
            bool continuar = true;

            while (continuar)
            {
                Console.Clear();
                Console.WriteLine("=== MENÚ PRINCIPAL ===");
                Console.WriteLine("1. Listar todos los usuarios");
                Console.WriteLine("2. Agregar un nuevo usuario");
                Console.WriteLine("3. Actualizar un usuario");
                Console.WriteLine("4. Eliminar un usuario");
                Console.WriteLine("5. Salir");
                Console.Write("Seleccione una opción: ");
                string opcion = Console.ReadLine();

                switch (opcion)
                {
                    case "1":
                        ListarUsuarios(context);
                        break;
                    case "2":
                        AgregarUsuario(context);
                        break;
                    case "3":
                        ActualizarUsuario(context);
                        break;
                    case "4":
                        EliminarUsuario(context);
                        break;
                    case "5":
                        continuar = false;
                        break;
                    default:
                        Console.WriteLine("Opción no válida. Intente de nuevo.");
                        break;
                }

                if (continuar)
                {
                    Console.WriteLine("\nPresione una tecla para continuar...");
                    Console.ReadKey();
                }
            }
        }
    }

    static void ListarUsuarios(EmpresadbContext context)
    {
        Console.Clear();
        Console.WriteLine("=== LISTA DE USUARIOS ===");
        foreach (var usuario in context.Usuarios.ToList())
        {
            Console.WriteLine($"{usuario.Id} - {usuario.Nombre} - {usuario.Email} - {usuario.Rol}");
        }
    }

    static void AgregarUsuario(EmpresadbContext context)
    {
        Console.Clear();
        Console.WriteLine("=== AGREGAR NUEVO USUARIO ===");

        Console.Write("Nombre: ");
        string nombre = Console.ReadLine();

        Console.Write("Email: ");
        string email = Console.ReadLine();

        Console.Write("Contraseña: ");
        string password = Console.ReadLine();

        Console.Write("Rol (admin, empleado, cliente): ");
        string rol = Console.ReadLine();

        if (rol != "admin" && rol != "empleado" && rol != "cliente")
        {
            Console.WriteLine("Rol no válido. Debe ser 'admin', 'empleado' o 'cliente'.");
            return;
        }

        Usuario nuevoUsuario = new Usuario
        {
            Nombre = nombre,
            Email = email,
            Password = password,
            Rol = rol
        };

        try
        {
            context.Usuarios.Add(nuevoUsuario);
            context.SaveChanges();
            Console.WriteLine("Usuario agregado correctamente.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error al agregar el usuario: {ex.Message}");
        }
    }

    static void ActualizarUsuario(EmpresadbContext context)
    {
        Console.Clear();
        Console.WriteLine("=== ACTUALIZAR USUARIO ===");

        Console.Write("Ingrese el ID del usuario a actualizar: ");
        if (!int.TryParse(Console.ReadLine(), out int id))
        {
            Console.WriteLine("ID no válido.");
            return;
        }

        var usuario = context.Usuarios.Find(id);
        if (usuario == null)
        {
            Console.WriteLine("Usuario no encontrado.");
            return;
        }

        Console.Write($"Nuevo nombre ({usuario.Nombre}): ");
        string nuevoNombre = Console.ReadLine();
        if (!string.IsNullOrWhiteSpace(nuevoNombre))
            usuario.Nombre = nuevoNombre;

        Console.Write($"Nuevo email ({usuario.Email}): ");
        string nuevoEmail = Console.ReadLine();
        if (!string.IsNullOrWhiteSpace(nuevoEmail))
            usuario.Email = nuevoEmail;

        Console.Write($"Nuevo rol ({usuario.Rol}): ");
        string nuevoRol = Console.ReadLine();
        if (!string.IsNullOrWhiteSpace(nuevoRol) && (nuevoRol == "admin" || nuevoRol == "empleado" || nuevoRol == "cliente"))
            usuario.Rol = nuevoRol;

        try
        {
            context.Usuarios.Update(usuario);
            context.SaveChanges();
            Console.WriteLine("Usuario actualizado correctamente.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error al actualizar usuario: {ex.Message}");
        }
    }

    static void EliminarUsuario(EmpresadbContext context)
    {
        Console.Clear();
        Console.WriteLine("=== ELIMINAR USUARIO ===");

        Console.Write("Ingrese el ID del usuario a eliminar: ");
        if (!int.TryParse(Console.ReadLine(), out int id))
        {
            Console.WriteLine("ID no válido.");
            return;
        }

        var usuario = context.Usuarios.Find(id);
        if (usuario == null)
        {
            Console.WriteLine("Usuario no encontrado.");
            return;
        }

        Console.Write($"¿Está seguro de eliminar al usuario {usuario.Nombre}? (S/N): ");
        string confirmacion = Console.ReadLine()?.ToUpper();
        if (confirmacion != "S")
        {
            Console.WriteLine("Operación cancelada.");
            return;
        }

        try
        {
            context.Usuarios.Remove(usuario);
            context.SaveChanges();
            Console.WriteLine("Usuario eliminado correctamente.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error al eliminar usuario: {ex.Message}");
        }
    }
}
