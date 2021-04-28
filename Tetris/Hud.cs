using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris
{
	static class Hud
	{
        public static TetrisBlock nextBlock = new TetrisBlock();
        public static int Score { get; set; }
		public static void DrawHud()
		{
			Console.BackgroundColor = ConsoleColor.DarkBlue;
            Console.ForegroundColor = ConsoleColor.Red;
            Console.SetCursorPosition(0,0);
            Engine.ClearScreen();
            Console.SetCursorPosition(10, 5);
            Console.WriteLine($"Your score: {Score}");
            Console.SetCursorPosition(10, 6);
            Console.WriteLine($"Your level: {PlayTetris.level}");
            Console.SetCursorPosition(53, 5);
            Console.Write("Next block:");
            nextBlock.DrawBlock(55, 8);

			Console.ResetColor();
		}
	}
}
