using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace resa2.Migrations
{
    /// <inheritdoc />
    public partial class categoryForeignFix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Hotels_Categories_CategoryId1",
                table: "Hotels");

            migrationBuilder.DropIndex(
                name: "IX_Hotels_CategoryId1",
                table: "Hotels");

            migrationBuilder.DropColumn(
                name: "CategoryId1",
                table: "Hotels");

            migrationBuilder.AddColumn<int>(
                name: "CategoryId",
                table: "Hotels",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Hotels_CategoryId",
                table: "Hotels",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Hotels_Categories_CategoryId",
                table: "Hotels",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Hotels_Categories_CategoryId",
                table: "Hotels");

            migrationBuilder.DropIndex(
                name: "IX_Hotels_CategoryId",
                table: "Hotels");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "Hotels");

            migrationBuilder.AddColumn<int>(
                name: "CategoryId1",
                table: "Hotels",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Hotels_CategoryId1",
                table: "Hotels",
                column: "CategoryId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Hotels_Categories_CategoryId1",
                table: "Hotels",
                column: "CategoryId1",
                principalTable: "Categories",
                principalColumn: "Id");
        }
    }
}
