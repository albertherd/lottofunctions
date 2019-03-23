using System;
using System.Threading.Tasks;
using LottoFunctions.Data.Models;
using LottoFunctions.Data.Repo;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

namespace LottoFunctions.EntryPoints
{
    public class OnDatabaseQueueMessage
    {
        private readonly IDrawsRepo _drawsRepo;
        public OnDatabaseQueueMessage(IDrawsRepo drawsRepo)
        {
            _drawsRepo = drawsRepo;
        }

        [FunctionName("OnDatabaseQueueMessage")]
        public async Task Run([QueueTrigger("database", Connection = "AzureWebJobsStorage")]Draw draw, ILogger log)
        {
            log.LogInformation($"C# Queue trigger function processed");
            await _drawsRepo.AddDraw(draw);
        }
    }
}
