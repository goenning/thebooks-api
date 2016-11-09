using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using TheBooks.Api.Models;
using Microsoft.EntityFrameworkCore;
using System;
using TheBooks.Api.Utils;

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
            var connectionString = GetDatabaseUrl();
            services.AddDbContext<TheBooksContext>(options => options.UseNpgsql(connectionString));
            services.AddMvc();
        }
 
        public void Configure(IApplicationBuilder app)
        {
            app.UseMvc();
        }
    }
}
