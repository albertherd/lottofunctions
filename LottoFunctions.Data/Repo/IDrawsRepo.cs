using LottoFunctions.Data.Models;
using Microsoft.Azure.WebJobs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LottoFunctions.Data.Repo
{
    public interface IDrawsRepo
    {
        Task<bool> DrawExists(int drawType, int drawNo);
        Task AddDraw(Draw draw);
        Task EnqueueDraw(Draw draw, params IAsyncCollector<Draw>[] queues);
        Task SaveChanges();
    }
}
