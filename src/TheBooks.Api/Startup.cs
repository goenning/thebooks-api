using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;
using TheBooks.Api.Utils;
using System.Data.Common;
using Npgsql;
using Serilog;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Hosting;

namespace TheBooks.Api
{
    public class Startup
    {
        private string GetDatabaseUrl()
        {
            var url = Environment.GetEnvironmentVariable("DATABASE_URL");
            if (string.IsNullOrWhiteSpace(url))
                return @"Server=localhost;Port=5555;Database=thebooks;User Id=thebooks;Password=thebooks-pw;";
            return DbHelpers.FormatUrlToConnectionString(url);
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<DbConnection>((provider) => {
                var connectionString = GetDatabaseUrl();
                Log.Information($"Creating new DbConnection to '{connectionString}'.");
                return new NpgsqlConnection(connectionString);
            });
            services.AddMvc();
            services.AddSwaggerGen();
            
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .Enrich.FromLogContext()
                .WriteTo.LiterateConsole()
                .CreateLogger();
        }
 
        public void Configure(IApplicationBuilder app, 
                              ILoggerFactory loggerFactory, 
                              IApplicationLifetime appLifetime)
        {
            loggerFactory.AddSerilog();
            appLifetime.ApplicationStopped.Register(Log.CloseAndFlush);

            app.UseMvc();
            app.UseSwagger();
            app.UseSwaggerUi();
        }
    }
}
