using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PowerChat.Persistence.Migrations
{
    public partial class UserLastActive : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "LastActive",
                schema: "idp",
                table: "User",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LastActive",
                schema: "idp",
                table: "User");
        }
    }
}
