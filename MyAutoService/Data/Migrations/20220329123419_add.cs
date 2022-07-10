using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyAutoService.Data.Migrations
{
    public partial class add : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ServiceHeaders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Miles = table.Column<double>(type: "float", nullable: false),
                    TotalPrice = table.Column<double>(type: "float", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateAdded = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CarId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiceHeaders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ServiceHeaders_Cars_CarId",
                        column: x => x.CarId,
                        principalTable: "Cars",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ServicesShoppingCarts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ServiceTypeId = table.Column<int>(type: "int", nullable: false),
                    CarId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServicesShoppingCarts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ServicesShoppingCarts_Cars_CarId",
                        column: x => x.CarId,
                        principalTable: "Cars",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ServicesShoppingCarts_ServicType_ServiceTypeId",
                        column: x => x.ServiceTypeId,
                        principalTable: "ServicType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ServicDetalis",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ServiceHeaderId = table.Column<int>(type: "int", nullable: false),
                    ServiceTypeId = table.Column<int>(type: "int", nullable: false),
                    ServicePrice = table.Column<double>(type: "float", nullable: false),
                    ServiceName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServicDetalis", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ServicDetalis_ServiceHeaders_ServiceHeaderId",
                        column: x => x.ServiceHeaderId,
                        principalTable: "ServiceHeaders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ServicDetalis_ServicType_ServiceTypeId",
                        column: x => x.ServiceTypeId,
                        principalTable: "ServicType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ServicDetalis_ServiceHeaderId",
                table: "ServicDetalis",
                column: "ServiceHeaderId");

            migrationBuilder.CreateIndex(
                name: "IX_ServicDetalis_ServiceTypeId",
                table: "ServicDetalis",
                column: "ServiceTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceHeaders_CarId",
                table: "ServiceHeaders",
                column: "CarId");

            migrationBuilder.CreateIndex(
                name: "IX_ServicesShoppingCarts_CarId",
                table: "ServicesShoppingCarts",
                column: "CarId");

            migrationBuilder.CreateIndex(
                name: "IX_ServicesShoppingCarts_ServiceTypeId",
                table: "ServicesShoppingCarts",
                column: "ServiceTypeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ServicDetalis");

            migrationBuilder.DropTable(
                name: "ServicesShoppingCarts");

            migrationBuilder.DropTable(
                name: "ServiceHeaders");
        }
    }
}
