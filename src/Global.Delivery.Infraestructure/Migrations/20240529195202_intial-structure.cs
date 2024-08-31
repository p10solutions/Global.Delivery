using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Global.Delivery.Infraestructure.Migrations
{
    /// <inheritdoc />
    public partial class intialstructure : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TB_DELIVERYMAN",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uuid", nullable: false),
                    NAME = table.Column<string>(type: "varchar(200)", nullable: false),
                    DOCUMENT = table.Column<string>(type: "varchar(20)", nullable: false),
                    DT_OF_BIRTH = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    LICENSE_NUMBER = table.Column<string>(type: "varchar(20)", nullable: false),
                    LICENSE_TYPE = table.Column<int>(type: "integer", nullable: false),
                    LICENSE_IMAGE = table.Column<string>(type: "varchar(200)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_DELIVERYMAN", x => x.ID);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TB_DELIVERYMAN");
        }
    }
}
