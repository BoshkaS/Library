using Library_kursova.Entities;
using Microsoft.EntityFrameworkCore;

namespace Library_kursova.Annotation
{
    public class BorrowsBookAnnotation : BaseEntityAnnotation<BorrowsBook>
    {
        public BorrowsBookAnnotation(ModelBuilder builder)
            : base(builder) { }

        public override void Annotate()
        {
            this.ModelBuilder.HasKey(b => b.BorrowsBookId);
            this.ModelBuilder.Property(b => b.BorrowsBookId).ValueGeneratedOnAdd().UseIdentityColumn().HasColumnName("borrows_book_id");
            this.ModelBuilder.Property(c => c.UserId).HasColumnName("user_id");
            this.ModelBuilder.Property(c => c.BookId).HasColumnName("book_id");
            this.ModelBuilder.Property(b => b.DateOfBorrowing).HasColumnName("date_of_borrowing");
            this.ModelBuilder.Property(b => b.DateOfBorrowingExpiration).HasColumnName("date_of_borrowing_expiration");
            this.ModelBuilder.HasOne(b => b.User).WithMany(b => b.BorrowsBooks).HasForeignKey(b => b.UserId).IsRequired(false).HasConstraintName("user_id");
            this.ModelBuilder.HasOne(b => b.Book).WithMany(b => b.BookBorrows).HasForeignKey(b => b.BookId).HasConstraintName("book_id");
        }
    }
}
