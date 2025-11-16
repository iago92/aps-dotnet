using System;
using System.ComponentModel.DataAnnotations;

namespace University.Domain.Validators
{
    /// <summary>
    /// Custom validation para garantir que o preço seja positivo e válido
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class ValidPriceAttribute : ValidationAttribute
    {
        public override string FormatErrorMessage(string name)
        {
            return $"O {name} deve ser um valor positivo com no máximo 2 casas decimais.";
        }

        public override bool IsValid(object value)
        {
            if (value == null)
                return true;

            if (decimal.TryParse(value.ToString(), out decimal price))
            {
                // Validar se é positivo e tem no máximo 2 casas decimais
                if (price <= 0)
                    return false;

                // Verificar se tem mais de 2 casas decimais
                var decimalPlaces = BitConverter.GetBytes(decimal.GetBits(price)[3])[2];
                return decimalPlaces <= 2;
            }

            return false;
        }
    }
}
