using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris
{
	class TetrisBlock
	{
		public TetrisBlock(int _shapeNumber, ConsoleColor _shapeColor)
		{
			// contains the block shape and color 
			ShapeNumber = _shapeNumber;
			Shape = SelectBlock(ShapeNumber);
			ShapeColor = _shapeColor;
			ShapePosition = 0;
		}
		public int ShapePosition { get; set; }
		public int ShapeNumber { get; set; }
		public bool[,] Shape { get; set; }
		public ConsoleColor ShapeColor { get; set; }
		public static bool[,] SelectBlock(int block)
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
		public static bool[,] VerticalBlock(int block2)
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
		public static bool[,] InvertedSelectBlock(int block3)
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
		public static bool[,] InvertedVerticalBlock(int block4)
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
		public void NewShape()
		{
			Random rand = new Random();
			Shape = SelectBlock(rand.Next(0, 7));
			ShapeColor = (ConsoleColor)rand.Next(9, 15);
		}
		public void DrawBlock(int xOffset,int yOffset)
		{
			for (int i = 0; i < Shape.GetLength(0); i++)
			{
				for (int j = 0; j < Shape.GetLength(1); j++)
				{
					if (Shape[i, j])
					{

						Console.SetCursorPosition(j + xOffset, i + yOffset);
						Console.Write("█");
					}
				}
			}
		}
	}
}
