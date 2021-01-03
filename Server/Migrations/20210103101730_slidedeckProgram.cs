using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Learning.Server.Migrations
{
    public partial class slidedeckProgram : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SlideDeckPrograms",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    AuthorId = table.Column<int>(type: "INTEGER", nullable: false),
                    Slug = table.Column<string>(type: "TEXT", maxLength: 160, nullable: true),
                    Title = table.Column<string>(type: "TEXT", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: false),
                    Categories = table.Column<string>(type: "TEXT", nullable: true),
                    CoverImage = table.Column<string>(type: "TEXT", nullable: true),
                    Views = table.Column<int>(type: "INTEGER", nullable: false),
                    Rating = table.Column<double>(type: "REAL", nullable: false),
                    Published = table.Column<DateTime>(type: "TEXT", nullable: false),
                    IsDeleted = table.Column<bool>(type: "INTEGER", nullable: false),
                    Featured = table.Column<bool>(type: "INTEGER", nullable: false),
                    AccessLevel = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SlideDeckPrograms", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SlideDeckPrograms_Users_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SlideDeckProgramEntries",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    SlideDeckProgramId = table.Column<int>(type: "INTEGER", nullable: false),
                    SlideDeckId = table.Column<int>(type: "INTEGER", nullable: false),
                    Repititions = table.Column<int>(type: "INTEGER", nullable: false),
                    Duration = table.Column<int>(type: "INTEGER", nullable: false),
                    Comments = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SlideDeckProgramEntries", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SlideDeckProgramEntries_SlideDeckPrograms_SlideDeckProgramId",
                        column: x => x.SlideDeckProgramId,
                        principalTable: "SlideDeckPrograms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SlideDeckProgramEntries_SlideDecks_SlideDeckId",
                        column: x => x.SlideDeckId,
                        principalTable: "SlideDecks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserAccessSlideDeckPrograms",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    SlideDeckProgramId = table.Column<int>(type: "INTEGER", nullable: false),
                    UserId = table.Column<int>(type: "INTEGER", nullable: false),
                    Comments = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserAccessSlideDeckPrograms", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserAccessSlideDeckPrograms_SlideDeckPrograms_SlideDeckProgramId",
                        column: x => x.SlideDeckProgramId,
                        principalTable: "SlideDeckPrograms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserAccessSlideDeckPrograms_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SlideDeckProgramEntries_SlideDeckId",
                table: "SlideDeckProgramEntries",
                column: "SlideDeckId");

            migrationBuilder.CreateIndex(
                name: "IX_SlideDeckProgramEntries_SlideDeckProgramId",
                table: "SlideDeckProgramEntries",
                column: "SlideDeckProgramId");

            migrationBuilder.CreateIndex(
                name: "IX_SlideDeckPrograms_AuthorId",
                table: "SlideDeckPrograms",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_UserAccessSlideDeckPrograms_SlideDeckProgramId",
                table: "UserAccessSlideDeckPrograms",
                column: "SlideDeckProgramId");

            migrationBuilder.CreateIndex(
                name: "IX_UserAccessSlideDeckPrograms_UserId",
                table: "UserAccessSlideDeckPrograms",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SlideDeckProgramEntries");

            migrationBuilder.DropTable(
                name: "UserAccessSlideDeckPrograms");

            migrationBuilder.DropTable(
                name: "SlideDeckPrograms");
        }
    }
}
