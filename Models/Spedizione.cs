using System;

namespace AgenziaSpedizioni.Models
{
    public class Spedizione
    {
        public int Id { get; set; }
        public DateTime DataSpedizione { get; set; }
        public decimal Peso { get; set; }
        public string CittàDestinataria { get; set; }
        public string Indirizzo { get; set; }
        public string NominativoDestinatario { get; set; }
        public decimal CostiSpedizione { get; set; }
        public DateTime DataArrivo { get; set; }
        public string NominativoVenditore { get; set; }
    }
}
