using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DummyBankApi.Requests
{
	public record InvoiceRequest
	{
		public string CardNumber { get; set; }
		public string ExpiryDate { get; set; }
		public string Cvv { get; set; }
		public string Currency { get; set; }
		public decimal Amount { get; set; }
	}
}
