using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BitcoinApi.Data.Migrations
{
    public partial class InitDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "transactions",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Account = table.Column<string>(maxLength: 400, nullable: false),
                    Address = table.Column<string>(maxLength: 400, nullable: false),
                    Category = table.Column<string>(maxLength: 400, nullable: false),
                    Amount = table.Column<decimal>(nullable: false),
                    Label = table.Column<string>(maxLength: 400, nullable: true),
                    TransactionId = table.Column<string>(maxLength: 400, nullable: false),
                    Confirmations = table.Column<int>(nullable: false),
                    BlockHash = table.Column<string>(maxLength: 400, nullable: false),
                    BlockIndex = table.Column<int>(nullable: false),
                    BlockTime = table.Column<int>(nullable: false),
                    Time = table.Column<int>(nullable: false),
                    TimeReceived = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_transactions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "wallets",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Account = table.Column<string>(maxLength: 400, nullable: false),
                    Balance = table.Column<decimal>(nullable: false),
                    Address = table.Column<string>(maxLength: 400, nullable: false),
                    Created = table.Column<DateTime>(nullable: false),
                    Updated = table.Column<DateTime>(nullable: false),
                    Removed = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_wallets", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "transactions");

            migrationBuilder.DropTable(
                name: "wallets");
        }
    }
}
