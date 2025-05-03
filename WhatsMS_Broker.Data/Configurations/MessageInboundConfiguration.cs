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
    public class MessageInboundConfiguration : IEntityTypeConfiguration<MessageInbound>
    {
        public void Configure(EntityTypeBuilder<MessageInbound> builder)
        {
            builder.ToTable("tb_message_inbound");

            builder.Property(x => x.Id)
                    .HasColumnName("id")
                    .UseIdentityColumn();

            builder.Property(x => x.IdMessageWhatsApp)
                .HasColumnName("id_message_whatsApp")
                .IsRequired();

            builder.Property(x => x.AccountId)
                    .HasColumnName("account_id");

            builder.HasOne(x => x.Account)
                   .WithMany() 
                   .HasForeignKey(x => x.AccountId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.Property(x => x.DateReceived)
                  .HasColumnName("date_received")
                  .IsRequired()
                  .HasDefaultValueSql("CURRENT_TIMESTAMP");

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
            
            builder.Property(x => x.ProfileName)
               .HasColumnName("profile_name")
               .HasColumnType("text");

            builder.Property(x => x.NotifyName)
               .HasColumnName("notify_name")
               .HasColumnType("text");

            builder.Property(x => x.Author)
               .HasColumnName("author")
               .HasColumnType("text");

            builder.Property(x => x.Latitude)
                .HasColumnName("latitude")
                .HasColumnType("decimal(10, 6)");

            builder.Property(x => x.Longitude)
                .HasColumnName("longitude")
                .HasColumnType("decimal(10, 6)");

            builder.Property(x => x.IsForwarded)
                .HasColumnName("is_forwarded")
                .IsRequired()
                .HasDefaultValue(false);

            builder.Property(x => x.ForwardingScore)
               .HasColumnName("forwarding_score");

            builder.Property(x => x.IsGroup).HasColumnName("is_group")
                   .IsRequired();

            builder.Property(x => x.IsColetado)
               .HasColumnName("is_coletado")
               .IsRequired()
               .HasDefaultValue(false);

            builder.HasIndex(x => x.DateReceived); // Índice para Data de Recebimento, útil para queries
        }
    }
}
