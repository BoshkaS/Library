using Library.Entities;
using Microsoft.EntityFrameworkCore;

namespace Library.Annotation
{
    public class BorrowRequestAnnotation : BaseEntityAnnotation<BorrowExtensionRequest>
    {
        public BorrowRequestAnnotation(ModelBuilder builder)
            : base(builder) { }

        public override void Annotate()
        {
            this.ModelBuilder.HasKey(a => a.BorrowExtensionRequestId);
            this.ModelBuilder.Property(a => a.BorrowExtensionRequestId).ValueGeneratedOnAdd().UseIdentityColumn().HasColumnName("borrow_extension_request_id");
            this.ModelBuilder.Property(a => a.BorrowsBookId).IsRequired().HasColumnName("borrows_book_id");
            this.ModelBuilder.Property(a => a.Approved).HasColumnName("approved");
            this.ModelBuilder.Property(a => a.RequestedAt).HasColumnName("requested_at").IsRequired();
        }
    }
}
