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
            builder.Property(x => x.Id).HasColumnName("id");

            // Relacionamento com AccountMS (associando AccountId com a chave primária de AccountMS)
            builder.HasOne(x => x.Account)
                   .WithMany() // Supondo que AccountMS pode ter muitos MessageOutbounds
                   .HasForeignKey(x => x.AccountId)
                   .OnDelete(DeleteBehavior.Restrict); // Restrição de exclusão caso a conta seja excluída

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

            builder.Property(x => x.CreatedAt)
                   .HasColumnName("created_at")
                   .IsRequired()
                   .HasDefaultValueSql("CURRENT_TIMESTAMP");

            builder.Property(x => x.SentAt)
                   .HasColumnName("sent_at")
                   .IsRequired();

            // Se MessageStatus for um enum, utilize o seguinte mapeamento
            builder.Property(x => x.Status)
                   .IsRequired()
                   .HasConversion<string>(); // Converte o enum para string (se for do tipo enum)

            builder.HasIndex(x => x.Status); // Índice para Status, útil para queries
        }
    }
}
