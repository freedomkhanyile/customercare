using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CustomerCare.Data.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    Cellphone = table.Column<string>(nullable: true),
                    AmountTotal = table.Column<double>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Types",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Types", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CustomerTypes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerId = table.Column<int>(nullable: false),
                    TypeId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerTypes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CustomerTypes_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CustomerTypes_Types_TypeId",
                        column: x => x.TypeId,
                        principalTable: "Types",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "Id", "AmountTotal", "Cellphone", "Date", "Email", "FirstName", "IsDeleted", "LastName" },
                values: new object[,]
                {
                    { 1, 0.0, "074587545", new DateTime(2021, 6, 24, 22, 43, 15, 239, DateTimeKind.Local).AddTicks(1143), "john@mail.com", "John", false, "Doe" },
                    { 2, 0.0, "07451215", new DateTime(2021, 6, 24, 22, 43, 15, 240, DateTimeKind.Local).AddTicks(1164), "fedicashadmin@mail.com", "Fedi", false, "Cash" },
                    { 3, 0.0, "011100444", new DateTime(2021, 6, 24, 22, 43, 15, 240, DateTimeKind.Local).AddTicks(1185), "health@department.gov.za", "Dpt Health", false, "Saftey" }
                });

            migrationBuilder.InsertData(
                table: "Types",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Person" },
                    { 2, "Organisation" },
                    { 3, "Government" }
                });

            migrationBuilder.InsertData(
                table: "CustomerTypes",
                columns: new[] { "Id", "CustomerId", "TypeId" },
                values: new object[] { 1, 1, 1 });

            migrationBuilder.InsertData(
                table: "CustomerTypes",
                columns: new[] { "Id", "CustomerId", "TypeId" },
                values: new object[] { 2, 2, 2 });

            migrationBuilder.InsertData(
                table: "CustomerTypes",
                columns: new[] { "Id", "CustomerId", "TypeId" },
                values: new object[] { 3, 3, 3 });

            migrationBuilder.CreateIndex(
                name: "IX_CustomerTypes_CustomerId",
                table: "CustomerTypes",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerTypes_TypeId",
                table: "CustomerTypes",
                column: "TypeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CustomerTypes");

            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.DropTable(
                name: "Types");
        }
    }
}
