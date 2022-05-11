using PaymentGateway.Domain.Enums;
using PaymentGateway.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace PaymentGateway.Domain.Entities
{
	public class PaymentDetails : IPaymentDetails
	{
		public string CardNumber { get; set; }
		public string ExpiryDate { get; set; }
		public string Cvv { get; set; }

		[JsonConverter(typeof(JsonStringEnumConverter))]
		public PaymentStatus PaymentStatus { get; set; }
	}
}
