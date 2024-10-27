using Microsoft.EntityFrameworkCore;
using SoftPlex.DataAccess.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SoftPlex.Domain;

namespace SoftPlex.DataAccess.Configuration
{
	public class ProductEntityConfig: IEntityTypeConfiguration<ProductEntity>
	{
		public void Configure(EntityTypeBuilder<ProductEntity> builder)
		{
			builder.ToTable("product");

			builder.HasKey(x => x.Id);
			builder.HasIndex(e => e.Name);

			builder.Property(x => x.Name)
				.HasMaxLength(Product.MAX_NAME_LENGHT)
				.IsRequired()
				;

			builder.Property(x => x.Description)
				;

			builder.HasMany(i => i.ProductVersionEntities)
				.WithOne()
				.HasForeignKey(e => e.ProductId)
				.OnDelete(DeleteBehavior.Cascade)
				;

		}
	}
}
