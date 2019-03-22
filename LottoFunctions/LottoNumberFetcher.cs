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
        private IDrawsRepo _drawsRepo;
        public LottoNumberFetcher(ILogger<LottoNumberFetcher> log, IDrawsRepo drawsRepo)
        {
            _drawsRepo = drawsRepo;
            _log = log;
        }
        
        [FunctionName("LottoNumberFetcher")]
        public async Task Run([TimerTrigger("0 */15 18-21 * * *")]TimerInfo myTimer)
        {
            _log.LogCritical($"lol");
            _log.LogInformation($"LottoNumberFetcher - Started Execution on: {DateTime.Now}");
            //await _webScraper.ProcessNumbers(DrawType.Lotto, "https://www.maltco.com/lotto/results/do_results.php");
            _log.LogInformation($"LottoNumberFetcher - Finished Execution on: {DateTime.Now}");
        }
    }
}
