using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Tetris
{
	static class HighScores
	{
		static string[] highScoresNames = new string[10] { "name", "name", "name", "name", "name", "name", "name", "name", "name", "name" };
		static int[] highScoresNumbers = new int[10] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
		static int teller;
		static List<TetrisBlock> tetrisBlocks = new List<TetrisBlock>();
		static int[] xPos = new int[6] {2,8, 16, 53,60,70};
		static int[] yPos = new int[6] { 0,10,20,15,28,5};
		static bool showControls = true;

		static HighScores()
		{
			for (int i = 0; i < 6; i++)
			{
				tetrisBlocks.Add(new TetrisBlock());
			}
		}
		public static void GetHighScores()
		{
			if (File.Exists(Engine.GetHighScoreDirectory()))
			{
				using (StreamReader streamReader = new StreamReader(Engine.GetHighScoreDirectory()))
				{
					while (!streamReader.EndOfStream)
					{
						string line = streamReader.ReadLine();
						string[] values = line.Split('*');
						if (teller < 10)
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
			using (StreamWriter streamWriter = new StreamWriter(Engine.GetHighScoreDirectory()))
			{
				for (int i = 0; i < highScoresNames.Length; i++)
				{
					streamWriter.WriteLine($"{highScoresNames[i]}*{highScoresNumbers[i]}");
				}
			}
		}
		public static void ShowHighScores()
		{
			Engine.ClearScreen();
			showControls = true;
			do
			{
				Console.BackgroundColor = ConsoleColor.DarkBlue;
				Console.ForegroundColor = ConsoleColor.Red;
				Console.SetCursorPosition(0, 0);
				Engine.ClearScreen();
				for (int i = 0; i < highScoresNames.Length; i++)
				{
					Console.SetCursorPosition(30, 10 + i);
					Console.WriteLine($"{i + 1}  {highScoresNames[i]}  {highScoresNumbers[i]} \n\n");
				}

				BlockAnimation();

				Thread.Sleep(50);
				showControls = Engine.EscapeNotPressed();
			} while (showControls);

			// to prevent draw bug keypress
			Engine.GlitchFix();
		}
		private static void BlockAnimation()
		{
			for (int i = 0; i < yPos.Length; i++)
			{
				yPos[i]++;
				if (yPos[i] > 32)
				{
					yPos[i] = 0;
					tetrisBlocks[i].ResetBlock();
				}
			}
			DrawBlock();	
		}
		private static void DrawBlock()
		{
			for (int k = 0; k < tetrisBlocks.Count; k++)
			{
				tetrisBlocks[k].DrawBlock(xPos[k], yPos[k]);
			}
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
				if (!once)
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
					Console.CursorVisible = true;
					Console.SetCursorPosition(20, 3);
					highScoresNames[i] = Console.ReadLine();
					SaveHighScores();
					Console.CursorVisible = false;
					once = false;
				}
			}
			Console.ResetColor();
			Engine.ClearScreen();
		}
	}
}
