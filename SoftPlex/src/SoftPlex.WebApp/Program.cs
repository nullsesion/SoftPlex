using SoftPlex.WebApp.Services;
using System.Security.Claims;
using Microsoft.Net.Http.Headers;

namespace SoftPlex.WebApp
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			// Add services to the container.
			builder.Services.AddControllersWithViews();
			RazorRuntimeCompilationMvcBuilderExtensions
				.AddRazorRuntimeCompilation(builder.Services
					.AddControllersWithViews());


			builder.Services.AddAuthentication("Cookie")
				.AddCookie("Cookie",
					config =>
					{
						config.LoginPath = "/Admin/Login";
						config.LogoutPath = "/Admin/Logout";
					})
				;
			builder.Services.AddAuthorization(options =>
			{
				options.AddPolicy("Administrator", builder =>
				{
					builder.RequireClaim(ClaimTypes.Role, "Administrator");
				});
			});

			builder.Services.AddHttpClient(nameof(ClientApiProductService), (HttpClient client) =>
			{
				var section = builder.Configuration.GetSection("AppSettings");
				string host = section.GetValue(nameof(ClientApiProductService), "https://localhost:7044");
				client.BaseAddress = new Uri(host);
				client.DefaultRequestHeaders.Add(HeaderNames.UserAgent,"WebApp");
			})
			;
			builder.Services.AddScoped<ClientApiProductService>();


			var app = builder.Build();

			// Configure the HTTP request pipeline.
			if (!app.Environment.IsDevelopment())
			{
				app.UseExceptionHandler("/Home/Error");
				// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
				app.UseHsts();
			}

			app.UseHttpsRedirection();
			app.MapControllers();
			app.UseStaticFiles();

			app.UseRouting();

			app.UseAuthentication();
			app.UseAuthorization();

			app.MapDefaultControllerRoute();
			/*
			app.MapControllerRoute(
				name: "default",
				pattern: "{controller=Home}/{action=Index}/{id?}");
			*/

			app.Run();
		}
	}
}
