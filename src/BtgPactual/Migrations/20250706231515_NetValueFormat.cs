using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BtgPactual.Migrations
{
    /// <inheritdoc />
    public partial class NetValueFormat : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "NetValue",
                table: "Rescues",
                type: "DECIMAL(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NetValue",
                table: "Rescues");
        }
    }
}
