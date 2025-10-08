﻿using GymManagementDAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementDAL.Data.Configurations
{
    internal class PlanConiguration : IEntityTypeConfiguration<Plan>
    {
        public void Configure(EntityTypeBuilder<Plan> builder)
        {
            builder.Property(x => x.Name)
                   .HasColumnType("varchar")
                   .HasMaxLength(50);

            builder.Property(x => x.Description)
               .HasColumnType("varchar")
               .HasMaxLength(200);

            builder.Property(x => x.Price)
                   .HasPrecision(10, 2);  // l2n hya by default 18,2 

            builder.ToTable(Tb =>
            {
                Tb.HasCheckConstraint("PlanDurationCheck", "DurationDays Between 1 and 365 ");
            });
        }
    }
}
