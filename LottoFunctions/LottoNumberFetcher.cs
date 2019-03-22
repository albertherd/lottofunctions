using Microsoft.Azure.WebJobs;
using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using LottoFunctions.Data.Models;
using LottoFunctions.Interfaces;

namespace LottoFunctions
{
    public class LottoNumberFetcher
    {
        private IWebScraper _webScraper;
        private ILogger<LottoNumberFetcher> _log;
        public LottoNumberFetcher(IWebScraper webScraper, ILogger<LottoNumberFetcher> log)
        {
            _webScraper = webScraper;
            _log = log;
        }


        [FunctionName("LottoNumberFetcher")]
        public async Task Run(
            [TimerTrigger("30 * 18-23 * * *")]TimerInfo myTimer)
        {
            _log.LogInformation($"LottoNumberFetcher - Started Execution on: {DateTime.Now}");
            await _webScraper.ProcessNumbers(DrawType.Lotto, "https://www.maltco.com/lotto/results/do_results.php");
            _log.LogInformation($"LottoNumberFetcher - Finished Execution on: {DateTime.Now}");
        }
    }
}
