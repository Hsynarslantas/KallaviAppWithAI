using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace YumApp.WebAPI.Migrations
{
    /// <inheritdoc />
    public partial class mig_update_column_groupreservation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "GroupReservations",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PersonCount",
                table: "GroupReservations",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                table: "GroupReservations");

            migrationBuilder.DropColumn(
                name: "PersonCount",
                table: "GroupReservations");
        }
    }
}
