using Microsoft.EntityFrameworkCore.Migrations;

namespace Learning.Server.Migrations
{
    public partial class forgottenpassword2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CountOfAsked",
                table: "ForgottenPasswordRequests");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CountOfAsked",
                table: "ForgottenPasswordRequests",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }
    }
}
