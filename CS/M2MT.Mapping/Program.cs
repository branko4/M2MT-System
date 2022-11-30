using M2MT.Shared.Service.Model;
using M2MT.Shared.IService.InformationModel;
using Npgsql;
using System.Data;
using M2MT.Shared.IRepository.InformationModel;
using M2MT.Shared.Repository.Model;
using M2MT.Shared.IService.Mapping;
using M2MT.Shared.Service.Mapping;
using M2MT.Shared.IRepository.Mapping;
using M2MT.Shared.Repository.Mapping;

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
            
            // Services
            builder.Services.AddTransient<IInformationModelReadService, InformationModelReadService>();
            builder.Services.AddTransient<IElementReadService, ElementReadService>();
            builder.Services.AddTransient<IAttributeReadService, AttributeReadService>();
            builder.Services.AddTransient<IMappingCRUDService, MappingCRUDService>();
            builder.Services.AddTransient<IMappingRelationCRUDService, MappingRelationCRUDService>();
            builder.Services.AddTransient<IMappingRuleCRUDService, MappingRuleCRUDService>();
            builder.Services.AddTransient<IMappingReadService, MappingReadService>();
            builder.Services.AddTransient<IMappingRuleReadService, MappingRuleReadService>();
            builder.Services.AddTransient<IMappingRelationReadService, MappingRelationReadService>();

            // Repositories
            builder.Services.AddTransient<IInformationModelReadRepository, InformationModelReadRepository>();
            builder.Services.AddTransient<IElementReadRepository, ElementReadRepository>();
            builder.Services.AddTransient<IAttributeReadRepository, AttributeReadRepository>();
            builder.Services.AddTransient<IMappingCRUDRepository, MappingCRUDRepository>();
            builder.Services.AddTransient<IMappingRelationCRUDRepository, MappingRelationCRUDRepository>();
            builder.Services.AddTransient<IMappingRuleCRUDRepository, MappingRuleCRUDRepository>();
            builder.Services.AddTransient<IMappingReadRepository, MappingReadRepository>();
            builder.Services.AddTransient<IMappingRelationReadRepository, MappingRelationReadRepository>();
            builder.Services.AddTransient<IMappingRuleReadRepository, MappingRuleReadRepository>();
            

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