using Microsoft.EntityFrameworkCore.Migrations;

namespace PowerChat.Persistence.Migrations
{
    public partial class UserAboutColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "About",
                schema: "idp",
                table: "User",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "About",
                schema: "idp",
                table: "User");
        }
    }
}
