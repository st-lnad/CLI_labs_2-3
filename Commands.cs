using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace commands
{
	public delegate void Command();
	public static class Main_menu
	{
		public static Dictionary<string, Command> AvailableComms = new Dictionary<string, Command>() {
			{"Получить дневник по студенту", get_by_stud },
			{"Новая запись в журнал", add_to_gradebook },
			{"Удаление записи из журнала", delete_from_gradebook },
			{"Получить дневник по предмету", get_by_sub },
			{"Сохранить журнал", save_gradebook },
			{"Загрузить журнал", load_gradebook },
			{"Выход", arrivederci_babe },
		};
		public static void get_by_stud() { }
		public static void add_to_gradebook() { }
		public static void delete_from_gradebook() { }
		public static void get_by_sub() { }
		public static void save_gradebook() { }
		public static void load_gradebook() { }
		public static void arrivederci_babe() {
			Proga.Status.end = true;
		}
		public static void printVar()
		{
			Console.WriteLine("Главное меню.\nДоступные команды:");
			foreach (var now in AvailableComms)
			{
				Console.WriteLine(now.Key);
			}
		}

	}
	class Cycle_menu {
		Dictionary<string, Command> CycleMenu = new Dictionary<string, Command>();
		void action() { }
		void go_back() { }
		Cycle_menu(string action_name)
		{
			CycleMenu.Add(action_name, action);
			CycleMenu.Add("Вернуться в предыдущее меню", go_back);
		}
	}
	class In_action { }
}
