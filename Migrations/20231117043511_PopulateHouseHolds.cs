using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FoodPantry2k23.Migrations
{
    /// <inheritdoc />
    public partial class PopulateHouseHolds : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var sqlFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"Migrations\SqlScripts\PopulateHouseHolds.sql");
            migrationBuilder.Sql(File.ReadAllText(sqlFile));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("Truncate table Households");
        }
    }
}
