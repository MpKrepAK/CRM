using System.Text.RegularExpressions;

namespace BLL.Models.Services.Validation;

public static class EmailValidator
{
    public static bool IsValidEmail(string email)
    {
        string pattern = @"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$";

        return Regex.IsMatch(email, pattern);
    }
}