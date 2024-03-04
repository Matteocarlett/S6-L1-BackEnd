using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using AgenziaSpedizioni.Models;

namespace Blog.Controllers
{
    public class LoginController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(Utente author)
        {
            var hasValidCredentials = false;


            hasValidCredentials = true;

            if (hasValidCredentials)
            {
                FormsAuthentication.SetAuthCookie(author.UtenteId.ToString(), true);
                return RedirectToAction("", ""); 
            }
            return RedirectToAction("Index");
        }
    }
}