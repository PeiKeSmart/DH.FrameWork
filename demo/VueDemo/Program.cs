
using Microsoft.AspNetCore.SpaServices;

using NewLife.Log;

using VueCliMiddleware;

namespace VueDemo
{
    public class Program
    {
        public static void Main(string[] args)
        {
            XTrace.UseConsole();

            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            // NOTE: PRODUCTION Ensure this is the same path that is specified in your webpack output
            builder.Services.AddSpaStaticFiles(opt => opt.RootPath = "ClientApp/dist");

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            //builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                //app.UseSwagger();
                //app.UseSwaggerUI();
            }

            //app.UseHttpsRedirection();

            // NOTE: PRODUCTION uses webpack static files
            app.UseSpaStaticFiles();

            app.UseStaticFiles();

            app.UseAuthorization();


            app.MapControllers();

            //app.MapWhen(context => context.Request.Path.Value?.Contains(".txt") == true, application => {
            //    XTrace.WriteLine($"½øÀ´ÁËÂð£¿");
            //    application.Use(async (context, next) =>
            //    {
            //        await context.Response.WriteAsync("q");
            //        await next();
            //    });
            //});

            // NOTE: VueCliProxy is meant for developement and hot module reload
            // NOTE: SSR has not been tested
            // Production systems should only need the UseSpaStaticFiles() (above)
            // You could wrap this proxy in either
            // if (System.Diagnostics.Debugger.IsAttached)
            // or a preprocessor such as #if DEBUG
            app.MapToVueCliProxy(
                "{*path}",
                new SpaOptions { SourcePath = "ClientApp" },
                npmScript: (System.Diagnostics.Debugger.IsAttached) ? "serve" : null,
                regex: "Compiled successfully",
                forceKill: true
                );

            app.Run();
        }
    }
}