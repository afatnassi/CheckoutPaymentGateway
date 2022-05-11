using PaymentGateway.Domain.Entities;
using System;
using System.Threading.Tasks;

namespace PaymentGateway.Application
{
	public interface ITransactionRepository
	{
		Task CreateTransaction(Transaction transaction);

		Task<Transaction> GetById(Guid id);

		Task<Transaction> GetByBankId(Guid bankTransactionId);

		Task UpdateTransaction(Transaction transaction);
	}
}
