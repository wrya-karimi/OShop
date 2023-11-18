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

        builder.Services.AddTransient<ProductReadRepository>();
        builder.Services.AddTransient<IProductReadRepository, CacheProductReadRepository>();
        builder.Services.AddTransient<IProductWriteRepository, ProductWriteRepository>();

        //builder.Services.AddScoped<CategoryReadRepository>();
        //builder.Services.AddScoped<ICategoryReadRepository, CacheCategoryReadRepository>();
        builder.Services.AddTransient<ICategoryReadRepository, CategoryReadRepository>();
        builder.Services.AddTransient<ICategoryWriteRepository, CategoryWriteRepository>();

        builder.Services.AddStackExchangeRedisCache(redisOptions =>
        {
            string connection = builder.Configuration.GetConnectionString("Redis");
            redisOptions.Configuration = connection;
        });

        builder.Services.AddTransient<ICategoryService, CategoryService>();
        builder.Services.AddTransient<IProductService, ProductService>();

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