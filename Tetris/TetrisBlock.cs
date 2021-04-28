using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris
{
	class TetrisBlock
	{
		static Random rand = new Random();
		private int ShapePosition { get; set; }
		public int ShapeNumber { get; private set; }
		public bool[,] Shape { get; private set; }
		public ConsoleColor ShapeColor { get; private set; }
		public int XPos { get; set; } = 8;
		public int YPos { get; set; } = 0;
		public TetrisBlock()
		{
			ResetBlock();
		}
		public TetrisBlock(int shapeNumber, ConsoleColor shapeColor)
		{
			// contains the block shape and color 
			SetBlock(shapeNumber, shapeColor);
		}
		private bool[,] SelectBlock(int block)
		{
			switch (block)
			{
				case 0: // ████████
					return new bool[,] { { true, true, true, true, true, true, true, true } };
				case 1: //     ██
						// ██████
					return new bool[,] { { false, false, false, false, true, true }, { true, true, true, true, true, true } };
				case 2: // ██    
						// ██████
					return new bool[,] { { true, true, false, false, false, false }, { true, true, true, true, true, true } };
				case 3: // ████
						// ████
					return new bool[,] { { true, true, true, true }, { true, true, true, true } };
				case 4: //   ████
						// ████
					return new bool[,] { { false, false, true, true, true, true }, { true, true, true, true, false, false } };
				case 5: // ████
						//   ████
					return new bool[,] { { true, true, true, true, false, false }, { false, false, true, true, true, true } };
				case 6: //   ██
						// ██████
					return new bool[,] { { false, false, true, true, false, false }, { true, true, true, true, true, true } };
				default:// ████████
					return new bool[,] { { true, true, true, true, true, true, true, true } };
			}
		}
		private bool[,] VerticalBlock(int block2)
		{
			switch (block2)
			{
				case 0: // ██
						// ██
						// ██
						// ██
					return new bool[,] { { true, true }, { true, true }, { true, true }, { true, true }};
				case 1: // ██  
						// ██
						// ████
					return new bool[,] { { true, true, false, false }, { true, true, false, false }, { true, true, true, true } };
				case 2: // ████    
						// ██
						// ██
					return new bool[,] { { true, true, true, true }, { true, true, false, false }, { true, true, false, false } };
				case 3: // ████ same
						// ████
					return new bool[,] { { true, true, true, true }, { true, true, true, true } };
				case 4: // ██
						// ████
						//   ██
					return new bool[,] { { true, true, false, false }, { true, true, true, true }, { false, false, true, true } };
				case 5: //   ██
						// ████
						// ██
					return new bool[,] { { false, false, true, true }, { true, true, true, true }, { true, true, false, false } };
				case 6: // ██
						// ████
						// ██
					return new bool[,] { { true, true, false, false }, { true, true, true, true }, { true, true, false, false } };
				default:// ████████
					return new bool[,] { { true, true }, { true, true }, { true, true }, { true, true } };
			}
		}
		private bool[,] InvertedSelectBlock(int block3)
		{
			switch (block3)
			{
				case 0: // ████████
					return new bool[,] { { true, true, true, true, true, true, true, true } };
				case 1: //     ██
						// ██████
					return new bool[,] { { true, true, true, true, true, true },{ true, true,false, false, false, false } };
				case 2: // ██    
						// ██████
					return new bool[,] { { true, true, true, true, true, true },{ false, false, false, false, true, true } };
				case 3: // ████
						// ████
					return new bool[,] { { true, true, true, true },{ true, true, true, true } };
				case 4: //   ████
						// ████
					return new bool[,] { { false, false, true, true, true, true }, { true, true, true, true, false, false } };
				case 5: // ████
						//   ████
					return new bool[,] { { true, true, true, true, false, false }, { false, false, true, true, true, true } };
				case 6: //   ██
						// ██████
					return new bool[,] { { true, true, true, true, true, true },{ false, false, true, true, false, false } };
				default:// ████████
					return new bool[,] { { true, true, true, true, true, true, true, true } };
			}
		}
		private bool[,] InvertedVerticalBlock(int block4)
		{
			switch (block4)
			{
				case 0: // ██
						// ██
						// ██
						// ██
					return new bool[,] { { true, true }, { true, true }, { true, true }, { true, true } };
				case 1: // ██  
						// ██
						// ████
					return new bool[,] { { true, true, true, true }, { false, false, true, true  }, { false, false, true, true } };
				case 2: // ████    
						// ██
						// ██
					return new bool[,] { { false, false, true, true }, { false, false,true, true }, {  true, true, true, true } };
				case 3: // ████ same
						// ████
					return new bool[,] { { true, true, true, true }, { true, true, true, true } };
				case 4: // ██
						// ████
						//   ██
					return new bool[,] { { true, true, false, false }, { true, true, true, true }, { false, false, true, true } };
				case 5: //   ██
						// ████
						// ██
					return new bool[,] { { false, false, true, true }, { true, true, true, true }, { true, true, false, false } };
				case 6: // ██
						// ████
						// ██
					return new bool[,] { { false, false,true, true }, { true, true, true, true }, { false, false,true, true } };
				default:// ████████
					return new bool[,] { { true, true }, { true, true }, { true, true }, { true, true } };
			}
		}
		public void ResetBlock()
		{
			ShapeNumber = rand.Next(0, 7);
			Shape = SelectBlock(ShapeNumber);
			ShapeColor = (ConsoleColor)rand.Next(9, 15);
			ShapePosition = 0;
		}
		public void DrawBlock(int xPos, int yPos)
		{
			Console.ForegroundColor = ShapeColor;
			for (int i = 0; i < Shape.GetLength(0); i++)
			{
				for (int j = 0; j < Shape.GetLength(1); j++)
				{
					if (Shape[i, j])
					{

						Console.SetCursorPosition(j + xPos, i + yPos);
						Console.Write("█");
					}
				}
			}
			Console.ResetColor();
		}
		public void RotateShape()
		{
			TetrisBlock collisionCheckBlock = new TetrisBlock(ShapeNumber, ShapeColor);
			if (ShapePosition == 0)
			{
				collisionCheckBlock.Shape = VerticalBlock(ShapeNumber);
				collisionCheckBlock.ShapePosition = 1;
			}
			else if (ShapePosition == 1)
			{
				collisionCheckBlock.Shape = InvertedSelectBlock(ShapeNumber);
				collisionCheckBlock.ShapePosition = 2;
			}
			else if (ShapePosition == 2)
			{
				collisionCheckBlock.Shape = InvertedVerticalBlock(ShapeNumber);
				collisionCheckBlock.ShapePosition = 3;
			}
			else if (ShapePosition == 3)
			{
				collisionCheckBlock.Shape = SelectBlock(ShapeNumber);
				collisionCheckBlock.ShapePosition = 0;
			}
			if(PlayTetris.playField != null)
			{ 
				if (!PlayTetris.playField.CollisionCheck(collisionCheckBlock))
				{
					Shape = collisionCheckBlock.Shape;
					ShapePosition = collisionCheckBlock.ShapePosition;
				}
			}
		}
		public void SetBlock(int shapeNumber, ConsoleColor shapeColor)
		{
			ShapeNumber = shapeNumber;
			Shape = SelectBlock(ShapeNumber);
			ShapeColor = shapeColor;
			ShapePosition = 0;
		}
	}
}
