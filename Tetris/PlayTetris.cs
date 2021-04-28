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
		private static bool isPlaying = true;
		private static bool pauseMenu = false;
		private static int xOffset = 30;
		private static int yOffset = 5;
		private static int tick = 0;

		public static PlayField playField;
		public static int level = 1;

		static public void PlayGame()
		{
			Hud.Score = 0;
			level = 1;
			int dropSpeed = 20;
			isPlaying = true;

			TetrisBlock activeBlock = new TetrisBlock();
			playField = new PlayField();
			
			Engine.ClearScreen();
			SetCursorPosition(0, 0);

			while (isPlaying)
			{
				tick++;

				KeyInputCheck(activeBlock, playField);

				if (tick % (dropSpeed - level) == 0)
				{
					activeBlock.YPos++;
					tick = 0;
				}

				if (playField.CollisionCheck(activeBlock))
				{
					playField.UpdateField(activeBlock);
					playField.ScoreCheck();

					// game over
					if (playField.CollisionCheck(activeBlock) && isPlaying)
					{
						HighScores.CheckHighScore(Hud.Score);
						isPlaying = false;
					}
				}

				Hud.DrawHud();
				playField.DrawPlayField(xOffset, yOffset);
				activeBlock.DrawBlock(xOffset + activeBlock.XPos, yOffset + activeBlock.YPos);

				Thread.Sleep(40);
			}

			// remove keyboard input glitch
			Engine.GlitchFix();
		}
		private static void KeyInputCheck(TetrisBlock activeBlock, PlayField playField)
		{
			string pauseText = @"
      ___     |      ___     |      ___     |      ___     |      ___     
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
			if (KeyAvailable)
			{
				ConsoleKeyInfo key = ReadKey();

				if (key.Key == ConsoleKey.Escape)
				{
					isPlaying = false;
				}

				if (key.Key == ConsoleKey.Enter && pauseMenu == false)
				{
					Engine.ClearScreen();
					Engine.DrawTitle(pauseText, 10);
					pauseMenu = true;
					ReadKey();
				}

				if (key.Key == ConsoleKey.Enter && pauseMenu == true)
				{
					Engine.ClearScreen();
					pauseMenu = false;
				}

				if (key.Key == ConsoleKey.RightArrow)
				{
					if ((activeBlock.XPos < playField.XWith - (activeBlock.Shape.GetLength(1) + 1)))
					{
						activeBlock.XPos += 2;
						if (playField.CollisionCheck(activeBlock))
						{
							activeBlock.XPos -= 2;
						}
					}
				}

				if (key.Key == ConsoleKey.LeftArrow)
				{
					if (activeBlock.XPos >= 1)
					{
						activeBlock.XPos -= 2;
						if (playField.CollisionCheck(activeBlock))
						{
							activeBlock.XPos += 2;
						}
					}
				}

				if (key.Key == ConsoleKey.UpArrow)
				{
					activeBlock.RotateShape();
				}

				if (key.Key == ConsoleKey.DownArrow)
				{
					tick = 1;
					activeBlock.YPos++;
				}

				if (key.Key == ConsoleKey.Spacebar)
				{
					while (!playField.CollisionCheck(activeBlock))
					{
						activeBlock.YPos++;
						Hud.Score++;
					}

					playField.UpdateField(activeBlock);
					playField.ScoreCheck();

					// game over
					if (playField.CollisionCheck(activeBlock))
					{
						HighScores.CheckHighScore(Hud.Score);
						isPlaying = false;
					}
				}
			}
		}
		
		
	}
}
