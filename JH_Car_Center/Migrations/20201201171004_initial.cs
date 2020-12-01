using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace JH_Car_Center.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CenterVM",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TypeOfVehicle = table.Column<string>(nullable: true),
                    ChassisNo = table.Column<string>(nullable: true),
                    EngineNO = table.Column<string>(nullable: true),
                    ManufactureYear = table.Column<int>(nullable: false),
                    CC = table.Column<int>(nullable: false),
                    Colour = table.Column<string>(nullable: true),
                    LoadCapacity = table.Column<int>(nullable: false),
                    Accessories = table.Column<string>(nullable: true),
                    UnitPrice = table.Column<decimal>(nullable: false),
                    PaymentMethod = table.Column<string>(nullable: true),
                    Delivery = table.Column<int>(nullable: false),
                    Vaidity = table.Column<int>(nullable: false),
                    CustomerId = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Address = table.Column<string>(nullable: true),
                    Date = table.Column<DateTime>(nullable: false),
                    ChallanNo = table.Column<int>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    ReceiptNo = table.Column<int>(nullable: false),
                    BillNO = table.Column<int>(nullable: false),
                    Quantity = table.Column<int>(nullable: false),
                    Remarks = table.Column<string>(nullable: true),
                    SerialNo = table.Column<int>(nullable: false),
                    TotalPrice = table.Column<decimal>(nullable: false),
                    PaymentType = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CenterVM", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    Address = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Offers",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TypeOfVehicle = table.Column<string>(nullable: true),
                    ChassisNo = table.Column<string>(nullable: true),
                    EngineNO = table.Column<string>(nullable: true),
                    Date = table.Column<DateTime>(nullable: false),
                    ManufactureYear = table.Column<int>(nullable: false),
                    CC = table.Column<int>(nullable: false),
                    Colour = table.Column<string>(nullable: true),
                    LoadCapacity = table.Column<int>(nullable: false),
                    Accessories = table.Column<string>(nullable: true),
                    UnitPrice = table.Column<decimal>(nullable: false),
                    PaymentMethod = table.Column<string>(nullable: true),
                    Delivery = table.Column<int>(nullable: false),
                    Vaidity = table.Column<int>(nullable: false),
                    CustomerId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Offers", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Offers_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    ChallanNo = table.Column<int>(nullable: false),
                    ReceiptNo = table.Column<int>(nullable: false),
                    Quantity = table.Column<int>(nullable: false),
                    Remarks = table.Column<string>(nullable: true),
                    paymentype = table.Column<string>(nullable: true),
                    BillNO = table.Column<int>(nullable: false),
                    SerialNo = table.Column<int>(nullable: false),
                    TotalPrice = table.Column<decimal>(nullable: false),
                    OferId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Orders_Offers_OferId",
                        column: x => x.OferId,
                        principalTable: "Offers",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Offers_CustomerId",
                table: "Offers",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_OferId",
                table: "Orders",
                column: "OferId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CenterVM");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Offers");

            migrationBuilder.DropTable(
                name: "Customers");
        }
    }
}
