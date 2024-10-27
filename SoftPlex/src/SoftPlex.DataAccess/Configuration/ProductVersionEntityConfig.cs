using Microsoft.EntityFrameworkCore;
using SoftPlex.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SoftPlex.Domain;

namespace SoftPlex.DataAccess.Configuration
{
	internal class ProductVersionEntityConfig: IEntityTypeConfiguration<ProductVersionEntity>
	{
		public void Configure(EntityTypeBuilder<ProductVersionEntity> builder)
		{
			builder.ToTable("product_version");
			builder.HasKey(x => x.Id);

			builder.Property(x => x.Name)
				.HasMaxLength(ProductVersion.MAX_TITLE_LENGHT)
				.IsRequired();

			builder.Property(x => x.Description)
				;
			
			builder.Property(x => x.Width)
				.IsRequired();

			builder.Property(x => x.Height)
				.IsRequired();

			builder.Property(x => x.Length)
				.IsRequired();

			builder
				.Property(x => x.CreatingDate)
				.HasDefaultValueSql("now() at time zone 'utc'")
				;

			builder.HasIndex(e => e.CreatingDate);
			builder.HasIndex(e => e.Width);
			builder.HasIndex(e => e.Height);
			builder.HasIndex(e => e.Length);
		}
	}
}
