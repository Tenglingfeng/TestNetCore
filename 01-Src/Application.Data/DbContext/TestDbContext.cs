using Microsoft.EntityFrameworkCore;
using T.Application.Data.DbContext;

namespace Application.Data.DbContext
{
    public class TestDbContext : Microsoft.EntityFrameworkCore.DbContext
    {
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            DbContextHelper.DbSet(modelBuilder);
            DbContextHelper.LogicDelete(modelBuilder);

            base.OnModelCreating(modelBuilder);
        }
    }
}