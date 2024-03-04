﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AgenziaSpedizioni.Models
{
    public class Utente
    {
        [Key]
        public int UtenteId { get; set; }

        [Required(ErrorMessage = "Campo obbligatorio")]
        [StringLength(20, MinimumLength = 3, ErrorMessage = "Min 3, max 20 caratteri")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Campo obbligatorio")]
        [StringLength(15, MinimumLength = 8, ErrorMessage = "Min 8, max 15 caratteri")]
        public string Password { get; set; }
    }
}