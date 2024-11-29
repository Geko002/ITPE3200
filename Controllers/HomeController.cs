using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using PixNote.Models;

namespace PixNote.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly string _imagePath;

    

    public HomeController(ILogger<HomeController> logger, IWebHostEnvironment env)
    {
        _logger = logger;
        _imagePath = Path.Combine(env.WebRootPath, "images"); // Correct path
    }

    public IActionResult Index()
    {
        // Check if the images directory exists and has files
        bool hasImages = Directory.Exists(_imagePath) && Directory.GetFiles(_imagePath).Any();

        // Pass the result to the view via ViewData
        ViewData["HasImages"] = hasImages;
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
