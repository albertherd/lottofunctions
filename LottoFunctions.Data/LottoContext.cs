using System;
using Microsoft.EntityFrameworkCore;
using LottoFunctions.Data.Models;

namespace LottoFunctions.Data
{
    public class LottoContext : DbContext
    {
        public LottoContext(DbContextOptions<LottoContext> dbContextOptions) :
            base(dbContextOptions)
        {
        }

        public virtual DbSet<Draw> Draws { get; set; }
        public virtual DbSet<DrawDetail> DrawDetails { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }
    }
}
