using Library.Entities;
using Microsoft.EntityFrameworkCore;

namespace Library.Annotation
{
    public class CategoryAnnotation : BaseEntityAnnotation<Category>
    {
        public CategoryAnnotation(ModelBuilder builder)
            : base(builder) { }

        public override void Annotate()
        {
            this.ModelBuilder.HasKey(c => c.CategoryId);
            this.ModelBuilder.Property(c => c.CategoryId).ValueGeneratedOnAdd().UseIdentityColumn().HasColumnName("category_id");
            this.ModelBuilder.Property(c => c.Name).IsRequired().HasMaxLength(30).HasColumnName("name");
            this.ModelBuilder.HasMany(c => c.Books).WithOne(c => c.Category).HasForeignKey(c => c.CategoryId).HasConstraintName("category_id");
        }
    }
}
