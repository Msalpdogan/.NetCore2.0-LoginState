using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BiilkLogin.Models;
using Microsoft.AspNetCore.Http;

namespace BiilkLogin.Controllers
{
    public class HomeController : Controller
    {

        private SessionLayer sessionLyr;

        public HomeController(SessionLayer _session)
        {
            sessionLyr = _session;
        }
        private string GetMessage()
        {
           
                return $"{sessionLyr.Id}-{sessionLyr.Name}-{sessionLyr.Username}-{sessionLyr.Passwordd}-{sessionLyr.Mail}-{sessionLyr.Addresskey}  Kullanıcısısın zaten!";
          
               
            
        }
        public IActionResult Index()
        {
            
            return View();
        }

        public IActionResult About()
        {
           

            return View("About", GetMessage());
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
