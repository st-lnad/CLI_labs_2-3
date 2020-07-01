using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gradebook
{
	class Gradebook
	{
		internal class Line
		{
			internal string student { get; set; }
			internal string subject { get; set; }
			internal string mark { get; set; }
		}

		private List<Line> lines = new List<Line>();
		Dictionary<string, List<Dictionary<string, string>>> subStudMark = new Dictionary<string, List<Dictionary<string, string>>>(); 
		Dictionary<string, List<Dictionary<string, string>>> studSubMark = new Dictionary<string, List<Dictionary<string, string>>>();


		internal void makeDicts() {
			foreach (var now_line in lines)
			{
				addNewLineDicts(now_line);
			}
		}
		internal List<Line> GetLines()
		{
			return lines;
		}
		internal void addNewLineDicts(Line now_line) {
			if (!studSubMark.Keys.Contains(now_line.student))
			{
				studSubMark.Add(now_line.student, new List<Dictionary<string, string>>() { new Dictionary<string, string>() { { now_line.subject, now_line.mark } } });
			}
			else
			{
				studSubMark[now_line.student].Add(new Dictionary<string, string>() { { now_line.subject, now_line.mark } });
			}
			if (!subStudMark.Keys.Contains(now_line.subject))
			{
				subStudMark.Add(now_line.subject, new List<Dictionary<string, string>>() { new Dictionary<string, string>() { { now_line.student, now_line.mark } } });
			}
			else
			{
				subStudMark[now_line.subject].Add(new Dictionary<string, string>() { { now_line.student, now_line.mark } });
			}
		}
		internal void Add(string name, string sub, string mark)
		{
			if (studSubMark.Keys.Contains(name))
			{
				bool error = false;
				foreach (var now in studSubMark[name].Select(x => x.Keys.Contains(sub))) { //Я долго пытался с этой лямбдой, но она не пригодилась, пихнул сюда
					error = error || now;
				}
				if (error)
				{
					Console.WriteLine("Error. Оценка уже стоит"); //TODO исключение???
					return;
				}
			}
			else
			{
				lines.Add(new Line() { student = name, subject = sub, mark = mark });
				addNewLineDicts(lines[lines.Count - 1]);
				Console.WriteLine("Success.");
			}
		}
		internal List<Line> get_by_student(string name)
		{
			if (!studSubMark.Keys.Contains(name))
			{
				Console.WriteLine("Error: there isn't that person");
				return new List<Line>();
			}
			List<Line> res = new List<Line>();
			foreach (var subs_marks in studSubMark[name])
			{
				foreach (var now in subs_marks)

					res.Add(new Line() { student = name, subject = now.Key, mark = now.Value });
			}
			return res;
		}
		internal List<Line> get_by_sub(string sub)
		{
			if (!subStudMark.Keys.Contains(sub))
			{
				Console.WriteLine("Error: there isn't that person");
				return new List<Line>();
			}
			List<Line> res = new List<Line>();
			foreach (var stud_marks in subStudMark[sub])
			{
				foreach (var now in stud_marks)

					res.Add(new Line() { student = now.Key, subject = sub, mark = now.Value });
			}
			return res;
		}
		internal void Delete(string name, string sub)
		{
			/*
			for (int i = 0; i<lines.Count; i++)
			{
				if (lines[i].student == name && lines[i].subject == sub)
				{
					lines.Remove(lines[i]);
				}
			}*/
		}
		
		
	}
}
