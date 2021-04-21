using System;
using System.Threading;


namespace Tetris
{
	static class Controls
	{
		public static void ShowControls()
		{
			string control = @"==========+OOOOOOOOOOO======$$$$$$$O888888===~~~~~~~~$$7ZOOOIIIIIII........... .
=============$OOOOOOOO======$$$$$$$O88888OI+~~~~~~~~~~~~$IIIIIIIIIIII+... . .. .
OOI============+OOOOOO======$$$$$$$O88O?IIII?I+=~~~~~~~~~~~IIIIIIIIIIIII?...... 
OOOOZ===========IIIOO8======$$$$$$$$?IIIIII?IIIII=~~~~~~~~~~~+IIIIIIIIIIIII~.. .
OOOOOOOO========+IIII7======$$$$$$$$$$IIIIIIIIIIIIII=~~~~~~~~~~~+IIIIIIIIIIIII~.
OOOOOOOZ$=======+IIIII======$$$$$$$$$$$$$IIII?IIIIIIIII=~~~~~~~~~~~?IIIIIIIIIIII
ZOOOZ$$$$========IIIII======$$$$$$$$$$$$$$$$IIIIIIII?IIIII=~~~~~~~~~~~+IIIIIIIII
II$$$$$$$===*MMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMM7*~~~?IIIIII
II$$$$$$$==*MM  Quit  MMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMM*~~~~~~IIII
II$$$$$$7==*MM   \/   MMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMM*~~~===?$7$
II$$$$=====*MM   /\   MMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMII?*=====+$7$
~?7+=======*MM  Back  MMMMMMMMMMMMMMMMMM  Pause  MMMMMMMMMMMMMMMMMMO88*=====+7$$
~~=========*MMMMMMMMMMMMMMMMMMMMMMMMMMMM     |   MMMMMMMMMMMMMMMMMM888*======777
~~=========*MMMMMMMMMMMMMMMMMMMMMMMMMMMM   <--   MMMMMMMMMMMMMMMMMM888*+=====$$7
~~======~..*MMMMMMMMMMMMMMMMMMMMMMMMMMMMMM       MMMMMMMMMMMMMMMMMM888*=========
~=====.....*MMMMMMMMMMMMMMMMMMMMMMMMMMMMMM       MMMMMMMMMMMMMMMMMM887*=========
===........*MMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMM   ^    MMMMMMMMMMM*=========
...  ......*MMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMM   |    MMMMMMMMMMM*=========
.....  ....*MMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMM rotate MMMMMMMMMMM*======+..
...  .   ..*MMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMM*=+... . .
===+... ...*MM7                           MM      MN down  MO      MMM*=+...... 
=======....*MM        drop block          MM left Z   |    M right MMM*... ...  
=========:.*MM         spacebar           MM  <-- Z   v    M  -->  MMM*...... ..
===========*MMO                           MM      MM       MD      MMM*......~~~
===========*7MMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMM7*.:~~~~~~~
OOZOO=========+777+++++??+?++???????????:,:::::,:::,,,+++====++++~~~~~~~~~~~~~~~
ZOZOOOO$=======IIIII?================:.............~~=~~~~~~~~~==~~~~~~~~~~~~~~I
OOOOOOO$$======IIII?III7=============+...........===~~~~~~~~=IIII~~~~~~~~~===$II
OOOO$$$$7======?IIIIIII$$7?=============+........=====~~=~IIIIIIIIII~~~======$$$
II$$$$$$$=======IIIIIII$$$$$$I=============+.....=======7IIIIIIIIIIII7======+$$$
II$$$$$$$==========7III$$$$OOOOO===============..=======$$$7IIIIII8887=======$$$
II$$$$$$$=============I$OOOOOOOOOO8=====================$$$$$$7888888Z=======$$$
II$$$$OOOOO?============+OOOOOOOOOOOOO+=================$$$$$$$OOO888$====~~~~7$
II$OOOOOOOOOOO$========+===+OOOOOOOOOOOOZ===============$$$$$$$8888O8Z+~~~~~~~~~";
			Console.Clear();
			Console.ForegroundColor = ConsoleColor.DarkGray;
			bool gray = true;

			for (int i = 0; i < control.Length; i++)
			{
				if(control[i] == '*' && gray == true)
				{
					Console.ForegroundColor = ConsoleColor.White;
					gray = false;
				}else if(control[i] == '*')
				{
					Console.ForegroundColor = ConsoleColor.DarkGray;
					gray = true;
				}

				Console.Write(control[i]);
				if(i%80 == 0)
				Thread.Sleep(1);
			}
			do
			{
			} while (Console.ReadKey().Key != ConsoleKey.Escape);

			// to prevent draw bug keypress
			Console.WriteLine("M");
			Console.Clear();
		}
	}
}
