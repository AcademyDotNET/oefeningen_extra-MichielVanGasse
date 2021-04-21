using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris
{
	static class Hud
	{
		public static int Score { get; set; }
		public static void DrawHud()
		{
			string hud = @"                                                                                
                                                                                
                                                                                
                                                                                
                                                                                
                                                                                
                                                                                
                                                                                
                                                                                
                                                                                
                                                                                
                                                                                
                                                                                
                                                                                
                                                                                
                                                                                
                                                                                
                                                                                
                                                                                
                                                                                
                                                                                
                                                                                
                                                                                
                                                                                
                                                                                
                                                                                
                                                                                
                                                                                
                                                                                
                                                                                
                                                                                
                                                                                
                                                                                
                                                                                ";
			Console.BackgroundColor = ConsoleColor.DarkBlue;
            Console.ForegroundColor = ConsoleColor.Red;
            Console.SetCursorPosition(0,0);
			Console.WriteLine(hud);
            Console.SetCursorPosition(10, 5);
            Console.Write($"Your score: {Score}");
            Console.SetCursorPosition(53, 5);
            Console.Write("Next block:");
            PlayTetris.nextBlock.DrawBlock(55, 8);

			Console.ResetColor();
		}
	}
}
