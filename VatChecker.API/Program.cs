using FluentValidation;
using Microsoft.EntityFrameworkCore;
using VatChecker.API.Validators;
using VatChecker.Business;
using VatChecker.Business.Services;
using VatChecker.Contracts;
using VatChecker.DataAccess;
using VatChecker.DataAccess.Repository;
using VatChecker.WSDL;

var builder = WebApplication.CreateBuilder(args);

// --- Services registration ---

builder.Services.AddControllers();

// Register Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// EF Core, CustomerService, WSDL client, FluentValidation
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<VatCheckerDbContext>(options =>
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<ICustomerService, CustomerService>();

builder.Services.AddSingleton<checkVatPortTypeClient>();
builder.Services.AddSingleton<VatWebServiceClient>();

builder.Services.AddValidatorsFromAssemblyContaining<VatRequestValidator>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<VatCheckerDbContext>();
    db.Database.EnsureCreated(); // crea DB e tabelle se non esistono
}

// --- Middleware pipeline ---

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();         // <-- must come AFTER AddSwaggerGen
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();