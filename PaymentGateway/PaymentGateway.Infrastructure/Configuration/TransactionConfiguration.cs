using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PaymentGateway.Domain.Entities;
using System;

namespace PaymentGateway.Infrastructure.Configuration
{
	public class TransactionConfiguration : IEntityTypeConfiguration<Transaction>
	{
		public void Configure(EntityTypeBuilder<Transaction> builder)
		{
			builder.HasKey(t => t.Id);
		}
	}
}
