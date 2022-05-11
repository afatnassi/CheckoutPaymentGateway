using PaymentGateway.Domain.Entities;
using PaymentGateway.Domain.Enums;
using PaymentGateway.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentGateway.Application
{
	public interface IPaymentGatewayService
	{
		Task<Guid> AddTransaction(IPaymentRequest paymentRequest);
		Task UpdateTransaction(Guid id, IPaymentResponse paymentResponse);
	}
}
