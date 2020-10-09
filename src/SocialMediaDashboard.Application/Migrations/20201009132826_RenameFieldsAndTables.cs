using Microsoft.EntityFrameworkCore.Migrations;

namespace SocialMediaDashboard.Application.Migrations
{
    public partial class RenameFieldsAndTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Statistics_Counters_CounterId",
                schema: "counter",
                table: "Statistics");

            migrationBuilder.DropTable(
                name: "Counters",
                schema: "counter");

            migrationBuilder.DropTable(
                name: "CounterTypes",
                schema: "counter");

            migrationBuilder.DropTable(
                name: "Kinds",
                schema: "counter");

            migrationBuilder.DropIndex(
                name: "IX_Statistics_CounterId",
                schema: "counter",
                table: "Statistics");

            migrationBuilder.DropColumn(
                name: "CounterId",
                schema: "counter",
                table: "Statistics");

            migrationBuilder.AddColumn<int>(
                name: "SubscriptionId",
                schema: "counter",
                table: "Statistics",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Observations",
                schema: "counter",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(maxLength: 63, nullable: false),
                    Comment = table.Column<string>(maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Observations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SubscriptionTypes",
                schema: "counter",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PlatformId = table.Column<int>(nullable: false),
                    ObservationId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubscriptionTypes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SubscriptionTypes_Observations_ObservationId",
                        column: x => x.ObservationId,
                        principalSchema: "counter",
                        principalTable: "Observations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SubscriptionTypes_Platforms_PlatformId",
                        column: x => x.PlatformId,
                        principalSchema: "counter",
                        principalTable: "Platforms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Subscriptions",
                schema: "counter",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(nullable: false),
                    Name = table.Column<string>(maxLength: 127, nullable: false),
                    SubscriptionTypeId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subscriptions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Subscriptions_SubscriptionTypes_SubscriptionTypeId",
                        column: x => x.SubscriptionTypeId,
                        principalSchema: "counter",
                        principalTable: "SubscriptionTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Subscriptions_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Statistics_SubscriptionId",
                schema: "counter",
                table: "Statistics",
                column: "SubscriptionId");

            migrationBuilder.CreateIndex(
                name: "IX_Subscriptions_SubscriptionTypeId",
                schema: "counter",
                table: "Subscriptions",
                column: "SubscriptionTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Subscriptions_UserId",
                schema: "counter",
                table: "Subscriptions",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_SubscriptionTypes_ObservationId",
                schema: "counter",
                table: "SubscriptionTypes",
                column: "ObservationId");

            migrationBuilder.CreateIndex(
                name: "IX_SubscriptionTypes_PlatformId",
                schema: "counter",
                table: "SubscriptionTypes",
                column: "PlatformId");

            migrationBuilder.AddForeignKey(
                name: "FK_Statistics_Subscriptions_SubscriptionId",
                schema: "counter",
                table: "Statistics",
                column: "SubscriptionId",
                principalSchema: "counter",
                principalTable: "Subscriptions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Statistics_Subscriptions_SubscriptionId",
                schema: "counter",
                table: "Statistics");

            migrationBuilder.DropTable(
                name: "Subscriptions",
                schema: "counter");

            migrationBuilder.DropTable(
                name: "SubscriptionTypes",
                schema: "counter");

            migrationBuilder.DropTable(
                name: "Observations",
                schema: "counter");

            migrationBuilder.DropIndex(
                name: "IX_Statistics_SubscriptionId",
                schema: "counter",
                table: "Statistics");

            migrationBuilder.DropColumn(
                name: "SubscriptionId",
                schema: "counter",
                table: "Statistics");

            migrationBuilder.AddColumn<int>(
                name: "CounterId",
                schema: "counter",
                table: "Statistics",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Kinds",
                schema: "counter",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Comment = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Description = table.Column<string>(type: "nvarchar(63)", maxLength: 63, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kinds", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CounterTypes",
                schema: "counter",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    KindId = table.Column<int>(type: "int", nullable: false),
                    PlatformId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CounterTypes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CounterTypes_Kinds_KindId",
                        column: x => x.KindId,
                        principalSchema: "counter",
                        principalTable: "Kinds",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CounterTypes_Platforms_PlatformId",
                        column: x => x.PlatformId,
                        principalSchema: "counter",
                        principalTable: "Platforms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Counters",
                schema: "counter",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CounterTypeId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(127)", maxLength: 127, nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Counters", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Counters_CounterTypes_CounterTypeId",
                        column: x => x.CounterTypeId,
                        principalSchema: "counter",
                        principalTable: "CounterTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Counters_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Statistics_CounterId",
                schema: "counter",
                table: "Statistics",
                column: "CounterId");

            migrationBuilder.CreateIndex(
                name: "IX_Counters_CounterTypeId",
                schema: "counter",
                table: "Counters",
                column: "CounterTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Counters_UserId",
                schema: "counter",
                table: "Counters",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_CounterTypes_KindId",
                schema: "counter",
                table: "CounterTypes",
                column: "KindId");

            migrationBuilder.CreateIndex(
                name: "IX_CounterTypes_PlatformId",
                schema: "counter",
                table: "CounterTypes",
                column: "PlatformId");

            migrationBuilder.AddForeignKey(
                name: "FK_Statistics_Counters_CounterId",
                schema: "counter",
                table: "Statistics",
                column: "CounterId",
                principalSchema: "counter",
                principalTable: "Counters",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
