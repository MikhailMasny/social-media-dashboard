using Microsoft.EntityFrameworkCore.Migrations;

namespace SocialMediaDashboard.Application.Migrations
{
    public partial class RenameNameToAccountNameSubscriptionTypesTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                schema: "counter",
                table: "Subscriptions");

            migrationBuilder.AddColumn<string>(
                name: "AccountName",
                schema: "counter",
                table: "Subscriptions",
                maxLength: 127,
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AccountName",
                schema: "counter",
                table: "Subscriptions");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                schema: "counter",
                table: "Subscriptions",
                type: "nvarchar(127)",
                maxLength: 127,
                nullable: false,
                defaultValue: "");
        }
    }
}
