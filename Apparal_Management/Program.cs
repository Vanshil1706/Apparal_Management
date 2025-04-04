//using Apparal_Management.Data;
//using Apparal_Management.Models;
//using Apparal_Management.Services;
//using Apparel_Management.Services.IServices;
//using Apparel_Management.Services;
//using Microsoft.AspNetCore.Identity;
//using Microsoft.EntityFrameworkCore;

//var builder = WebApplication.CreateBuilder(args);

//builder.Services.AddDbContext<ApplicationDbContext>(options =>
//    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

//builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
//    .AddEntityFrameworkStores<ApplicationDbContext>();

//builder.Services.AddScoped<IUserService, UserService>();
//builder.Services.AddControllers();
//builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();

//builder.Services.AddScoped<IUserAuthService, UserAuthService>();
//builder.Services.AddScoped<IUserService, UserService>();

//var app = builder.Build();

//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI();
//}

//app.UseHttpsRedirection();

//app.UseAuthentication();
//app.UseAuthorization();

//app.MapControllers();
//app.Run();

using Apparal_Management.Data;
using Apparal_Management.Models;
using Apparal_Management.Services;
using Apparal_Management.Services.IServices;
using Apparel_Management.Services;
using Apparel_Management.Services.IServices;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

// CORS policy to allow any origin
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins", builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});

// Identity configuration
builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
{
    options.Password.RequireDigit = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequiredLength = 6; // Minimum length 6 for security
    options.Password.RequiredUniqueChars = 0;
})
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

// Authentication using JWT
//var jwtKey = builder.Configuration["Jwt:Key"];
//if (string.IsNullOrWhiteSpace(jwtKey))
//{
//    throw new Exception("JWT Key is missing in configuration!");
//}
/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
//builder.Services.AddAuthentication(options =>
//{
//    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
//    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
//}).AddJwtBearer(options =>
//{
//    options.TokenValidationParameters = new TokenValidationParameters
//    {
//        ValidateIssuer = true,
//        ValidateAudience = true,
//        ValidateLifetime = true,
//        ValidateIssuerSigningKey = true,
//        ValidIssuer = builder.Configuration["Jwt:Issuer"],
//        ValidAudience = builder.Configuration["Jwt:Audience"],
//        //IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey))
//    };
/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = false, // 🔹 Don't validate signing key
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"]
        };
    });


// Registering Services (Dependency Injection)
builder.Services.AddScoped<IUserAuthService, UserAuthService>();
builder.Services.AddScoped<IUserService, UserService>();

// Controllers and API documentation
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();


builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Your API",
        Version = "v1",
        Description = "JWT Authentication with Swagger"
    });

    // Add JWT Authentication to Swagger
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Enter 'Bearer' [space] and then your valid token in the text input below.\nExample: 'Bearer your_token_here'"
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});


var app = builder.Build();

// Configure Middleware
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Apparel Management API v1"));

}


app.UseHttpsRedirection();

app.UseCors("AllowAllOrigins");
//app.UseHttpsRedirection(); // Uncomment if using HTTPS
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();







//using Apparal_Management.Data;
//using Apparal_Management.Models;
//using Apparal_Management.Services;
//using Apparal_Management.Services.IServices;
//using Apparel_Management.Services.IServices;
//using Apparel_Management.Services;
//using Microsoft.AspNetCore.Authentication.JwtBearer;
//using Microsoft.AspNetCore.Identity;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.IdentityModel.Tokens;
//using Microsoft.OpenApi.Models;
//using System.Text;

//var builder = WebApplication.CreateBuilder(args);

//// 🔹 Database Connection (SQL Server)
//builder.Services.AddDbContext<ApplicationDbContext>(options =>
//{
//    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
//});

//// 🔹 CORS Policy - Allow all origins (Modify if needed)
//builder.Services.AddCors(options =>
//{
//    options.AddPolicy("AllowAllOrigins", builder =>
//    {
//        builder.AllowAnyOrigin()
//               .AllowAnyMethod()
//               .AllowAnyHeader();
//    });
//});

//// 🔹 Identity Configuration (User Authentication)
//builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
//{
//    options.Password.RequireDigit = false;
//    options.Password.RequireLowercase = false;
//    options.Password.RequireUppercase = false;
//    options.Password.RequireNonAlphanumeric = false;
//    options.Password.RequiredLength = 6; // Minimum length 6
//    options.Password.RequiredUniqueChars = 0;
//})
//    .AddEntityFrameworkStores<ApplicationDbContext>()
//    .AddDefaultTokenProviders();

//// 🔹 JWT Authentication Setup
//var jwtKey = builder.Configuration["Jwt:Key"];
//if (string.IsNullOrWhiteSpace(jwtKey))
//{
//    throw new Exception("JWT Key is missing in configuration!");
//}

//builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
//    .AddJwtBearer(options =>
//    {
//        options.TokenValidationParameters = new TokenValidationParameters
//        {
//            ValidateIssuer = true,
//            ValidateAudience = true,
//            ValidateLifetime = true,
//            ValidateIssuerSigningKey = true,
//            ValidIssuer = builder.Configuration["Jwt:Issuer"],
//            ValidAudience = builder.Configuration["Jwt:Audience"],
//            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey))
//        };
//    });

//builder.Services.AddAuthorization();

//// 🔹 Dependency Injection (Register Services)
//builder.Services.AddScoped<IUserAuthService, UserAuthService>();
//builder.Services.AddScoped<IUserService, UserService>();

//// 🔹 Add Controllers
//builder.Services.AddControllers();

//// 🔹 Add API Endpoints
//builder.Services.AddEndpointsApiExplorer();

//// 🔹 Swagger Configuration (with JWT support)
//builder.Services.AddSwaggerGen(options =>
//{
//    options.SwaggerDoc("v1", new OpenApiInfo
//    {
//        Title = "Apparel Management API",
//        Version = "v1",
//        Description = "ASP.NET Core Web API with JWT Authentication"
//    });

//    // 🔸 Enable JWT Authentication in Swagger UI
//    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
//    {
//        Name = "Authorization",
//        Type = SecuritySchemeType.Http,
//        Scheme = "Bearer",
//        BearerFormat = "JWT",
//        In = ParameterLocation.Header,
//        Description = "Enter 'Bearer' followed by your JWT token.\nExample: Bearer your_token_here"
//    });

//    options.AddSecurityRequirement(new OpenApiSecurityRequirement
//    {
//        {
//            new OpenApiSecurityScheme
//            {
//                Reference = new OpenApiReference
//                {
//                    Type = ReferenceType.SecurityScheme,
//                    Id = "Bearer"
//                }
//            },
//            new string[] {}
//        }
//    });
//});

//// 🔹 Build the Application
//var app = builder.Build();

//// 🔹 Middleware Setup
//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Apparel Management API v1"));
//}

//app.UseHttpsRedirection();
//app.UseCors("AllowAllOrigins");

//app.UseAuthentication();  // 🔥 Ensures JWT authentication works
//app.UseAuthorization();

//app.MapControllers();

//app.Run();
