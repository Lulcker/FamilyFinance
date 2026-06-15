using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FamilyFinance.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Add_Column_AuxiliaryWords_For_Category : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string[]>(
                name: "AuxiliaryWords",
                table: "Category",
                type: "text[]",
                nullable: false,
                defaultValue: new string[0]);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AuxiliaryWords",
                table: "Category");
        }
    }
}
