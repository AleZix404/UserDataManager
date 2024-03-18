
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using UserDataManager.EntityFramework.Context;
using UserDataManager.EntityFramework.DTO;
using UserDataManager.EntityFramework.Models;
using UserDataManager.Services;
using UserDataManager.Services.Interface;

namespace UserDataManager
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddDbContext<UserDataContext>(options => 
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("UserDataConnection"));
            });
            builder.Services.AddScoped<IUserDataClientServices, UserDataClientServices>();
            builder.Services.AddHttpClient<IUserDataClientServices, UserDataClientServices>(
                 n =>
                 n.BaseAddress = new Uri(builder.Configuration["UrlUserData"])
                 );
            builder.Services.AddScoped<IUserDataCRUDServices<UserData.UserDataResponse, UserDataDTO, UserDataInsertDTO, UserData.Address, AdressDataInsertDTO>, UserDataServices>();

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

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
