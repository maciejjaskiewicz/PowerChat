using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PowerChat.Persistence.Migrations
{
    public partial class Friendship : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Friendship",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    RequestedById = table.Column<long>(nullable: false),
                    RequestedToId = table.Column<long>(nullable: false),
                    Approved = table.Column<bool>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Friendship", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Friendship_User_RequestedById",
                        column: x => x.RequestedById,
                        principalSchema: "idp",
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Friendship_User_RequestedToId",
                        column: x => x.RequestedToId,
                        principalSchema: "idp",
                        principalTable: "User",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Friendship_RequestedById",
                table: "Friendship",
                column: "RequestedById");

            migrationBuilder.CreateIndex(
                name: "IX_Friendship_RequestedToId",
                table: "Friendship",
                column: "RequestedToId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Friendship");
        }
    }
}
