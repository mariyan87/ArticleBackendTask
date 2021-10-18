using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Model.Entities;

namespace DataAccessImpl
{
    /// <summary>
    /// Implements the DbContext for the Database.
    /// </summary>
    public class DatabaseModelContext : DbContext, IDbContext
    {
        public DbSet<Article> Articles { get; set; }
        public DbSet<Individual> Individuals { get; set; }
        public DbSet<ApiToken> ApiTokens { get; set; }

        public DatabaseModelContext(DbContextOptions<DatabaseModelContext> options) : base(options)
        {
        }

        /// <inheritdoc/>
        public override int SaveChanges()
        {
            SetModifiedOnAndCreatedOn();

            return base.SaveChanges();
        }

        /// <inheritdoc/>
        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            SetModifiedOnAndCreatedOn();

            return base.SaveChangesAsync(cancellationToken);
        }
        private void SetModifiedOnAndCreatedOn()
        {
            var now = DateTime.UtcNow;
            var entries = ChangeTracker
               .Entries()
               .Where(e => e.Entity is Entity && (e.State == EntityState.Added || e.State == EntityState.Modified));

            foreach (var entry in entries)
            {
                ((Entity)entry.Entity).ModifiedOn = DateTime.Now;

                if (entry.State == EntityState.Added)
                {
                    var entityEntry = entry.Entity as Entity;

                    if (entityEntry.CreatedOn != DateTime.MinValue)
                    {
                        continue;
                    }

                    entityEntry.CreatedOn = now;
                }
                else
                {
                    // Don't override the value in the database.
                    entry.Property("CreatedOn").IsModified = false;
                }
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Individual>().HasData(new Individual { Id = 1, Email = "admin@SharpIT.com" });

            for (var i = 0; i < 100; i++)
            {
                modelBuilder.Entity<Article>().HasData(new Article
                {
                    Id = i + 1,
                    Title = "Article " + i,
                    Body = $"body {i} {DateTime.Now.Ticks}"
                });
            }
        }
    }
}
