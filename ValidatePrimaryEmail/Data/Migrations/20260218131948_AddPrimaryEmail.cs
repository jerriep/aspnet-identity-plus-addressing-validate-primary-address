using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ValidatePrimaryEmail.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddPrimaryEmail : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PrimaryEmail",
                table: "AspNetUsers",
                type: "TEXT",
                maxLength: 256,
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "PrimaryEmailName",
                table: "AspNetUsers",
                column: "PrimaryEmail",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "PrimaryEmailName",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "PrimaryEmail",
                table: "AspNetUsers");
        }
    }
}
