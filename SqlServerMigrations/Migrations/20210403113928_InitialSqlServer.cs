using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SqlServerMigrations {
    public partial class InitialSqlServer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Blobs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Data = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Blobs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ForgottenPasswordRequests",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WhenAskedForNew = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ForgottenPasswordRequests", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Media",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FullFileName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Media", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Username = table.Column<string>(type: "nvarchar(16)", maxLength: 16, nullable: true),
                    Bio = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    PasswordSalt = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserRole = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BlogPosts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AuthorId = table.Column<int>(type: "int", nullable: false),
                    Slug = table.Column<string>(type: "nvarchar(160)", maxLength: 160, nullable: true),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Categories = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CoverImage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Published = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Featured = table.Column<bool>(type: "bit", nullable: false),
                    AccessLevel = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BlogPosts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BlogPosts_Users_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SlideDeckPrograms",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AuthorId = table.Column<int>(type: "int", nullable: false),
                    Slug = table.Column<string>(type: "nvarchar(160)", maxLength: 160, nullable: true),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Categories = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CoverImage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Views = table.Column<int>(type: "int", nullable: false),
                    Rating = table.Column<double>(type: "float", nullable: false),
                    Published = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Featured = table.Column<bool>(type: "bit", nullable: false),
                    AccessLevel = table.Column<int>(type: "int", nullable: false)
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
                name: "SlideDecks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AuthorId = table.Column<int>(type: "int", nullable: false),
                    Slug = table.Column<string>(type: "nvarchar(160)", maxLength: 160, nullable: true),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Categories = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CoverImage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Views = table.Column<int>(type: "int", nullable: false),
                    Rating = table.Column<double>(type: "float", nullable: false),
                    Published = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Featured = table.Column<bool>(type: "bit", nullable: false),
                    AccessLevel = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SlideDecks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SlideDecks_Users_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserAvatars",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BlobId = table.Column<int>(type: "int", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserAvatars", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserAvatars_Blobs_BlobId",
                        column: x => x.BlobId,
                        principalTable: "Blobs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserAvatars_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SlideDeckProgramEntries",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SlideDeckProgramId = table.Column<int>(type: "int", nullable: false),
                    SlideDeckId = table.Column<int>(type: "int", nullable: false),
                    Repititions = table.Column<int>(type: "int", nullable: false),
                    Duration = table.Column<int>(type: "int", nullable: false),
                    Comments = table.Column<string>(type: "nvarchar(max)", nullable: true)
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
                name: "Slides",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SlideDeckId = table.Column<int>(type: "int", nullable: false),
                    Page = table.Column<int>(type: "int", nullable: false),
                    TextContent = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VideoUrl = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Slides", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Slides_SlideDecks_SlideDeckId",
                        column: x => x.SlideDeckId,
                        principalTable: "SlideDecks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CompletedSlideDeckPrograms",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserAvatarId = table.Column<int>(type: "int", nullable: false),
                    SlideDeckProgramId = table.Column<int>(type: "int", nullable: false),
                    Comment = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserAvatarFeedback = table.Column<int>(type: "int", nullable: false),
                    IsPublic = table.Column<bool>(type: "bit", nullable: false),
                    CompletedOn = table.Column<DateTime>(type: "datetime2", nullable: false)
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

            migrationBuilder.CreateTable(
                name: "UserAccessSlideDeckPrograms",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SlideDeckProgramId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    UserAvatarId = table.Column<int>(type: "int", nullable: true),
                    Comments = table.Column<string>(type: "nvarchar(max)", nullable: true)
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
                        name: "FK_UserAccessSlideDeckPrograms_UserAvatars_UserAvatarId",
                        column: x => x.UserAvatarId,
                        principalTable: "UserAvatars",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserAccessSlideDeckPrograms_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CompletedProgramReviewables",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompletedSlideDeckProgramId = table.Column<int>(type: "int", nullable: false),
                    UserAvatarId = table.Column<int>(type: "int", nullable: false),
                    IsReviewed = table.Column<bool>(type: "bit", nullable: false),
                    IsReviewReadByUser = table.Column<bool>(type: "bit", nullable: false),
                    ReviewedComment = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompletedProgramReviewables", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CompletedProgramReviewables_CompletedSlideDeckPrograms_CompletedSlideDeckProgramId",
                        column: x => x.CompletedSlideDeckProgramId,
                        principalTable: "CompletedSlideDeckPrograms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CompletedProgramReviewables_UserAvatars_UserAvatarId",
                        column: x => x.UserAvatarId,
                        principalTable: "UserAvatars",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AzureBlobs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Uri = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Mime = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CompletedProgramReviewableId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AzureBlobs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AzureBlobs_CompletedProgramReviewables_CompletedProgramReviewableId",
                        column: x => x.CompletedProgramReviewableId,
                        principalTable: "CompletedProgramReviewables",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ReviewableAzureBlobs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AzureBlobId = table.Column<int>(type: "int", nullable: false),
                    CompletedProgramReviewableId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReviewableAzureBlobs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ReviewableAzureBlobs_AzureBlobs_AzureBlobId",
                        column: x => x.AzureBlobId,
                        principalTable: "AzureBlobs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ReviewableAzureBlobs_CompletedProgramReviewables_CompletedProgramReviewableId",
                        column: x => x.CompletedProgramReviewableId,
                        principalTable: "CompletedProgramReviewables",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AzureBlobs_CompletedProgramReviewableId",
                table: "AzureBlobs",
                column: "CompletedProgramReviewableId");

            migrationBuilder.CreateIndex(
                name: "IX_BlogPosts_AuthorId",
                table: "BlogPosts",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_CompletedProgramReviewables_CompletedSlideDeckProgramId",
                table: "CompletedProgramReviewables",
                column: "CompletedSlideDeckProgramId");

            migrationBuilder.CreateIndex(
                name: "IX_CompletedProgramReviewables_UserAvatarId",
                table: "CompletedProgramReviewables",
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
                name: "IX_ReviewableAzureBlobs_AzureBlobId",
                table: "ReviewableAzureBlobs",
                column: "AzureBlobId");

            migrationBuilder.CreateIndex(
                name: "IX_ReviewableAzureBlobs_CompletedProgramReviewableId",
                table: "ReviewableAzureBlobs",
                column: "CompletedProgramReviewableId");

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
                name: "IX_SlideDecks_AuthorId",
                table: "SlideDecks",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_Slides_SlideDeckId",
                table: "Slides",
                column: "SlideDeckId");

            migrationBuilder.CreateIndex(
                name: "IX_UserAccessSlideDeckPrograms_SlideDeckProgramId",
                table: "UserAccessSlideDeckPrograms",
                column: "SlideDeckProgramId");

            migrationBuilder.CreateIndex(
                name: "IX_UserAccessSlideDeckPrograms_UserAvatarId",
                table: "UserAccessSlideDeckPrograms",
                column: "UserAvatarId");

            migrationBuilder.CreateIndex(
                name: "IX_UserAccessSlideDeckPrograms_UserId",
                table: "UserAccessSlideDeckPrograms",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserAvatars_BlobId",
                table: "UserAvatars",
                column: "BlobId");

            migrationBuilder.CreateIndex(
                name: "IX_UserAvatars_UserId",
                table: "UserAvatars",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BlogPosts");

            migrationBuilder.DropTable(
                name: "ForgottenPasswordRequests");

            migrationBuilder.DropTable(
                name: "Media");

            migrationBuilder.DropTable(
                name: "ReviewableAzureBlobs");

            migrationBuilder.DropTable(
                name: "SlideDeckProgramEntries");

            migrationBuilder.DropTable(
                name: "Slides");

            migrationBuilder.DropTable(
                name: "UserAccessSlideDeckPrograms");

            migrationBuilder.DropTable(
                name: "AzureBlobs");

            migrationBuilder.DropTable(
                name: "SlideDecks");

            migrationBuilder.DropTable(
                name: "CompletedProgramReviewables");

            migrationBuilder.DropTable(
                name: "CompletedSlideDeckPrograms");

            migrationBuilder.DropTable(
                name: "SlideDeckPrograms");

            migrationBuilder.DropTable(
                name: "UserAvatars");

            migrationBuilder.DropTable(
                name: "Blobs");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
