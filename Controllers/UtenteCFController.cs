using System.Configuration;
using System.Data.SqlClient;
using System.Web.Mvc;
using System.Web.Security;
using AgenziaSpedizioni.Models;


namespace AgenziaSpedizioni.Controllers
{
    public class UtenteCFController : Controller
    {
        public ActionResult Index()
        {
            if (HttpContext.User.Identity.IsAuthenticated) return RedirectToAction("Logged");
            return View();
        }
        [HttpPost]
        public ActionResult Index(UtenteCF utentecf)
        {
            string connString = ConfigurationManager.ConnectionStrings["DbAgenziaSpedizioni"].ToString();
            using (var conn = new SqlConnection(connString))
            {
                conn.Open();
                var command = new SqlCommand("SELECT * FROM UtenteCF WHERE Username = @Username AND Password = @Password", conn);
                command.Parameters.AddWithValue("@Username", utentecf.Username);
                command.Parameters.AddWithValue("@Password", utentecf.Password);
                var reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    reader.Read();
                    FormsAuthentication.SetAuthCookie(reader["Id"].ToString(), true);
                    return RedirectToAction("Index", "UtenteCF"); 
                }
            }

            return RedirectToAction("Index");
        }


        [Authorize]
        public ActionResult Logged()
        {
            var Id = HttpContext.User.Identity.Name;
            ViewBag.Id = Id;
            return View();
        }

        [Authorize, HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();

            return RedirectToAction("Index", "UtenteCF");

        }
    }
}