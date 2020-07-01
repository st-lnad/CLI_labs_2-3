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
	}
	class Proga
	{
		
		static void Main()
		{
			
			{
				Gradebook.Gradebook gradebook = new Gradebook.Gradebook();
				while (!Status.end)
				{
					Main_menu.printVar();
					string cmd = Console.ReadLine();
					if (Main_menu.AvailableComms.Keys.Contains(cmd))
					{
						Main_menu.AvailableComms[cmd](gradebook);
					}
					else
					{
						Console.WriteLine("Неизвестная команда");
					}
				}
			}
		}
	}
}
