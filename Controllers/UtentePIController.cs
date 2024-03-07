using System.Configuration;
using System.Data.SqlClient;
using System.Web.Mvc;
using AgenziaSpedizioni.Models;

namespace AgenziaSpedizioni.Controllers
{
    public class UtentePIController : Controller
    {
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(UtentePI utentePI)
        {
            if (ModelState.IsValid)
            {
                string connString = ConfigurationManager.ConnectionStrings["DbAgenziaSpedizioni"].ToString();
                using (var conn = new SqlConnection(connString))
                {
                    conn.Open();
                    var command = new SqlCommand(@"
                        INSERT INTO UtentePI
                        (PartitaIVA, Username, Email, Password, Nome, Cognome)
                        VALUES (@partitaIva, @username, @email, @password, @nome, @cognome)
                    ", conn);
                    command.Parameters.AddWithValue("@partitaIva", utentePI.PartitaIVA);
                    command.Parameters.AddWithValue("@username", utentePI.Username);
                    command.Parameters.AddWithValue("@email", utentePI.Email);
                    command.Parameters.AddWithValue("@password", utentePI.Password);
                    command.Parameters.AddWithValue("@nome", utentePI.Nome);
                    command.Parameters.AddWithValue("@cognome", utentePI.Cognome);
                    command.ExecuteNonQuery();
                }
                return RedirectToAction("Index", "Login");
            }

            return View(utentePI);
        }
    }
}
