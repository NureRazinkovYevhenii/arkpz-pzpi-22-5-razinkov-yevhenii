using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AquaSense.Migrations
{
    /// <inheritdoc />
    public partial class AddRol : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Aquariums_Users_UserId",
                table: "Aquariums");

            migrationBuilder.DropIndex(
                name: "IX_Aquariums_UserId",
                table: "Aquariums");

            migrationBuilder.RenameColumn(
                name: "Email",
                table: "Users",
                newName: "Role");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Role",
                table: "Users",
                newName: "Email");

            migrationBuilder.CreateIndex(
                name: "IX_Aquariums_UserId",
                table: "Aquariums",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Aquariums_Users_UserId",
                table: "Aquariums",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
