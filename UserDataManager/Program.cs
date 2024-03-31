
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using UserDataManager.Automappers;
using UserDataManager.EntityFramework.Context;
using UserDataManager.EntityFramework.DTO;
using UserDataManager.EntityFramework.Models;
using UserDataManager.Repository.Class;
using UserDataManager.Repository.Interface;
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
                //options.UseOracle(builder.Configuration.GetConnectionString("UserDataConnection"));
            });
            builder.Services.AddScoped<IUserDataClientServices, UserDataClientServices>();
            builder.Services.AddHttpClient<IUserDataClientServices, UserDataClientServices>(
                 n =>
                 n.BaseAddress = new Uri(builder.Configuration["UrlUserData"])
                 );
            builder.Services.AddScoped<IDataInsertServices<UserDataDTO, UserDataInsertDTO, AddressDTO, AddressDataInsertDTO>, UserDataInsertServices>();
            builder.Services.AddScoped<IReadDataServices<UserDataDTO>, UserDataReadServices>();
            builder.Services.AddScoped<IDataUpdateServices<UserDataUpdateDTO, UserDataDTO>, UserDataUpdateServices>();
            builder.Services.AddScoped<IDataDeleteServices, UserDataDeleteServices>();
            builder.Services.AddScoped<IRepository<UserData.UserDResp, UserData.Address>, UserDataRepository>();

            builder.Services.AddAutoMapper(typeof(MapperProfile));

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
