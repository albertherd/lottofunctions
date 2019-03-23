using Microsoft.Azure.WebJobs;
using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using LottoFunctions.Data.Models;
using LottoFunctions.Interfaces;
using LottoFunctions.Data.Repo;

namespace LottoFunctions
{
    public class Super5NumberFetcher
    {        
        private ILogger<LottoNumberFetcher> _log;
        private IWebScraper _webScraper;
        private IDrawsRepo _drawsRepo;

        public Super5NumberFetcher(ILogger<LottoNumberFetcher> log, IWebScraper webScraper, IDrawsRepo drawsRepo)
        {
            _webScraper = webScraper;
            _log = log;
            _drawsRepo = drawsRepo;
        }

        //"0 */15 18-21 * * *
        [FunctionName("Super5NumberFetcher")]
        public async Task Run([TimerTrigger("*/30 * * * * *")]TimerInfo myTimer,
            [Queue("facebook", Connection = "AzureWebJobsStorage")] IAsyncCollector<Draw> facebookQueue,
            [Queue("database", Connection = "AzureWebJobsStorage")] IAsyncCollector<Draw> databaseQueue)
        {
            _log.LogInformation($"LottoNumberFetcher - Started Execution on: {DateTime.Now}");

            Draw draw = await _webScraper.GetDraw(DrawType.Super5, "https://www.maltco.com/super/results/do_results.php");
            if (!await _drawsRepo.DrawExists(draw.DrawType, draw.DrawNo))
            {
                await facebookQueue.AddAsync(draw);
                await databaseQueue.AddAsync(draw);
            }
            _log.LogInformation($"LottoNumberFetcher - Finished Execution on: {DateTime.Now}");
        }
    }
}
