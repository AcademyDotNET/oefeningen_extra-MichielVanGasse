using System;
using System.Threading;

namespace Tetris
{
	static class Credits
	{
		private static bool showCredits = true;
		private static int xDirection = 1;
		private static int yDirection = 1;
		private static int xOld = 0;
		private static int yOld = 0;
		private static double x = 0;
		private static double y = 0;
		private static Random random = new Random();
		public static void ShowCredits()
		{
			Engine.ClearScreen();
			Console.ForegroundColor = (ConsoleColor)random.Next(9, 15);
			showCredits = true;

			do
			{
				Engine.ClearScreen();
				PrintName();
				Animation();
				Thread.Sleep(50);
				showCredits = Engine.EscapeNotPressed();

			} while (showCredits);

			Engine.GlitchFix();
		}
		private static void Animation()
		{
			xOld = (int)x;
			yOld = (int)y;

			x += xDirection;
			y += yDirection;

			if (x > 33 || x < 1)
			{
				xDirection *= -1;
				Console.ForegroundColor = (ConsoleColor)random.Next(9, 15);
			}
			if (y > 14 || y < 1)
			{
				yDirection *= -1;
				Console.ForegroundColor = (ConsoleColor)random.Next(9, 15);
			}
		}
		private static void PrintName()
		{
			string text = @"███    ███ ██  ██████ ██   ██ ██ ███████ ██     
████  ████ ██ ██      ██   ██ ██ ██      ██     
██ ████ ██ ██ ██      ███████ ██ █████   ██     
██  ██  ██ ██ ██      ██   ██ ██ ██      ██     
██      ██ ██  ██████ ██   ██ ██ ███████ ███████
                                                
                                                
██    ██  █████  ███    ██                      
██    ██ ██   ██ ████   ██                     
██    ██ ███████ ██ ██  ██                     
 ██  ██  ██   ██ ██  ██ ██                 
  ████   ██   ██ ██   ████                 
                                            
                                           
 ██████   █████  ███████ ███████ ███████  
██       ██   ██ ██      ██      ██       
██   ███ ███████ ███████ ███████ █████    
██    ██ ██   ██      ██      ██ ██     
 ██████  ██   ██ ███████ ███████ ███████";
			string[] name2LineSplit = text.Split("\n");

			for (int i = 0; i < name2LineSplit.Length; i++)
			{
				Console.SetCursorPosition((int)x, (int)y + i);
				Console.WriteLine($"{name2LineSplit[i]}");
			}
		}
	}
}
