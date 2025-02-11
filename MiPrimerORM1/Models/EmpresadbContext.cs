using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Scaffolding.Internal;

namespace MiPrimerORM1.Models;

public partial class EmpresadbContext : DbContext
{
    public EmpresadbContext()
    {
    }

    public EmpresadbContext(DbContextOptions<EmpresadbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Categoria> Categorias { get; set; }

    public virtual DbSet<Cliente> Clientes { get; set; }

    public virtual DbSet<Compra> Compras { get; set; }

    public virtual DbSet<DetallesCompra> DetallesCompras { get; set; }

    public virtual DbSet<DetallesPedido> DetallesPedidos { get; set; }

    public virtual DbSet<Empleado> Empleados { get; set; }

    public virtual DbSet<Envio> Envios { get; set; }

    public virtual DbSet<Factura> Facturas { get; set; }

    public virtual DbSet<Inventario> Inventarios { get; set; }

    public virtual DbSet<MetodosPago> MetodosPagos { get; set; }

    public virtual DbSet<Pago> Pagos { get; set; }

    public virtual DbSet<Pedido> Pedidos { get; set; }

    public virtual DbSet<Producto> Productos { get; set; }

    public virtual DbSet<Proveedore> Proveedores { get; set; }

    public virtual DbSet<Sucursale> Sucursales { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseMySql("server=localhost;database=empresadb;user=root", Microsoft.EntityFrameworkCore.ServerVersion.Parse("10.4.32-mariadb"));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_general_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<Categoria>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("categorias");

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id");
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .HasColumnName("nombre");
        });

        modelBuilder.Entity<Cliente>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("clientes");

            entity.HasIndex(e => e.UsuarioId, "usuario_id").IsUnique();

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id");
            entity.Property(e => e.Direccion)
                .HasColumnType("text")
                .HasColumnName("direccion");
            entity.Property(e => e.Telefono)
                .HasMaxLength(20)
                .HasColumnName("telefono");
            entity.Property(e => e.UsuarioId)
                .HasColumnType("int(11)")
                .HasColumnName("usuario_id");

