using LottoFunctions.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LottoFunctions.Interfaces
{
    public interface IWebScraper
    {
        Task ProcessNumbers(DrawType drawType, string resultsUrl);
    }
}
