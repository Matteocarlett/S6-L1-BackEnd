using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace AgenziaSpedizioni.Models
{
    public class Post
    {
        [Key]
        public int PostId { get; set; }

        [Display(Name = "Titolo")]
        [Required(ErrorMessage = "Campo obbligatorio")]
        [StringLength(100, MinimumLength = 10, ErrorMessage = "Il titolo deve avere min 10 e max 100 caratteri")]
        public string Title { get; set; }

        [AllowHtml]
        [DataType(DataType.MultilineText)]
        [Required(ErrorMessage = "Campo obbligatorio")]
        [Display(Name = "Contenuto")]
        public string Contents { get; set; }

        [Display(Name = "Utente")]
        public int Id { get; set; }

        public Utente utente { get; set; }
    }
}