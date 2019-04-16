using Microsoft.EntityFrameworkCore.Migrations;

namespace SportsStoreApi.Migrations
{
    public partial class AmountOfMoney : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "AmountOfMoney",
                table: "PaymentDatas",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AmountOfMoney",
                table: "PaymentDatas");
        }
    }
}
