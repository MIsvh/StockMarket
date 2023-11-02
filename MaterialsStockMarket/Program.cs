using System.Reflection;
using Hangfire;
using Hangfire.PostgreSql;
using Microsoft.EntityFrameworkCore;
using MaterialsStockMarket.Data;
using MaterialsStockMarket.Entities.Material.Commands;
using MediatR;
using Microsoft.OpenApi.Models;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Получение строки подключения к базе данных
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
//регистрация контекста базы данных
builder.Services.AddDbContext<StockMarketDb>
    (options => options.UseNpgsql(connectionString));

//Подключение MediatR
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

//Добавление Hangfire
builder.Services.AddHangfire(configururation => configururation
    .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
    .UseSimpleAssemblyNameTypeSerializer()
    .UseRecommendedSerializerSettings()
    .UsePostgreSqlStorage(connectionString)
);

builder.Services.AddHangfireServer();

//Добавление информации об API
builder.Services.AddSwaggerGen(c =>
{
    c.EnableAnnotations();
    c.SwaggerDoc("v1", new OpenApiInfo 
        {   Title = "StockMarket API", 
            Version = "v1" 
        });
});

var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHangfireDashboard();

//получение класса посредника
IMediator? mediator = app.Services.GetService<IMediator>() ?? throw new ArgumentNullException("Service mediator mast not be null");

//создание повторяющейся задачи обновления цен в 8:00 каждого деня
RecurringJob.AddOrUpdate(
    "RandomUpdateMaterialsPrices"
    , () => mediator.Send(new UpdateMaterialsPricesCommand(),new CancellationToken()
    )
    , "0 8 * * *"
    , new RecurringJobOptions(){ TimeZone = TimeZoneInfo.Local}
);

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();