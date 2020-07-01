using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using commands;

namespace Proga
{
	static public class Status
	{
		static public bool end = false;
		public static bool testMode;
	}
	class Proga
	{
		
		static void Main()
		{
			if (Status.testMode) { test(); }
			else
			{
				while (!Status.end)
				{
					Main_menu.printVar();
					string cmd = Console.ReadLine();
					if (Main_menu.AvailableComms.Keys.Contains(cmd))
					{
						Main_menu.AvailableComms[cmd]();
					}
					else
					{
						Console.WriteLine("Неизвестная команда");
					}
				}
			}
		}
		static void test()
		{
			Console.WriteLine("Тесты - такие тесты");
			
		}
	}
}
