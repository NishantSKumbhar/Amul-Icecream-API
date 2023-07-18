using Amul.Data;
using Amul.Mappings;
using Amul.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// INSERT 1
builder.Services.AddDbContext<AmulDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("AmulConnectionString")));

builder.Services.AddAutoMapper(typeof(AutoMapperProfile));

builder.Services.AddScoped<IIcecreamRepository, SQLIcecreamRepository>();
builder.Services.AddScoped<ICategoriesRepository, SQLCategoryRepository>();

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
