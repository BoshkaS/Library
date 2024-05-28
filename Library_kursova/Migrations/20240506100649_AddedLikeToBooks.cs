using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Library_kursova.Migrations
{
    /// <inheritdoc />
    public partial class AddedLikeToBooks : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LikedBook",
                columns: table => new
                {
                    liked_book_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    user_id = table.Column<int>(type: "integer", nullable: false),
                    book_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LikedBook", x => x.liked_book_id);
                    table.ForeignKey(
                        name: "book_id",
                        column: x => x.book_id,
                        principalTable: "Book",
                        principalColumn: "book_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "user_id",
                        column: x => x.user_id,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_LikedBook_book_id",
                table: "LikedBook",
                column: "book_id");

            migrationBuilder.CreateIndex(
                name: "IX_LikedBook_user_id",
                table: "LikedBook",
                column: "user_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "book_id",
                table: "AuthorBook");

            migrationBuilder.DropForeignKey(
                name: "user_id",
                table: "BookmarkBook");

            migrationBuilder.DropTable(
                name: "LikedBook");
        }
    }
}
