using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.Mvc;
using AgenziaSpedizioni.Models;

namespace AgenziaSpedizioni.Controllers
{
    public class SpedizioniController : Controller
    {
        public ActionResult ElencoSpedizioni()
        {
            List<Spedizione> spedizioni = new List<Spedizione>();
            try
            {
                string connString = ConfigurationManager.ConnectionStrings["DbAgenziaSpedizioni"].ToString();
                using (var conn = new SqlConnection(connString))
                {
                    conn.Open();
                    var command = new SqlCommand("SELECT * FROM Spedizione", conn);
                    var reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        var spedizione = new Spedizione
                        {
                            Id = (int)reader["Id"],
                            DataSpedizione = (DateTime)reader["DataSpedizione"],
                            Peso = (decimal)reader["Peso"],
                            CittàDestinataria = (string)reader["CittàDestinataria"],
                            Indirizzo = (string)reader["Inidrizzo"],
                            NominativoDestinatario = (string)reader["NominativoDestinatario"],
                            CostiSpedizione = (decimal)reader["CostiSpedizione"],
                            DataArrivo = (DateTime)reader["DataArrivo"],
                            NominativoVenditore = (string)reader["NominativoVenditore"]
                        };
                        spedizioni.Add(spedizione);
                    }
                }
            }
            catch (Exception ex)
            {
            }

            return View(spedizioni);
        }

        public ActionResult AggiungiSpedizione()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AggiungiSpedizione(Spedizione spedizione)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    string connString = ConfigurationManager.ConnectionStrings["DbAgenziaSpedizioni"].ToString();
                    using (var conn = new SqlConnection(connString))
                    {
                        conn.Open();
                        var command = new SqlCommand(@"
                            INSERT INTO Spedizione
                            (DataSpedizione, Peso, CittàDestinataria, Indirizzo, NominativoDestinatario, CostiSpedizione, DataArrivo, NominativoVenditore)
                            VALUES (@dataSpedizione, @peso, @cittàDestinataria, @indirizzo, @nominativoDestinatario, @costiSpedizione, @dataArrivo, @nominativoVenditore)
                        ", conn);
                        command.Parameters.AddWithValue("@dataSpedizione", spedizione.DataSpedizione);
                        command.Parameters.AddWithValue("@peso", spedizione.Peso);
                        command.Parameters.AddWithValue("@cittàDestinataria", spedizione.CittàDestinataria);
                        command.Parameters.AddWithValue("@inidrizzo", spedizione.Indirizzo);
                        command.Parameters.AddWithValue("@nominativoDestinatario", spedizione.NominativoDestinatario);
                        command.Parameters.AddWithValue("@costiSpedizione", spedizione.CostiSpedizione);
                        command.Parameters.AddWithValue("@dataArrivo", spedizione.DataArrivo);
                        command.Parameters.AddWithValue("@nominativoVenditore", spedizione.NominativoVenditore);

                        command.ExecuteNonQuery();
                    }

                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                }
            }

            return View(spedizione);
        }
    }
}
