using System;
using System.Linq;
using System.Threading.Tasks;
using LottoFunctions.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace LottoFunctions.Data.Repo
{
    public class DrawsRepo : IDisposable, IDrawsRepo
    {
        private LottoContext _lottoContext;

        public DrawsRepo(LottoContext lottoContext)
        {
            _lottoContext = lottoContext;
        }

        public async Task AddDraw(Draw draw)
        {
            await _lottoContext.Draws.AddAsync(draw);            
        }

        public async Task<bool> DrawExists(int drawType, int drawNo)
        {
            return await _lottoContext.Draws.Where(draw => draw.DrawType.Equals((int)drawType) && draw.DrawNo.Equals(drawNo)).AnyAsync();
        }

        public async Task SaveChanges()
        {
            await _lottoContext.SaveChangesAsync();
        }

        public void Dispose()
        {
            _lottoContext.Dispose();
        }        
    }
}
