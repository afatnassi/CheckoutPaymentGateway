using DummyBankApi.Enums;
using DummyBankApi.Requests;
using DummyBankApi.Responses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DummyBankApi.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class DummyBankController : ControllerBase
	{
		private readonly ILogger<DummyBankController> _logger;

		public DummyBankController(ILogger<DummyBankController> logger)
		{
			_logger = logger;
		}

		[HttpPost]
		[Route("process-payment")]
		public IActionResult ProcessPayment([FromBody]InvoiceRequest invoiceRequest)
		{

			if (invoiceRequest.Currency.ToUpper() == "EUR")
			{
				var successfulBankResponse = new BankResponse()
				{
					Id = Guid.NewGuid(),
					Status = PaymentStatus.Succeeded
				};
				return Ok(successfulBankResponse);
			}

			var failedBankResponse = new BankResponse()
			{
				Id = Guid.NewGuid(),
				Status = PaymentStatus.Failed
			};

			return StatusCode(402, failedBankResponse);
		}
	}
}
