using AspNetCoreMVC.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace AspNetCoreMVC.Controllers;

public class HomeController : Controller
{
	private readonly ILogger<HomeController> _logger;

	public HomeController(ILogger<HomeController> logger)
	{
		_logger = logger;
	}

	public IActionResult Index()
	{
		return View();
	}

	public IActionResult Privacy()
	{
		return View();
	}

	public IActionResult ImageSave()
	{
		return View();
	}

	[HttpPost]
	public async Task<IActionResult> ImageSave(IFormFile imageFile)
	{
		if (imageFile != null && imageFile.Length > 0)
		{
			var fileName = Guid.NewGuid().ToString() + Path.GetExtension(imageFile.FileName);

			var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", fileName);

			using (var stream = new FileStream(path, FileMode.Create))
			{
				await imageFile.CopyToAsync(stream);
			}
		}

		return View();
	}


	[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
	public IActionResult Error()
	{
		return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
	}
}