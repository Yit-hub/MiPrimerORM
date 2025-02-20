CREATE DATABASE EmpresaDB;
USE EmpresaDB;
-- Tabla de Usuarios
CREATE TABLE Usuarios (
    id INT AUTO_INCREMENT PRIMARY KEY,
    nombre VARCHAR(100),
    email VARCHAR(100) UNIQUE,
    password VARCHAR(255),
    rol ENUM('admin', 'empleado', 'cliente')
);
-- Tabla de Productos
CREATE TABLE Productos (
    id INT AUTO_INCREMENT PRIMARY KEY,
    nombre VARCHAR(100),
    descripcion TEXT,
    precio DECIMAL(10, 2),
    stock INT
);
-- Tabla de Categorías
CREATE TABLE Categorias (
    id INT AUTO_INCREMENT PRIMARY KEY,
    nombre VARCHAR(100)
);
-- Tabla de Productos_Categorias (Relación N:N)
CREATE TABLE Productos_Categorias (
    producto_id INT,
    categoria_id INT,
    PRIMARY KEY (producto_id, categoria_id),
    FOREIGN KEY (producto_id) REFERENCES Productos(id),
    FOREIGN KEY (categoria_id) REFERENCES Categorias(id)
);
-- Tabla de Clientes
CREATE TABLE Clientes (
    id INT AUTO_INCREMENT PRIMARY KEY,
    usuario_id INT UNIQUE,
    direccion TEXT,
    telefono VARCHAR(20),
    FOREIGN KEY (usuario_id) REFERENCES Usuarios(id)
);
-- Tabla de Pedidos
CREATE TABLE Pedidos (
    id INT AUTO_INCREMENT PRIMARY KEY,
    cliente_id INT,
    fecha DATETIME DEFAULT CURRENT_TIMESTAMP,
    estado ENUM('pendiente', 'enviado', 'entregado', 'cancelado'),
    FOREIGN KEY (cliente_id) REFERENCES Clientes(id)
);
-- Tabla de Detalles de Pedido
CREATE TABLE Detalles_Pedido (
    id INT AUTO_INCREMENT PRIMARY KEY,
    pedido_id INT,
    producto_id INT,
    cantidad INT,
    precio DECIMAL(10, 2),
    FOREIGN KEY (pedido_id) REFERENCES Pedidos(id),
    FOREIGN KEY (producto_id) REFERENCES Productos(id)
);
-- Tabla de Facturas
CREATE TABLE Facturas (
    id INT AUTO_INCREMENT PRIMARY KEY,
    pedido_id INT UNIQUE,
    fecha_emision DATETIME DEFAULT CURRENT_TIMESTAMP,
    total DECIMAL(10, 2),
    FOREIGN KEY (pedido_id) REFERENCES Pedidos(id)
);
-- Tabla de Métodos de Pago
CREATE TABLE Metodos_Pago (
    id INT AUTO_INCREMENT PRIMARY KEY,
    nombre VARCHAR(50)
);
-- Tabla de Pagos
CREATE TABLE Pagos (
    id INT AUTO_INCREMENT PRIMARY KEY,
    factura_id INT UNIQUE,
    metodo_pago_id INT,
    monto DECIMAL(10, 2),
    fecha_pago DATETIME DEFAULT CURRENT_TIMESTAMP,
    FOREIGN KEY (factura_id) REFERENCES Facturas(id),
    FOREIGN KEY (metodo_pago_id) REFERENCES Metodos_Pago(id)
);
-- Tabla de Inventario
CREATE TABLE Inventario (
    id INT AUTO_INCREMENT PRIMARY KEY,
    producto_id INT UNIQUE,
    cantidad INT,
    ultima_actualizacion TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    FOREIGN KEY (producto_id) REFERENCES Productos(id)
);
-- Tabla de Proveedores
CREATE TABLE Proveedores (
    id INT AUTO_INCREMENT PRIMARY KEY,
    nombre VARCHAR(100),
    contacto VARCHAR(100)
);
-- Tabla de Compras
CREATE TABLE Compras (
    id INT AUTO_INCREMENT PRIMARY KEY,
    proveedor_id INT,
    fecha DATETIME DEFAULT CURRENT_TIMESTAMP,
    total DECIMAL(10, 2),
    FOREIGN KEY (proveedor_id) REFERENCES Proveedores(id)
);
-- Tabla de Detalles de Compras
CREATE TABLE Detalles_Compra (
    id INT AUTO_INCREMENT PRIMARY KEY,
    compra_id INT,
    producto_id INT,
    cantidad INT,
    precio DECIMAL(10, 2),
    FOREIGN KEY (compra_id) REFERENCES Compras(id),
    FOREIGN KEY (producto_id) REFERENCES Productos(id)
);
-- Tabla de Empleados
CREATE TABLE Empleados (
    id INT AUTO_INCREMENT PRIMARY KEY,
    usuario_id INT UNIQUE,
    puesto VARCHAR(100),
    salario DECIMAL(10, 2),
    FOREIGN KEY (usuario_id) REFERENCES Usuarios(id)
);
-- Tabla de Sucursales
CREATE TABLE Sucursales (
    id INT AUTO_INCREMENT PRIMARY KEY,
    nombre VARCHAR(100),
    direccion TEXT
);
-- Tabla de Envíos
CREATE TABLE Envios (
    id INT AUTO_INCREMENT PRIMARY KEY,
    pedido_id INT UNIQUE,
    sucursal_id INT,
    fecha_envio DATETIME DEFAULT CURRENT_TIMESTAMP,
    fecha_entrega DATETIME,
    FOREIGN KEY (pedido_id) REFERENCES Pedidos(id),
    FOREIGN KEY (sucursal_id) REFERENCES Sucursales(id)
);
USE EmpresaDB;
-- Insertar datos en Usuarios
INSERT INTO Usuarios (nombre, email, password, rol)
VALUES (
        'Juan Pérez',
        'juan@example.com',
        '123456',
        'admin'
    ),
    (
        'Ana Gómez',
        'ana@example.com',
        'abcdef',
        'empleado'
    ),
    (
        'Carlos López',
        'carlos@example.com',
        'pass123',
        'cliente'
    );
