
using MongoDB.Driver;
using MongoDB_Demo.Repository;
using MongoDB_Demo.Serrvices.Mapping;

namespace MongoDB_Demo
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            PollMapping.Register();
            OptionMapping.Register();

            builder.Services.Configure<Models.MongoDbSettings>(
                builder.Configuration.GetSection("MongoDbSettings"));

            builder.Services.AddSingleton<IMongoClient>(sp =>
            {
                var settings = builder.Configuration.GetSection("MongoDbSettings").Get<Models.MongoDbSettings>();
                return new MongoClient(settings!.ConnectionString);
            });

            builder.Services.AddScoped<IPollRepository, PollRepository>();
            builder.Services.AddScoped<IOptionRepository,OptionRepository>();

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
