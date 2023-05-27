using Microsoft.EntityFrameworkCore;
using OdinApi.Models;
using OdinApi.Models.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<OdinContext>(opciones =>
    opciones.UseSqlServer(builder.Configuration.GetConnectionString("OdinContext")));

//Esta configuracion permite referencias circulares
builder.Services.AddControllers().AddNewtonsoftJson(options =>
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddTransient<IUserModel, UserModel>();
builder.Services.AddTransient<IRolModel, RolModel>();
builder.Services.AddTransient<IBranchModel, BranchModel>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();