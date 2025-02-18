using Library_kursova.Entities;
using Microsoft.EntityFrameworkCore;

namespace Library_kursova.Annotation
{
    public class BookCopyAnnotation : BaseEntityAnnotation<BookCopy>
    {
        public BookCopyAnnotation(ModelBuilder builder)
            : base(builder) { }

        public override void Annotate()
        {
            this.ModelBuilder.HasKey(b => b.BookCopyId);
            this.ModelBuilder.Property(b => b.BookCopyId).ValueGeneratedOnAdd().UseIdentityColumn().HasColumnName("borrows_book_id");
            this.ModelBuilder.HasOne(b => b.Book).WithMany(b => b.Copies).HasForeignKey(b => b.BookId).HasConstraintName("book_id");
            this.ModelBuilder.Property(b => b.Status).HasConversion<string>();
        }
    }
}
