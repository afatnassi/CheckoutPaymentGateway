using Microsoft.Extensions.Logging;
using PaymentGateway.Domain.Entities;
using PaymentGateway.Domain.Enums;
using PaymentGateway.Domain.Exceptions;
using PaymentGateway.Domain.Interfaces;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace PaymentGateway.Application.Implementation
{
	public class PaymentFacade : IPaymentFacade
	{
		private IBankService BankService { get; }
		private IPaymentGatewayService PaymentService { get; }
		private ITransactionRepository TransactionRepository { get; }
		private ILogger<PaymentFacade> Logger { get; }
		public PaymentFacade(
			IPaymentGatewayService paymentService,
			IBankService bankService,
			ILogger<PaymentFacade> logger, 
			ITransactionRepository transactionRepository)
		{
			PaymentService = paymentService;
			BankService = bankService;
			Logger = logger;
			TransactionRepository = transactionRepository;
		}

		public async Task<IPaymentResponse> ProcessPayment(IPaymentRequest paymentRequest)
		{
			var id = await PaymentService.AddTransaction(paymentRequest);
			try
			{
				var response = await BankService.ProcessTransaction(paymentRequest);
				await PaymentService.UpdateTransaction(id, response);
				return response;
			}
			catch (HttpRequestException ex)
			{
				Logger.LogError($"Transaction {id} : Error occured when calling bank service", ex);
				return new PaymentResponse()
				{
					Status = PaymentStatus.Processing
				};
			}
			
		}

		public async Task<IPaymentDetails> RetrievePaymentDetails(Guid id)
		{
			var transaction = await TransactionRepository.GetByBankId(id);

			if (transaction is null)
			{
				throw new NotFoundException($"Transaction with id: {id} not found");
			}

			return new PaymentDetails()
			{
				CardNumber = "XXXX XXXX XXXX " + transaction.CardNumber.Substring(12,4),
				Cvv = "XXX",
				ExpiryDate = transaction.ExpiryDate,
				PaymentStatus = transaction.PaymentStatus
			};
		}
	}
}
