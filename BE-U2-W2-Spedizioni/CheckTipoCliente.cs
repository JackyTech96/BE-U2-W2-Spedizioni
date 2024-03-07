using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BE_U2_W2_Spedizioni
{
    public class CheckTipoCliente : ValidationAttribute
    {
        public string AllowType { get; set; }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {           
            string[] myarr = AllowType.ToString().Split(',');
            if (myarr.Contains(value))
            {
                return ValidationResult.Success;
            }
            else
            {
                return new ValidationResult("Scegli un valore tra quelli ammessi (Privato, Azienda)");
            }
        }
    }
}