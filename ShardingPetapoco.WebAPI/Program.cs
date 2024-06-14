using PetaPoco;
using ShardingPetapoco.Data.Data;
using ShardingPetapoco.Data.Factory;
using ShardingPetatpoco.Services.Contracts;
using ShardingPetatpoco.Services.Implementations;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton((sp) =>
{
    var db = new Database(builder.Configuration["ConnectionStrings:MasterDB"], "System.Data.SqlClient");
    db.OpenSharedConnection();
    return db;
});
builder.Services.AddSingleton<MasterDbContext>();
builder.Services.AddScoped<DbContextFactory>();
builder.Services.AddScoped<IEmployeeService, EmployeeService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
