using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
//using System.Threading;
using System.Text;
using System.Media;
using static Tetris.MainMenu;

namespace Tetris
{
	enum SelectedMenu { StartGame, HighScore, Controls, Credits, Exit }
	class Program
	{
		static void Main(string[] args)
		{
			Console.Title = "Tetris Michiel Van Gasse";
			if (OperatingSystem.IsWindows()) // prevent warning windows only
			{
				Console.SetWindowSize(80, 35);
			}
			Console.CursorVisible = false; // disable cursor flickering around

			while (true) // infinity game loop
			{
				SelectedMenu selectedMenu = MenuSelection();

				switch (selectedMenu)
				{
					case SelectedMenu.StartGame:
						PlayTetris.Start();
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
						Environment.Exit(0);
						break;
					default:
						break;
				}
			}
		}
	}
}