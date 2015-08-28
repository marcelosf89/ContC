using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ContC.presentation.mvc.CustomValidators
{
    public class CouldNotBeZeroStringAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value == null)
            {
                return true;
            }
            int getal;
            if (!int.TryParse(value.ToString(), out getal)) return false;
            return getal > 0;
        }
    }

}