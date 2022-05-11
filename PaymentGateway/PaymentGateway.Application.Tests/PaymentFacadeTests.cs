using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using PaymentGateway.Application.Implementation;
using PaymentGateway.Domain.Entities;
using PaymentGateway.Domain.Enums;
using PaymentGateway.Domain.Exceptions;
using PaymentGateway.Domain.Interfaces;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace PaymentGateway.Application.Tests
{
	public class PaymentFacadeTests
	{
		private IPaymentFacade paymentFacade;
		private Mock<IBankService> mockBankService;
		private Mock<ITransactionRepository> mockRepo;

		[SetUp]
		public void Setup()
		{
			mockRepo = new Mock<ITransactionRepository>();		
			mockBankService = new Mock<IBankService>();
			var mockPaymentService = new Mock<IPaymentGatewayService>();
			var mockLogger = new Mock<ILogger<PaymentFacade>>();

			paymentFacade = new PaymentFacade(mockPaymentService.Object,
				mockBankService.Object, mockLogger.Object, mockRepo.Object);
		}

		[Test]
		[TestCase(PaymentStatus.Succeeded)]
		[TestCase(PaymentStatus.Failed)]
		public async Task ProcessPayment_Should_return_the_bank_response_When_BankService_Returns_A_Response(PaymentStatus paymentStatus)
		{
			var paymentRequest = new InvoiceRequest()
			{
				CardNumber = "4242424242424242",
				Cvv = "424",
				ExpiryDate = "01/24",
				Amount = 10,
				Currency = "EUR"
			};

			var paymentResponse = new PaymentResponse()
			{
				Id = Guid.NewGuid(),
				Status = paymentStatus
			};

			mockBankService.Setup(service => service.ProcessTransaction(paymentRequest)).ReturnsAsync(paymentResponse);

			var result = await paymentFacade.ProcessPayment(paymentRequest);
			result.Should().BeEquivalentTo(paymentResponse);
		}

		[Test]
		public async Task ProcessPayment_Should_return_Processing_Response_When_BankService_Fails_to_return_A_response()
		{
			var paymentRequest = new InvoiceRequest()
			{
				CardNumber = "4242424242424242",
				Cvv = "424",
				ExpiryDate = "01/24",
				Amount = 10,
				Currency = "EUR"
			};

			var paymentResponse = new PaymentResponse()
			{
				Id = null,
				Status = PaymentStatus.Processing
			};

			mockBankService.Setup(service => service.ProcessTransaction(paymentRequest)).ThrowsAsync(new HttpRequestException());

			var result = await paymentFacade.ProcessPayment(paymentRequest);
			result.Should().BeEquivalentTo(paymentResponse);
		}

		[Test]
		public async Task RetrievePaymentDetails_Should_return_A_Response_With_Masked_Card_Number_Except_Last_four_Digits_and_Masked_Cvv()
		{
			var request = Guid.NewGuid();

			var transaction = new Transaction()
			{
				CardNumber = "4242424242425555",
				ExpiryDate = "02/25",
				Cvv = "123",
				PaymentStatus = PaymentStatus.Succeeded
			};

			mockRepo.Setup(repo => repo.GetByBankId(It.IsAny<Guid>())).ReturnsAsync(transaction);

			var result = await paymentFacade.RetrievePaymentDetails(request);


			result.CardNumber.Should().BeEquivalentTo("XXXX XXXX XXXX 5555");
			result.Cvv.Should().BeEquivalentTo("XXX");
			result.PaymentStatus.Should().Be(PaymentStatus.Succeeded);
			result.ExpiryDate.Should().BeEquivalentTo("02/25");
		}

		[Test]
		public async Task RetrievePaymentDetails_Should_throw_NotFoundException_when_transaction_doesnt_exist()
		{
			var request = Guid.NewGuid();

			Transaction transaction = null;

			mockRepo.Setup(repo => repo.GetByBankId(It.IsAny<Guid>())).ReturnsAsync(transaction);

			Func<Task> method = async () => await paymentFacade.RetrievePaymentDetails(request);

			await method.Should().ThrowAsync<NotFoundException>()
;			
		}
	}
}