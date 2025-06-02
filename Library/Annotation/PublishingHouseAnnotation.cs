using Library.Entities;
using Microsoft.EntityFrameworkCore;

namespace Library.Annotation
{
    public class PublishingHouseAnnotation : BaseEntityAnnotation<PublishingHouse>
    {
        public PublishingHouseAnnotation(ModelBuilder builder)
            : base(builder) { }

        public override void Annotate()
        {
            this.ModelBuilder.HasKey(c => c.PublishingHouseId);
            this.ModelBuilder.Property(c => c.PublishingHouseId).ValueGeneratedOnAdd().UseIdentityColumn().HasColumnName("publishing_house_id");
            this.ModelBuilder.Property(c => c.Name).IsRequired().HasMaxLength(30).HasColumnName("name");
            this.ModelBuilder.HasMany(c => c.Books).WithOne(c => c.PublishingHouse).HasForeignKey(c => c.PublishingHouseId).HasConstraintName("publishing_house_id");

        }
    }
}
