using System;
using LottoFunctions.Data;
using LottoFunctions.Data.Models;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Threading.Tasks;

namespace LottoFunctions.EntryPoints
{
    public class OnFacebookQueueMessage
    {
        [FunctionName("OnFacebookQueueMessage")]
        public async Task Run([QueueTrigger("facebook", Connection = "AzureWebJobsStorage")]Draw draw, ILogger log)
        {
            log.LogInformation($"C# Queue trigger function processed");
            Facebook facebook = new Facebook(
                Environment.GetEnvironmentVariable("facebook_pageaccesstoken"),
                Environment.GetEnvironmentVariable("facebook_pageid"));

            string numbers = string.Join(", ", draw.DrawDetails.Select(drawDetail => drawDetail.Number).ToList());
            string post =
                $"{(DrawType)draw.DrawType} draw on {draw.DrawDate.ToString("dddd, dd MMMM yyyy")} - {numbers}";

            await facebook.PublishSimplePost(post);
        }
    }
}
