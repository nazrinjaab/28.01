using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using TaroTime.Domain.Entities;
using TaroTime.Domain.Entities.common;
using TaroTime.Persistence.Contexts.Common;
using Color = TaroTime.Domain.Entities.Color;

namespace TaroTime.Persistence.Contexts
{
    internal class AppDbContext : IdentityDbContext<AppUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            modelBuilder.ApplyAllQueryFilters();
            // Global filterlər
            modelBuilder.Entity<Product>()
                .HasQueryFilter(p => !p.IsDeleted);

            modelBuilder.Entity<Tag>()
                .HasQueryFilter(t => !t.IsDeleted);

            // Əlaqəli entity-lərə də filter əlavə et
            modelBuilder.Entity<ProductColor>()
                .HasQueryFilter(pc => !pc.Product.IsDeleted);

            modelBuilder.Entity<ProductTag>()
                .HasQueryFilter(pt => !pt.Tag.IsDeleted);



            modelBuilder.Entity<Appointment>()
    .HasOne(a => a.User)
    .WithMany()
    .HasForeignKey(a => a.UserId)
    .OnDelete(DeleteBehavior.Restrict); // ❌ Cascade yox

modelBuilder.Entity<Appointment>()
    .HasOne(a => a.Expert)
    .WithMany()
    .HasForeignKey(a => a.ExpertId)
    .OnDelete(DeleteBehavior.Cascade); // ✅ yalnız birində cascade



            base.OnModelCreating(modelBuilder);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            _setDateTime();

            return base.SaveChangesAsync(cancellationToken);
        }


        private void _setDateTime()
        {
            var datas = ChangeTracker.Entries<BaseAccountableEntity>();
            foreach (var entry in datas)
            {
                switch (entry.State)
                {
                    //case EntityState.Detached:
                    //    break;
                    //case EntityState.Unchanged:  
                    //    break;
                    //case EntityState.Deleted:
                    //    break;
                    case EntityState.Modified:
                        var isDeletedChanged = entry.OriginalValues.GetValue<bool>(nameof(Category.IsDeleted))
                        != entry.CurrentValues.GetValue<bool>(nameof(Category.IsDeleted));
                        if (!isDeletedChanged)
                        {
                            entry.Entity.UpdatedAt = DateTime.UtcNow;
                        }    
                       
                        break;
                    case EntityState.Added:
                        entry.Entity.CreatedAt = DateTime.UtcNow;
                        break;
                   
                }
            }
        }


        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<ProductTag> ProductTags { get; set; }
        public DbSet<Color> Colors { get; set; }
        public DbSet<ProductColor> ProductColors { get; set; }
        public DbSet<TarotReading> TarotReadings { get; set; }
        public DbSet<CoffeeReading> CoffeeReadings { get; set; }
        public DbSet<PalmReading> PalmReadings { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<Feedback> Feedbacks { get; set; }
        


    }
}
