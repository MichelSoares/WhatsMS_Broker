using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhatsMS_Broker.Data.Configurations;
using WhatsMS_Broker.Domain.Entidades;

namespace WhatsMS_Broker.Data.Context
{
    public class BrokerDbContext : DbContext
    {
        public BrokerDbContext(DbContextOptions<BrokerDbContext> options) : base(options) { }

        public DbSet<AccountMS> Accounts { get; set; }
        public DbSet<MessageInbound> MessageInbounds { get; set; }
        public DbSet<MessageOutbound> MessageOutbounds { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(BrokerDbContext).Assembly);

            //modelBuilder.ApplyConfiguration(new AccountMSConfiguration());
            //modelBuilder.ApplyConfiguration(new MessageInboundConfiguration());
            //modelBuilder.ApplyConfiguration(new MessageOutboundConfiguration());

            base.OnModelCreating(modelBuilder);
        }
    }
}