-- Insertar datos en Productos
INSERT INTO Productos (nombre, descripcion, precio, stock)
VALUES (
        'Laptop Dell',
        'Laptop con 16GB RAM y 512GB SSD',
        1200.50,
        10
    ),
    (
        'Mouse Logitech',
        'Mouse inalámbrico con Bluetooth',
        25.99,
        50
    ),
    (
        'Teclado Mecánico',
        'Teclado mecánico RGB',
        80.00,
        20
    );
-- Insertar datos en Categorias
INSERT INTO Categorias (nombre)
VALUES ('Electrónica'),
    ('Accesorios'),
    ('Periféricos');
-- Relacionar Productos con Categorias
INSERT INTO Productos_Categorias (producto_id, categoria_id)
VALUES (1, 1),
    -- Laptop Dell -> Electrónica
    (2, 2),
    -- Mouse Logitech -> Accesorios
    (3, 3);
-- Teclado Mecánico -> Periféricos
-- Insertar datos en Clientes
INSERT INTO Clientes (usuario_id, direccion, telefono)
VALUES (3, 'Calle 123, Ciudad A', '555-1234');
-- Insertar datos en Pedidos
INSERT INTO Pedidos (cliente_id, estado)
VALUES (1, 'pendiente');
-- Insertar datos en Detalles de Pedido
INSERT INTO Detalles_Pedido (pedido_id, producto_id, cantidad, precio)
VALUES (1, 1, 1, 1200.50),
    (1, 2, 2, 25.99);
-- Insertar datos en Facturas
INSERT INTO Facturas (pedido_id, total)
VALUES (1, 1252.48);
-- Insertar datos en Métodos de Pago
INSERT INTO Metodos_Pago (nombre)
VALUES ('Tarjeta de Crédito'),
    ('PayPal'),
    ('Transferencia Bancaria');
-- Insertar datos en Pagos
INSERT INTO Pagos (factura_id, metodo_pago_id, monto)
VALUES (1, 1, 1252.48);
-- Insertar datos en Inventario
INSERT INTO Inventario (producto_id, cantidad)
VALUES (1, 9),
    -- Laptop Dell (quedó una menos)
    (2, 48),
    -- Mouse Logitech (quedaron dos menos)
    (3, 20);
-- Teclado Mecánico (sin cambios)
-- Insertar datos en Proveedores
INSERT INTO Proveedores (nombre, contacto)
VALUES ('TechCorp', 'techcorp@example.com'),
    ('AccesoriosPlus', 'accesoriosplus@example.com');
-- Insertar datos en Compras
INSERT INTO Compras (proveedor_id, total)
VALUES (1, 5000.00);
-- Insertar datos en Detalles de Compras
INSERT INTO Detalles_Compra (compra_id, producto_id, cantidad, precio)
VALUES (1, 1, 5, 1000.00),
    (1, 2, 20, 20.00);
-- Insertar datos en Empleados
INSERT INTO Empleados (usuario_id, puesto, salario)
VALUES (2, 'Gerente de Ventas', 3000.00);
-- Insertar datos en Sucursales
INSERT INTO Sucursales (nombre, direccion)
VALUES ('Sucursal Central', 'Av. Principal 456');
-- Insertar datos en Envíos
INSERT INTO Envios (pedido_id, sucursal_id, fecha_entrega)
VALUES (1, 1, '2024-02-15 14:00:00');