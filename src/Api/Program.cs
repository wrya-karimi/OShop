using OShop.Domain.Abstracts.Application;
using OShop.Domain.Abstracts.Repositories.CategoryRepositories;
using OShop.Domain.Abstracts.Repositories.ProductRepositories;
using OShop.Domain.Application;
using OShop.Infrastructures.Persistence.Contexts;
using OShop.Infrastructures.Persistence.Respositories.CategoryRepositories;
using OShop.Infrastructures.Persistence.Respositories.ProductRepositories;
using Serilog;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        Log.Logger = new LoggerConfiguration()
            .ReadFrom.Configuration(builder.Configuration).CreateLogger();
        //.MinimumLevel.Information()


        builder.Host.UseSerilog();

        builder.Services.AddScoped<ProductReadRepository>();
        builder.Services.AddScoped<IProductReadRepository, CacheProductReadRepository>();
        builder.Services.AddScoped<IProductWriteRepository, ProductWriteRepository>();

        //builder.Services.AddScoped<CategoryReadRepository>();
        //builder.Services.AddScoped<ICategoryReadRepository, CacheCategoryReadRepository>();
        builder.Services.AddScoped<ICategoryReadRepository, CategoryReadRepository>();
        builder.Services.AddScoped<ICategoryWriteRepository, CategoryWriteRepository>();

        builder.Services.AddStackExchangeRedisCache(redisOptions =>
        {
            string connection = builder.Configuration.GetConnectionString("Redis");
            redisOptions.Configuration = connection;
        });

        builder.Services.AddScoped<ICategoryService, CategoryService>();
        builder.Services.AddScoped<IProductService, ProductService>();

        builder.Services.AddDbContext<ApplicationDbContext>();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseSerilogRequestLogging();

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}