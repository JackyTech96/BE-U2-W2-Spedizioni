using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BE_U2_W2_Spedizioni.Models
{
    public class Spedizione
    {
        [ScaffoldColumn(false)]
        [Display(Name = "ID Spedizione")]
        public int IDSpedizione { get; set; }

        [Required(ErrorMessage = "Il campo ID Cliente è obbligatorio")]
        [Display(Name = "ID Cliente")]
        public int IDCliente { get; set; }

        [Required(ErrorMessage = "Il campo Numero Identificativo è obbligatorio")]
        [Display(Name = "Numero Identificativo")]
        public int NumeroIdentificativo { get; set; }

        [Required(ErrorMessage = "Il campo Data Spedizione è obbligatorio")]
        [DataType(DataType.Date)]
        [Display(Name = "Data Spedizione")]
        public DateTime DataSpedizione { get; set; }

        [Display(Name = "Peso(g)")]
        [Required(ErrorMessage = "Il campo Peso è obbligatorio")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Il peso deve essere maggiore di 0")]
        [DataType(DataType.Currency)]
        [DisplayFormat(DataFormatString = "{0:n2}", ApplyFormatInEditMode = true)]
        public decimal Peso { get; set; }

        [Required(ErrorMessage = "Il campo Città è obbligatorio")]
        public string Citta { get; set; }

        [Required(ErrorMessage = "Il campo Indirizzo è obbligatorio")]
        public string Indirizzo { get; set; }

        [Required(ErrorMessage = "Il campo Nominativo Destinatario è obbligatorio")]
        [Display(Name = "Nominativo Destinatario")]
        public string NominativoDestinatario { get; set; }

        [Required(ErrorMessage = "Il campo Costo Spedizione è obbligatorio")]
        [Range(0.0, double.MaxValue, ErrorMessage = "Il costo di spedizione deve essere maggiore o uguale a 0")]
        [DataType(DataType.Currency)]
        [DisplayFormat(DataFormatString = "{0:n2}", ApplyFormatInEditMode = true)]
        [Display(Name = "Costo Spedizione")]
        public decimal CostoSpedizione { get; set; }

        [Required(ErrorMessage = "Il campo Data Consegna Prevista è obbligatorio")]
        [DataType(DataType.Date)]
        [Display(Name = "Data Consegna Prevista")]
        public DateTime DataConsegnaPrevista { get; set; }

        public Spedizione() { }

        public Spedizione(int iDSpedizione, int iDCliente, int numeroIdentificativo, DateTime dataSpedizione, decimal peso, string citta, string indirizzo, string nominativoDestinatario, decimal costoSpedizione, DateTime dataConsegnaPrevista)
        {
            IDSpedizione = iDSpedizione;
            IDCliente = iDCliente;
            NumeroIdentificativo = numeroIdentificativo;
            DataSpedizione = dataSpedizione;
            Peso = peso;
            Citta = citta;
            Indirizzo = indirizzo;
            NominativoDestinatario = nominativoDestinatario;
            CostoSpedizione = costoSpedizione;
            DataConsegnaPrevista = dataConsegnaPrevista;
        }
    }
}