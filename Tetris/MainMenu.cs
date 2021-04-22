using System;
using System.Threading;
using static System.Console;

namespace Tetris
{
	static class MainMenu
	{
		public static string MenuDecoration()
		{
			return @"
  ████████╗,███████╗,████████╗,██████╗ ,██╗,███████▓
  ╚▓═██╔░═╝,██╔════╝,╚▓═██╔▒═╝,██╔══██╗,██║,██╔════▒
   ▒ ██║▒  ,█████╗  , ▒ ██║░  ,██████╔╝,██║,███████░
   ░ ██║░  ,██╔══╝  , ░ ██║░  ,██╔══██╗,██║,╚════██▒
   ▒ ██║   ,███████▓, ▒ ██║   ,██║  ██║,██║,███████░
   ░ ╚═╝   ,╚▓═════╝, ░ ▓═╝▒  ,╚═╝  ╚▓╝,╚░╝,╚═══▓══▒
     ░    ░  , ▒      ,   ▒  ░  ,      ▒ ,   , ▒  ▒  ░  
           ,       ░      , ░ ░     , ░    ░ ,   ,    ░         
           ,              ,   ░     ,        ,   ,    ░        
                                               
";
		}
		public static SelectedMenu MenuSelection()
		{
			// just add a string to add items to the main menu
			string[] selectMenu = { "Start Game", "High Scores","Controls", "Credits", "Exit" };
			int selection = 0;

			HighScores.GetHighScores();
			ResetColor();
			Clear();
			DrawMenuDecoration();

			ConsoleKey keyDown; // to store the pressed key

			do
			{
				DrawMenu(selection, selectMenu);

				ConsoleKeyInfo keyInfo = ReadKey(true); //get the pressed key
				keyDown = keyInfo.Key; //safe the pressed key

				if (keyDown == ConsoleKey.UpArrow || keyDown == ConsoleKey.Z)
				{
					selection--;
					if(selection == -1)
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
		private static void DrawMenuDecoration()
		{
			// print tetris logo
			string[] lineSeperation = MenuDecoration().Split("\n");
			
			for (int i = 1; i < lineSeperation.Length-1; i++)
			{
				// fansy animation + position middle of the screen
				SetCursorPosition(40 - lineSeperation[i].Length / 2, 5 + i);
				string[] letterSeperation = lineSeperation[i].Split(",");

				// seperate words for color
				for (int j = 0; j < letterSeperation.Length; j++)
				{
					ForegroundColor = SwitchColors(j);
					Write(letterSeperation[j]);
					//Thread.Sleep(1);
				}
				WriteLine();
			}
		}
		public static ConsoleColor SwitchColors(int count)
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
