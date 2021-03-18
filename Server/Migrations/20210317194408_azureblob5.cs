using Microsoft.EntityFrameworkCore.Migrations;

namespace Learning.Server.Migrations
{
    public partial class azureblob5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsReviewReadByUser",
                table: "CompletedProgramReviewables",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "ReviewedComment",
                table: "CompletedProgramReviewables",
                type: "TEXT",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsReviewReadByUser",
                table: "CompletedProgramReviewables");

            migrationBuilder.DropColumn(
                name: "ReviewedComment",
                table: "CompletedProgramReviewables");
        }
    }
}
