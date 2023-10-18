using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace GeneralInfrastructure.Migrations
{
    /// <inheritdoc />
    public partial class fifth : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "user_friend",
                schema: "user",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    user_id = table.Column<int>(type: "integer", nullable: false),
                    friend_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user_friend", x => x.id);
                    table.ForeignKey(
                        name: "FK_user_friend_user_friend_id",
                        column: x => x.friend_id,
                        principalSchema: "user",
                        principalTable: "user",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_user_friend_user_user_id",
                        column: x => x.user_id,
                        principalSchema: "user",
                        principalTable: "user",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "user_photos",
                schema: "user",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    user_id = table.Column<int>(type: "integer", nullable: false),
                    file_link = table.Column<string>(type: "text", nullable: false),
                    add_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user_photos", x => x.id);
                    table.ForeignKey(
                        name: "FK_user_photos_user_user_id",
                        column: x => x.user_id,
                        principalSchema: "user",
                        principalTable: "user",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_user_friend_friend_id",
                schema: "user",
                table: "user_friend",
                column: "friend_id");

            migrationBuilder.CreateIndex(
                name: "IX_user_friend_user_id",
                schema: "user",
                table: "user_friend",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_user_photos_user_id",
                schema: "user",
                table: "user_photos",
                column: "user_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "user_friend",
                schema: "user");

            migrationBuilder.DropTable(
                name: "user_photos",
                schema: "user");
        }
    }
}
