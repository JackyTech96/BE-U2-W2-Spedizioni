using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BE_U2_W2_Spedizioni
{
    public class CheckStatus : ValidationAttribute
    {
        public string AllowStatus { get; set; }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            string[] allowedStatuses = AllowStatus.Split(',');

            if (allowedStatuses.Contains(value?.ToString().Trim(), StringComparer.OrdinalIgnoreCase))
            {
                return ValidationResult.Success;
            }
            else
            {
                return new ValidationResult("Scegli un valore tra quelli ammessi (In transito, In Consegna, Consegnato, Non consegnato)");
            }
        }
    }
}
