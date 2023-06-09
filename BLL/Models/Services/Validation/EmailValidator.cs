using System.Text.RegularExpressions;

namespace BLL.Models.Services.Validation;

public static class EmailValidator
{
    public static bool IsValidEmail(string email)
    {
        // Паттерн для проверки формата электронной почты
        string pattern = @"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$";

        // Проверка формата с использованием регулярного выражения
        return Regex.IsMatch(email, pattern);
    }
}