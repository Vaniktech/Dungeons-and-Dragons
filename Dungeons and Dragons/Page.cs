using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;
using System.IO;

namespace Dungeons_and_Dragons
{
	class Page
	{
		public void Delete_File()
		{
			string path = "C:\\json\\DataTent.json";
			FileInfo fileInf = new FileInfo(path);
			if (fileInf.Exists)
			{
				fileInf.Delete();
			}
		}
		public void JsWrite(object arg)
		{
			JsonSerializerOptions options = new JsonSerializerOptions
			{
				Encoder = JavaScriptEncoder.Create(UnicodeRanges.All),
				WriteIndented = true
			};
			var json = JsonSerializer.Serialize(arg, options);

			string path = "C:\\json\\DataTent.json";
			StreamWriter sw = new StreamWriter(File.Open(path, FileMode.Append));
			sw.WriteLine(json);
			sw.Close();
		}
		static void Main(string[] args)
		{
			Page page = new Page();
			page.Delete_File();
			Tentaculus tentaculus = new Tentaculus();
			tentaculus.Get_Info();
			tentaculus.Quit();

		}
	}
}