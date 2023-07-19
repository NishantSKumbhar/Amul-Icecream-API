using Amul.Data;
using Amul.Mappings;
using Amul.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// INSERT 1
builder.Services.AddDbContext<AmulDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("AmulConnectionString")));
// INSERT 6
builder.Services.AddDbContext<AmulAuthDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("AmulAuthConnectionString")));
// INSERT 2
builder.Services.AddAutoMapper(typeof(AutoMapperProfile));
// INSERT 3
builder.Services.AddScoped<IIcecreamRepository, SQLIcecreamRepository>();
builder.Services.AddScoped<ICategoriesRepository, SQLCategoryRepository>();

// INSERT 7
builder.Services.AddIdentityCore<IdentityUser>()
    .AddRoles<IdentityRole>()
    .AddTokenProvider<DataProtectorTokenProvider<IdentityUser>>("Amul")
    .AddEntityFrameworkStores<AmulAuthDbContext>()
    .AddDefaultTokenProviders();
// after 7 => for password , do it as per your requirements.
builder.Services.Configure<IdentityOptions>(options =>
{
    options.Password.RequireDigit = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    options.Password.RequiredLength = 6;
    options.Password.RequiredUniqueChars = 1;
});


// INSERT 4
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
// INSERT 5
// Before Authorization , Authentication should happen.
app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
