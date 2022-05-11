using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PaymentGateway.Infrastructure.Migrations
{
    public partial class Create_Transaction_Table : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Transactions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    CardNumber = table.Column<string>(type: "TEXT", nullable: true),
                    ExpiryDate = table.Column<string>(type: "TEXT", nullable: true),
                    Cvv = table.Column<string>(type: "TEXT", nullable: true),
                    PaymentStatus = table.Column<int>(type: "INTEGER", nullable: false),
                    BankTransactionId = table.Column<Guid>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transactions", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Transactions");
        }
    }
}
