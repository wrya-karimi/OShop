using Data;
using Data.Respositories.CategoryRepository;
using Data.Respositories.ProductRepository;
using Serilog;
using Services.Categories;
using Services.Products;

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

        builder.Services.AddScoped<ProductRepository>();
        builder.Services.AddScoped<IProductRepository, CacheProductRepository>();



        builder.Services.AddStackExchangeRedisCache(redisOptions =>
        {
            string connection = builder.Configuration.GetConnectionString("Redis");
            redisOptions.Configuration = connection;
        });

        builder.Services.AddTransient<ICategoryService, CategoryService>();
        builder.Services.AddTransient<ICategoryRepository, CategoryRepository>();

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