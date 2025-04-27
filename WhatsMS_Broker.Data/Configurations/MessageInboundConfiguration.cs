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

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnName("id");

            // Relacionamento com AccountMS (associando AccountId com a chave primária de AccountMS)
            builder.HasOne(x => x.Account)
                   .WithMany() 
                   .HasForeignKey(x => x.AccountId)
                   .OnDelete(DeleteBehavior.Restrict); 
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
                   .HasColumnType("text"); // Caso o conteúdo da mensagem seja grande (base64 ou texto longo)

            builder.Property(x => x.DateReceived)
                   .HasColumnName("date_received")
                   .IsRequired()
                   .HasDefaultValueSql("CURRENT_TIMESTAMP");

            builder.Property(x => x.IsGroup).HasColumnName("is_group")
                   .IsRequired();

            builder.HasIndex(x => x.DateReceived); // Índice para Data de Recebimento, útil para queries
        }
    }
}
