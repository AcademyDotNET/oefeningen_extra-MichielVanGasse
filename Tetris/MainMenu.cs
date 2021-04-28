using System;
using System.Threading;
using static System.Console;

namespace Tetris
{
	static class MainMenu
	{
		public static SelectedMenu MenuSelection()
		{
			string tetris = @"
  ████████╗|███████╗|████████╗|██████╗ |██╗|███████▓
  ╚▓═██╔░═╝|██╔════╝|╚▓═██╔▒═╝|██╔══██╗|██║|██╔════▒
   ▒ ██║▒  |█████╗  | ▒ ██║░  |██████╔╝|██║|███████░
   ░ ██║░  |██╔══╝  | ░ ██║░  |██╔══██╗|██║|╚════██▒
   ▒ ██║   |███████▓| ▒ ██║   |██║  ██║|██║|███████░
   ░ ╚═╝   |╚▓═════╝| ░ ▓═╝▒  |╚═╝  ╚▓╝|╚░╝|╚═══▓══▒
     ░    ░  | ▒      |   ▒  ░  |      ▒ |   | ▒  ▒  ░  
           |       ░      | ░ ░     | ░    ░ |   |    ░         
           |              |   ░     |        |   |    ░        
                                               
";
			// just add a string to add items to the main menu
			string[] selectMenu = { "Start Game", "High Scores", "Controls", "Credits", "Exit" };
			int selection = 0;

			HighScores.GetHighScores();
			ResetColor();
			Engine.ClearScreen();
			Engine.DrawTitle(tetris,5);

			ConsoleKey keyDown; // to store the pressed key

			do
			{
				DrawMenu(selection, selectMenu);

				ConsoleKeyInfo keyInfo = ReadKey(true); //get the pressed key
				keyDown = keyInfo.Key; //safe the pressed key

				if (keyDown == ConsoleKey.UpArrow || keyDown == ConsoleKey.Z)
				{
					selection--;
					if (selection == -1)
					{
						selection = selectMenu.Length - 1;
					}
				}
				if (keyDown == ConsoleKey.DownArrow || keyDown == ConsoleKey.S)
				{
					selection++;
					if (selection == selectMenu.Length)
					{
						selection = 0;
					}
				}
			} while (keyDown != ConsoleKey.Enter);

			return (SelectedMenu)selection;
		}
		private static void DrawMenu(int selection, string[] selectMenu)
		{
			// selection to highlight the selected menu
			// cycles trough the selected menu
			Random random = new Random();
			for (int i = 0; i < selectMenu.Length; i++)
			{
				// +6 for the >>  << extra to find the middle of the option to put in the middle of the screen
				SetCursorPosition(40 - (selectMenu[i].Length + 6) / 2, 18 + i);

				if (i == selection)
				{
					ForegroundColor = ConsoleColor.Black;
					BackgroundColor = (ConsoleColor)random.Next(9, 15);
					Write($">> {selectMenu[i]} <<");
				}
				else
				{
					ForegroundColor = ConsoleColor.White;
					BackgroundColor = ConsoleColor.Black;
					Write($"   {selectMenu[i]}   ");
				}
			}
		}
	}
}
