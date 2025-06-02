using Library.Entities;
using Microsoft.EntityFrameworkCore;

namespace Library.Annotation
{
    public class LanguageAnnotation : BaseEntityAnnotation<Language>
    {
        public LanguageAnnotation(ModelBuilder builder)
            : base(builder) { }

        public override void Annotate()
        {
            this.ModelBuilder.HasKey(c => c.LanguageId);
            this.ModelBuilder.Property(c => c.LanguageId).ValueGeneratedOnAdd().UseIdentityColumn().HasColumnName("language_id");
            this.ModelBuilder.Property(c => c.Name).IsRequired().HasMaxLength(30).HasColumnName("name");
            
            this.ModelBuilder.HasMany(c => c.Books).WithOne(c => c.Language).HasForeignKey(c => c.LanguageId).HasConstraintName("language_id");
        }
    }
}
