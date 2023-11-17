using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FoodPantry2k23.Migrations
{
    /// <inheritdoc />
    public partial class ReconcileConsentDate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("Update Households Set ConsentFormSigned = null where ConsentFormOnFile = 0");
            migrationBuilder.Sql("Update Households Set VerbalConsentGivenOn = null where VerbalConsentGiven = 0");

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
