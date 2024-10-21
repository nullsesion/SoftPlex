using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SoftPlex.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddEventLog : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
			migrationBuilder.CreateTable(
				name: "event_log",
				columns: table => new
				{
					id = table.Column<Guid>(type: "uuid", nullable: false),
					event_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
					description = table.Column<string>(type: "text", nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("pk_event_log", x => x.id);
				});
		}

		/// <inheritdoc />
		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropTable(
				name: "event_log");
		}
	}
}
