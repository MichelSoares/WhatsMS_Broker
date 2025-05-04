using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhatsMS_Broker.Domain.Entidades;
using WhatsMS_Broker.Domain.Enums;

namespace WhatsMS_Broker.Data.Configurations
{
    public class MessageStatusConfiguration : IEntityTypeConfiguration<MessageStatus>
    {
        public void Configure(EntityTypeBuilder<MessageStatus> builder)
        {
            builder.ToTable("tb_message_status");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                   .HasColumnName("id")
                   .ValueGeneratedNever();

            builder.Property(x => x.Descricao)
                   .HasColumnName("descricao")
                   .IsRequired()
                   .HasMaxLength(50);

            builder.HasData(
                Enum.GetValues(typeof(MessageStatusEnum))
                    .Cast<MessageStatusEnum>()
                    .Select(e => new MessageStatus
                    {
                        Id = (int)e,
                        Descricao = e.ToString()
                    })
            );
        }
    }

}
