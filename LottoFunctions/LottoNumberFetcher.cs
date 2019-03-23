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
        
        [FunctionName("LottoNumberFetcher")]
        public async Task Run([TimerTrigger("*/30 * * * * *")]TimerInfo myTimer,
            [Queue("facebookQueue")] ICollector<string> facebookQueue,
            [Queue("databaseQueue")] ICollector<string> databaseQueue)
        {
            _log.LogInformation($"LottoNumberFetcher - Started Execution on: {DateTime.Now}");

            Draw draw = await _webScraper.GetDraw(DrawType.Lotto, "https://www.maltco.com/lotto/results/do_results.php");
            if(!await _drawsRepo.DrawExists(draw.DrawType, draw.DrawNo))
            {
                facebookQueue.Add(Newtonsoft.Json.JsonConvert.SerializeObject(draw));
                databaseQueue.Add(Newtonsoft.Json.JsonConvert.SerializeObject(draw));
            }


            _log.LogInformation($"LottoNumberFetcher - Finished Execution on: {DateTime.Now}");
        }
    }
}
