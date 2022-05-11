using PaymentGateway.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentGateway.Domain.Interfaces
{
	public interface IPaymentResponse
	{
		public Guid? Id { get; set; }
		public PaymentStatus Status { get; set; }
	}
}
