using System;

namespace Donut
{
	class Program
	{
		static void Main(string[] args)
		{
			if (OperatingSystem.IsWindows()) // prevent warning windows only
			{
				Console.SetWindowSize(80, 35);
			}
			RenderDonut();
		}

		private static void RenderDonut()
		{
			Console.CursorVisible = false; // disable cursor flickering around

			double xRotation = 0, zRotation = 0; // rotates the torus over time
			double[] zBuffer = new double[1760]; // check depth object, 0 draws nothing
			char[] output = new char[1760]; // draws character depending on the depth
			int drawWith = 80;
			int drawLength = 1760;

			// infinite loop
			for (; ; )
			{
				// reset zbuffer and output
				for (int it = 0; it < drawLength; it++)
				{
					output[it] = ' '; // fill output with spaces
					zBuffer[it] = 0; // set to 0 for infinite depth
				}

				// fill the theta (the circle) of the torus
				for (double theta = 0; theta < 6.28; theta += 0.07)
				{
					// fill the phi (center) of the torus
					for (double phi = 0; phi < 6.28; phi += 0.02)
					{
						double phiSin = Math.Sin(phi);
						double phiCos = Math.Cos(phi);
						double thetaCos = Math.Cos(theta);
						double thetaSin = Math.Sin(theta);
						double xSin = Math.Sin(xRotation);
						double xCos = Math.Cos(xRotation);
						double zCos = Math.Cos(zRotation);
						double zSin = Math.Sin(zRotation);
						int distanceTorus = 5;
						int drawSize = 2;

						// the x coordinate of the circle 
						double circleX = thetaCos + drawSize;
						// to draw 1 not to draw 0 for zbuffer
						double oneOverZ = 1 / (phiSin * circleX * xSin + thetaSin * xCos + distanceTorus);
						// eye to screen x' and y'
						double eyeToScreen = phiSin * circleX * xCos - thetaSin * xSin;
						// 3D x coordinate, eye to object
						int x = (int)(40 + 30 * oneOverZ * (phiCos * circleX * zCos - eyeToScreen * zSin));
						// 3D y coordinate, eye to object
						int y = (int)(12 + 15 * oneOverZ * (phiCos * circleX * zSin + eyeToScreen * zCos));
						// location where to print on one line so we don't need a 2d array
						int positionIndex = x + drawWith * y;
						// luminance based on the surface normal
						int luminanceIndex = (int)(8 * ((thetaSin * xSin - phiSin * thetaCos * xCos) * zCos - phiSin * thetaCos * xSin - thetaSin * xCos - phiCos * thetaCos * zSin));

						// stay in array check & need to draw check / closer to viewer
						if (22 > y && y > 0 && x > 0 && drawWith > x && oneOverZ > zBuffer[positionIndex])
						{
							// closest z value
							zBuffer[positionIndex] = oneOverZ;

							if (luminanceIndex < 0)
							{
								luminanceIndex = 0;
							}
							// change signs depending on the luminance
							output[positionIndex] = ".,-~:;=!*#$@"[luminanceIndex];
						}
					}
				}

				// reset cursor pos
				Console.SetCursorPosition(0, 5);

				for (int k = 0; k < drawLength; k++)
				{
					if (k % drawWith == 0)
					{
						// new line every drawWith 
						Console.WriteLine();
					}
					else
					{
						// draw the torus
						Console.Write(output[k]);
					}

					// rotate the torus
					xRotation += 0.00004;
					zRotation += 0.00002;
				}
			}
		}
	}
}

