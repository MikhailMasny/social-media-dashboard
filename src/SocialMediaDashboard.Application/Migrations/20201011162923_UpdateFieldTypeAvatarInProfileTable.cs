using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SocialMediaDashboard.Application.Migrations
{
    public partial class UpdateFieldTypeAvatarInProfileTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Avatar",
                schema: "account",
                table: "Profiles");

            migrationBuilder.AddColumn<byte[]>(
                name: "UserAvatar",
                schema: "account",
                table: "Profiles",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserAvatar",
                schema: "account",
                table: "Profiles");

            migrationBuilder.AddColumn<string>(
                name: "Avatar",
                schema: "account",
                table: "Profiles",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
