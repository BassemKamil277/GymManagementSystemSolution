using GymManagementDAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementDAL.Data.Configurations
{
    // hayb2a feh el common configurations between mamber & trainer
    internal class GymUserConfiguration<T> : IEntityTypeConfiguration<T> where T : GymUser
        // el T dy 3mltha kda 34an yb2a ynf3 ad5l ay haga no3ha GymUser aw ay haga btwrs mnha zy [mamber aw trainer]
    {
        public void Configure(EntityTypeBuilder<T> builder)
        {
            builder.Property(x => x.Name)
                   .HasColumnType("varchar")
                   .HasMaxLength(50);

            builder.Property(x => x.Email)
                 .HasColumnType("varchar")
                 .HasMaxLength(100);

            builder.Property(x => x.Phone)
                  .HasColumnType("varchar")
                  .HasMaxLength(11);

            //lw 3aiz a7ot constraint 3la ay haga 
            builder.ToTable(Tb =>
            {
                Tb.HasCheckConstraint("GymUserValidEmailCheck", "Email Like '_%@_%._%'");
                Tb.HasCheckConstraint("GymUserValidPhoneCheck", "Phone Like '01%' and Phone Not Like '%[^0-9]%'");
            });

            //unique non clustered index
            builder.HasIndex(x => x.Email).IsUnique();
            builder.HasIndex(x => x.Phone).IsUnique();


            // 34an a3rf a8yr esm el coloum 34an kan hayb2a Address.street msln
            builder.OwnsOne(x => x.Address, AddressBuilder =>
            {
                AddressBuilder.Property(x => x.Street)
                              .HasColumnName("Street")
                              .HasColumnType("varchar")
                              .HasMaxLength(30);

                AddressBuilder.Property(x => x.City)
                              .HasColumnName("City")
                              .HasColumnType("varchar")
                              .HasMaxLength(30);

                AddressBuilder.Property(x => x.BuildingNumber)
                              .HasColumnName("BuildingNumber");
            });





        }
    }
}
