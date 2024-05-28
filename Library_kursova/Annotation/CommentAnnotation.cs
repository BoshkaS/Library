using Library_kursova.Entities;
using Microsoft.EntityFrameworkCore;

namespace Library_kursova.Annotation
{
    public class CommentAnnotation : BaseEntityAnnotation<Comment>
    {
        public CommentAnnotation(ModelBuilder builder)
            : base(builder) { }

        public override void Annotate()
        {
            this.ModelBuilder.HasKey(c => c.CommentId);
            this.ModelBuilder.Property(c => c.CommentId).ValueGeneratedOnAdd().UseIdentityColumn().HasColumnName("comment_id");
            this.ModelBuilder.Property(c => c.Text).IsRequired().HasMaxLength(500).HasColumnName("text");
            this.ModelBuilder.Property(c => c.CreatedDate).IsRequired().HasColumnName("created_date");
            this.ModelBuilder.Property(c => c.ModifiedDate).HasColumnName("modified_date");
            this.ModelBuilder.Property(c => c.UserId).HasColumnName("user_id");
            this.ModelBuilder.Property(c => c.BookId).HasColumnName("book_id");
            this.ModelBuilder.HasOne(c => c.User).WithMany(c => c.Comments).HasForeignKey(c => c.UserId).IsRequired(false).HasConstraintName("user_id");
            this.ModelBuilder.HasOne(c => c.Book).WithMany(c => c.Comments).HasForeignKey(c => c.BookId).IsRequired(false).HasConstraintName("book_id");
        }
    }
}
