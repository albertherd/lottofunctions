using Microsoft.Azure.WebJobs;
using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using LottoFunctions.Data.Models;
using LottoFunctions.Interfaces;

namespace LottoFunctions
{
    public class Super5NumberFetcher
    {
        private IWebScraper _webScraper;
        private ILogger<Super5NumberFetcher> _log;
        public Super5NumberFetcher(ILogger<Super5NumberFetcher> log)
        {
            _log = log;
        }

        [FunctionName("Super5NumberFetcher")]
        public async Task Run([TimerTrigger("0 */15 18-21 * * *")]TimerInfo myTimer)
        {
            _log.LogInformation($"Super5NumberFetcher - Started Execution on: {DateTime.Now}");
            //await _webScraper.ProcessNumbers(DrawType.Super5, "https://www.maltco.com/super/results/do_results.php");
            _log.LogInformation($"Super5NumberFetcher - Finished Execution on: {DateTime.Now}");
        }
    }
}
