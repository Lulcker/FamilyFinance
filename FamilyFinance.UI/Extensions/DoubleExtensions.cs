using System.Globalization;

namespace FamilyFinance.UI.Extensions;

internal static class DoubleExtensions
{
    internal static string FormatRubles(this double value)
    {
        var culture = new CultureInfo("ru-RU")
        {
            NumberFormat =
            {
                NumberGroupSeparator = " "
            }
        };

        return $"{value.ToString("#,0.##", culture)} ₽";
    }
}