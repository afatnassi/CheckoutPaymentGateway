using Microsoft.EntityFrameworkCore;
using PaymentGateway.Domain.Entities;
using PaymentGateway.Domain.Enums;
using PaymentGateway.Domain.Interfaces;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace PaymentGateway.Application.Implementation
{
	public class PaymentGatewayService : IPaymentGatewayService
	{
		private ITransactionRepository TransactionRepository { get; }

		public PaymentGatewayService(
			ITransactionRepository transactionRepository)
		{
			TransactionRepository = transactionRepository;
		}

		public async Task<Guid> AddTransaction(IPaymentRequest paymentRequest)
		{
			var transaction = new Transaction()
			{
				Id = Guid.NewGuid(),
				CardNumber = paymentRequest.CardNumber,
				Cvv = paymentRequest.Cvv,
				ExpiryDate = paymentRequest.ExpiryDate,
				PaymentStatus = PaymentStatus.Processing
			};

			await TransactionRepository.CreateTransaction(transaction);
			return transaction.Id;
		}

		public async Task UpdateTransaction(Guid id, IPaymentResponse paymentResponse)
		{
			var transaction = await TransactionRepository.GetById(id);

			transaction.PaymentStatus = paymentResponse.Status;
			transaction.BankTransactionId = paymentResponse.Id;

			await TransactionRepository.UpdateTransaction(transaction);
		}
	}
}
