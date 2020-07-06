using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows.Controls;

namespace ht1.ValidationRules
{
	public class EmailAddress : ValidationRule
	{
		public override ValidationResult Validate(object value, CultureInfo cultureInfo)
		{
			if (!(value is string address)) 
				return new ValidationResult(false, "Неккоректные данные");
			if (!Regex.IsMatch(address, @"[a-zA-A]\w*@\w+\.\w+"))
				return new ValidationResult(false, "Введен некорректный адрес");
			return ValidationResult.ValidResult;
		}
	}
}
