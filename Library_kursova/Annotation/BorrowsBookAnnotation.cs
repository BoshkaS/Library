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
            this.ModelBuilder.Property(c => c.BookCopyId).HasColumnName("book_copy_id");
            this.ModelBuilder.Property(b => b.BorrowDate).HasColumnName("borrow_date");
            this.ModelBuilder.Property(b => b.ReturnDate).HasColumnName("return_date");
            this.ModelBuilder.HasOne(b => b.User).WithMany(b => b.BorrowsBooks).HasForeignKey(b => b.UserId).IsRequired(false).HasConstraintName("user_id");
            this.ModelBuilder.HasOne(b => b.BookCopy).WithMany(b => b.BookBorrows).HasForeignKey(b => b.BookCopyId).HasConstraintName("book_copy_id");
        }
    }
}
