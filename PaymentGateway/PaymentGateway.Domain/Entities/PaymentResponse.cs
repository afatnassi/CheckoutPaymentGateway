using PaymentGateway.Domain.Enums;
using PaymentGateway.Domain.Interfaces;
using System;
using System.Text.Json.Serialization;

namespace PaymentGateway.Domain.Entities
{
	public class PaymentResponse : IPaymentResponse
	{
		public Guid? Id { get; set; }
		[JsonConverter(typeof(JsonStringEnumConverter))]
		public PaymentStatus Status { get; set; }
	}
}
