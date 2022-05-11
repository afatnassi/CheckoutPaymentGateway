using PaymentGateway.Domain.Enums;

namespace PaymentGateway.Domain.Interfaces
{
	public interface IPaymentDetails : ICardDetails
	{
		public PaymentStatus PaymentStatus { get; set; }
	}
}
