//using Menu_Digital.Repositories.Implementations;
//using Menu_Digital.Repositories.Interfaces;
//using Menu_Digital.Services.Implementation;
//using Menu_Digital.Services.Interfaces;
//using MenuDigital.Data;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.IdentityModel.Tokens;
//using Microsoft.OpenApi.Models;
//using System.Text;

//var builder = WebApplication.CreateBuilder(args);

//// Add services to the container.

//builder.Services.AddControllers();
//// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
//builder.Services.AddEndpointsApiExplorer();

//builder.Services.AddSwaggerGen();

//builder.Services.AddSwaggerGen(setupAction =>
//{
//    setupAction.AddSecurityDefinition("ApiBearerAuth", new OpenApiSecurityScheme
//    {
//        Type = SecuritySchemeType.Http,
//        Scheme = "Bearer",
//        Description = "Acá pegar el token generado al loguearse."
//    });

//    setupAction.AddSecurityRequirement(new OpenApiSecurityRequirement
//    {
//        {
//            new OpenApiSecurityScheme
//            {
//                Reference = new OpenApiReference
//                {
//                    Type = ReferenceType.SecurityScheme,
//                    Id = "ApiBearerAuth"
//                }
//            },
//            new List<string>()
//        }
//    });
//});

//builder.Services.AddAuthentication("Bearer")
//    .AddJwtBearer(options =>
//    {
//        options.TokenValidationParameters = new()
//        {
//            ValidateIssuer = true,
//            ValidateAudience = true,
//            ValidateIssuerSigningKey = true,
//            ValidIssuer = builder.Configuration["Authentication:Issuer"],
//            ValidAudience = builder.Configuration["Authentication:Audience"],
//            IssuerSigningKey = new SymmetricSecurityKey(
//                Encoding.ASCII.GetBytes(builder.Configuration["Authentication:SecretForKey"]))
//        };
//    });
//builder.Services.AddScoped<IRestaurantService, RestaurantService>();
//builder.Services.AddScoped<IRestaurantRepository, RestaurantRepository>();
//builder.Services.AddScoped<IProductRepository, ProductRepository>();
//builder.Services.AddScoped<IProductService, ProductService>();
//builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
//builder.Services.AddScoped<ICategoryService,CategoryService>();


//builder.Services.AddDbContext < MenuDigitalContext > (dbContextOptions => dbContextOptions.UseSqlite(builder.Configuration["ConnectionStrings:MenuDigitalDBConnectionString"]));
//var app = builder.Build();

//// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI();
//}

//app.UseHttpsRedirection();

//app.UseAuthorization();

//app.MapControllers();

//app.Run();}

using Menu_Digital.Repositories.Implementations;
using Menu_Digital.Repositories.Implementations;
using Menu_Digital.Repositories.Interfaces;
using Menu_Digital.Services.Implementation;
using Menu_Digital.Services.Interfaces;
using MenuDigital.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// ======================================================
//  C O R S — permitir acceso del frontend (5173)
// ======================================================
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

// ======================================================
//  Controllers
// ======================================================
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

// ======================================================
//  Swagger + JWT Authentication docs
// ======================================================
builder.Services.AddSwaggerGen(setupAction =>
{
    setupAction.AddSecurityDefinition("ApiBearerAuth", new OpenApiSecurityScheme
    {
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        Description = "Pegá acá el token obtenido al loguearte."
    });

    setupAction.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "ApiBearerAuth"
                }
            },
            new List<string>()
        }
    });
});

// ======================================================
//  JWT Authentication
// ======================================================
builder.Services.AddAuthentication("Bearer")
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new()
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Authentication:Issuer"],
            ValidAudience = builder.Configuration["Authentication:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.ASCII.GetBytes(builder.Configuration["Authentication:SecretForKey"]))
        };
    });

// ======================================================
//  Dependency Injection
// ======================================================
builder.Services.AddScoped<IRestaurantService, RestaurantService>();
builder.Services.AddScoped<IRestaurantRepository, RestaurantRepository>();

builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IProductService, ProductService>();

builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<ICategoryService, CategoryService>();

// ======================================================
//  Database
// ======================================================
builder.Services.AddDbContext<MenuDigitalContext>(options =>
    options.UseSqlite(builder.Configuration["ConnectionStrings:MenuDigitalDBConnectionString"])
);

// ======================================================
//  BUILD APP
// ======================================================
var app = builder.Build();

// ======================================================
//  Middleware
// ======================================================
app.UseCors("AllowFrontend");

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();   // NECESARIO si tenés JWT
app.UseAuthorization();

app.MapControllers();

app.Run();
