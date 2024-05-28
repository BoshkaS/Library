using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Library_kursova.Migrations
{
    /// <inheritdoc />
    public partial class updateBorrowsBook : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateOnly>(
                name: "date_of_borrowing",
                table: "BorrowsBook",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));

            migrationBuilder.AddColumn<DateOnly>(
                name: "date_of_borrowing_expiration",
                table: "BorrowsBook",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "date_of_borrowing",
                table: "BorrowsBook");

            migrationBuilder.DropColumn(
                name: "date_of_borrowing_expiration",
                table: "BorrowsBook");
        }
    }
}
