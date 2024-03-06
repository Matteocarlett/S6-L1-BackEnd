using System.ComponentModel.DataAnnotations;

namespace AgenziaSpedizioni.Models
{
    public class Utente
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Campo obbligatorio")]
        [StringLength(20, MinimumLength = 3, ErrorMessage = "Min 3, max 20 caratteri")]
        public string Username { get; set; }

        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessage = "Campo obbligatorio")]
        public string Email { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Campo obbligatorio")]
        [StringLength(15, MinimumLength = 3, ErrorMessage = "Min 3, max 15 caratteri")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Campo obbligatorio")]
        public string CodiceFiscale { get; set; }

        [Required(ErrorMessage = "Campo obbligatorio")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Campo obbligatorio")]
        public string Cognome { get; set; }
    }
}
