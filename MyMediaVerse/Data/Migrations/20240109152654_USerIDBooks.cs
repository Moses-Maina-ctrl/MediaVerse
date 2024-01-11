using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyMediaVerse.Data.Migrations
{
    /// <inheritdoc />
    public partial class USerIDBooks : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "OwnerUserID",
                table: "Books",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OwnerUserID",
                table: "Books");
        }
    }
}
