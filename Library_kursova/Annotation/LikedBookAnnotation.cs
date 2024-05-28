using Library_kursova.Entities;
using Microsoft.EntityFrameworkCore;

namespace Library_kursova.Annotation
{
    public class LikedBookAnnotation : BaseEntityAnnotation<LikedBook>
    {
        public LikedBookAnnotation(ModelBuilder builder)
            : base(builder) { }

        public override void Annotate()
        {
            this.ModelBuilder.HasKey(b => b.LikedBookId);
            this.ModelBuilder.Property(b => b.LikedBookId).ValueGeneratedOnAdd().UseIdentityColumn().HasColumnName("liked_book_id");
            this.ModelBuilder.Property(c => c.UserId).HasColumnName("user_id");
            this.ModelBuilder.Property(c => c.BookId).HasColumnName("book_id");
            this.ModelBuilder.HasOne(b => b.User).WithMany(b => b.LikedBooks).HasForeignKey(b => b.UserId).IsRequired(false).HasConstraintName("user_id");
            this.ModelBuilder.HasOne(b => b.Book).WithMany(b => b.BookLikes).HasForeignKey(b => b.BookId).HasConstraintName("book_id");
        }
    }
}
