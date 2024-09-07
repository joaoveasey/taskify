using System.ComponentModel.DataAnnotations;

namespace Taskify.API.Models.Validations
{
    public class FutureDateValidation : ValidationAttribute
    {
        public override bool IsValid(object? value)
        {
            if (value is DateTime date) 
                return date >= DateTime.Now;

            return false;
        }
    }
}
