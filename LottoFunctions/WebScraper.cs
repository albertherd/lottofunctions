using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using LottoFunctions.Data.Models;
using LottoFunctions.Data.Repo;
using LottoFunctions.Interfaces;

namespace LottoFunctions
{
    public class WebScraper: IWebScraper
    {
        private HtmlDocument _htmlDocument;
        private static HttpClient _httpClient = new HttpClient();

        public async Task<Draw> GetDraw(DrawType drawType, string resultsUrl)
        {
            string html = await GetHtmlAsString(resultsUrl);
            _htmlDocument = GetHtmlDocument(html);

            Draw draw = new Draw
            {
                DrawType = (int)drawType,
                DrawDate = GetDrawDate(),
                DrawNo = GetDrawNumber(),
                DrawDetails = new List<DrawDetail>()
            };

            List<int> numbers = GetNumbers();
            for (int i = 0; i < numbers.Count; i++)
            {
                draw.DrawDetails.Add(new DrawDetail { Number = numbers[i], DrawOrder = i + 1 });
            }

            return draw;
        }

        private async Task<string> GetHtmlAsString(string resultsUrl)
        {
            Dictionary<string, string> formContent = new Dictionary<string, string>();
            formContent.Add("LoadData", "LoadData");
            var postRequest = await _httpClient.PostAsync(resultsUrl, new FormUrlEncodedContent(formContent));
            string result = await postRequest.Content.ReadAsStringAsync();
            return result;
        }

        private HtmlDocument GetHtmlDocument(string html)
        {
            HtmlDocument document = new HtmlDocument();
            document.LoadHtml(html);
            return document;
        }

        private List<int> GetNumbers()
        {
            HtmlNode resultsDiv = _htmlDocument.GetElementbyId("draw_holder_results_numbers");
            HtmlNode centerElement = resultsDiv.ChildNodes.Where(node => node.Name.Equals("center")).FirstOrDefault();
            return centerElement.ChildNodes.Where(node => node.Id.Equals("number_style")).ToList()
                .Select(node => Convert.ToInt32(CleanupString(node.InnerText))).ToList();
        }

        private DateTime GetDrawDate()
        {
            var div = _htmlDocument.GetElementbyId("7_day");
            string dateAsString = GetDateFromNameAttribute(_htmlDocument.GetElementbyId("7_day"));
            return DateTime.ParseExact(dateAsString, "yyyy-MM-dd", null);
        }

        private int GetDrawNumber()
        {
            return Convert.ToInt32(CleanupString(_htmlDocument.GetElementbyId("draw_holder").InnerText).Split(':')[1]);
        }

        private string GetDateFromNameAttribute(HtmlNode htmlNode)
        {
            return htmlNode.Attributes.FirstOrDefault(attribute => attribute.Name.Equals("name")).Value;
        }

        private string CleanupString(string s)
        {
            return s.Replace("\n", string.Empty).Replace("\t", string.Empty);
        }
    }
}
