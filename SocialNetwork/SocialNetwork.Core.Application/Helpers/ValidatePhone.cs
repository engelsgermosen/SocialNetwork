using System.Text.RegularExpressions;

namespace SocialNetwork.Core.Application.Helpers
{
    public class ValidatePhone
    {
        public static bool NumeroPermitido(string numero)
        {
            string pattern = @"^(?:\+?1[\s-]?)?(?:\((809|829|849)\)|(809|829|849))(?:(?:-\d{3}-\d{4})|\d{7})$";
            return Regex.IsMatch(numero, pattern);
        }
    }
}
