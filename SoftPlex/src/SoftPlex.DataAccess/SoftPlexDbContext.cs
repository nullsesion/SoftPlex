using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using SoftPlex.Application.Interfaces;
using SoftPlex.DataAccess.Configuration;
using SoftPlex.DataAccess.Entities;
using SoftPlex.Domain;

namespace SoftPlex.DataAccess
{
	public class SoftPlexDbContext : DbContext, ISoftPlexDbContext
	{

		private readonly IConfiguration _configuration;
		public SoftPlexDbContext(IConfiguration configuration) => _configuration = configuration;

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder
				.UseNpgsql(_configuration.GetConnectionString(nameof(SoftPlexDbContext)))
				.UseSnakeCaseNamingConvention() //todo: коммент для ревью ТЗ имена в базах регистронезависимы 
				.UseLoggerFactory(CreateLoggerFactory)
				.EnableSensitiveDataLogging()
				;
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.ApplyConfiguration(new ProductEntityConfig());
			modelBuilder.ApplyConfiguration(new ProductVersionEntityConfig());
			modelBuilder.Entity<FilterEngineEntity>().HasNoKey().ToView(null);

			base.OnModelCreating(modelBuilder);
		}

		private static readonly ILoggerFactory CreateLoggerFactory
			= LoggerFactory.Create(builder => { builder.AddConsole(); });

		public DbSet<ProductEntity> Products { get; set; }
		public DbSet<ProductVersionEntity> ProductVersions { get; set; }
		public DbSet<FilterEngineEntity> filterEngineEntity { get; set; }

	}
}
