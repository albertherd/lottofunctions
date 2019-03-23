using Microsoft.Azure.WebJobs;
using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using LottoFunctions.Data.Models;
using LottoFunctions.Interfaces;
using LottoFunctions.Data.Repo;

namespace LottoFunctions
{
    public class LottoNumberFetcher
    {
        private ILogger<LottoNumberFetcher> _log;
        private IWebScraper _webScraper;
        private IDrawsRepo _drawsRepo;

        public LottoNumberFetcher(ILogger<LottoNumberFetcher> log, IWebScraper webScraper, IDrawsRepo drawsRepo)
        {
            _webScraper = webScraper;
            _log = log;
            _drawsRepo = drawsRepo;
        }
        //"0 */15 18-21 * * *"
        [FunctionName("LottoNumberFetcher")]
        public async Task Run([TimerTrigger("0 */15 18-21 * * *")]TimerInfo myTimer,
            [Queue("facebook", Connection = "AzureWebJobsStorage")] IAsyncCollector<Draw> facebookQueue,
            [Queue("database", Connection = "AzureWebJobsStorage")] IAsyncCollector<Draw> databaseQueue)
        {
            _log.LogInformation($"LottoNumberFetcher - Started Execution on: {DateTime.Now}");

            Draw draw = await _webScraper.GetDraw(DrawType.Lotto, "https://www.maltco.com/lotto/results/do_results.php");
            if(!await _drawsRepo.DrawExists(draw.DrawType, draw.DrawNo))
            {
                await facebookQueue.AddAsync(draw);
                await databaseQueue.AddAsync(draw);
            }
            _log.LogInformation($"LottoNumberFetcher - Finished Execution on: {DateTime.Now}");
        }
    }
}
