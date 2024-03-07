using System.ComponentModel.DataAnnotations;

namespace AgenziaSpedizioni.Models
{
    public class UtentePI
    {
        public int Id { get; set; }

        [Required]
        public string PartitaIVA { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        public string Nome { get; set; }

        [Required]
        public string Cognome { get; set; }
    }
}
