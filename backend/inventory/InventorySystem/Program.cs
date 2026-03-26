
using InventorySystem.API.Extensions;
using InventorySystem.Infrastracture.Data;
using InventorySystem.Infrastracture.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Serilog;
using Serilog.Events;

namespace InventorySystem
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            //DbContext
            builder.Services.AddDbContext<InventoryDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            builder.Services.Configure<JwtSettings>(
            builder.Configuration.GetSection("JwtSettings"));
            // Add services to the container.
            builder.Services.AddIdentityService(builder.Configuration);
            builder.Services.AddApplicationService();

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Host.UseSerilog((ctx, lc) => lc.MinimumLevel.Information().MinimumLevel
          .Override("Microsoft.AspNetCore", LogEventLevel.Warning)
         .MinimumLevel.Override("Microsoft.EntityFrameworkCore", LogEventLevel.Warning)
       .WriteTo.Console()
       .WriteTo.File(
             path: Path.Combine(ctx.HostingEnvironment.ContentRootPath, "Logs", "app-.log"),
             rollingInterval: RollingInterval.Day,
             outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level:u3}] {Message:lj}{NewLine}{Exception}",
             retainedFileCountLimit: 31).Enrich.FromLogContext()
              .Enrich.WithMachineName()
             );

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAll",
                    policy =>
                    {
                        policy.AllowAnyOrigin()
                              .AllowAnyHeader()
                              .AllowAnyMethod();
                    });
            });


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

         
            app.UseHttpsRedirection();
            app.UseCors("AllowAll");
            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
