using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using static BE_U2_W2_Spedizioni.CheckStatus;

namespace BE_U2_W2_Spedizioni.Models
{
    public class AggiornamentoStatoSpedizione
    {
        public int IDAggiornamento { get; set; }

        [Required(ErrorMessage = "Il campo IDSpedizione è obbligatorio")]
        public int IDSpedizione { get; set; }

        [CheckStatus(AllowStatus = "In Transito,In Consegna,Consegnato,Non Consegnato", ErrorMessage = "Scegli un valore tra quelli ammessi (In transito,In Consegna,Consegnato,Non consegnato) ")]
        public string Stato { get; set; }

        [Required(ErrorMessage = "Il campo Luogo è obbligatorio")]
        public string Luogo { get; set; }

        public string DescrizioneEventuale { get; set; }

        [Required(ErrorMessage = "Il campo DataAggiornamento è obbligatorio")]
        [DataType(DataType.DateTime)]
        public DateTime DataAggiornamento { get; set; }
    }
}