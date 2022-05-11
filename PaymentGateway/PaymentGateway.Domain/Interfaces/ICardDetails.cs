using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentGateway.Domain.Interfaces
{
	public interface ICardDetails
	{
		public string CardNumber { get; set; }
		public string ExpiryDate { get; set; }
		public string Cvv { get; set; }
	}
}
