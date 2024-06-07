using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            
            migrationBuilder.InsertData(
                table: "Address",
                columns: new[] { "Id", "City", "PostalCode", "Street" },
                values: new object[,]
                {
                    { 1, "Kraków", "30-001", "Długa 5" },
                    { 2, "Kraków", "30-001", "Szewska 2" }
                });

            migrationBuilder.InsertData(
                table: "Restaurants",
                columns: new[] { "Id", "AddressId", "Category", "ContactEmail", "ContactName", "Description", "HasDelivery", "Name" },
                values: new object[,]
                {
                    { 1, 1, "Fast Food", "contact@kfc.com", "John Doe", "KFC (short for Kentucky Fried Chicken) is an American fast food restaurant chain headquartered in Louisville, Kentucky, that specializes in fried chicken.", true, "KFC" },
                    { 2, 2, "Fast Food", "contact@mcdonald.com", "Jane Smith", "McDonald's Corporation (McDonald's), incorporated on December 21, 1964, operates and franchises McDonald's restaurants.", true, "McDonald Szewska" }
                });

            migrationBuilder.InsertData(
                table: "Dishes",
                columns: new[] { "Id", "Description", "Name", "Price", "RestaurantId" },
                values: new object[,]
                {
                    { 1, "Spicy fried chicken inspired by the flavors of Nashville.", "Nashville Hot Chicken", 10.30m, 1 },
                    { 2, "Crispy and juicy chicken nuggets, perfect for snacking.", "Chicken Nuggets", 5.30m, 1 },
                    { 3, "Spicy fried chicken inspired by the flavors of Nashville.", "Nashville Hot Chicken", 10.30m, 2 },
                    { 4, "Crispy and juicy chicken nuggets, perfect for snacking.", "Chicken Nuggets", 5.30m, 2 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Dishes");

            migrationBuilder.DropTable(
                name: "Restaurants");

            migrationBuilder.DropTable(
                name: "Address");
        }
    }
}
