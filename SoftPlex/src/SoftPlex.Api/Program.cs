
using SoftPlex.Api.MapperConfig;
using SoftPlex.Application;
using SoftPlex.Application.Interfaces;
using SoftPlex.DataAccess;
using SoftPlex.DataAccess.Repositories;

namespace SoftPlex.Api
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			// Add services to the container.

			builder.Services.AddControllers();
			builder.Services.AddApplication();

			builder.Services.AddDbContext<ISoftPlexDbContext, SoftPlexDbContext>();

			builder.Services.AddScoped<IProductRepository, ProductRepository>();
			builder.Services.AddMediatR(cfg
				=> cfg.RegisterServicesFromAssembly(typeof(IProductRepository).Assembly)
			);

			builder.Services.AddAutoMapper(cfg =>
			{
				cfg.AddProfile(typeof(AppMappingProfile));
			});

			// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
			builder.Services.AddEndpointsApiExplorer();
			builder.Services.AddSwaggerGen();

			var app = builder.Build();

			// Configure the HTTP request pipeline.
			if (app.Environment.IsDevelopment())
			{
				app.UseSwagger();
				app.UseSwaggerUI();
			}

			app.UseHttpsRedirection();

			app.UseAuthorization();


			app.MapControllers();

			app.Run();
		}
	}
}
