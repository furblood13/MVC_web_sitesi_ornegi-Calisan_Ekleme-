using CalisanBilgiler.Models.DBEntities;
using Microsoft.EntityFrameworkCore;

namespace CalisanBilgiler.DataErisimKatmani_DAL_
{
    public class CalisanDbContext : DbContext
    {
        public CalisanDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Calisan> Calisans { get; set; }
    }
}
