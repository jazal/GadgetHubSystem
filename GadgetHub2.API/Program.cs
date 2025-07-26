using GadgetHub.API.Base;
using GadgetHub.API.Data;
using GadgetHub.API.Distributors;
using GadgetHub.API.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Add EF Core

builder.Services.AddDbContext<GadgetHubContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

//Automapper
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddAutoMapper(typeof(Program));

//repositroy
builder.Services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));

//Services
builder.Services.AddScoped<ProductService>();
builder.Services.AddScoped<UserService>();
builder.Services.AddHttpClient<QuotationService>();
builder.Services.AddScoped<OrderService>();

builder.Services.Configure<List<ExternalApiEndpoint>>(builder.Configuration.GetSection("ExternalApiSettings"));
builder.Services.AddHttpClient<ExternalApiService>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
