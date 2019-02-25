using Microsoft.EntityFrameworkCore.Migrations;

namespace CustomerAddressManager.Dal.Migrations
{
    public partial class InititalCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Addresses",
                columns: table => new
                {
                    CustomerId = table.Column<string>(unicode: false, maxLength: 5, nullable: false),
                    AddressType = table.Column<string>(unicode: false, maxLength: 1, nullable: false),
                    Name = table.Column<string>(maxLength: 100, nullable: true),
                    Street = table.Column<string>(maxLength: 100, nullable: true),
                    Zip = table.Column<string>(unicode: false, maxLength: 20, nullable: true),
                    City = table.Column<string>(maxLength: 100, nullable: true),
                    Country = table.Column<string>(unicode: false, maxLength: 2, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Addresses", x => new { x.CustomerId, x.AddressType });
                });

            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    CustomerId = table.Column<string>(unicode: false, maxLength: 5, nullable: false),
                    Name = table.Column<string>(maxLength: 100, nullable: false),
                    Street = table.Column<string>(maxLength: 100, nullable: true),
                    Zip = table.Column<string>(unicode: false, maxLength: 20, nullable: true),
                    City = table.Column<string>(maxLength: 100, nullable: true),
                    Country = table.Column<string>(unicode: false, maxLength: 2, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => new { x.CustomerId, x.Name });
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Addresses");

            migrationBuilder.DropTable(
                name: "Customers");
        }
    }
}
