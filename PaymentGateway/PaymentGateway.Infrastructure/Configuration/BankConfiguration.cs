using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentGateway.Infrastructure.Configuration
{
	public class BankConfiguration
	{
		public Uri EndpointUrl { get; set; }

		public string RequestUri { get; set; }

        public JsonSerializerSettings JsonSerializerSettings { get; } = new()
        {
            ContractResolver = new DefaultContractResolver
            {
                NamingStrategy = new SnakeCaseNamingStrategy()
            },
            NullValueHandling = NullValueHandling.Ignore
        };

        public string JsonMediaType = "application/json";
    }
}
