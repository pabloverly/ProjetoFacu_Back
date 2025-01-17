using System.Security.Cryptography;
using System.Text;
using ApiTools;
using ApiTools.Context;
using ApiTools.Model;
using ApiTools.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
// builder.Services.AddSwaggerGen(); //sem bearer
builder.Services.AddHttpClient();
//cors
builder.Services.AddCors(c =>
{
    c.AddPolicy("AllowOrigin", opções =>
    {
        opções.AllowAnyOrigin();
        opções.AllowAnyHeader();
        opções.AllowAnyMethod();

    });
});
builder.Services.AddScoped<IProdutoRepository, ProdutoRepository>();
builder.Services.AddScoped<IProdutoService, ProdutoService>();

// configuracao do context db
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseMySQL(builder.Configuration.GetConnectionString("MySqlConnection"));
});


//com bearer
builder.Services.AddSwaggerGen(c =>
   {
       c.SwaggerDoc("v1", new OpenApiInfo { Title = "Coffee API", Version = "v1" });

       // Adicionar esquema de segurança JWT ao Swagger
       c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
       {
           Description = "JWT Authorization header using the Bearer scheme. Example: \"Bearer {token}\"",
           Name = "Authorization",
           In = ParameterLocation.Header,
           Type = SecuritySchemeType.ApiKey,
           Scheme = "Bearer"
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
                new string[] { }
            }
       });
   });

var key = Encoding.ASCII.GetBytes(Settings.Secret);


builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(x =>
{
    x.RequireHttpsMetadata = false;
    x.SaveToken = true;
    x.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = false,
        ValidateAudience = false
    };
});

var app = builder.Build();

// Configure the HTTP request pipeline.
// if (app.Environment.IsDevelopment())
// {
app.UseSwagger();
app.UseSwaggerUI();
// }

app.UseHttpsRedirection();

//primeiro autenticacao depois autorizacao
app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

//cors
app.UseCors("AllowOrigin");

app.Run();
