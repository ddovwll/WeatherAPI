using WeatherAPI.BLL;
using WeatherAPI.BLL.Region;
using WeatherAPI.BLL.RegionType;
using WeatherAPI.DAL;
using WeatherAPI.DAL.Region;
using WeatherAPI.DAL.RegionType;

namespace WeatherAPI;

public class Program
{
    public static void Main(string[] args)
    {
        var context = new Context(); 
        context.Database.EnsureDeleted();
        context.Database.EnsureCreated();
        
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddAuthorization();
        builder.Services.AddAuthentication();

        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        builder.Services.AddControllers();

        builder.Services.AddSingleton<IAccountDAL, AccountDAL>();
        builder.Services.AddScoped<IAccountBLL, AccountBLL>();
        builder.Services.AddSingleton<IEncrypt, Encrypt>();
        builder.Services.AddSingleton<IAuth, Auth>();
        builder.Services.AddSingleton<IRegionDAL, RegionDAL>();
        builder.Services.AddScoped<IRegionBLL, RegionBLL>();
        builder.Services.AddSingleton<IRegionTypeDAL, RegionTypeDAL>();
        builder.Services.AddScoped<IRegionTypeBLL, RegionTypeBLL>();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseRouting();
        
        app.UseAuthorization();

        app.UseAuthentication();
        
        app.UseEndpoints(endpoints => { endpoints.MapControllers();});

        app.MapControllerRoute(
            name: "accountsId",
            pattern: "{controller=Account}/{action=GetAccountById}/{id}");

        app.Run();
    }
}