using System;
using static Tetris.MainMenu;

namespace Tetris
{
	enum SelectedMenu { StartGame, HighScore, Controls, Credits, Exit }
	class Program
	{
		static void Main(string[] args)
		{
			bool play = true;
			Console.Title = "Tetris Michiel Van Gasse";
			if (OperatingSystem.IsWindows()) // prevent warning windows only
			{
				Console.SetWindowSize(80, 35);
			}
			Console.CursorVisible = false; // disable cursor flickering around

			while (play) 
			{
				SelectedMenu selectedMenu = MenuSelection();

				switch (selectedMenu)
				{
					case SelectedMenu.StartGame:
						PlayTetris.PlayGame();
						break;
					case SelectedMenu.HighScore:
						HighScores.ShowHighScores();
						break;
					case SelectedMenu.Controls:
						Controls.ShowControls();
						break;
					case SelectedMenu.Credits:
						Credits.ShowCredits();
						break;
					case SelectedMenu.Exit:
						play = false;
						break;
					default:
						break;
				}
			}
		}
	}
}