using Library.Entities;
using Microsoft.EntityFrameworkCore;

namespace Library.Annotation
{
    public class BookAnnotation : BaseEntityAnnotation<Book>
    {
        public BookAnnotation(ModelBuilder builder)
            : base(builder) { }

        public override void Annotate()
        {
            this.ModelBuilder.HasKey(b => b.BookId);
            this.ModelBuilder.Property(b => b.BookId).ValueGeneratedOnAdd().UseIdentityColumn().HasColumnName("book_id");
            this.ModelBuilder.Property(b => b.Title).IsRequired().HasMaxLength(50).HasColumnName("title");
            this.ModelBuilder.Property(b => b.Description).IsRequired().HasMaxLength(500).HasColumnName("description");
            this.ModelBuilder.Property(b => b.BookImage).HasColumnName("book_image");
            this.ModelBuilder.Property(b => b.NumberOfBorrows).IsRequired().HasColumnName("number_of_borrows");
            this.ModelBuilder.Property(b => b.NumberOfComments).IsRequired().HasColumnName("number_of_comments");
            this.ModelBuilder.Property(b => b.NumberOfLikes).IsRequired().HasColumnName("number_of_likes");
            this.ModelBuilder.Property(b => b.YearOfPublication).IsRequired().HasColumnName("year_of_publication");
            this.ModelBuilder.Property(b => b.ISBN).IsRequired().HasColumnName("isbn");
            this.ModelBuilder.Property(b => b.CreatedDate).IsRequired().HasColumnName("created_date");
            this.ModelBuilder.Property(b => b.ModifiedDate).HasColumnName("modified_date").IsRequired(false);
            this.ModelBuilder.Property(c => c.LanguageId).HasColumnName("language_id");
            this.ModelBuilder.Property(c => c.PublishingHouseId).HasColumnName("publishing_house_id");
            this.ModelBuilder.Property(c => c.CategoryId).HasColumnName("category_id");

        }
    }
}
