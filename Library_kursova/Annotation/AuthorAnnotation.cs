using Library_kursova.Entities;
using Microsoft.EntityFrameworkCore;

namespace Library_kursova.Annotation
{
    public class AuthorAnnotation :BaseEntityAnnotation<Author>
    {
        public AuthorAnnotation(ModelBuilder builder)
            : base(builder) { }

        public override void Annotate()
        {
            this.ModelBuilder.HasKey(a => a.AuthorId);
            this.ModelBuilder.Property(a => a.AuthorId).ValueGeneratedOnAdd().UseIdentityColumn().HasColumnName("author_id");
            this.ModelBuilder.Property(a => a.Pseudonym).IsRequired().HasMaxLength(50).HasColumnName("pseudonym");
        }
    }
}
