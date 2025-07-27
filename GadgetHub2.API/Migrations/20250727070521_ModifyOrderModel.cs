using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GadgetHub2.API.Migrations
{
    /// <inheritdoc />
    public partial class ModifyOrderModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ApiUrl",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "AssignedOn",
                table: "Orders",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DistributorName",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "QuotationId",
                table: "Orders",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ApiUrl",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "AssignedOn",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "DistributorName",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "QuotationId",
                table: "Orders");
        }
    }
}
