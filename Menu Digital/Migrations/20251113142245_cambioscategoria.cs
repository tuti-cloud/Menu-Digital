using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Menu_Digital.Migrations
{
    /// <inheritdoc />
    public partial class cambioscategoria : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Categories_Restaurants_RestaurantId",
                table: "Categories");

            migrationBuilder.DropIndex(
                name: "IX_Categories_RestaurantId",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "RestaurantId",
                table: "Categories");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RestaurantId",
                table: "Categories",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 1,
                column: "RestaurantId",
                value: 1);

            migrationBuilder.CreateIndex(
                name: "IX_Categories_RestaurantId",
                table: "Categories",
                column: "RestaurantId");

            migrationBuilder.AddForeignKey(
                name: "FK_Categories_Restaurants_RestaurantId",
                table: "Categories",
                column: "RestaurantId",
                principalTable: "Restaurants",
                principalColumn: "RestaurantId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
