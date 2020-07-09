using Applications.Domains.Model;
using Microsoft.EntityFrameworkCore;
using T.Application.Data.DbContext;

namespace Applications.Data.DbContext
{
    public class TestDbContext : Microsoft.EntityFrameworkCore.DbContext
    {
        public TestDbContext(DbContextOptions<TestDbContext>  options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<User>().ToTable("User").Property(x=>x.Id).HasColumnName("UserId");

            DbContextHelper.DbSet(modelBuilder);
            DbContextHelper.LogicDelete(modelBuilder);

            base.OnModelCreating(modelBuilder);
        }

     
    }
}