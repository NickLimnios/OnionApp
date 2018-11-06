using Microsoft.EntityFrameworkCore;
using Onion.Data.Configuration;

namespace Onion.Data
{
    public class OnionContext : DbContext
    {
        
        public OnionContext()
        { }

        public OnionContext(DbContextOptions<OnionContext> options)
           : base(options)
        { }

        public DbSet<TEnt> GetDbSet<TEnt>() where TEnt : class
        {
            return Set<TEnt>();
        }

        public virtual void Commit()
        {
            base.SaveChanges(); 
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                #warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=OnionDB;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new PersonConfiguration());
            modelBuilder.ApplyConfiguration(new ProjectConfiguration());
            modelBuilder.ApplyConfiguration(new EntryConfiguration());
        }
    }
}
