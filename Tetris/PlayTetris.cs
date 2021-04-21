using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using static System.Console;

namespace Tetris
{
	class PlayTetris
	{
		static bool isPlaying = true;
		static bool pauseMenu = false;
		static string shapeSymbol = "█";
		static int xWith = 20;
		static int yLength = 21;
		static int xShapePosition = 10;
		static int yShapePosition = 0;
		static int xOffset = 30;
		static int yOffset = 5;
		static int tick = 0;
		static int dropSpeed = 20;
		static int level = 1;
		static bool[,] playField = new bool[yLength, xWith];

		static Random rand = new Random();
		static TetrisBlock activeBlock = new TetrisBlock(rand.Next(0, 7), (ConsoleColor)rand.Next(9, 15));
		public static TetrisBlock nextBlock = new TetrisBlock(rand.Next(0, 7), (ConsoleColor)rand.Next(9, 15));

		static public void Start()
		{
			isPlaying = true;
			Clear();
			SetCursorPosition(0, 0);

			while (isPlaying)
			{
				tick++;

				KeyInputCheck();

				if (tick % (dropSpeed - level) == 0)
				{
					yShapePosition++;
					tick = 0;
				}

				if (CollisionCheck(activeBlock.Shape))
				{
					UpdateField();
					ScoreCheck();

					// game over
					if (CollisionCheck(activeBlock.Shape))
					{
						HighScores.CheckHighScore(Hud.Score);
						isPlaying = false;
					}
				}

				Hud.DrawHud();
				DrawPlayField();
				DrawActiveBlock();

				Thread.Sleep(40);
			}

			// remove keyboard input glitch
			WriteLine("m");
			ResetColor();
			Clear();
		}
		private static void ScoreCheck()
		{
			int lines = 0;

			for (int i = 0; i < playField.GetLength(0); i++)
			{
				bool fullLine = true;
				for (int j = 0; j < playField.GetLength(1); j++)
				{
					if (!playField[i, j])
					{
						fullLine = false;
						break;
					}
				}

				if (fullLine)
				{
					for (int k = i; k >= 1; k--)
					{
						for (int j = 0; j < playField.GetLength(1); j++)
						{
							playField[k, j] = playField[k - 1, j];
						}
					}
					lines++;
				}
			}
			int[] fullLineScore = { 0, 40, 100, 300, 1200 };
			Hud.Score += fullLineScore[lines];
		}
		static void DrawPlayField()
		{
			ForegroundColor = ConsoleColor.Red;
			for (int i = 0; i < playField.GetLength(0); i++)
			{
				string xRow = "";
				for (int j = 0; j < playField.GetLength(1); j++)
				{
					if (playField[i, j])
					{
						xRow += $"{shapeSymbol}";
					}
					else
					{
						xRow += ".";
					}
				}
				SetCursorPosition(xOffset, i + yOffset);
				Write(xRow);
			}
			ResetColor();
		}
		static void DrawActiveBlock()
		{
			ForegroundColor = activeBlock.ShapeColor;
			for (int i = 0; i < activeBlock.Shape.GetLength(0); i++)
			{
				for (int j = 0; j < activeBlock.Shape.GetLength(1); j++)
				{
					if (activeBlock.Shape[i, j])
					{

						SetCursorPosition(j + xOffset + xShapePosition, i + yOffset + yShapePosition);
						Write(shapeSymbol);
					}
				}
			}
			ResetColor();
		}
		private static void KeyInputCheck()
		{
			if (KeyAvailable)
			{
				ConsoleKeyInfo key = ReadKey();

				if (key.Key == ConsoleKey.Escape)
				{
					isPlaying = false;
				}

				if (key.Key == ConsoleKey.Enter && pauseMenu == false)
				{
					Pause();
					pauseMenu = true;
					ReadKey();
				}

				if (key.Key == ConsoleKey.Enter && pauseMenu == true)
				{
					Clear();
					pauseMenu = false;
				}

				if (key.Key == ConsoleKey.RightArrow)
				{
					if ((xShapePosition < xWith - (activeBlock.Shape.GetLength(1) + 1)))
					{
						xShapePosition += 2;
					}
				}

				if (key.Key == ConsoleKey.LeftArrow)
				{
					if (xShapePosition >= 1)
					{
						xShapePosition -= 2;
					}
				}

				if (key.Key == ConsoleKey.UpArrow)
				{
					RotateShape();
				}

				if (key.Key == ConsoleKey.DownArrow)
				{
					tick = 1;
					//Score += Level;
					yShapePosition++;
				}
			}
		}
		public static bool CollisionCheck(bool[,] shape)
		{
			// sides collision
			if (xShapePosition > xWith - shape.GetLength(1))
			{
				return true;
			}
			// bottom collision
			if (yShapePosition + shape.GetLength(0) == yLength)
			{
				return true;
			}
			// playfield collision
			for (int i = 0; i < shape.GetLength(0); i++)
			{
				for (int j = 0; j < shape.GetLength(1); j++)
				{
					if (shape[i, j] && playField[yShapePosition + i + 1, xShapePosition + j])
					{
						return true;
					}
				}
			}
			return false;
		}
		private static void Pause()
		{
			string pauseAsc = @"      ___     |      ___     |      ___     |      ___     |      ___     
     /\--\    |     /\--\    |     /\__\    |     /\--\    |     /\--\    
    /##\--\   |    /##\--\   |    /#/--/    |    /##\--\   |    /##\--\   
   /#/\#\--\  |   /#/\#\--\  |   /#/--/     |   /#/\#\--\  |   /#/\#\--\  
  /##\-\#\--\ |  /##\-\#\--\ |  /#/--/  ___ |  _\#\-\#\--\ |  /##\-\#\--\ 
 /#/\#\-\#\__\| /#/\#\-\#\__\| /#/__/  /\__\| /\-\#\-\#\__\| /#/\#\-\#\__\
 \/__\#\/#/--/| \/__\#\/#/--/| \#\--\ /#/--/| \#\-\#\-\/__/| \#\-\#\-\/__/
      \##/--/ |      \##/--/ |  \#\--/#/--/ |  \#\-\#\__\  |  \#\-\#\__\  
       \/__/  |      /#/--/  |   \#\/#/--/  |   \#\/#/--/  |   \#\-\/__/  
              |     /#/--/   |    \##/--/   |    \##/--/   |    \#\__\    
              |     \/__/    |     \/__/    |     \/__/    |     \/__/    
";

			// print tetris logo
			string[] lineSeperation = pauseAsc.Split("\n");

			for (int i = 0; i < lineSeperation.Length; i++)
			{
				// fansy animation + position middle of the screen
				SetCursorPosition(40 - lineSeperation[i].Length / 2, 5 + i);
				string[] letterSeperation = lineSeperation[i].Split("|");

				// seperate words for color
				for (int j = 0; j < letterSeperation.Length; j++)
				{
					ForegroundColor = MainMenu.SwitchColors(j);
					Write(letterSeperation[j]);
				}
				WriteLine();
			}

		}
		private static void UpdateField()
		{
			for (int i = 0; i < activeBlock.Shape.GetLength(0); i++)
			{
				for (int j = 0; j < activeBlock.Shape.GetLength(1); j++)
				{
					if (activeBlock.Shape[i, j])
					{
						playField[yShapePosition + i, xShapePosition + j] = true;
					}
				}
			}
			activeBlock.Shape = nextBlock.Shape;
			activeBlock.ShapeColor = nextBlock.ShapeColor;
			activeBlock.ShapePosition = nextBlock.ShapePosition;
			activeBlock.ShapeNumber = nextBlock.ShapeNumber;
			// reset 
			nextBlock.ShapeNumber = rand.Next(0, 7);
			nextBlock.ShapeColor = (ConsoleColor)rand.Next(9, 15);
			nextBlock.Shape = TetrisBlock.SelectBlock(nextBlock.ShapeNumber);
			xShapePosition = 10;
			yShapePosition = 0;
		}
		public static void RotateShape()
		{
			
			TetrisBlock collisionCheckBlock = new TetrisBlock(activeBlock.ShapeNumber, activeBlock.ShapeColor);
			if (activeBlock.ShapePosition == 0)
			{
				collisionCheckBlock.Shape = TetrisBlock.VerticalBlock(activeBlock.ShapeNumber);
				collisionCheckBlock.ShapePosition = 1;
			}
			else if (activeBlock.ShapePosition == 1)
			{
				collisionCheckBlock.Shape = TetrisBlock.InvertedSelectBlock(activeBlock.ShapeNumber);
				collisionCheckBlock.ShapePosition = 2;
			}
			else if (activeBlock.ShapePosition == 2)
			{
				collisionCheckBlock.Shape = TetrisBlock.InvertedVerticalBlock(activeBlock.ShapeNumber);
				collisionCheckBlock.ShapePosition = 3;
			}
			else if (activeBlock.ShapePosition == 3)
			{
				collisionCheckBlock.Shape = TetrisBlock.SelectBlock(activeBlock.ShapeNumber);
				collisionCheckBlock.ShapePosition = 0;
			}

			if (!PlayTetris.CollisionCheck(collisionCheckBlock.Shape))
			{
				activeBlock.Shape = collisionCheckBlock.Shape;
				activeBlock.ShapePosition = collisionCheckBlock.ShapePosition;
			}

		}
	}
}
