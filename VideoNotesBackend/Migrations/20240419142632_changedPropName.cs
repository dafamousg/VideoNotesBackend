using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VideoNotesBackend.Migrations
{
    /// <inheritdoc />
    public partial class changedPropName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Released",
                table: "Videos",
                newName: "ReleaseDate");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ReleaseDate",
                table: "Videos",
                newName: "Released");
        }
    }
}
