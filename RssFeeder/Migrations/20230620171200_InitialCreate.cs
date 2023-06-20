using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace RssFeeder.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "IdentityUsers",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    Login = table.Column<string>(type: "text", nullable: false),
                    Password = table.Column<string>(type: "text", nullable: false),
                    Role = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IdentityUsers", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "RssFeeds",
                columns: table => new
                {
                    ItemId = table.Column<Guid>(type: "uuid", nullable: false),
                    Title = table.Column<string>(type: "text", nullable: false),
                    Link = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    PubDate = table.Column<string>(type: "text", nullable: false),
                    DbAdded = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RssFeeds", x => x.ItemId);
                });

            migrationBuilder.CreateTable(
                name: "Readed",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    ItemId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Readed", x => new { x.UserId, x.ItemId });
                    table.ForeignKey(
                        name: "FK_Readed_IdentityUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "IdentityUsers",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Readed_RssFeeds_ItemId",
                        column: x => x.ItemId,
                        principalTable: "RssFeeds",
                        principalColumn: "ItemId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "IdentityUsers",
                columns: new[] { "UserId", "Login", "Password", "Role" },
                values: new object[,]
                {
                    { new Guid("6e1b8139-e6f2-47ae-8fed-401b6e46b192"), "User", "User", "user" },
                    { new Guid("921eac24-b306-4c5e-95b9-56b71feea6ae"), "Admin", "Admin", "admin" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_IdentityUsers_UserId",
                table: "IdentityUsers",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Readed_ItemId",
                table: "Readed",
                column: "ItemId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RssFeeds_ItemId",
                table: "RssFeeds",
                column: "ItemId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RssFeeds_Title",
                table: "RssFeeds",
                column: "Title",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Readed");

            migrationBuilder.DropTable(
                name: "IdentityUsers");

            migrationBuilder.DropTable(
                name: "RssFeeds");
        }
    }
}
