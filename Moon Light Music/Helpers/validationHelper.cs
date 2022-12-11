using System.Net.Mail;
using System.Text.RegularExpressions;

namespace Moon_Light_Music.Helpers;
public static class validationHelper
{
    public static bool IsValidEmail(string emailaddress)
    {
        try
        {
            var m = new MailAddress(emailaddress);

            return true;
        }
        catch (FormatException)
        {
            return false;
        }
    }


    public static bool IsValidPassword(string password)
    {
        var hasNumber = new Regex(@"[0-9]+");
        var hasUpperChar = new Regex(@"[A-Z]+");
        var hasMinimum8Chars = new Regex(@".{8,}");
        if (hasNumber.IsMatch(password) && hasUpperChar.IsMatch(password) && hasMinimum8Chars.IsMatch(password))
        {
            return true;
        }
        else
        {
            return false;

        }
    }


}
