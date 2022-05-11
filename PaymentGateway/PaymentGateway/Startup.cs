using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using PaymentGateway.Application;
using PaymentGateway.Application.Implementation;
using PaymentGateway.Infrastructure;
using PaymentGateway.Infrastructure.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace PaymentGateway
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{

			services.AddControllers();
			services.AddSwaggerGen(c =>
			{
				c.SwaggerDoc("v1", new OpenApiInfo { Title = "PaymentGateway", Version = "v1" });

				var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
				c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
			});

			services.AddDbContext<PaymentContext>(options => options.UseSqlite(@"Data Source=C:\PaymentGateway.db"));

			services.AddScoped<IPaymentContext>(provider => provider.GetRequiredService<PaymentContext>());

			services.AddTransient<IPaymentFacade, PaymentFacade>();
			services.AddTransient<IPaymentGatewayService, PaymentGatewayService>();
			services.AddTransient<ITransactionRepository, TransactionRepository>();

			services.AddHttpClient<IBankService, BankService>(client =>
			{
				client.BaseAddress = new Uri(Configuration["BankConfiguration:Uri"]);
			});

			services
				.Configure<BankConfiguration>(Configuration.GetSection("BankConfiguration"));
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
				app.UseSwagger();
				app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "PaymentGateway v1"));
			}

			app.UseHttpsRedirection();

			app.UseRouting();

			app.UseAuthorization();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers();
			});
		}
	}
}
