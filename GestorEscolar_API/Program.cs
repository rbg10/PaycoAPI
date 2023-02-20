using Data.Repositories;
using Data;
using Business.IRepositorios;
using Business;
using Data.Repositorios;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
string conn = builder.Configuration.GetConnectionString("ConnectionDB");

#region Dependencias Data
// Inyecta dependencias data Repository.
builder.Services.AddTransient<ICuentasRepositorio>(a => new CuentasRespositorio(conn));
builder.Services.AddTransient<ICatalogosRepositorio>(a => new CatalogosRepositorio(conn));
builder.Services.AddTransient<INotificacionesRepositorio>(a => new NotificacionesRepositorio(conn));

#endregion

#region Dependencias Business
// Inyecta dependencias business.
builder.Services.AddTransient<ICuentas, Cuentas>();
builder.Services.AddTransient<ICatalogos, Catalogos>();
builder.Services.AddTransient<INotificaciones, Notificaciones>();

#endregion


#region JWT
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(o =>
{
    o.TokenValidationParameters = new TokenValidationParameters
    {

        IssuerSigningKey = new SymmetricSecurityKey
        (Encoding.UTF8.GetBytes(builder.Configuration["JWT:Key"])),
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true
    };
});
#endregion

builder.Services.AddAuthorization();
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
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();
