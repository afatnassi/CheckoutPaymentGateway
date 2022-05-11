using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PaymentGateway.Application;
using PaymentGateway.Domain.Entities;
using PaymentGateway.Domain.Enums;
using PaymentGateway.Domain.Exceptions;
using PaymentGateway.Domain.Interfaces;
using System;
using System.Threading.Tasks;

namespace PaymentGateway.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class PaymentGatewayController : ControllerBase
	{
		private ILogger<PaymentGatewayController> Logger {get;}
		private IPaymentFacade PaymentFacade { get; set; }

		public PaymentGatewayController(
			ILogger<PaymentGatewayController> logger, 
			IPaymentFacade paymentFacade)
		{
			Logger = logger;
			PaymentFacade = paymentFacade;
		}

		[HttpGet]
		[Route("payment")]
		public async Task<IActionResult> GetPayment([FromQuery] Guid id)
		{
			try
			{
				var response = await PaymentFacade.RetrievePaymentDetails(id);
				return Ok(response);
			}
			catch(NotFoundException ex)
			{
				return NotFound(ex.Message);
			}
		}

		/// <summary>
		/// Processes Payment
		/// </summary>
		/// <param name="request"></param>
		/// <returns></returns>
		/// <remarks>
		/// Sample Request:
		///		
		///		{
		///			"cardNumber": "4242424242424242",
		///			"expiryDate": "01/24",
		///			"cvv": "424",
		///			"currency": "EUR",
		///			"amount": 10
		///		}
		///		
		/// </remarks>
		[HttpPost]
		[Route("process-payment")]
		public async Task<IActionResult> ProcessPayment([FromBody] InvoiceRequest request)
		{
			var response = await PaymentFacade.ProcessPayment(request);
			switch (response.Status)
			{
				case PaymentStatus.Processing:
					return Accepted("GetPayment", response);
				case PaymentStatus.Failed:
					return StatusCode(402, response);
				case PaymentStatus.Succeeded:
					return Ok(response);
				default:
					return StatusCode(500);
			}
		}
	}
}
