using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Dojodachi.Models;

namespace Dojodachi.Controllers;

public class HomeController : Controller
{
    public IActionResult Index(MyDachi dachi)
    {
        int? fullness = HttpContext.Session.GetInt32("Fullness");
        int? happiness = HttpContext.Session.GetInt32("Happiness");
        int? meals = HttpContext.Session.GetInt32("Meals");
        int? energy = HttpContext.Session.GetInt32("Energy");

        if (fullness == null) 
        {
            HttpContext.Session.SetInt32("Fullness", dachi.Fullness);
        }
        if (happiness == null) 
        {
            HttpContext.Session.SetInt32("Happiness", dachi.Happiness);
        }
        if (meals == null) 
        {
            HttpContext.Session.SetInt32("Meals", dachi.Meals);
        }
        if (energy == null) 
        {
            HttpContext.Session.SetInt32("Energy", dachi.Energy);
        }

        return View("Index", dachi);
    }
    
    [HttpPost("feed")]
    public IActionResult Feed(MyDachi dachi)
    {
        // Console.WriteLine("*Munching* *Burrrp*");
        Random rand = new Random();
        if (dachi.Meals > 0)
        {
            dachi.Meals -= 1;
            dachi.Fullness += rand.Next(5,11);
            dachi.Status = "*Munching* *Burrrp*";
        } else
        {
            dachi.Status = "Wait, my bowl is empty.  :(";
        }
        // Console.WriteLine(dachi.Meals);
        // Console.WriteLine(dachi.Fullness);
        return RedirectToAction("Index");
    }
    public string Play()
    {
        return "Play meh";
    }
    public string Work()
    {
        return "Work meh";
    }
    public string Sleep()
    {
        return "Sleep meh";
    }

    public IActionResult Privacy()
    {
        return View();
    }
}
