using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SocialMediaDashboard.Application.Migrations
{
    public partial class AddFieldsInProfileTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "BirthDate",
                schema: "account",
                table: "Profiles",
                type: "date",
                nullable: false,
                defaultValue: new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc));

            migrationBuilder.AddColumn<string>(
                name: "Country",
                schema: "account",
                table: "Profiles",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Gender",
                schema: "account",
                table: "Profiles",
                nullable: false,
                defaultValue: -1);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BirthDate",
                schema: "account",
                table: "Profiles");

            migrationBuilder.DropColumn(
                name: "Country",
                schema: "account",
                table: "Profiles");

            migrationBuilder.DropColumn(
                name: "Gender",
                schema: "account",
                table: "Profiles");
        }
    }
}
