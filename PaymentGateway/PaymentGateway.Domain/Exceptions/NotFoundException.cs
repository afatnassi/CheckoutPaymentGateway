using System;

namespace PaymentGateway.Domain.Exceptions
{
	public class NotFoundException : ArgumentException
	{
		public NotFoundException(string message) : base(message) { }
	}
}
