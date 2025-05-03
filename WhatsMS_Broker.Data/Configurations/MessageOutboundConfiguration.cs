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
    public class MessageOutboundConfiguration : IEntityTypeConfiguration<MessageOutbound>
    {
        public void Configure(EntityTypeBuilder<MessageOutbound> builder)
        {
            builder.ToTable("tb_message_outbound");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                   .HasColumnName("id")
                   .UseIdentityColumn();

            builder.Property(x => x.IdMsg)
                   .HasColumnName("id_msg")
                   .IsRequired();

            builder.Property(x => x.AccountId)
                   .HasColumnName("account_id");

            builder.HasOne(x => x.Account)
                   .WithMany()
                   .HasForeignKey(x => x.AccountId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.Property(x => x.CreatedAt)
                   .HasColumnName("created_at")
                   .IsRequired()
                   .HasDefaultValueSql("CURRENT_TIMESTAMP");

            builder.Property(x => x.SentAt)
                   .HasColumnName("sent_at")
                   .IsRequired();

            builder.Property(x => x.FromNumber)
                   .HasColumnName("from_number")
                   .IsRequired()
                   .HasMaxLength(20);

            builder.Property(x => x.ToNumber)
                   .HasColumnName("to_number")
                   .IsRequired()
                   .HasMaxLength(20);

            builder.Property(x => x.MessageType)
                   .HasColumnName("message_type")
                   .IsRequired()
                   .HasMaxLength(50);

            builder.Property(x => x.Content)
                   .HasColumnName("content")
                   .IsRequired()
                   .HasColumnType("text");

            builder.Property(x => x.Type)
                   .HasColumnName("type")
                   .HasColumnType("text");

            builder.Property(x => x.MidiaContentType)
                   .HasColumnName("midia_content_type")
                   .HasColumnType("text");

            builder.Property(x => x.MidiaURL)
                   .HasColumnName("midia_url")
                   .HasColumnType("text");

            builder.Property(x => x.Latitude)
                    .HasColumnName("latitude")
                    .HasColumnType("decimal(10, 6)");

            builder.Property(x => x.Longitude)
                    .HasColumnName("longitude")
                    .HasColumnType("decimal(10, 6)");

            builder.Property(x => x.IsGroup)
                   .HasColumnName("is_group");

            builder.Property(x => x.Status)
                   .HasColumnName("status")
                   .IsRequired()
                   .HasConversion<string>();

            builder.HasIndex(x => x.Status);
        }
    }

}
