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
            int? chance = rand.Next(0,4);
            if(chance == 0)
            {
                numOfMeals--;
                updateStatus = "I wasn't hungry though...";
                HttpContext.Session.SetInt32("Meals", (int)numOfMeals);
                HttpContext.Session.SetString("Status", updateStatus);
            } else
            {
                numOfMeals--;
                fullnessLvl += rand.Next(5,11);
                updateStatus = "*Munch munch munch* Yummy!";
                HttpContext.Session.SetInt32("Meals", (int)numOfMeals);
                HttpContext.Session.SetInt32("Fullness", (int)fullnessLvl);
                HttpContext.Session.SetString("Status", updateStatus);
            }
        } else
        {
            updateStatus = "Wait, there's no meal...";
            HttpContext.Session.SetString("Status", updateStatus);
        }
        return RedirectToAction("Index");
    }

    [HttpPost("play")]
    public IActionResult Play()
    {
        Random rand = new Random();
        int? energyLvl = HttpContext.Session.GetInt32("Energy");
        int? happinessLvl = HttpContext.Session.GetInt32("Happiness");
        string? updateStatus = HttpContext.Session.GetString("Status");
        if (energyLvl > 0)
        {
            int chance = rand.Next(0,4);
            if (chance == 0){
                energyLvl -= 5;
                updateStatus = "I didn't really feel like playing though...";
                HttpContext.Session.SetInt32("Energy", (int)energyLvl);
                HttpContext.Session.SetString("Status", updateStatus);
            } else 
            {
                energyLvl -= 5;
                happinessLvl += rand.Next(5,11);
                updateStatus = "So fun!  Yay!";
                HttpContext.Session.SetInt32("Energy", (int)energyLvl);
                HttpContext.Session.SetInt32("Happiness", (int)happinessLvl);
                HttpContext.Session.SetString("Status", updateStatus);
            }
        } else
        {
            updateStatus = "Chopper has fainted from exhaustion...";
            HttpContext.Session.SetString("Status", updateStatus);
        }
        return RedirectToAction("Index");
    }

    [HttpPost("work")]
    public IActionResult Work()
    {
        Random rand = new Random();
        int? energyLvl = HttpContext.Session.GetInt32("Energy");
        int? numOfMeals = HttpContext.Session.GetInt32("Meals");
        string? updateStatus = HttpContext.Session.GetString("Status");

        if (energyLvl > 0)
        {
            energyLvl -= 5;
            numOfMeals += rand.Next(1,4);
            updateStatus = "Right! Let's do our best!";
            HttpContext.Session.SetInt32("Energy", (int)energyLvl);
            HttpContext.Session.SetInt32("Meals", (int)numOfMeals);
            HttpContext.Session.SetString("Status", updateStatus);
        } else
        {
            updateStatus = "Chopper has fainted from exhaustion...";
            HttpContext.Session.SetString("Status", updateStatus);
        } 
        return RedirectToAction("Index");
    }

    [HttpPost("sleep")]
    public IActionResult Sleep()
    {
        int? energyLvl = HttpContext.Session.GetInt32("Energy");
        int? fullnessLvl = HttpContext.Session.GetInt32("Fullness");
        int? happinessLvl = HttpContext.Session.GetInt32("Happiness");
        string? updateStatus = HttpContext.Session.GetString("Status");

        if (energyLvl > 0)
        {
            energyLvl += 15;
            fullnessLvl -= 5;
            happinessLvl -=5;
            updateStatus = "Zzzzzzzz *snores*";
            HttpContext.Session.SetInt32("Energy", (int)energyLvl);
            HttpContext.Session.SetInt32("Fullness", (int)fullnessLvl);
            HttpContext.Session.SetInt32("Happiness", (int)happinessLvl);
        } else
        {
            updateStatus = "Chopper has fainted from exhaustion...";
            HttpContext.Session.SetString("Status", updateStatus);
        }
        return RedirectToAction("Index");
    }

    [HttpPost("clear")]
    public IActionResult Restart()
    {
        HttpContext.Session.Clear();
        return RedirectToAction("Index");
    }

    public IActionResult Privacy()
    {
        return View();
    }
}
