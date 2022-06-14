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
        string? status = HttpContext.Session.GetString("Status");

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

        if (status == null)
        {
            HttpContext.Session.SetString("Status", dachi.Status);
        }

        return View("Index", dachi);
    }
    
    [HttpPost("feed")]
    public IActionResult Feed(MyDachi dachi)
    {
        // Console.WriteLine("*Munching* *Burrrp*");
        Random rand = new Random();
        int? numOfMeals = HttpContext.Session.GetInt32("Meals");
        int? fullnessLvl = HttpContext.Session.GetInt32("Fullness");
        string? updateStatus = HttpContext.Session.GetString("Status");
        if (numOfMeals > 0)
        {
            numOfMeals--;
            fullnessLvl += rand.Next(5,11);
            updateStatus = "*Munch munch munch* Yummy!";
            HttpContext.Session.SetInt32("Meals", (int)numOfMeals);
            HttpContext.Session.SetInt32("Fullness", (int)fullnessLvl);
            HttpContext.Session.SetString("Status", updateStatus);
        } else
        {
            updateStatus = "Wait, there's no meal...";
            HttpContext.Session.SetString("Status", updateStatus);
        }
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

    [HttpGet("clear")]
    public IActionResult ClearSession()
    {
        HttpContext.Session.Remove("Reset");
        return RedirectToAction("Index");
    }

    public IActionResult Privacy()
    {
        return View();
    }
}
