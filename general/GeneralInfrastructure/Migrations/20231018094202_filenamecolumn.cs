using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GeneralInfrastructure.Migrations
{
    /// <inheritdoc />
    public partial class filenamecolumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "file_link",
                schema: "user",
                table: "user_photos",
                newName: "file_name");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "file_name",
                schema: "user",
                table: "user_photos",
                newName: "file_link");
        }
    }
}
