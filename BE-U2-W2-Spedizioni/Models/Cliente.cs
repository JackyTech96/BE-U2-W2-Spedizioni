using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BE_U2_W2_Spedizioni.Models
{
    public class Cliente
    {
        [ScaffoldColumn(false)]
        public int IDCliente { get; set; }

        [Required(ErrorMessage = "Il nome è obbligatorio")]
        [StringLength(50, ErrorMessage = "Il nome deve contenere al massimo 50 caratteri")]
        public string Nome { get; set; }

        [Required(ErrorMessage ="Il cognome è obbligatorio")]
        [StringLength(50, ErrorMessage = "Il cognome deve contenere al massimo 50 caratteri")]
        public string Cognome { get; set; }

        [StringLength(16, ErrorMessage = "Il Codice FIscale deve contenere esattamente 16 caratteri")]
        public string CodiceFiscale { get; set; }

        [StringLength(20, ErrorMessage = "La Partita IVA deve contenere al massimo 20 caratteri")]
        public string PartitaIVA { get; set; }

        [Required(ErrorMessage = "Inserire Privato o Azienda")]
        [CheckTipoCliente(AllowType = "Privato,Azienda", ErrorMessage = ("Scegli tra: 'Privato', 'Azienda'"))]
        public string TipoCliente { get; set; }
    }
}