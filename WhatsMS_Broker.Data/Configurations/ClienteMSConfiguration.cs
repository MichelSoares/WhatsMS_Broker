using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhatsMS_Broker.Domain.Entidades;

namespace WhatsMS_Broker.Data.Configurations
{
    public class ClienteMSConfiguration : IEntityTypeConfiguration<ClienteMS>
    {
        public void Configure(EntityTypeBuilder<ClienteMS> builder)
        {
            builder.ToTable("tb_clientes");

            builder.Property(x => x.Id)
                    .HasColumnName("id")
                    .UseIdentityColumn();

            builder.Property(x => x.NameUser)
                   .HasColumnName("name_user")
                   .HasMaxLength(100)
                   .IsRequired();

            builder.Property(x => x.Email)
                   .HasColumnName("email")
                   .HasMaxLength(100)
                   .IsRequired();

            builder.Property(x => x.Password)
                   .HasColumnName("password")
                   .HasMaxLength(100)
                   .IsRequired();
        }
    }

}
