using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Global.Delivery.Infraestructure.Migrations
{
    /// <inheritdoc />
    public partial class updatedatetime : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "DT_OF_BIRTH",
                table: "TB_DELIVERYMAN",
                type: "timestamp without time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "DT_OF_BIRTH",
                table: "TB_DELIVERYMAN",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");
        }
    }
}
