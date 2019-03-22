using LottoFunctions.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LottoFunctions.Data.Repo
{
    public interface IDrawsRepo
    {
        Task<bool> DrawExists(DrawType drawType, int drawNo);
        Task AddDraw(Draw draw);
        Task SaveChanges();
    }
}
