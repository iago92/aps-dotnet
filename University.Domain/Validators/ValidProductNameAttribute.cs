using System;
using System.ComponentModel.DataAnnotations;

namespace University.Domain.Validators
{
    /// <summary>
    /// Custom validation para garantir que o nome não contém caracteres especiais inválidos
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class ValidProductNameAttribute : ValidationAttribute
    {
        public override string FormatErrorMessage(string name)
        {
            return $"O {name} não pode conter caracteres especiais ou números no início.";
        }

        public override bool IsValid(object value)
        {
            if (value == null)
                return true;

            var name = value.ToString();

            // Não pode começar com número
            if (char.IsDigit(name[0]))
                return false;

            // Caracteres inválidos
            var invalidChars = new[] { '<', '>', '&', '%', '$', '#', '@', '!' };
            foreach (var ch in invalidChars)
            {
                if (name.Contains(ch))
                    return false;
            }

            return true;
        }
    }
}
