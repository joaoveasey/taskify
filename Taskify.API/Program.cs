using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using System.Text.Json;
using Taskify.API.Infra;
using Taskify.API.Interfaces;
using Taskify.API.Models;
using Taskify.API.Repository;
using Taskify.API.Services;

var builder = WebApplication.CreateBuilder(args);
var secretKey = builder.Configuration["JWT:SecretKey"] 
    ?? throw new ArgumentException("Chave JWT inválida.");

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAuthorization();

// injeção de dependencia
builder.Services.AddScoped<ITasksRepository, TasksRepository>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<ITokenService, TokenService>();

builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

// middleware de autenticação JWT
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme; // define o esquema de autenticação padrão como JWT
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options => { // adiciona a configuração do middleware de autenticação JWT
    options.SaveToken = true; 
    options.RequireHttpsMetadata = false;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ClockSkew = TimeSpan.Zero, // remove a margem de erro do token
        ValidIssuer = builder.Configuration["JWT:ValidIssuer"],
        ValidAudience = builder.Configuration["JWT:ValidAudience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey))
    };
    options.Events = new JwtBearerEvents
    {
        OnChallenge = context =>
        {
            context.HandleResponse();

            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            context.Response.ContentType = "application/json";
            var result = JsonSerializer.Serialize(new { message = "Acesso não autorizado. Faça login para continuar." });
            return context.Response.WriteAsync(result);
        }
    };
});

// contexto do banco de dados
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    var connection = builder.Configuration.GetConnectionString("Default");
    var serverVersion = new MySqlServerVersion(new Version(8, 0, 21));
    options.UseMySql(connection, serverVersion);
});

// configurando o swagger
builder.Services.AddSwaggerGen(c =>
{
    c.EnableAnnotations(); // configurando annotations nos endpoints do swagger
    c.SwaggerDoc("v1", new() { Title = "TaskifyAPI", Version = "v1" });

    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme // adiciona a definição de segurança para o swagger como Bearer
    {
        Description = "Bearer JWT",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
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
            new string [] { }
        }
    });
});

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
