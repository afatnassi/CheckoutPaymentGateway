using PaymentGateway.Domain.Interfaces;
using System;
using System.Threading.Tasks;

namespace PaymentGateway.Application
{
	public interface IPaymentFacade
	{
		Task<IPaymentResponse> ProcessPayment(IPaymentRequest paymentRequest);

		Task<IPaymentDetails> RetrievePaymentDetails(Guid id);
	}
}
