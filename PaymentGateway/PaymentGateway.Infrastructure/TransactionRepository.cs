using Microsoft.EntityFrameworkCore;
using PaymentGateway.Application;
using PaymentGateway.Domain.Entities;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace PaymentGateway.Infrastructure
{
	public class TransactionRepository : ITransactionRepository
	{
		private IPaymentContext PaymentContext { get; }

		public TransactionRepository(IPaymentContext paymentContext)
		{
			PaymentContext = paymentContext;
		}

		public async Task CreateTransaction(Transaction transaction)
		{
			await PaymentContext.Transactions.AddAsync(transaction);
			await PaymentContext.SaveChangesAsync();
		}

		public async Task<Transaction> GetById(Guid id)
		{
			return await PaymentContext.Transactions.FindAsync(id);
		}

		public async Task<Transaction> GetByBankId(Guid bankTransactionId)
		{
			var transaction = await PaymentContext
				.Transactions
				.Where(t => t.BankTransactionId == bankTransactionId)
				.FirstOrDefaultAsync();

			return transaction;
		}

		public async Task UpdateTransaction(Transaction transaction)
		{
			PaymentContext.Transactions.Update(transaction);
			await PaymentContext.SaveChangesAsync();
		}
	}
}
