# MiPrimerORM

## Descripción del Proyecto

Este proyecto consta de tres componentes principales que trabajan juntos para proporcionar una aplicación de gestión de usuarios utilizando el patrón MVC (Model-View-Controller) y un ORM (Object-Relational Mapping) para interactuar con una base de datos.

1. **MiPrimerORM**: Una aplicación de consola que actúa como ORM para la base de datos `empresadb`.
2. **api**: Una aplicación ASP.NET Core Web API que crea controladores para conectar el ORM con la vista.
3. **vista**: Una aplicación web ASP.NET (.NET Framework) que funciona como interfaz de usuario para el cliente y permite realizar operaciones CRUD en la tabla `usuarios`.

## Requisitos

- .NET Core SDK (para `MiPrimerORM` y `api`)
- .NET Framework (para `vista`)
- Visual Studio (recomendado para desarrollo)
- Base de datos SQL Server (para `empresadb`)

## Configuración y Ejecución

### Paso 1: Configurar la Base de Datos

1. Ejecutar script.sql en MySQL usando MySQL Workbench o Xampp.

### Paso 2: Configurar las Aplicaciones
MiPrimerORM:
Abrir MiPrimerORM en Visual Studio.
Configurar la cadena de conexión en Program.cs para apuntar a la base de datos empresadb.
Compilar y ejecutar la aplicación.
api:
Abrir api en Visual Studio.
Configurar la cadena de conexión en Startup.cs para apuntar a la base de datos empresadb.
Ejecutar la aplicación para iniciar el servidor Web API.
vista:
Abrir vista en Visual Studio.
Configurar la URL de la API en HomeController.cs para apuntar al servidor Web API.
Ejecutar la aplicación para iniciar la interfaz de usuario.
Paso 3: Ejecutar el Proyecto
Ejecutar MiPrimerORM para asegurarse de que el ORM esté funcionando correctamente.
Ejecutar api para que el servidor Web API esté disponible.
Ejecutar vista para abrir la interfaz de usuario y realizar operaciones CRUD en la tabla usuarios.
Funcionalidades
CRUD en la Tabla usuarios:
Crear: Agregar nuevos usuarios.
Leer: Ver la lista de usuarios.
Actualizar: Modificar los datos de un usuario existente.
Eliminar: Eliminar un usuario existente.

