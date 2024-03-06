using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.Mvc;
using AgenziaSpedizioni.Models;

namespace AgenziaSpedizioni.Controllers
{
    public class UtenteController : Controller
    {
        public ActionResult ElencoUtenti()
        {
            string connString = ConfigurationManager.ConnectionStrings["DbAgenziaSpedizioni"].ToString();
            using (var conn = new SqlConnection(connString))
            {
                conn.Open();
                var command = new SqlCommand("SELECT * FROM UtenteCF", conn);
                var reader = command.ExecuteReader();

                var utenti = new List<Utente>();
                while (reader.Read())
                {
                    var utente = new Utente
                    {
                        Id = (int)reader["Id"],
                        Nome = (string)reader["Nome"],
                        Cognome = (string)reader["Cognome"],
                        Email = (string)reader["Email"],
                        CodiceFiscale = (string)reader["CodiceFiscale"],
                        Username = (string)reader["Username"],
                    };
                    utenti.Add(utente);
                }
                return View(utenti);
            }
        }
    }
}
