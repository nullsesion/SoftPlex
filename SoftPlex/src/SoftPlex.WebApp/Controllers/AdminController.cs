using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SoftPlex.WebApp.Models;

namespace SoftPlex.WebApp.Controllers;

[Authorize]
public class AdminController : Controller
{
	// GET
	public IActionResult Index()
	{
		return View();
	}

	[AllowAnonymous]
	public async Task<IActionResult> Login(string returnUrl)
	{
		return View();
	}

	[AllowAnonymous]
	[HttpPost]
	public async Task<IActionResult> Login(LoginViewModel loginViewModel)
	{
		if (!ModelState.IsValid)
		{
			return View(loginViewModel);
		}

		var claims = new List<Claim>
		{
			new Claim(ClaimTypes.Role,"Admin")
		};
		var claimIdentity = new ClaimsIdentity(claims, "Cookie");
		var claimPrincipal = new ClaimsPrincipal(claimIdentity);
		await HttpContext.SignInAsync("Cookie", claimPrincipal);

		
		return Redirect( "/");
	}

	public IActionResult Logout()
	{
		HttpContext.SignOutAsync("Cookie");
		return Redirect("/");
	}
}