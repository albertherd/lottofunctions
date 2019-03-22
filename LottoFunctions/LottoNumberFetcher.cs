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
        private WebScraper _webScraper;
        private ILogger<LottoNumberFetcher> _log;
        public LottoNumberFetcher(WebScraper webScraper, ILogger<LottoNumberFetcher> log)
        {
            _webScraper = webScraper;
            _log = log;
        }
        
        [FunctionName("LottoNumberFetcher")]
        public async Task Run([TimerTrigger("0 */15 18-21 * * *")]TimerInfo myTimer)
        {
            _log.LogCritical($"lol");
            _log.LogInformation($"LottoNumberFetcher - Started Execution on: {DateTime.Now}");
            await _webScraper.ProcessNumbers(DrawType.Lotto, "https://www.maltco.com/lotto/results/do_results.php");
            _log.LogInformation($"LottoNumberFetcher - Finished Execution on: {DateTime.Now}");
        }
    }
}
