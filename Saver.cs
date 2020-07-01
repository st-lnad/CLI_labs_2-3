using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gradebook;
using System.Configuration;
using System.IO;
using System.Runtime.Serialization;
using System.Xml;
using System.Xml.Linq;


namespace Saver
{
	static class Saver
	{
		delegate void save_meth(string path, Gradebook.Gradebook gradebook);
		delegate Gradebook.Gradebook load_meth(string path);

		static Dictionary<string, save_meth> Var_of_save = new Dictionary<string, save_meth>()
		{
			{"XMLDocument", By_XML_Doc.save_gradebook },
			{"LINQtoXML", LINQ_to_XML.save_gradebook },
			{"AutoSerialization", Auto_Seri.save_gradebook }
		};
		static Dictionary<string, load_meth> Var_of_load = new Dictionary<string, load_meth>()
		{
			{"XMLDocument", By_XML_Doc.load_gradebook },
			{"LINQtoXML", LINQ_to_XML.load_gradebook },
			{"AutoSerialization", Auto_Seri.load_gradebook }
		};
		public static void save_gradebook()
		{

			Var_of_save[ConfigurationManager.AppSettings["KindOfStorage"]](); //я не знаю, в чем ошибка
		}
		public static Gradebook.Gradebook load_gradebook() { return Var_of_load[ConfigurationManager.AppSettings["KindOfStorage"]](); }
	}
	internal static class By_XML_Doc
	{
		static internal Gradebook.Gradebook load_gradebook(string path)
		{
			Gradebook.Gradebook gradebook = new Gradebook.Gradebook();
			XmlDocument xDoc = new XmlDocument();
			xDoc.Load(path);

			foreach (XmlNode xnode in xDoc.DocumentElement)
			{
				string name = "",
					sub = "",
					mark = "";
				if (xnode.Attributes.Count > 0)
				{
					XmlNode attr = xnode.Attributes.GetNamedItem("name");
					if (attr != null)
						name = attr.Value;
				}
				// обходим все дочерние узлы элемента user
				foreach (XmlNode childnode in xnode.ChildNodes)
				{
					// если узел - company
					if (childnode.Name == "subject")
					{
						sub = childnode.InnerText;
					}
					// если узел age
					if (childnode.Name == "mark")
					{
						mark = childnode.InnerText;
					}
				}
				gradebook.Add(name, sub, mark);
			}
			return gradebook;
		}

		internal static void save_gradebook(string path, Gradebook.Gradebook gradebook)
		{
			XmlDocument xDoc = new XmlDocument();
			var root = xDoc.CreateElement("students");
			xDoc.AppendChild(root);
			foreach (var now in gradebook.GetLines())
			{
				XmlElement stud = xDoc.CreateElement("student");
				XmlAttribute nameAttr = xDoc.CreateAttribute("name");
				XmlElement subElem = xDoc.CreateElement("subject");
				XmlElement markElem = xDoc.CreateElement("mark");

				nameAttr.AppendChild(xDoc.CreateTextNode(now.student));
				subElem.AppendChild(xDoc.CreateTextNode(now.subject));
				markElem.AppendChild(xDoc.CreateTextNode(now.mark));
				stud.Attributes.Append(nameAttr);
				stud.AppendChild(subElem);
				stud.AppendChild(markElem);
				root.AppendChild(stud);
			}
			xDoc.Save(path);
		}
	}
	static internal class LINQ_to_XML
	{
		static internal Gradebook.Gradebook load_gradebook(string path)
		{
			Gradebook.Gradebook gradebook = new Gradebook.Gradebook();
			XDocument xdoc = XDocument.Load(path);
			foreach (XElement stud in xdoc.Element("students").Elements("student"))
			{
				XAttribute nameAttribute = stud.Attribute("name");
				XElement subElement = stud.Element("subject");
				XElement markElement = stud.Element("mark");

				if (nameAttribute != null && subElement != null && markElement != null)
				{
					gradebook.Add(nameAttribute.ToString(), subElement.ToString(), nameAttribute.ToString());
				}
				
			}
			return new Gradebook.Gradebook();
		}

		static internal void save_gradebook(string path, Gradebook.Gradebook gradebook)
		{
			XDocument xdoc = XDocument.Load(path);
			XElement root = xdoc.Element("students");
			foreach (var now in gradebook.GetLines())
			{
				root.Add(new XElement("student",
						   new XAttribute("name", now.student),
						   new XElement("subject", now.subject),
						   new XElement("mark", now.mark)));
			}
			xdoc.Save("pnones1.xml");
		}
	}
	static internal class Auto_Seri
	{
		static internal Gradebook.Gradebook load_gradebook(string path)
		{
			using (var stream = new FileStream(path, FileMode.Open))
			using (var reader = XmlReader.Create(stream))
			{
				var serializer = new DataContractSerializer(typeof(Gradebook.Gradebook));
				return serializer.ReadObject(reader) as Gradebook.Gradebook;
			}
		}

		static internal void save_gradebook(string path, Gradebook.Gradebook gradebook)
		{
			using (var stream = new FileStream(path, FileMode.Create))
			{
				var serializer = new DataContractSerializer(typeof(Gradebook.Gradebook));
				serializer.WriteObject(stream, gradebook);
			}
		}
	}

}
