using PaymentGateway.Domain.Enums;
using PaymentGateway.Domain.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace PaymentGateway.Domain.Entities
{
	public record InvoiceRequest : IPaymentRequest
	{
		[Required(ErrorMessage = "The CardNumber field is required")]
		[CreditCard(ErrorMessage = "This card Number is not valid")]
		public string CardNumber { get; set; }

		[Required(ErrorMessage = "The ExpiryDate field is required")]
		[RegularExpression(@"^(0[1-9]|1[0-2])\/([0-9]{2})$", ErrorMessage = "The ExpiryDate format should be MM/YY")]
		public string ExpiryDate { get; set; }

		[Required(ErrorMessage = "The Cvv field is required")]
		[RegularExpression(@"^([0-9]{3})$", ErrorMessage = "The Cvv format should be three digit number")]
		public string Cvv { get; set; }

		[Required(ErrorMessage = "The Currency field is required")]
		[EnumDataType(typeof(Currency), ErrorMessage = "This currency is not supported. use either 'EUR' or 'USD'")]
		public string Currency { get; set; }

		[Required(ErrorMessage = "The Amount field is required")]
		public decimal Amount { get; set; }
	}
}
