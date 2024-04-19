using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VideoNotesBackend.Migrations
{
    /// <inheritdoc />
    public partial class RemovedNotesCollection : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Notes_Videos_VideoId",
                table: "Notes");

            migrationBuilder.DropIndex(
                name: "IX_Notes_VideoId",
                table: "Notes");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Notes_VideoId",
                table: "Notes",
                column: "VideoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Notes_Videos_VideoId",
                table: "Notes",
                column: "VideoId",
                principalTable: "Videos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
