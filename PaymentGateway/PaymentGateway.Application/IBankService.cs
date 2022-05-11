using PaymentGateway.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentGateway.Application
{
	public interface IBankService
	{
		Task<IPaymentResponse> ProcessTransaction(IPaymentRequest PaymentRequest);
	}
}
