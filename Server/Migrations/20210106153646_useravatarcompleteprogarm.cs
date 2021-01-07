using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Learning.Server.Migrations
{
    public partial class useravatarcompleteprogarm : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UserRoel",
                table: "Users",
                newName: "UserRole");

            migrationBuilder.AddColumn<int>(
                name: "UserAvatarId",
                table: "UserAccessSlideDeckPrograms",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UserAvatarId",
                table: "SlideDeckPrograms",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "UserAvatars",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UserId = table.Column<int>(type: "INTEGER", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    Description = table.Column<string>(type: "TEXT", nullable: true),
                    CoverImage = table.Column<string>(type: "TEXT", nullable: true),
                    IsDeleted = table.Column<bool>(type: "INTEGER", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserAvatars", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserAvatars_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CompletedSlideDeckPrograms",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UserAvatarId = table.Column<int>(type: "INTEGER", nullable: false),
                    SlideDeckProgramId = table.Column<int>(type: "INTEGER", nullable: false),
                    Comment = table.Column<string>(type: "TEXT", nullable: true),
                    UserAvatarFeedback = table.Column<int>(type: "INTEGER", nullable: false),
                    IsPublic = table.Column<bool>(type: "INTEGER", nullable: false),
                    CompletedOn = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompletedSlideDeckPrograms", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CompletedSlideDeckPrograms_SlideDeckPrograms_SlideDeckProgramId",
                        column: x => x.SlideDeckProgramId,
                        principalTable: "SlideDeckPrograms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CompletedSlideDeckPrograms_UserAvatars_UserAvatarId",
                        column: x => x.UserAvatarId,
                        principalTable: "UserAvatars",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserAccessSlideDeckPrograms_UserAvatarId",
                table: "UserAccessSlideDeckPrograms",
                column: "UserAvatarId");

            migrationBuilder.CreateIndex(
                name: "IX_SlideDeckPrograms_UserAvatarId",
                table: "SlideDeckPrograms",
                column: "UserAvatarId");

            migrationBuilder.CreateIndex(
                name: "IX_CompletedSlideDeckPrograms_SlideDeckProgramId",
                table: "CompletedSlideDeckPrograms",
                column: "SlideDeckProgramId");

            migrationBuilder.CreateIndex(
                name: "IX_CompletedSlideDeckPrograms_UserAvatarId",
                table: "CompletedSlideDeckPrograms",
                column: "UserAvatarId");

            migrationBuilder.CreateIndex(
                name: "IX_UserAvatars_UserId",
                table: "UserAvatars",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_SlideDeckPrograms_UserAvatars_UserAvatarId",
                table: "SlideDeckPrograms",
                column: "UserAvatarId",
                principalTable: "UserAvatars",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserAccessSlideDeckPrograms_Users_UserAvatarId",
                table: "UserAccessSlideDeckPrograms",
                column: "UserAvatarId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SlideDeckPrograms_UserAvatars_UserAvatarId",
                table: "SlideDeckPrograms");

            migrationBuilder.DropForeignKey(
                name: "FK_UserAccessSlideDeckPrograms_Users_UserAvatarId",
                table: "UserAccessSlideDeckPrograms");

            migrationBuilder.DropTable(
                name: "CompletedSlideDeckPrograms");

            migrationBuilder.DropTable(
                name: "UserAvatars");

            migrationBuilder.DropIndex(
                name: "IX_UserAccessSlideDeckPrograms_UserAvatarId",
                table: "UserAccessSlideDeckPrograms");

            migrationBuilder.DropIndex(
                name: "IX_SlideDeckPrograms_UserAvatarId",
                table: "SlideDeckPrograms");

            migrationBuilder.DropColumn(
                name: "UserAvatarId",
                table: "UserAccessSlideDeckPrograms");

            migrationBuilder.DropColumn(
                name: "UserAvatarId",
                table: "SlideDeckPrograms");

            migrationBuilder.RenameColumn(
                name: "UserRole",
                table: "Users",
                newName: "UserRoel");
        }
    }
}
