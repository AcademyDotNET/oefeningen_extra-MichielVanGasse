using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris
{
	static class HighScores
	{
		static string[] highScoresNames = new string[10] { "name", "name", "name", "name", "name", "name", "name", "name", "name", "name" };
		static int[] highScoresNumbers = new int[10] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
		static int teller;
		public static void GetHighScores()
		{
			if (File.Exists(@"D:\HighScores.txt"))
			{
				using (StreamReader streamReader = new StreamReader(@"D:\HighScores.txt"))
				{
					while (!streamReader.EndOfStream)
					{
						string line = streamReader.ReadLine();
						string[] values = line.Split('*');
						if(teller < 10)
						{ 
						highScoresNames[teller] = values[0];
						highScoresNumbers[teller] = Convert.ToInt32(values[1]);
						}
						teller++;
					}
				}
			}
		}
		public static void SaveHighScores()
		{
			using (StreamWriter streamWriter = new StreamWriter(@"D:\HighScores.txt"))
			{
				for (int i = 0; i < highScoresNames.Length; i++)
				{
					streamWriter.WriteLine($"{highScoresNames[i]}*{highScoresNumbers[i]}");
				}
			}
		}
		public static void ShowHighScores()
		{
			Console.BackgroundColor = ConsoleColor.DarkBlue;
			Console.Clear();
			Console.ForegroundColor = ConsoleColor.Red;
			for (int i = 0; i < highScoresNames.Length; i++)
			{
				Console.SetCursorPosition(20, 5 + i);
				Console.WriteLine($"{highScoresNames[i]}  {highScoresNumbers[i]}");
			}
			do
			{
			} while (Console.ReadKey().Key != ConsoleKey.Escape);

			// to prevent draw bug keypress
			Console.WriteLine("M");
			Console.Clear();
			Console.ResetColor();
		}
		public static void CheckHighScore(int score)
		{
			int oldScore = 0;
			int oldScoreTemp = 0;
			string oldNameScore = "";
			string oldNameScoreTemp = "";
			bool once = true;
			for (int i = 0; i < highScoresNames.Length; i++)
			{
				if (once)
				{
					oldScoreTemp = highScoresNumbers[i];
					highScoresNumbers[i] = oldScore;
					oldScore = oldScoreTemp;

					oldNameScoreTemp = highScoresNames[i];
					highScoresNames[i] = oldNameScore;
					oldNameScore = oldNameScoreTemp;
				}
				if (score > highScoresNumbers[i] && once)
				{
					oldScore = highScoresNumbers[i];
					highScoresNumbers[i] = score;
					oldNameScore = highScoresNames[i];
					Console.BackgroundColor = ConsoleColor.DarkBlue;
					Console.Clear();
					Console.SetCursorPosition(20, 2);
					Console.BackgroundColor = ConsoleColor.Red;
					Console.WriteLine("Give your name:");
					highScoresNames[i] = Console.ReadLine();
					SaveHighScores();
					once = false;
				}
			}
			Console.ResetColor();
			Console.Clear();
		}
	}
}
