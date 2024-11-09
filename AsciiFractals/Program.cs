
using System.Diagnostics;
using System.Text;

namespace AsciiFractals;

internal class Program
{
    private double xMin = -2.5 * 4;
    private double xMax = 1.0 * 4;
    private double yMin = -1.0 * 4;
    private double yMax = 1.0 * 4;
    private readonly int _maxIters = 25000;
	private readonly double zoomCenterX = -1.7499386236239305;
    private readonly double zoomCenterY = -0.0000064245648392358135;

    private readonly string _chars = " `.-':_,^=;><+!rc*/z?sLTv)J7(|Fi{C}fI31tlu[neoZ5Yxjya]2ESwqkP6h9d4VpOGbUAKXHm8RD#$Bg0MNWQ%&@";
    // private readonly string _chars = " ░▒▓";

	static void Main()
	{
        Console.WriteLine("Press any key to start!");
		Console.ReadKey(true);
		Console.CursorVisible = false;
		string[,] charList = new string[Console.WindowWidth * 2, Console.WindowHeight];
        StringBuilder sb = new();
		Program p = new();
		Stopwatch sw = Stopwatch.StartNew();

		// Console.Write("\x1b[1m"); // Make everything bold (optional)

		while (true)
		{
			sw.Restart();

			int width = Console.WindowWidth;
			int height = Console.WindowHeight;

			Parallel.For(0, width * height, i =>
			{
				int x = i % width;
				int y = i / width;
				
                (string, char) c = p.GetCharFromCoord(x, y, width, height);
                charList[x * 2, y] = c.Item1;
                charList[x * 2 + 1, y] = c.Item2.ToString();
			});

			sb.Clear();

			for (int j = 0; j < charList.GetLength(1); j++)
			{
				for (int i = 0; i < charList.GetLength(0); i++)
				{
					sb.Append(charList[i, j]);
				}

				if (j != height - 1) sb.AppendLine();
			}

			Console.SetCursorPosition(0, 0);
			Console.Write(sb);
			while (sw.ElapsedMilliseconds <= (1000f / 10f)) { }

			double newWid = (p.xMax - p.xMin) / 1.02;
			double newHei = (p.yMax - p.yMin) / 1.02;

            p.xMin = p.zoomCenterX - newWid / 2;
            p.xMax = p.zoomCenterX + newWid / 2;
            p.yMin = p.zoomCenterY - newHei / 2;
            p.yMax = p.zoomCenterY + newHei / 2;
        }
	}

    public (string, char) GetCharFromCoord(int x, int y, int width, int height)
    {
        double u = (double)x / width;
        double v = (double)y / height;
        double cx = xMin + u * (xMax - xMin);
        double cy = yMin + v * (yMax - yMin);
        double zx = 0;
        double zy = 0;

        for (int i = 0; i < _maxIters; i++)
        {
            (zx, zy) = (zx * zx - zy * zy + cx, 2 * zx * zy + cy);

            if (zx * zx + zy * zy > 1024)
            {
                double ratio = (double)i / _maxIters;
                int charIndex = (int)(ratio * (_chars.Length - 1));
                return (ConsoleColour.GetColour(ratio), _chars[charIndex]);
            }
        }

        return (ConsoleColour.GetColour(1), _chars[^1]);
    }
}