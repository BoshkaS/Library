using Library.Entities;
using Microsoft.EntityFrameworkCore;

namespace Library.Annotation
{
    public class AuthorBookAnnotation : BaseEntityAnnotation<AuthorBook>
    {
        public AuthorBookAnnotation(ModelBuilder builder)
            : base(builder) { }

        public override void Annotate()
        {
            this.ModelBuilder.HasKey(b => b.AuthorBookId);
            this.ModelBuilder.Property(b => b.AuthorBookId).ValueGeneratedOnAdd().UseIdentityColumn().HasColumnName("author_book_id");
            this.ModelBuilder.Property(c => c.AuthorId).HasColumnName("author_id");
            this.ModelBuilder.Property(c => c.BookId).HasColumnName("book_id");
            this.ModelBuilder.HasOne(b => b.Author).WithMany(b => b.AuthorBooks).HasForeignKey(b => b.AuthorId).IsRequired(false).HasConstraintName("user_id");
            this.ModelBuilder.HasOne(b => b.Book).WithMany(b => b.BookAuthors).HasForeignKey(b => b.BookId).HasConstraintName("book_id");
        }
    }
}
