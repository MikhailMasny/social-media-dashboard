using Microsoft.EntityFrameworkCore.Migrations;

namespace SocialMediaDashboard.Application.Migrations
{
    public partial class AddSubscriptionTable : Migration
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1062:Validate arguments of public methods", Justification = "<Pending>")]
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Statistics_Medias_MediaId",
                table: "Statistics");

            migrationBuilder.DropIndex(
                name: "IX_Statistics_MediaId",
                table: "Statistics");

            migrationBuilder.DropColumn(
                name: "MediaId",
                table: "Statistics");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "Statistics");

            migrationBuilder.AddColumn<int>(
                name: "SubscriptionId",
                table: "Statistics",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Type",
                table: "Medias",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Subscriptions",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<int>(nullable: false),
                    IsDisplayed = table.Column<bool>(nullable: false),
                    MediaId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subscriptions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Subscriptions_Medias_MediaId",
                        column: x => x.MediaId,
                        principalTable: "Medias",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Statistics_SubscriptionId",
                table: "Statistics",
                column: "SubscriptionId");

            migrationBuilder.CreateIndex(
                name: "IX_Subscriptions_MediaId",
                table: "Subscriptions",
                column: "MediaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Statistics_Subscriptions_SubscriptionId",
                table: "Statistics",
                column: "SubscriptionId",
                principalTable: "Subscriptions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1062:Validate arguments of public methods", Justification = "<Pending>")]
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Statistics_Subscriptions_SubscriptionId",
                table: "Statistics");

            migrationBuilder.DropTable(
                name: "Subscriptions");

            migrationBuilder.DropIndex(
                name: "IX_Statistics_SubscriptionId",
                table: "Statistics");

            migrationBuilder.DropColumn(
                name: "SubscriptionId",
                table: "Statistics");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "Medias");

            migrationBuilder.AddColumn<int>(
                name: "MediaId",
                table: "Statistics",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Type",
                table: "Statistics",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Statistics_MediaId",
                table: "Statistics",
                column: "MediaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Statistics_Medias_MediaId",
                table: "Statistics",
                column: "MediaId",
                principalTable: "Medias",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