            entity.HasOne(d => d.Usuario).WithOne(p => p.Cliente)
                .HasForeignKey<Cliente>(d => d.UsuarioId)
                .HasConstraintName("clientes_ibfk_1");
        });

        modelBuilder.Entity<Compra>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("compras");

            entity.HasIndex(e => e.ProveedorId, "proveedor_id");

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id");
            entity.Property(e => e.Fecha)
                .HasDefaultValueSql("current_timestamp()")
                .HasColumnType("datetime")
                .HasColumnName("fecha");
            entity.Property(e => e.ProveedorId)
                .HasColumnType("int(11)")
                .HasColumnName("proveedor_id");
            entity.Property(e => e.Total)
                .HasPrecision(10, 2)
                .HasColumnName("total");

            entity.HasOne(d => d.Proveedor).WithMany(p => p.Compras)
                .HasForeignKey(d => d.ProveedorId)
                .HasConstraintName("compras_ibfk_1");
        });

        modelBuilder.Entity<DetallesCompra>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("detalles_compra");

            entity.HasIndex(e => e.CompraId, "compra_id");

            entity.HasIndex(e => e.ProductoId, "producto_id");

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id");
            entity.Property(e => e.Cantidad)
                .HasColumnType("int(11)")
                .HasColumnName("cantidad");
            entity.Property(e => e.CompraId)
                .HasColumnType("int(11)")
                .HasColumnName("compra_id");
            entity.Property(e => e.Precio)
                .HasPrecision(10, 2)
                .HasColumnName("precio");
            entity.Property(e => e.ProductoId)
                .HasColumnType("int(11)")
                .HasColumnName("producto_id");

            entity.HasOne(d => d.Compra).WithMany(p => p.DetallesCompras)
                .HasForeignKey(d => d.CompraId)
                .HasConstraintName("detalles_compra_ibfk_1");

            entity.HasOne(d => d.Producto).WithMany(p => p.DetallesCompras)
                .HasForeignKey(d => d.ProductoId)
                .HasConstraintName("detalles_compra_ibfk_2");
        });

        modelBuilder.Entity<DetallesPedido>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("detalles_pedido");

            entity.HasIndex(e => e.PedidoId, "pedido_id");

            entity.HasIndex(e => e.ProductoId, "producto_id");

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id");
            entity.Property(e => e.Cantidad)
                .HasColumnType("int(11)")
                .HasColumnName("cantidad");
            entity.Property(e => e.PedidoId)
                .HasColumnType("int(11)")
                .HasColumnName("pedido_id");
            entity.Property(e => e.Precio)
                .HasPrecision(10, 2)
                .HasColumnName("precio");
            entity.Property(e => e.ProductoId)
                .HasColumnType("int(11)")
                .HasColumnName("producto_id");

            entity.HasOne(d => d.Pedido).WithMany(p => p.DetallesPedidos)
                .HasForeignKey(d => d.PedidoId)
                .HasConstraintName("detalles_pedido_ibfk_1");

            entity.HasOne(d => d.Producto).WithMany(p => p.DetallesPedidos)
                .HasForeignKey(d => d.ProductoId)
                .HasConstraintName("detalles_pedido_ibfk_2");
        });

        modelBuilder.Entity<Empleado>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("empleados");

            entity.HasIndex(e => e.UsuarioId, "usuario_id").IsUnique();

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id");
            entity.Property(e => e.Puesto)
                .HasMaxLength(100)
                .HasColumnName("puesto");
            entity.Property(e => e.Salario)
                .HasPrecision(10, 2)
                .HasColumnName("salario");
            entity.Property(e => e.UsuarioId)
                .HasColumnType("int(11)")
                .HasColumnName("usuario_id");

            entity.HasOne(d => d.Usuario).WithOne(p => p.Empleado)
                .HasForeignKey<Empleado>(d => d.UsuarioId)
                .HasConstraintName("empleados_ibfk_1");
        });

        modelBuilder.Entity<Envio>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("envios");

            entity.HasIndex(e => e.PedidoId, "pedido_id").IsUnique();

            entity.HasIndex(e => e.SucursalId, "sucursal_id");

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id");
            entity.Property(e => e.FechaEntrega)
                .HasColumnType("datetime")
                .HasColumnName("fecha_entrega");
            entity.Property(e => e.FechaEnvio)
                .HasDefaultValueSql("current_timestamp()")
                .HasColumnType("datetime")
                .HasColumnName("fecha_envio");
            entity.Property(e => e.PedidoId)
                .HasColumnType("int(11)")
                .HasColumnName("pedido_id");
            entity.Property(e => e.SucursalId)
                .HasColumnType("int(11)")
                .HasColumnName("sucursal_id");

            entity.HasOne(d => d.Pedido).WithOne(p => p.Envio)
                .HasForeignKey<Envio>(d => d.PedidoId)
                .HasConstraintName("envios_ibfk_1");

            entity.HasOne(d => d.Sucursal).WithMany(p => p.Envios)
                .HasForeignKey(d => d.SucursalId)
                .HasConstraintName("envios_ibfk_2");
        });

        modelBuilder.Entity<Factura>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("facturas");

            entity.HasIndex(e => e.PedidoId, "pedido_id").IsUnique();

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id");
            entity.Property(e => e.FechaEmision)
                .HasDefaultValueSql("current_timestamp()")
                .HasColumnType("datetime")
                .HasColumnName("fecha_emision");
            entity.Property(e => e.PedidoId)
                .HasColumnType("int(11)")
                .HasColumnName("pedido_id");
            entity.Property(e => e.Total)
                .HasPrecision(10, 2)
                .HasColumnName("total");

            entity.HasOne(d => d.Pedido).WithOne(p => p.Factura)
                .HasForeignKey<Factura>(d => d.PedidoId)
                .HasConstraintName("facturas_ibfk_1");
        });

        modelBuilder.Entity<Inventario>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("inventario");

            entity.HasIndex(e => e.ProductoId, "producto_id").IsUnique();

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id");
            entity.Property(e => e.Cantidad)
                .HasColumnType("int(11)")
                .HasColumnName("cantidad");
            entity.Property(e => e.ProductoId)
                .HasColumnType("int(11)")
                .HasColumnName("producto_id");
            entity.Property(e => e.UltimaActualizacion)
                .HasDefaultValueSql("current_timestamp()")
                .HasColumnType("timestamp")
                .HasColumnName("ultima_actualizacion");

            entity.HasOne(d => d.Producto).WithOne(p => p.Inventario)
                .HasForeignKey<Inventario>(d => d.ProductoId)
                .HasConstraintName("inventario_ibfk_1");
        });

        modelBuilder.Entity<MetodosPago>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("metodos_pago");

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id");
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .HasColumnName("nombre");
        });

        modelBuilder.Entity<Pago>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("pagos");

            entity.HasIndex(e => e.FacturaId, "factura_id").IsUnique();

            entity.HasIndex(e => e.MetodoPagoId, "metodo_pago_id");

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id");
            entity.Property(e => e.FacturaId)
                .HasColumnType("int(11)")
                .HasColumnName("factura_id");
            entity.Property(e => e.FechaPago)
                .HasDefaultValueSql("current_timestamp()")
                .HasColumnType("datetime")
                .HasColumnName("fecha_pago");
            entity.Property(e => e.MetodoPagoId)
                .HasColumnType("int(11)")
                .HasColumnName("metodo_pago_id");
            entity.Property(e => e.Monto)
                .HasPrecision(10, 2)
                .HasColumnName("monto");

            entity.HasOne(d => d.Factura).WithOne(p => p.Pago)
                .HasForeignKey<Pago>(d => d.FacturaId)
                .HasConstraintName("pagos_ibfk_1");

            entity.HasOne(d => d.MetodoPago).WithMany(p => p.Pagos)
                .HasForeignKey(d => d.MetodoPagoId)
                .HasConstraintName("pagos_ibfk_2");
        });

        modelBuilder.Entity<Pedido>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("pedidos");

            entity.HasIndex(e => e.ClienteId, "cliente_id");

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id");
            entity.Property(e => e.ClienteId)
                .HasColumnType("int(11)")
                .HasColumnName("cliente_id");
            entity.Property(e => e.Estado)
                .HasColumnType("enum('pendiente','enviado','entregado','cancelado')")
                .HasColumnName("estado");
            entity.Property(e => e.Fecha)
                .HasDefaultValueSql("current_timestamp()")
                .HasColumnType("datetime")
                .HasColumnName("fecha");

            entity.HasOne(d => d.Cliente).WithMany(p => p.Pedidos)
                .HasForeignKey(d => d.ClienteId)
                .HasConstraintName("pedidos_ibfk_1");
        });

        modelBuilder.Entity<Producto>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("productos");

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id");
            entity.Property(e => e.Descripcion)
                .HasColumnType("text")
                .HasColumnName("descripcion");
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .HasColumnName("nombre");
            entity.Property(e => e.Precio)
                .HasPrecision(10, 2)
                .HasColumnName("precio");
            entity.Property(e => e.Stock)
                .HasColumnType("int(11)")
                .HasColumnName("stock");

            entity.HasMany(d => d.Categoria).WithMany(p => p.Productos)
                .UsingEntity<Dictionary<string, object>>(
                    "ProductosCategoria",
                    r => r.HasOne<Categoria>().WithMany()
                        .HasForeignKey("CategoriaId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("productos_categorias_ibfk_2"),
                    l => l.HasOne<Producto>().WithMany()
                        .HasForeignKey("ProductoId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("productos_categorias_ibfk_1"),
                    j =>
                    {
                        j.HasKey("ProductoId", "CategoriaId")
                            .HasName("PRIMARY")
                            .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });
                        j.ToTable("productos_categorias");
                        j.HasIndex(new[] { "CategoriaId" }, "categoria_id");
                        j.IndexerProperty<int>("ProductoId")
                            .HasColumnType("int(11)")
                            .HasColumnName("producto_id");
                        j.IndexerProperty<int>("CategoriaId")
                            .HasColumnType("int(11)")
                            .HasColumnName("categoria_id");
                    });
        });

        modelBuilder.Entity<Proveedore>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("proveedores");

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id");
            entity.Property(e => e.Contacto)
                .HasMaxLength(100)
                .HasColumnName("contacto");
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .HasColumnName("nombre");
        });

        modelBuilder.Entity<Sucursale>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("sucursales");

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id");
            entity.Property(e => e.Direccion)
                .HasColumnType("text")
                .HasColumnName("direccion");
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .HasColumnName("nombre");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("usuarios");

            entity.HasIndex(e => e.Email, "email").IsUnique();

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .HasColumnName("email");
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .HasColumnName("nombre");
            entity.Property(e => e.Password)
                .HasMaxLength(255)
                .HasColumnName("password");
            entity.Property(e => e.Rol)
                .HasColumnType("enum('admin','empleado','cliente')")
                .HasColumnName("rol");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
