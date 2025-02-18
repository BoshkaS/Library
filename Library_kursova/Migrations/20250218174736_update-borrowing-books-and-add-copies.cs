using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Library_kursova.Migrations
{
    /// <inheritdoc />
    public partial class updateborrowingbooksandaddcopies : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "book_id",
                table: "AuthorBook");

            migrationBuilder.RenameColumn(
                name: "book_id",
                table: "BorrowsBook",
                newName: "BookId");

            migrationBuilder.RenameColumn(
                name: "date_of_borrowing_expiration",
                table: "BorrowsBook",
                newName: "return_date");

            migrationBuilder.RenameColumn(
                name: "date_of_borrowing",
                table: "BorrowsBook",
                newName: "borrow_date");

            migrationBuilder.RenameIndex(
                name: "IX_BorrowsBook_book_id",
                table: "BorrowsBook",
                newName: "IX_BorrowsBook_BookId");

            migrationBuilder.AlterColumn<int>(
                name: "BookId",
                table: "BorrowsBook",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddColumn<int>(
                name: "book_copy_id",
                table: "BorrowsBook",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "available_copies",
                table: "Book",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "total_copies",
                table: "Book",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "BookCopy",
                columns: table => new
                {
                    BookCopyId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    BookId = table.Column<int>(type: "integer", nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookCopy", x => x.BookCopyId);
                    table.ForeignKey(
                        name: "FK_BookCopy_Book_BookId",
                        column: x => x.BookId,
                        principalTable: "Book",
                        principalColumn: "book_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BorrowsBook_book_copy_id",
                table: "BorrowsBook",
                column: "book_copy_id");

            migrationBuilder.CreateIndex(
                name: "IX_BookCopy_BookId",
                table: "BookCopy",
                column: "BookId");

            migrationBuilder.AddForeignKey(
                name: "FK_BorrowsBook_Book_BookId",
                table: "BorrowsBook",
                column: "BookId",
                principalTable: "Book",
                principalColumn: "book_id");

            migrationBuilder.AddForeignKey(
                name: "book_copy_id",
                table: "BorrowsBook",
                column: "book_copy_id",
                principalTable: "BookCopy",
                principalColumn: "BookCopyId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BorrowsBook_Book_BookId",
                table: "BorrowsBook");

            migrationBuilder.DropForeignKey(
                name: "book_copy_id",
                table: "BorrowsBook");

            migrationBuilder.DropTable(
                name: "BookCopy");

            migrationBuilder.DropIndex(
                name: "IX_BorrowsBook_book_copy_id",
                table: "BorrowsBook");

            migrationBuilder.DropColumn(
                name: "book_copy_id",
                table: "BorrowsBook");

            migrationBuilder.DropColumn(
                name: "available_copies",
                table: "Book");

            migrationBuilder.DropColumn(
                name: "total_copies",
                table: "Book");

            migrationBuilder.RenameColumn(
                name: "BookId",
                table: "BorrowsBook",
                newName: "book_id");

            migrationBuilder.RenameColumn(
                name: "return_date",
                table: "BorrowsBook",
                newName: "date_of_borrowing_expiration");

            migrationBuilder.RenameColumn(
                name: "borrow_date",
                table: "BorrowsBook",
                newName: "date_of_borrowing");

            migrationBuilder.RenameIndex(
                name: "IX_BorrowsBook_BookId",
                table: "BorrowsBook",
                newName: "IX_BorrowsBook_book_id");

            migrationBuilder.AlterColumn<int>(
                name: "book_id",
                table: "BorrowsBook",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "book_id",
                table: "BorrowsBook",
                column: "book_id",
                principalTable: "Book",
                principalColumn: "book_id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
