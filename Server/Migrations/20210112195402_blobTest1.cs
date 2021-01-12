using Microsoft.EntityFrameworkCore.Migrations;

namespace Learning.Server.Migrations
{
    public partial class blobTest1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CoverImage",
                table: "UserAvatars");

            migrationBuilder.AddColumn<int>(
                name: "BlobId",
                table: "UserAvatars",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Blobs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Data = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Blobs", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserAvatars_BlobId",
                table: "UserAvatars",
                column: "BlobId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserAvatars_Blobs_BlobId",
                table: "UserAvatars",
                column: "BlobId",
                principalTable: "Blobs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserAvatars_Blobs_BlobId",
                table: "UserAvatars");

            migrationBuilder.DropTable(
                name: "Blobs");

            migrationBuilder.DropIndex(
                name: "IX_UserAvatars_BlobId",
                table: "UserAvatars");

            migrationBuilder.DropColumn(
                name: "BlobId",
                table: "UserAvatars");

            migrationBuilder.AddColumn<string>(
                name: "CoverImage",
                table: "UserAvatars",
                type: "TEXT",
                nullable: true);
        }
    }
}
