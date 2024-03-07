using System.Configuration;
using System.Data.SqlClient;
using System.Web.Mvc;
using System.Web.Security;
using AgenziaSpedizioni.Models;


namespace AgenziaSpedizioni.Controllers
{
    public class LoginController : Controller
    {
        public ActionResult Index()
        {
            if (HttpContext.User.Identity.IsAuthenticated) return RedirectToAction("Logged");
            return View();
        }
        [HttpPost]
        public ActionResult Index(string Username, string Password )
        {
            string connString = ConfigurationManager.ConnectionStrings["DbAgenziaSpedizioni"].ToString();
            using (var conn = new SqlConnection(connString))
            {

                conn.Open();
                var command = new SqlCommand("SELECT * FROM UtenteCF WHERE Username = @Username AND Password = @password", conn);
                command.Parameters.AddWithValue("@Username", Username);
                command.Parameters.AddWithValue("@Password", Password);
                var reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    reader.Read();
                    FormsAuthentication.SetAuthCookie(reader["Id"].ToString()+" ,Utente Privato", true);
                    return RedirectToAction("Index", "Login"); 
                }
                reader.Close();

                command = new SqlCommand("SELECT * FROM UtentePI WHERE Username = @Username AND Password = @password", conn);
                command.Parameters.AddWithValue("@Username", Username);
                command.Parameters.AddWithValue("@Password", Password);
                reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    reader.Read();
                    FormsAuthentication.SetAuthCookie(reader["Id"].ToString()+" ,Azienda", true);
                    return RedirectToAction("Index", "Login");
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

            return RedirectToAction("Index", "Login");

        }
        public ActionResult Register()
        {
            if (HttpContext.User.Identity.IsAuthenticated) return RedirectToAction("Prova");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register([Bind(Exclude = "Id,Role")] Utente utente)
        {
            if (ModelState.IsValid)
            {
                string connString = ConfigurationManager.ConnectionStrings["DbAgenziaSpedizioni"].ToString();
                var conn = new SqlConnection(connString);
                conn.Open();
                var command = new SqlCommand(@"
                    INSERT INTO UtenteCF
                    (Username, Email, Password, CodiceFiscale, Nome, Cognome)
                    VALUES (@username, @email, @password,@codiceFiscale,@nome,@cognome)
                ", conn);
                command.Parameters.AddWithValue("@username", utente.Username);
                command.Parameters.AddWithValue("@email", utente.Email);
                command.Parameters.AddWithValue("@password", utente.Password);
                command.Parameters.AddWithValue("@codiceFiscale", utente.CodiceFiscale);
                command.Parameters.AddWithValue("@nome", utente.Nome);
                command.Parameters.AddWithValue("@cognome", utente.Cognome);
                var countRows = command.ExecuteNonQuery();
                return RedirectToAction("Index");
            }
            return View();
        }
        public ActionResult RegisterChoice()
        {
            return View();
        }

        [HttpPost]
        public ActionResult RegisterChoice(string registrationType)
        {
            if (registrationType == "CF")
            {
                return RedirectToAction("Register", "Login");
            }
            else if (registrationType == "PI")
            {
                return RedirectToAction("Register", "UtentePI");
            }
            else
            { 
                return RedirectToAction("Index", "Home");
            }
        }

    }
}