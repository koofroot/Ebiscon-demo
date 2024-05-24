using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EbisconDemo.Data.Migrations
{
    /// <inheritdoc />
    public partial class Add_Order_Status : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "Orders",
                type: "VARCHAR(40)",
                nullable: false,
                defaultValue: "Created");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "Orders");
        }
    }
}
