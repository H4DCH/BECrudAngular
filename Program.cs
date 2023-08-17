using AutoMapper;
using BE_CRUDNET.Models;
using Microsoft.EntityFrameworkCore;

namespace BE_CRUDNET
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            //Agregado de Cors para que el bakend corra en el mismo puerto del front
            builder.Services.AddCors(option => option.AddPolicy("AllowWebapp",
                builder => builder.AllowAnyOrigin()
                .AllowAnyHeader().
                AllowAnyMethod()));

            // Agregado del context
            builder.Services.AddDbContext<Context>(options => {
                options.UseSqlServer(builder.Configuration.GetConnectionString("Conexion"));
            });
                //AUTOMAPPER
            builder.Services.AddAutoMapper(typeof(Program));
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseCors("AllowWebapp");

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}