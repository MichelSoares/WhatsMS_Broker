using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhatsMS_Broker.Domain.Entidades;

namespace WhatsMS_Broker.Data.Configurations
{
    public class AccountMSConfiguration : IEntityTypeConfiguration<AccountMS>
    {
        public void Configure(EntityTypeBuilder<AccountMS> builder)
        {
            builder.ToTable("tb_accounts");

            builder.Property(x => x.Id)
                    .HasColumnName("id")
                    .UseIdentityColumn();

            builder.Property(x => x.ClientName)
                   .HasColumnName("client_name")
                   .IsRequired()
                   .HasMaxLength(100);

            builder.Property(x => x.PhoneNumber)
                   .HasColumnName("phone_number")
                   .IsRequired()
                   .HasMaxLength(20);

            builder.Property(x => x.SessionName)
                   .HasColumnName("session_name")
                   .HasMaxLength(50);

            builder.Property(x => x.WebhookUrl)
                   .HasColumnName("webhook_url")
                   .HasMaxLength(255);

            builder.Property(x => x.AuthToken)
                   .HasColumnName("auth_token")
                   .HasMaxLength(255);

            builder.Property(x => x.PortRun)
                   .HasColumnName("port_run")
                   .HasMaxLength(10);

            builder.Property(x => x.IsActive)
                   .HasColumnName("is_active")
                   .HasDefaultValue(false)
                   .IsRequired();

            builder.Property(x => x.ClientSessionID)
                  .HasColumnName("client_session_id");

            builder.Property(x => x.CreatedAt)
                   .HasColumnName("created_at")
                   .IsRequired()
                   .HasDefaultValueSql("CURRENT_TIMESTAMP");

            builder.Property(x => x.UpdatedAt)
                   .HasColumnName("updated_at")
                   .IsRequired()
                   .HasDefaultValueSql("CURRENT_TIMESTAMP");

            builder.Property(x => x.QrCodeBase64)
                  .HasColumnName("qrcode_base64")
                  .HasColumnType("text");
        }
    }
}
