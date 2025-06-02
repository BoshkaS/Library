using Library.Entities;
using Microsoft.EntityFrameworkCore;

namespace Library.Annotation
{
    public class BookmarkBookAnnotation : BaseEntityAnnotation<BookmarkBook>
    {
        public BookmarkBookAnnotation(ModelBuilder builder)
            : base(builder) { }

        public override void Annotate()
        {
            this.ModelBuilder.HasKey(b => b.BookmarkBookId);
            this.ModelBuilder.Property(b => b.BookmarkBookId).ValueGeneratedOnAdd().UseIdentityColumn().HasColumnName("bookmark_book_id");
            this.ModelBuilder.Property(c => c.UserId).HasColumnName("user_id");
            this.ModelBuilder.Property(c => c.BookId).HasColumnName("book_id");
            this.ModelBuilder.HasOne(b => b.User).WithMany(b => b.BookmarkBooks).HasForeignKey(b => b.UserId).IsRequired(false).HasConstraintName("user_id");
            this.ModelBuilder.HasOne(b => b.Book).WithMany(b => b.BookBookmarks).HasForeignKey(b => b.BookId).HasConstraintName("book_id");
        }
    }
}
