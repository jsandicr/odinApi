using Microsoft.EntityFrameworkCore;
using OdinApi.Models;
using OdinApi.Models.Data.Classes;
using OdinApi.Models.Data.Interfaces;

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
builder.Services.AddTransient<ICommentModel, CommentModel>();
builder.Services.AddTransient<IServiceModel, ServiceModel>();
builder.Services.AddTransient<IStatusModel, StatusModel>();
builder.Services.AddTransient<ITicketModel, TicketModel>();
builder.Services.AddTransient<IErrorLogModel, ErrorLogModel>();
builder.Services.AddTransient<ITransactionalLogModel, TransactionalLogModel>();

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