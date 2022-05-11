using DummyBankApi.Enums;
using System;
using System.Text.Json.Serialization;

namespace DummyBankApi.Responses
{
	public record BankResponse
	{
		public Guid Id { get; set; }

		[JsonConverter(typeof(JsonStringEnumConverter))]
		public PaymentStatus Status {get; set;}
	}
}
