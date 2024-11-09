
namespace AsciiFractals;

internal struct ConsoleColour()
{
    public static int[] Colours = [0, 30, 31, 32, 33, 34, 35, 36, 37];

    public static string GetColour(double u)
    {
        if (u < 0.2) return "\x1b[30m";
        double offset = 1;
        double r = Math.Sin((u + offset) * 5) / 2 + 0.5;
        double g = Math.Sin((u + offset) * 5 + 1) / 2 + 0.5;
        double b = Math.Sin((u + offset) * 5 + 2) / 2 + 0.5;

        return $"\x1b[38;2;{(int)(r * 255)};{(int)(g * 255)};{(int)(b * 255)}m";
    }

    public static string Reset = "\x1b[0m";
    public static string Black = "\x1b[30m";
    public static string Red = "\x1b[31m";
    public static string Green = "\x1b[32m";
    public static string Yellow = "\x1b[33m";
    public static string Blue = "\x1b[34m";
    public static string Magenta = "\x1b[35m";
    public static string Cyan = "\x1b[36m";
    public static string White = "\x1b[37m";
}
