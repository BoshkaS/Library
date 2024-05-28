using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Library_kursova.Annotation
{
    public abstract class BaseEntityAnnotation<T> : IEntityAnnotation
        where T : class
    {

        protected EntityTypeBuilder<T> ModelBuilder { get; }
        protected BaseEntityAnnotation(ModelBuilder builder)
        {
            this.ModelBuilder = builder.Entity<T>();
        }

        public abstract void Annotate();
    }
}
