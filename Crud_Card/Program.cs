using AutoMapper;
using Crud_Card.DAL;
using Crud_Card.DAL.Interfaces;
using Crud_Card.DAL.ManagerError;
using Crud_Card.DAL.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddScoped<IUnitOfWork , UnitOfWork>();


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var origenes=builder.Configuration.GetValue<string>("OrigenesPermitidos")!.Split(',');

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(rutas =>
    {
        rutas.WithOrigins(origenes).AllowAnyHeader().AllowAnyMethod();
    });
});

builder.Services.AddDbContext<ApplicationDbContext>(opciones =>
{
    opciones.UseSqlServer("name=DefaultConnection");
});

// Configurar AutoMapper manualmente
var mapperConfig = new MapperConfiguration(m =>
{
    m.AddProfile<MappingProfile>();
});

// Registrar como singleton
IMapper mapper=mapperConfig.CreateMapper();
builder.Services.AddSingleton(mapper);

var app = builder.Build();

// Configure the HTTP request pipeline.
if ( app.Environment.IsDevelopment() )
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseGlobalExceptionMiddleware();

app.UseHttpsRedirection();
app.UseCors();
app.UseAuthorization();

app.MapControllers();

app.Run();
