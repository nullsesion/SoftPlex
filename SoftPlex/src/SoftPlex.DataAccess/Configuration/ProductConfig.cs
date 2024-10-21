using System.Security.Cryptography.X509Certificates;
using Microsoft.EntityFrameworkCore;
using SoftPlex.Domain;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SoftPlex.DataAccess.Configuration
{
	public class ProductConfig : IEntityTypeConfiguration<Product>
	{
		private const string TABLE_NAME = "product";
		public void Configure(EntityTypeBuilder<Product> builder)
		{
			builder.ToTable(TABLE_NAME);

			builder.HasKey(x => x.Id);
			builder.HasIndex(e => e.Name);

			builder.Property(x => x.Name)
				.HasMaxLength(Product.MAX_NAME_LENGHT)
				.IsRequired()
				;

			builder.Property(x => x.Description)
				;

			builder.HasMany(i => i.ProductVersions)
				.WithOne()
				.HasForeignKey(e => e.ProductId)
				.OnDelete(DeleteBehavior.Cascade)
				;
 		}
	}
}
