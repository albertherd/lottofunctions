using LottoFunctions;
using LottoFunctions.Data;
using LottoFunctions.Data.Repo;
using LottoFunctions.Interfaces;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System;
using Microsoft.EntityFrameworkCore;


[assembly: WebJobsStartup(typeof(Startup))]
namespace LottoFunctions
{
    internal class Startup : IWebJobsStartup
    {
        public void Configure(IWebJobsBuilder builder)
        {
            var services = builder.Services;

            services.AddTransient<IWebScraper, WebScraper>();
            services.AddTransient<WebScraper, WebScraper>();
            services.AddTransient<IDrawsRepo, DrawsRepo>();
            services.AddDbContext<LottoContext>(options => options.UseSqlServer(Environment.GetEnvironmentVariable("sqldb_connection")), ServiceLifetime.Transient);
        }
    }

}