using System;
using MiPrimerORM1.Models;
using Microsoft.EntityFrameworkCore;
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
            var userController = new UserController(context);
            var orderController = new OrderController(context);
            var productController = new ProductController(context);

            bool continuar = true;

            while (continuar)
            {
                Console.Clear();
                Console.WriteLine("=== MENÚ PRINCIPAL ===");
                Console.WriteLine("1. Listar todos los usuarios");
                Console.WriteLine("2. Agregar un nuevo usuario");
                Console.WriteLine("3. Actualizar un usuario");
                Console.WriteLine("4. Eliminar un usuario");
                Console.WriteLine("5. Listar todos los pedidos");
                Console.WriteLine("6. Agregar un nuevo pedido");
                Console.WriteLine("7. Listar todos los productos");
                Console.WriteLine("8. Agregar un nuevo producto");
                Console.WriteLine("9. Salir");
                Console.Write("Seleccione una opción: ");
                string opcion = Console.ReadLine();

                switch (opcion)
                {
                    case "1":
                        userController.ListarUsuarios();
                        break;
                    case "2":
                        userController.AgregarUsuario();
                        break;
                    case "3":
                        userController.ActualizarUsuario();
                        break;
                    case "4":
                        userController.EliminarUsuario();
                        break;
                    case "5":
                        orderController.ListarPedidos();
                        break;
                    case "6":
                        orderController.AgregarPedido();
                        break;
                    case "7":
                        productController.ListarProductos();
                        break;
                    case "8":
                        productController.AgregarProducto();
                        break;
                    case "9":
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
}

public class UserController
{
    private readonly EmpresadbContext _context;

    public UserController(EmpresadbContext context)
    {
        _context = context;
    }

    public void ListarUsuarios()
    {
        Console.Clear();
        Console.WriteLine("=== LISTA DE USUARIOS ===");
        foreach (var usuario in _context.Usuarios.ToList())
        {
            Console.WriteLine($"{usuario.Id} - {usuario.Nombre} - {usuario.Email} - {usuario.Rol}");
        }
    }

    public void AgregarUsuario()
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
            _context.Usuarios.Add(nuevoUsuario);
            _context.SaveChanges();
            Console.WriteLine("Usuario agregado correctamente.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error al agregar el usuario: {ex.Message}");
        }
    }

    public void ActualizarUsuario()
    {
        Console.Clear();
        Console.WriteLine("=== ACTUALIZAR USUARIO ===");

        Console.Write("Ingrese el ID del usuario a actualizar: ");
        if (!int.TryParse(Console.ReadLine(), out int id))
        {
            Console.WriteLine("ID no válido.");
            return;
        }

        var usuario = _context.Usuarios.Find(id);
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
            _context.Usuarios.Update(usuario);
            _context.SaveChanges();
            Console.WriteLine("Usuario actualizado correctamente.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error al actualizar usuario: {ex.Message}");
        }
    }

    public void EliminarUsuario()
    {
        Console.Clear();
        Console.WriteLine("=== ELIMINAR USUARIO ===");

        Console.Write("Ingrese el ID del usuario a eliminar: ");
        if (!int.TryParse(Console.ReadLine(), out int id))
        {
            Console.WriteLine("ID no válido.");
            return;
        }

        var usuario = _context.Usuarios.Find(id);
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
            _context.Usuarios.Remove(usuario);
            _context.SaveChanges();
            Console.WriteLine("Usuario eliminado correctamente.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error al eliminar usuario: {ex.Message}");
        }
    }
}

public class OrderController
{
    private readonly EmpresadbContext _context;

    public OrderController(EmpresadbContext context)
    {
        _context = context;
    }

    public void ListarPedidos()
    {
        Console.Clear();
        Console.WriteLine("=== LISTA DE PEDIDOS ===");
        foreach (var pedido in _context.Pedidos.Include(p => p.Cliente).ToList())
        {
            Console.WriteLine($"Pedido ID: {pedido.Id}, Cliente: {pedido.Cliente}, Estado: {pedido.Estado}");
        }
    }

    public void AgregarPedido()
    {
        Console.Clear();
        Console.WriteLine("=== AGREGAR NUEVO PEDIDO ===");

        Console.Write("Ingrese el ID del cliente: ");
        if (!int.TryParse(Console.ReadLine(), out int clienteId))
        {
            Console.WriteLine("ID no válido.");
            return;
        }

        var cliente = _context.Clientes.Find(clienteId);
        if (cliente == null)
        {
            Console.WriteLine("Cliente no encontrado.");
            return;
        }

        Pedido nuevoPedido = new Pedido
        {
            ClienteId = clienteId,
            Fecha = DateTime.Now,
            Estado = "pendiente"
        };

        try
        {
            _context.Pedidos.Add(nuevoPedido);
            _context.SaveChanges();
            Console.WriteLine("Pedido agregado correctamente.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error al agregar el pedido: {ex.Message}");
        }
    }
}

public class ProductController
{
    private readonly EmpresadbContext _context;

    public ProductController(EmpresadbContext context)
    {
        _context = context;
    }

    public void ListarProductos()
    {
        Console.Clear();
        Console.WriteLine("=== LISTA DE PRODUCTOS ===");
        foreach (var producto in _context.Productos.ToList())
        {
            Console.WriteLine($"{producto.Id} - {producto.Nombre} - {producto.Precio} - {producto.Stock}");
        }
    }

    public void AgregarProducto()
    {
        Console.Clear();
        Console.WriteLine("=== AGREGAR NUEVO PRODUCTO ===");

        Console.Write("Nombre: ");
        string nombre = Console.ReadLine();

        Console.Write("Descripción: ");
        string descripcion = Console.ReadLine();

        Console.Write("Precio: ");
        if (!decimal.TryParse(Console.ReadLine(), out decimal precio))
        {
            Console.WriteLine("Precio no válido.");
            return;
        }

        Console.Write("Stock: ");
        if (!int.TryParse(Console.ReadLine(), out int stock))
        {
            Console.WriteLine("Stock no válido.");
            return;
        }

        Producto nuevoProducto = new Producto
        {
            Nombre = nombre,
            Descripcion = descripcion,
            Precio = precio,
            Stock = stock
        };

        try
        {
            _context.Productos.Add(nuevoProducto);
            _context.SaveChanges();
            Console.WriteLine("Producto agregado correctamente.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error al agregar el producto: {ex.Message}");
        }
    }
}