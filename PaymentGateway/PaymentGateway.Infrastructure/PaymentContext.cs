using Microsoft.EntityFrameworkCore;
using PaymentGateway.Application;
using PaymentGateway.Domain.Entities;
using PaymentGateway.Infrastructure.Configuration;
using System.Linq;
using System.Threading.Tasks;

namespace PaymentGateway.Infrastructure
{
	public class PaymentContext : DbContext, IPaymentContext
	{
		public PaymentContext(DbContextOptions<PaymentContext> options)
			: base(options) { }

		public DbSet<Transaction> Transactions => Set<Transaction>();

		public async Task<int> SaveChangesAsync()
		{
			return await base.SaveChangesAsync();
		}

		protected override void OnModelCreating(ModelBuilder builder)
		{
			builder.ApplyConfiguration(new TransactionConfiguration());
		}
	}
}
