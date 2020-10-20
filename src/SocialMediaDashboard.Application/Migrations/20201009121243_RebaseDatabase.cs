using Microsoft.EntityFrameworkCore.Migrations;

namespace SocialMediaDashboard.Application.Migrations
{
    public partial class RebaseDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Counters_CounterTypes_CounterTypeId",
                table: "Counters");

            migrationBuilder.DropForeignKey(
                name: "FK_CounterTypes_Kinds_KindId",
                table: "CounterTypes");

            migrationBuilder.DropForeignKey(
                name: "FK_CounterTypes_Platforms_PlatformId",
                table: "CounterTypes");

            migrationBuilder.DropForeignKey(
                name: "FK_RefreshTokens_AspNetUsers_UserId",
                table: "RefreshTokens");

            migrationBuilder.DropIndex(
                name: "IX_Profiles_UserId",
                table: "Profiles");

            migrationBuilder.EnsureSchema(
                name: "counter");

            migrationBuilder.EnsureSchema(
                name: "account");

            migrationBuilder.RenameTable(
                name: "Statistics",
                newName: "Statistics",
                newSchema: "counter");

            migrationBuilder.RenameTable(
                name: "RefreshTokens",
                newName: "RefreshTokens",
                newSchema: "account");

            migrationBuilder.RenameTable(
                name: "Profiles",
                newName: "Profiles",
                newSchema: "account");

            migrationBuilder.RenameTable(
                name: "Platforms",
                newName: "Platforms",
                newSchema: "counter");

            migrationBuilder.RenameTable(
                name: "Kinds",
                newName: "Kinds",
                newSchema: "counter");

            migrationBuilder.RenameTable(
                name: "CounterTypes",
                newName: "CounterTypes",
                newSchema: "counter");

            migrationBuilder.RenameTable(
                name: "Counters",
                newName: "Counters",
                newSchema: "counter");

            migrationBuilder.AlterColumn<string>(
                name: "Token",
                schema: "account",
                table: "RefreshTokens",
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "JwtId",
                schema: "account",
                table: "RefreshTokens",
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                schema: "account",
                table: "Profiles",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                schema: "account",
                table: "Profiles",
                maxLength: 127,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                schema: "counter",
                table: "Platforms",
                maxLength: 63,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Comment",
                schema: "counter",
                table: "Platforms",
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                schema: "counter",
                table: "Kinds",
                maxLength: 63,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Comment",
                schema: "counter",
                table: "Kinds",
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                schema: "counter",
                table: "Counters",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                schema: "counter",
                table: "Counters",
                maxLength: 127,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Profiles_UserId",
                schema: "account",
                table: "Profiles",
                column: "UserId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_RefreshTokens_AspNetUsers_UserId",
                schema: "account",
                table: "RefreshTokens",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Counters_CounterTypes_CounterTypeId",
                schema: "counter",
                table: "Counters",
                column: "CounterTypeId",
                principalSchema: "counter",
                principalTable: "CounterTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CounterTypes_Kinds_KindId",
                schema: "counter",
                table: "CounterTypes",
                column: "KindId",
                principalSchema: "counter",
                principalTable: "Kinds",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CounterTypes_Platforms_PlatformId",
                schema: "counter",
                table: "CounterTypes",
                column: "PlatformId",
                principalSchema: "counter",
                principalTable: "Platforms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RefreshTokens_AspNetUsers_UserId",
                schema: "account",
                table: "RefreshTokens");

            migrationBuilder.DropForeignKey(
                name: "FK_Counters_CounterTypes_CounterTypeId",
                schema: "counter",
                table: "Counters");

            migrationBuilder.DropForeignKey(
                name: "FK_CounterTypes_Kinds_KindId",
                schema: "counter",
                table: "CounterTypes");

            migrationBuilder.DropForeignKey(
                name: "FK_CounterTypes_Platforms_PlatformId",
                schema: "counter",
                table: "CounterTypes");

            migrationBuilder.DropIndex(
                name: "IX_Profiles_UserId",
                schema: "account",
                table: "Profiles");

            migrationBuilder.RenameTable(
                name: "Statistics",
                schema: "counter",
                newName: "Statistics");

            migrationBuilder.RenameTable(
                name: "Platforms",
                schema: "counter",
                newName: "Platforms");

            migrationBuilder.RenameTable(
                name: "Kinds",
                schema: "counter",
                newName: "Kinds");

            migrationBuilder.RenameTable(
                name: "CounterTypes",
                schema: "counter",
                newName: "CounterTypes");

            migrationBuilder.RenameTable(
                name: "Counters",
                schema: "counter",
                newName: "Counters");

            migrationBuilder.RenameTable(
                name: "RefreshTokens",
                schema: "account",
                newName: "RefreshTokens");

            migrationBuilder.RenameTable(
                name: "Profiles",
                schema: "account",
                newName: "Profiles");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Platforms",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 63);

            migrationBuilder.AlterColumn<string>(
                name: "Comment",
                table: "Platforms",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 255,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Kinds",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 63);

            migrationBuilder.AlterColumn<string>(
                name: "Comment",
                table: "Kinds",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 255,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Counters",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Counters",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 127);

            migrationBuilder.AlterColumn<string>(
                name: "Token",
                table: "RefreshTokens",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 255);

            migrationBuilder.AlterColumn<string>(
                name: "JwtId",
                table: "RefreshTokens",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 255);

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Profiles",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Profiles",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 127);

            migrationBuilder.CreateIndex(
                name: "IX_Profiles_UserId",
                table: "Profiles",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Counters_CounterTypes_CounterTypeId",
                table: "Counters",
                column: "CounterTypeId",
                principalTable: "CounterTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CounterTypes_Kinds_KindId",
                table: "CounterTypes",
                column: "KindId",
                principalTable: "Kinds",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CounterTypes_Platforms_PlatformId",
                table: "CounterTypes",
                column: "PlatformId",
                principalTable: "Platforms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RefreshTokens_AspNetUsers_UserId",
                table: "RefreshTokens",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
