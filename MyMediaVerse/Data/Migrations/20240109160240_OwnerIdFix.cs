using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyMediaVerse.Data.Migrations
{
    /// <inheritdoc />
    public partial class OwnerIdFix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "OwnerUserID",
                table: "Shows",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OwnerUserID",
                table: "Movies",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "OwnerUserID",
                table: "Books",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "OwnerUserID",
                table: "Articles",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OwnerUserID",
                table: "Shows");

            migrationBuilder.DropColumn(
                name: "OwnerUserID",
                table: "Movies");

            migrationBuilder.DropColumn(
                name: "OwnerUserID",
                table: "Articles");

            migrationBuilder.AlterColumn<string>(
                name: "OwnerUserID",
                table: "Books",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }
    }
}
