using Microsoft.EntityFrameworkCore.Migrations;

namespace Learning.Server.Migrations
{
    public partial class userAccessX : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserAccessSlideDeckPrograms_Users_UserAvatarId",
                table: "UserAccessSlideDeckPrograms");

            migrationBuilder.AddForeignKey(
                name: "FK_UserAccessSlideDeckPrograms_UserAvatars_UserAvatarId",
                table: "UserAccessSlideDeckPrograms",
                column: "UserAvatarId",
                principalTable: "UserAvatars",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserAccessSlideDeckPrograms_UserAvatars_UserAvatarId",
                table: "UserAccessSlideDeckPrograms");

            migrationBuilder.AddForeignKey(
                name: "FK_UserAccessSlideDeckPrograms_Users_UserAvatarId",
                table: "UserAccessSlideDeckPrograms",
                column: "UserAvatarId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
