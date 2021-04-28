using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris
{
	class PlayField
	{
		private static string shapeSymbol = "█";
		private static int totalLines = 0;
		private int YLength { get; set; } = 21;
		public int XWith { get; private set; } = 20;
		private bool[,] playField;
		public PlayField()
		{
			playField = new bool[YLength, XWith];
		}
		public bool CollisionCheck(TetrisBlock activeBlock)
		{
			// sides collision
			if (activeBlock.XPos > XWith - activeBlock.Shape.GetLength(1))
			{
				return true;
			}
			// bottom collision
			if (activeBlock.YPos + activeBlock.Shape.GetLength(0) == YLength)
			{
				return true;
			}
			// playfield collision
			for (int i = 0; i < activeBlock.Shape.GetLength(0); i++)
			{
				for (int j = 0; j < activeBlock.Shape.GetLength(1); j++)
				{
					if (activeBlock.Shape[i, j] && playField[activeBlock.YPos + i + 1, activeBlock.XPos + j])
					{
						return true;
					}
				}
			}
			return false;
		}
		public void UpdateField(TetrisBlock activeBlock)
		{
			for (int i = 0; i < activeBlock.Shape.GetLength(0); i++)
			{
				for (int j = 0; j < activeBlock.Shape.GetLength(1); j++)
				{
					if (activeBlock.Shape[i, j])
					{
						playField[activeBlock.YPos + i, activeBlock.XPos + j] = true;
					}
				}
			}

			activeBlock.SetBlock(Hud.nextBlock.ShapeNumber, Hud.nextBlock.ShapeColor);
			// reset 
			Hud.nextBlock.ResetBlock();
			activeBlock.XPos = 8;
			activeBlock.YPos = 0;
		}
		public void ScoreCheck()
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
					totalLines++;
					if (totalLines == 10)
					{
						totalLines = 0;
						PlayTetris.level++;
					}
				}
			}
			int[] fullLineScore = { 0, 40, 100, 300, 1200 };
			Hud.Score += fullLineScore[lines];
		}
		public void DrawPlayField(int xOffset, int yOffset)
		{
			Console.ForegroundColor = ConsoleColor.Red;
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
				Console.SetCursorPosition(xOffset, i + yOffset);
				Console.Write(xRow);
			}
			Console.ResetColor();
		}
	}
}
