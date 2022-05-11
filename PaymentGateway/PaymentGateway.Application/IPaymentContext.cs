using Microsoft.EntityFrameworkCore;
using PaymentGateway.Domain.Entities;
using System.Threading.Tasks;

namespace PaymentGateway.Application
{
	public interface IPaymentContext
	{
		DbSet<Transaction> Transactions { get; }

		Task<int> SaveChangesAsync();
	}
}
