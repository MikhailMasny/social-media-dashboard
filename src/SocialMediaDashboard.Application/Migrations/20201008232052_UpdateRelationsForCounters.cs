using Microsoft.EntityFrameworkCore.Migrations;

namespace SocialMediaDashboard.Application.Migrations
{
    public partial class UpdateRelationsForCounters : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Counters_AccountTypes_AccountTypeId",
                table: "Counters");

            migrationBuilder.DropForeignKey(
                name: "FK_Counters_SubscriptionTypes_SubscriptionTypeId",
                table: "Counters");

            migrationBuilder.DropIndex(
                name: "IX_Counters_AccountTypeId",
                table: "Counters");

            migrationBuilder.DropIndex(
                name: "IX_Counters_SubscriptionTypeId",
                table: "Counters");

            migrationBuilder.DropColumn(
                name: "AccountTypeId",
                table: "Counters");

            migrationBuilder.DropColumn(
                name: "SubscriptionTypeId",
                table: "Counters");

            migrationBuilder.AddColumn<int>(
                name: "AccountSubscriptionId",
                table: "Counters",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Counters_AccountSubscriptionId",
                table: "Counters",
                column: "AccountSubscriptionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Counters_AccountSubscriptions_AccountSubscriptionId",
                table: "Counters",
                column: "AccountSubscriptionId",
                principalTable: "AccountSubscriptions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Counters_AccountSubscriptions_AccountSubscriptionId",
                table: "Counters");

            migrationBuilder.DropIndex(
                name: "IX_Counters_AccountSubscriptionId",
                table: "Counters");

            migrationBuilder.DropColumn(
                name: "AccountSubscriptionId",
                table: "Counters");

            migrationBuilder.AddColumn<int>(
                name: "AccountTypeId",
                table: "Counters",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SubscriptionTypeId",
                table: "Counters",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Counters_AccountTypeId",
                table: "Counters",
                column: "AccountTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Counters_SubscriptionTypeId",
                table: "Counters",
                column: "SubscriptionTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Counters_AccountTypes_AccountTypeId",
                table: "Counters",
                column: "AccountTypeId",
                principalTable: "AccountTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Counters_SubscriptionTypes_SubscriptionTypeId",
                table: "Counters",
                column: "SubscriptionTypeId",
                principalTable: "SubscriptionTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
