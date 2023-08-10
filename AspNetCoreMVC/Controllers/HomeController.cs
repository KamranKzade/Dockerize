using AspNetCoreMVC.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.FileProviders;
using System.Diagnostics;

namespace AspNetCoreMVC.Controllers;

public class HomeController : Controller
{
	private readonly IFileProvider _fileProvider;
	private readonly IConfiguration _configuration;
	private readonly ILogger<HomeController> _logger;


    public HomeController(ILogger<HomeController> logger, IFileProvider fileProvider, IConfiguration configuration)
    {
        _logger = logger;
        _fileProvider = fileProvider;
        _configuration = configuration;
    }

    public IActionResult Index()
	{
		ViewBag.SqlCon = _configuration["SqlCon"];
        return View();
	}

	public IActionResult Privacy()
	{
		return View();
	}

	public IActionResult ImageShow()
	{
		var images = _fileProvider.GetDirectoryContents("wwwroot/images").ToList().Select(x => x.Name);
	
		return View(images);	
	}
	[HttpPost]
	public IActionResult ImageShow(string name)
	{
		var file = _fileProvider.GetDirectoryContents("wwwroot/images")
								.ToList()
								.FirstOrDefault(x=>x.Name==name);

		System.IO.File.Delete(file.PhysicalPath);

		return RedirectToAction("ImageShow");
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