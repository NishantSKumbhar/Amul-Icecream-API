using Amul.Data;
using Amul.Mappings;
using Amul.Middleware;
using Amul.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// INSERT 13  
var logger = new LoggerConfiguration()
    .WriteTo.Console()
    .WriteTo.File("Logs/Amul_log.txt", rollingInterval: RollingInterval.Minute)
    .MinimumLevel.Warning()  // min level set so to see warning message on console not debug or less than that.
    .CreateLogger();
builder.Logging.ClearProviders();
builder.Logging.AddSerilog(logger);


builder.Services.AddControllers();
//INSERT 10
builder.Services.AddHttpContextAccessor();


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

//INSERT 9
// Inorder to use swagger more effectively
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo { Title = "Amul Icecream API", Version = "v1" });
    options.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, new OpenApiSecurityScheme
    {
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = JwtBearerDefaults.AuthenticationScheme
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = JwtBearerDefaults.AuthenticationScheme
                },
                Scheme = "Oauth2",
                Name = JwtBearerDefaults.AuthenticationScheme,
                In = ParameterLocation.Header
            },
            new List<string>()
        }
    });
});

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
// INSERT 8
builder.Services.AddScoped<ITokenRepository,TokenRepository>();
// INSERT 11
builder.Services.AddScoped<IImageRepository, LocalImageRepository>();
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
// INSERT 14
app.UseMiddleware<ExceptionHandlerMiddleware>();

app.UseHttpsRedirection();
// INSERT 5
// Before Authorization , Authentication should happen.
app.UseAuthentication();

app.UseAuthorization();

// INSERT 12
//cause we can not see static files
//https://localhost:7098/Images/NishantGitHub.png
app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "Images")),
    RequestPath = "/Images"
});
app.MapControllers();

app.Run();
