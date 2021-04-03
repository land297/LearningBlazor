using Microsoft.EntityFrameworkCore.Migrations;

namespace Learning.Server.Migrations
{
    public partial class useravatarcompleteprogramschange : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SlideDeckPrograms_UserAvatars_UserAvatarId",
                table: "SlideDeckPrograms");

            migrationBuilder.DropIndex(
                name: "IX_SlideDeckPrograms_UserAvatarId",
                table: "SlideDeckPrograms");

            migrationBuilder.DropColumn(
                name: "UserAvatarId",
                table: "SlideDeckPrograms");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserAvatarId",
                table: "SlideDeckPrograms",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_SlideDeckPrograms_UserAvatarId",
                table: "SlideDeckPrograms",
                column: "UserAvatarId");

            migrationBuilder.AddForeignKey(
                name: "FK_SlideDeckPrograms_UserAvatars_UserAvatarId",
                table: "SlideDeckPrograms",
                column: "UserAvatarId",
                principalTable: "UserAvatars",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
