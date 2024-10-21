using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SoftPlex.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "product",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    description = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_product", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "product_version",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    product_id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    description = table.Column<string>(type: "character varying(1024)", maxLength: 1024, nullable: true),
                    creating_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now() at time zone 'utc'"),
                    height = table.Column<decimal>(type: "numeric", nullable: false),
                    length = table.Column<decimal>(type: "numeric", nullable: false),
                    width = table.Column<decimal>(type: "numeric", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_product_version", x => x.id);
                    table.ForeignKey(
                        name: "fk_product_version_product_product_id",
                        column: x => x.product_id,
                        principalTable: "product",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_product_name",
                table: "product",
                column: "name");

            migrationBuilder.CreateIndex(
                name: "ix_product_version_creating_date",
                table: "product_version",
                column: "creating_date");

            migrationBuilder.CreateIndex(
                name: "ix_product_version_product_id",
                table: "product_version",
                column: "product_id");

            migrationBuilder.CreateIndex(
	            name: "ix_product_version_width"
	            , schema: "public"
	            , table: "product_version"
	            , column: "width"
            );

            migrationBuilder.CreateIndex(
	            name: "ix_product_version_height"
	            , schema: "public"
	            , table: "product_version"
	            , column: "height"
            );

            migrationBuilder.CreateIndex(
	            name: "ix_product_version_length"
	            , schema: "public"
	            , table: "product_version"
	            , column: "length"
            );
		}

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "product_version");

            migrationBuilder.DropTable(
                name: "product");
        }
    }
}
