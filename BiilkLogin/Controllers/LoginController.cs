using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BiilkLogin.Extensions;
using BiilkLogin.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BiilkLogin.Controllers
{
    public class LoginController : Controller
    {
        private SessionLayer SessionLyr;

        public LoginController(SessionLayer _session)
        {
            SessionLyr = _session;
        }
      
        int Id;
        string Name ;
        string Username;
        string Password ;
        string Mail ;
        int Addresskey;
        UserDataAccessLayer objuser = new UserDataAccessLayer();
        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }
       
        private int GetSesion()
        {
            var sesion = HttpContext.Session.GetString("session");
            var sesionoj = Newtonsoft.Json.Linq.JObject.Parse(sesion); // parse as array
            var name = sesionoj.Property("Name");
            var idtemp = sesionoj.Property("Id");
            string id = idtemp.Value.ToString();
            if (name == null)
            {
                return 0;
            }
            else
            {
                return Int32.Parse(id);
            }
           
        }
        [HttpGet]
        public IActionResult Logout( )
        {

            if (GetSesion() == 0)
            {
                return RedirectToAction(actionName: "Index");
            }
            else
            {
                HttpContext.Session.Remove("sesion");
                return RedirectToRoute("Home/Index");
            }
        }

        
        [HttpPost]
        public IActionResult LoginState()
        {
             
            try
            {
                bool statususer = false;
                List<User> userlist = new List<User>();
                userlist = objuser.GetAllUser().ToList();
                String Tempusername = HttpContext.Request.Form["username"].ToString();
                string Temppassword =HttpContext.Request.Form["password"].ToString();
                foreach (var item in userlist)
                {
                    if (item.Username == Tempusername && item.Passwordd ==Temppassword && SessionLyr.Name==null)
                    {
                        statususer = true;
                        Id = item.Id;
                        Name = item.Name;
                        Username = item.Username;
                        Password = item.Passwordd;
                        Mail = item.Mail;
                        Addresskey = item.Addresskey;
                        break;
                    }
                }
                if (statususer)
                {
                    SessionLayer sesiontemp = new SessionLayer { Id = Id, Addresskey = Addresskey, Name = Name.ToString(), Username = Username.ToString(), Passwordd = Password.ToString(), Mail = Mail.ToString() };
                    HttpContext.Session.Set("session",sesiontemp);

                    ViewBag.Result = "Kullanıcı adı veya sifre Dogru";

                }
                else
                {
                    ViewBag.Result = "Kullanıcı adı veya sifre hatalı";
                }
                
            }
            catch (Exception)
            {
                ViewBag.Result = "Erişim hatası ";
            }
            return View("Index");
        }
    }
}