using System;
using System.Threading;

namespace Tetris
{
	static class Credits
	{
		private static int xDirection = 1;
		private static int yDirection = 1;
		private static int xOld = 0;
		private static int yOld = 0;
		private static double x = 0;
		private static double y = 0;
		private static bool showCredits = true;
		private static Random random = new Random();
		public static void ShowCredits()
		{
			Console.Clear();
			Console.ForegroundColor = (ConsoleColor)random.Next(9, 15);	
			
			do
			{
				EraseName();
				PrintName();
				Animation();
				Thread.Sleep(50);
				BackToMenu(); // when escape is pressed
			} while (showCredits);

			// remove keyboard input glitch
			Console.WriteLine("m");
			Console.ResetColor();
			Console.Clear();
		}
		private static void BackToMenu()
		{
			if (Console.KeyAvailable)
			{
				ConsoleKeyInfo key = Console.ReadKey();
				if (key.Key == ConsoleKey.Escape)
				{
					showCredits = false;
				}
			}
		}
		private static void Animation()
		{
			xOld = (int)x;
			yOld = (int)y;

			x += xDirection;
			y += yDirection;

			if (x > 33)
			{
				xDirection = -1;
				Console.ForegroundColor = (ConsoleColor)random.Next(9, 15);
			}
			if (x < 1)
			{
				xDirection = 1;
				Console.ForegroundColor = (ConsoleColor)random.Next(9, 15);
			}
			if (y > 10)
			{
				yDirection = -1;
				Console.ForegroundColor = (ConsoleColor)random.Next(9, 15);
			}
			if (y < 1)
			{
				yDirection = 1;
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
		private static void EraseName()
		{
			string textErase = @"                                                              
                                                              
                                                               
                                                              
                                                         
                                                     
                                                     
                                                                            
                                                                             
                                                                               
                                                               
                                                           
                                                     
                                                     
                                                        
                                                       
                                                       
                                                       
                                                                     
                                                     
                                                     ";
			string[] name1LineSplit = textErase.Split("\n");

			for (int i = 0; i < name1LineSplit.Length; i++)
			{
				Console.SetCursorPosition(xOld, yOld + i);
				Console.WriteLine($"{name1LineSplit[i]}");
		}
		}
	}
}
