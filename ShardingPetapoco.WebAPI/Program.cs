using Microsoft.Extensions.Configuration;
using PetaPoco;
using ShardingPetapoco.Data.Data;
using ShardingPetapoco.Data.Factory;
using ShardingPetatpoco.Services.Contracts;
using ShardingPetatpoco.Services.Implementations;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Logging.ClearProviders();
builder.Logging.AddConsole();
builder.Logging.AddDebug();


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddLogging();

builder.Services.AddScoped<RequestContextBuilder>();

builder.Services.AddScoped(sp =>
{
    var rc = sp.GetRequiredService<RequestContextBuilder>();
    return rc.BuildRequestContext();
});

string shardMapName = "TenantShardMap";
builder.Services.AddSingleton(sp =>
{
    var loggerFactory = sp.GetRequiredService<ILoggerFactory>();
    return new DbContextFactory(shardMapName, loggerFactory);
});

builder.Services.AddScoped(sp =>
{
    var dbContextFactory = sp.GetRequiredService<DbContextFactory>();
    var requestContext = sp.GetRequiredService<RequestContext>();
    return dbContextFactory.CreateDbContext(requestContext.TenantId);
});

builder.Services.AddScoped<IEmployeeService, EmployeeService>();

builder.Services.AddSingleton(new ShardingManager(builder.Configuration));

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