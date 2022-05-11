using PaymentGateway.Domain.Enums;
using PaymentGateway.Domain.Interfaces;
using System;

namespace PaymentGateway.Domain.Entities
{
	public class Transaction : IEntity
	{
		public Guid Id { get; set; }
		public string CardNumber { get; set; }
		public string ExpiryDate { get; set; }
		public string Cvv { get; set; }
		public PaymentStatus PaymentStatus { get; set; }
		public Guid? BankTransactionId {get; set;}
	}
}
