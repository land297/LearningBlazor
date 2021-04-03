using Microsoft.EntityFrameworkCore.Migrations;

namespace Learning.Server.Migrations
{
    public partial class azureblob2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AzureBlobs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Uri = table.Column<string>(type: "TEXT", nullable: true),
                    Name = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AzureBlobs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CompletedProgramReviewables",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CompletedSlideDeckProgramId = table.Column<int>(type: "INTEGER", nullable: false),
                    IsReviewed = table.Column<bool>(type: "INTEGER", nullable: false)
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
                });

            migrationBuilder.CreateTable(
                name: "ReviewableAzureBlobs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    AzureBlobId = table.Column<int>(type: "INTEGER", nullable: false),
                    CompletedProgramReviewableId = table.Column<int>(type: "INTEGER", nullable: false)
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
                name: "IX_CompletedProgramReviewables_CompletedSlideDeckProgramId",
                table: "CompletedProgramReviewables",
                column: "CompletedSlideDeckProgramId");

            migrationBuilder.CreateIndex(
                name: "IX_ReviewableAzureBlobs_AzureBlobId",
                table: "ReviewableAzureBlobs",
                column: "AzureBlobId");

            migrationBuilder.CreateIndex(
                name: "IX_ReviewableAzureBlobs_CompletedProgramReviewableId",
                table: "ReviewableAzureBlobs",
                column: "CompletedProgramReviewableId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ReviewableAzureBlobs");

            migrationBuilder.DropTable(
                name: "AzureBlobs");

            migrationBuilder.DropTable(
                name: "CompletedProgramReviewables");
        }
    }
}
