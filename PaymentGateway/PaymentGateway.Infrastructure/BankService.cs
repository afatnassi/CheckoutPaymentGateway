using PaymentGateway.Application;
using PaymentGateway.Domain.Interfaces;
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;
using PaymentGateway.Infrastructure.Configuration;
using System.Text;
using Microsoft.Extensions.Options;
using PaymentGateway.Domain.Entities;

namespace PaymentGateway.Infrastructure
{
	public class BankService : IBankService
	{
		private HttpClient HttpClient { get; }
		private BankConfiguration Configuration { get; }

		public BankService(
			HttpClient httpClient, 
			IOptions<BankConfiguration> configuration)
		{

			HttpClient = httpClient;
			Configuration = configuration.Value;
		}

		public async Task<IPaymentResponse> ProcessTransaction(
			IPaymentRequest paymentRequest)
		{
			var json = JsonConvert.SerializeObject(paymentRequest, Configuration.JsonSerializerSettings);

			var httpResponse = await HttpClient.PostAsync(
				Configuration.RequestUri,
				new StringContent(json, Encoding.UTF8, Configuration.JsonMediaType));

			var jsonResponse = await httpResponse.Content.ReadAsStringAsync();

			var response = JsonConvert.DeserializeObject<PaymentResponse>(jsonResponse, Configuration.JsonSerializerSettings);

			return response;
		}
	}
}
