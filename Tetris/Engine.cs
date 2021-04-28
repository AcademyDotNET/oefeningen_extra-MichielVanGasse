using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris
{
	static class Engine
	{
		private static string toPrint = "";	
		public static void DrawTitle(string title, int yoffset)
		{
			string[] lineSeperation = title.Split("\n");

			for (int i = 1; i < lineSeperation.Length; i++)
			{
				// fansy animation + position middle of the screen
				Console.SetCursorPosition(40 - lineSeperation[i].Length / 2, yoffset + i);
				string[] letterSeperation = lineSeperation[i].Split("|");

				// seperate words for color
				for (int j = 0; j < letterSeperation.Length; j++)
				{
					Console.ForegroundColor = Engine.SwitchGameColors(j);
					Console.Write(letterSeperation[j]);
					//Thread.Sleep(1);
				}
				Console.WriteLine();
			}
		}
		public static void ClearScreen()
		{
			if (toPrint == "")
			{
				for (int i = 0; i < Console.WindowHeight-1; i++)
				{
					for (int j = 0; j < Console.WindowWidth; j++)
					{
						toPrint += " ";
					}
					toPrint += "\n";
				}
			}
			Console.SetCursorPosition(0, 0);
			Console.Write(toPrint);
		}
		public static void GlitchFix()
		{
			Console.WriteLine("m");
			Console.ResetColor();
			Engine.ClearScreen();
		}
		public static bool EscapeNotPressed()
		{
			if (Console.KeyAvailable)
			{
				ConsoleKeyInfo key = Console.ReadKey();
				if (key.Key == ConsoleKey.Escape)
				{
					return false;
				}
			}
			return true;
		}
		public static ConsoleColor SwitchGameColors(int count)
		{
			switch (count)
			{
				case 0:
					return ConsoleColor.Red;
				case 1:
					return ConsoleColor.Green;
				case 2:
					return ConsoleColor.Blue;
				case 3:
					return ConsoleColor.Magenta;
				case 4:
					return ConsoleColor.Cyan;
				case 5:
					return ConsoleColor.Yellow;
				default:
					return ConsoleColor.Gray;
			}
		}
		public static string GetHighScoreDirectory()
		{
			return System.AppContext.BaseDirectory.Replace(@"\bin\Debug\net5.0\", "") + @"\HighScores.txt";
		}
	}
}
