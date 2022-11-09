using M2MT.Shared.Service.Model;
using M2MT.Shared.IService.InformationModel;
using Npgsql;
using System.Data;
using M2MT.Shared.IRepository.InformationModel;
using M2MT.Shared.Repository.Model;

namespace M2MT.Mapping
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

            string connectionString = builder.Configuration.GetConnectionString("PostgresConnectionString");

            builder.Services.AddTransient<IInformationModelReadService, InformationModelReadService>();
            builder.Services.AddTransient<IInformationModelReadRepository, InformationModelReadRepository>();

            builder.Services.AddScoped<IDbConnection, NpgsqlConnection>((IServiceProvider sp) =>
            {
                return new NpgsqlConnection(connectionString);
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
            app.UseRouting();


            app.MapControllers();

            app.Run();
        }
    }
}