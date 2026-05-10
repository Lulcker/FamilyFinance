using MudBlazor;

namespace FamilyFinance.UI;

internal static class Consts
{
    internal static readonly MudTheme MudTheme = new()
    {
        PaletteLight = new PaletteLight
        {
            Primary = "#274edb"
        }
    };

    internal static readonly List<(int Key, string Name)> Months =
    [
        (1, "Январь"),
        (2, "Февраль"),
        (3, "Март"),
        (4, "Апрель"),
        (5, "Май"),
        (6, "Июнь"),
        (7, "Июль"),
        (8, "Август"),
        (9, "Сентябрь"),
        (10, "Октябрь"),
        (11, "Ноябрь"),
        (12, "Декабрь"),
    ];
}