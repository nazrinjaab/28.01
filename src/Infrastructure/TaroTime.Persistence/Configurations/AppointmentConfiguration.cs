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
    internal class AppointmentConfiguration : IEntityTypeConfiguration<Appointment>
    {
        public void Configure(EntityTypeBuilder<Appointment> builder)
        {
            builder.HasKey(a => a.Id);

            builder.HasOne(a => a.User)
                .WithMany()
                .HasForeignKey(a => a.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(a => a.Expert)
                .WithMany()
                .HasForeignKey(a => a.ExpertId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Property(a => a.StartTime)
                .IsRequired();

            builder.Property(a => a.EndTime)
                .IsRequired();

            builder.Property(a => a.Status)
                .IsRequired()
                .HasConversion<string>();

            builder.Property(a => a.MeetingType)
                .IsRequired()
                .HasConversion<string>();

            builder.Property(a => a.MeetingLink)
                .HasMaxLength(500);

            builder.Property(a => a.CreatedAt)
                .HasDefaultValueSql("GETUTCDATE()");
        }
    }
}
