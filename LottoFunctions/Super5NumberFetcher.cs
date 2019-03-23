﻿using Microsoft.Azure.WebJobs;
using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using LottoFunctions.Data.Models;
using LottoFunctions.Interfaces;

namespace LottoFunctions
{
    public class Super5NumberFetcher
    {        
        private ILogger<Super5NumberFetcher> _log;
        private IWebScraper _webScraper;

        public Super5NumberFetcher(IWebScraper webScraper, ILogger<Super5NumberFetcher> log)
        {
            _webScraper = webScraper;
            _log = log;
        }

        [FunctionName("Super5NumberFetcher")]
        public async Task Run([TimerTrigger("0 */15 18-21 * * *")]TimerInfo myTimer)
        {
/*            _log.LogInformation($"Super5NumberFetcher - Started Execution on: {DateTime.Now}");
            await _webScraper.ProcessNumbers(DrawType.Super5, "https://www.maltco.com/super/results/do_results.php");
            _log.LogInformation($"Super5NumberFetcher - Finished Execution on: {DateTime.Now}");*/
        }
    }
}
