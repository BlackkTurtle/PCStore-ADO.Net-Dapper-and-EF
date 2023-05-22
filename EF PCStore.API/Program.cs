using System.Data;
using System.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using PCStoreEF.BLL.EFRepositories;
using PCStoreEF.BLL.EFRepositories.Contracts;
using PCStoreEF.DbContexts;
using PCStoreEF.EFRepositories;
using PCStoreEF.EFRepositories.Contracts;
using PCStoreEF.Entities;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
// Connection/Transaction for ADO.NET/DAPPER database
/*builder.Services.AddScoped((s) => new SqlConnection(builder.Configuration.GetConnectionString("MSSQLConnection")));
builder.Services.AddScoped<IDbTransaction>(s =>
{
    SqlConnection conn = s.GetRequiredService<SqlConnection>();
    conn.Open();
    return conn.BeginTransaction();
});
builder.Services.AddDbContext<PCStoreDbContext>(options =>
{
    string connectionString = builder.Configuration.GetConnectionString("MSSQLConnection");
    options.UseSqlServer(connectionString,b=>b.MigrationsAssembly("EF PCStore.API"));
});*/
builder.Services.AddDbContext<PCStoreDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("MSSQLConnection"), b => b.MigrationsAssembly("EF PCStore.API"))).AddIdentity<User, Role>(config =>
{
    config.Password.RequireNonAlphanumeric = false;
    config.Password.RequireDigit = false;
    config.Password.RequiredLength = 6;
    config.Password.RequireLowercase = false;
    config.Password.RequireUppercase = false;
})
    .AddEntityFrameworkStores<PCStoreDbContext>();

builder.Services.ConfigureApplicationCookie(config => {
    config.LoginPath = "/Admin/Login";
    config.AccessDeniedPath = "/Admin/AccessDenied";
});

builder.Services.AddScoped<IEFTypesRepository, EFTypesRepository>();
builder.Services.AddScoped<IEFBrandsRepository, EFBrandsRepository>();
builder.Services.AddScoped<IEFCommentsRepository, EFCommentRepository>();
builder.Services.AddScoped<IEFOrdersRepository, EFOrdersRepository>();
builder.Services.AddScoped<IEFPartOrdersRepository, EFPartOrdersRepository>();
builder.Services.AddScoped<IEFProductsRepository, EFProductsRepository>();
builder.Services.AddScoped<IEFStatusesRepository, EFStatusesRepository>();
builder.Services.AddScoped<IEFUsersRepository, EFUsersRepository>();
builder.Services.AddScoped<IEFUnitOfWork, EFUnitOfWork>();

builder.Services.AddScoped<IEFFullProductsRepository, EFFullProductsRepository>();
builder.Services.AddScoped<IEFBLLUnitOfWork, EFBLLUnitOfWork>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();