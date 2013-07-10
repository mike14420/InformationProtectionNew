using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace IpRequestMvc2.Validators
{
    public class BooleanRequiredToBeTrueAttribute : RequiredAttribute
    {
        public override bool IsValid(object value)
        {
            return value != null && (bool)value;
        }
    }
}