using Microsoft.EntityFrameworkCore.Migrations;

namespace Learning.Server.Migrations
{
    public partial class azureblob3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserAvatarId",
                table: "CompletedProgramReviewables",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CompletedProgramReviewableId",
                table: "AzureBlobs",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CompletedProgramReviewables_UserAvatarId",
                table: "CompletedProgramReviewables",
                column: "UserAvatarId");

            migrationBuilder.CreateIndex(
                name: "IX_AzureBlobs_CompletedProgramReviewableId",
                table: "AzureBlobs",
                column: "CompletedProgramReviewableId");

            migrationBuilder.AddForeignKey(
                name: "FK_AzureBlobs_CompletedProgramReviewables_CompletedProgramReviewableId",
                table: "AzureBlobs",
                column: "CompletedProgramReviewableId",
                principalTable: "CompletedProgramReviewables",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CompletedProgramReviewables_UserAvatars_UserAvatarId",
                table: "CompletedProgramReviewables",
                column: "UserAvatarId",
                principalTable: "UserAvatars",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AzureBlobs_CompletedProgramReviewables_CompletedProgramReviewableId",
                table: "AzureBlobs");

            migrationBuilder.DropForeignKey(
                name: "FK_CompletedProgramReviewables_UserAvatars_UserAvatarId",
                table: "CompletedProgramReviewables");

            migrationBuilder.DropIndex(
                name: "IX_CompletedProgramReviewables_UserAvatarId",
                table: "CompletedProgramReviewables");

            migrationBuilder.DropIndex(
                name: "IX_AzureBlobs_CompletedProgramReviewableId",
                table: "AzureBlobs");

            migrationBuilder.DropColumn(
                name: "UserAvatarId",
                table: "CompletedProgramReviewables");

            migrationBuilder.DropColumn(
                name: "CompletedProgramReviewableId",
                table: "AzureBlobs");
        }
    }
}
