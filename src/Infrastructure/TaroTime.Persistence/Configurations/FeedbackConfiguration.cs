using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaroTime.Domain.Entities;

namespace TaroTime.Persistence.Configurations
{
    internal class FeedbackConfiguration : IEntityTypeConfiguration<Feedback>
    {
        public void Configure(EntityTypeBuilder<Feedback> builder)
        {
            builder.HasKey(f => f.Id);

            builder.Property(f => f.UserName)
                .HasMaxLength(100);

            builder.Property(f => f.Email)
                .HasMaxLength(200);

            builder.Property(f => f.Type)
                .IsRequired()
                .HasConversion<string>();

            builder.Property(f => f.Message)
                .HasMaxLength(1000);

            builder.Property(f => f.CreatedAt)
                .HasDefaultValueSql("GETUTCDATE()");
        }

    }
}
