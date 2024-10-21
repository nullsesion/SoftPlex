using Microsoft.EntityFrameworkCore;
using SoftPlex.Domain;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SoftPlex.DataAccess.Configuration
{
	public class ProductVersionConfig : IEntityTypeConfiguration<ProductVersion>
	{
		private const string TABLE_NAME = "product_version";
		public void Configure(EntityTypeBuilder<ProductVersion> builder)
		{
			builder.ToTable(TABLE_NAME);
			builder.HasKey(x => x.Id);

			builder.HasIndex(e => e.CreatingDate);

			builder.Property(x => x.Name)
				.HasMaxLength(ProductVersion.MAX_TITLE_LENGHT)
				.IsRequired();

			builder.Property(x => x.Description)
				.HasMaxLength(ProductVersion.MAX_DESCRIPTION_LENGHT);

			builder.ComplexProperty(x => x.SizeBox, b =>
			{
				b.IsRequired();

				b.Property(x => x.Width)
					.HasColumnName("width");

				b.Property(x => x.Height)
					.HasColumnName("height");

				b.Property(x => x.Length)
					.HasColumnName("length");
			});
			/*
			builder.ToSqlQuery("CREATE INDEX ix_product_version_width ON product_version (width);");
			builder.ToSqlQuery("CREATE INDEX ix_product_version_height ON product_version (height);");
			builder.ToSqlQuery("CREATE INDEX ix_product_version_length ON product_version (length);");
			*/

			builder
				.Property(x => x.CreatingDate)
				.HasDefaultValueSql("now() at time zone 'utc'")
				;


		}
	}
}
