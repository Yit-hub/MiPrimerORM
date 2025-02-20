using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

// Add Swagger/OpenAPI
builder.Services.AddSwaggerGen();

// Register your controllers as services
builder.Services.AddScoped<MiPrimerORM1.Controllers.UserController>();
builder.Services.AddScoped<MiPrimerORM1.Controllers.OrderController>();
builder.Services.AddScoped<MiPrimerORM1.Controllers.ProductController>();

// Configure the database context using the connection string from appsettings.json
builder.Services.AddDbContext<MiPrimerORM1.Models.EmpresadbContext>(options =>
    options.UseMySql(builder.Configuration.GetConnectionString("DefaultConnection"),
        new MySqlServerVersion(new Version(8, 0, 25))));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.MapGet("/listaUsuarios", async (HttpContext httpContext) =>
{
    // Use the controller from the dependency injection container
    var userController = httpContext.RequestServices.GetRequiredService<MiPrimerORM1.Controllers.UserController>();
    var users = await userController.GetAllUsersAsync();
    return users;
})
.WithName("listaUsuarios")
.WithOpenApi();

app.Run();

