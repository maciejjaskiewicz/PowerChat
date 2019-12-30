using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PowerChat.Persistence.Migrations
{
    public partial class MessageInterlocutor : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Interlocutor",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    ChannelId = table.Column<long>(nullable: false),
                    UserId = table.Column<long>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Interlocutor", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Interlocutor_Channel_ChannelId",
                        column: x => x.ChannelId,
                        principalTable: "Channel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Interlocutor_User_UserId",
                        column: x => x.UserId,
                        principalSchema: "idp",
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Message",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    ChannelId = table.Column<long>(nullable: false),
                    SenderId = table.Column<long>(nullable: false),
                    Content = table.Column<string>(nullable: true),
                    Seen = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Message", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Message_Channel_ChannelId",
                        column: x => x.ChannelId,
                        principalTable: "Channel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Message_User_SenderId",
                        column: x => x.SenderId,
                        principalSchema: "idp",
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Interlocutor_ChannelId",
                table: "Interlocutor",
                column: "ChannelId");

            migrationBuilder.CreateIndex(
                name: "IX_Interlocutor_UserId",
                table: "Interlocutor",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Message_ChannelId",
                table: "Message",
                column: "ChannelId");

            migrationBuilder.CreateIndex(
                name: "IX_Message_SenderId",
                table: "Message",
                column: "SenderId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Interlocutor");

            migrationBuilder.DropTable(
                name: "Message");
        }
    }
}
