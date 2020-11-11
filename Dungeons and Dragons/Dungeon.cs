using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Text.Json;
using System.Text.Unicode;
using System.IO;

namespace Dungeons_and_Dragons
{
    class Dungeon:Page
    {
		IWebDriver driver = new ChromeDriver();
		/// <summary>
		/// Получение списка элементов из карточек
		/// </summary>
		/// <returns>IList cards</returns>
		IList<IWebElement> GetCards()
		{
			driver.Url = "https://dungeon.su/articles/inventory/";
			IList<IWebElement> cards = driver.FindElements(By.ClassName("paper-1"));
			return cards;
		}
		/// <summary>
		/// Нажатие на кнопку "Читать далее"
		/// </summary>
		/// <param name="cards"></param>
		/// <param name="number"></param>
		void Press_button(IList<IWebElement> cards, int number)
		{
			cards[number].FindElement(By.ClassName("read-more")).Click();
		}
		/// <summary>
		/// Получение таблицы "Безделушки"
		/// </summary>
		/// <returns>Dictionary dict</returns>
		public Dictionary<string, string> bezdelushki()
		{
			Press_button(GetCards(), 0);
			IWebElement center_page1 = driver.FindElement(By.ClassName("center"));
			IList<IWebElement> tr = center_page1.FindElements(By.TagName("tr"));
			Dictionary<string, string> dict = new Dictionary<string, string>();
			foreach (var item in tr)
			{
				List<string> list_bez = new List<string>();
				IList<IWebElement> web = item.FindElements(By.TagName("td"));
				foreach (var j in web)
				{
					list_bez.Add(j.Text);
				}
				dict.Add(list_bez[0], list_bez[1]);
			}
			driver.Navigate().Back();
			return dict;
		}

		// TODO: Доспехи и щиты (Armor and shields)
		public void ArmorAndShields()
		{
			Press_button(GetCards(), 1);
			IWebElement center_page1 = driver.FindElement(By.ClassName("card-wrapper"));
			IList<IWebElement> webElements = center_page1.FindElements(By.TagName("table"));
			IList<IWebElement> tables = webElements[0].FindElements(By.ClassName("table_header"));

			DataSet armor = new DataSet("Armor and shields");
			DataTable armorandshields = new DataTable("armorandshields");
			armor.Tables.Add(armorandshields);
			foreach (var item in tables)
			{
				armorandshields.Columns.Add(item.Text);
			}
		}

		/// <summary>
		/// Получение таблиц со страницы "Драгоценные камни"
		/// </summary>
		/// <returns>Dictionary jewel[]</returns>
		public Dictionary<string, Dictionary<string, string>> Jewel()
		{
			Press_button(GetCards(), 2);
			IWebElement center_page = driver.FindElement(By.ClassName("desc"));
			IList<IWebElement> tables = center_page.FindElements(By.TagName("table"));
			IList<IWebElement> desc = center_page.FindElements(By.TagName("strong"));
			Dictionary<string, Dictionary<string, string>> jewel = new Dictionary<string, Dictionary<string, string>>();
			Dictionary<string, string> temp = new Dictionary<string, string>();
			List<string> list = new List<string>();
			int j = tables.Count();
			int c;
            for (int i = 1; i < j+1; i++)
            {
				IList<IWebElement> td = tables[i-1].FindElements(By.TagName("td"));
				foreach (var item in td)
				{
						list.Add(item.Text);
				}
				c = list.Count();
				List<Dictionary<string, string>> keyValuePairs = new List<Dictionary<string, string>>();
				keyValuePairs.Add(temp);
                for (int k = 0; k < c; k++)
                {
					temp.Add(list[k], list[++k]);
                }
				jewel.Add(desc[i].Text, temp);
                JsWrite(jewel);
				temp.Clear();
				list.Clear();
				jewel.Clear();
			}
			driver.Navigate().Back();
			return jewel;
		}

		// TODO: Инструменты (Tools)
		// TODO: Монеты
		// TODO: Оружие (Arms)

		/// <summary>
		/// Получение таблиц со страницы "Произведения искусства"
		/// </summary>
		/// <returns>Dictionary arts[]</returns>
		public Dictionary<string, string>[] Arts()
		{
			Press_button(GetCards(), 6);
			IWebElement center_page1 = driver.FindElement(By.ClassName("card-wrapper"));
			IList<IWebElement> tables = center_page1.FindElements(By.TagName("table"));
			Dictionary<string, string> zm_25 = new Dictionary<string, string>();
			Dictionary<string, string> zm_250 = new Dictionary<string, string>();
			Dictionary<string, string> zm_750 = new Dictionary<string, string>();
			Dictionary<string, string> zm_2500 = new Dictionary<string, string>();
			Dictionary<string, string> zm_7500 = new Dictionary<string, string>();
			int j = tables.Count();
			for (int i = 0; i < j; i++)
			{
				IList<IWebElement> tr = tables[i].FindElements(By.TagName("tr"));
				foreach (var item in tr)
				{
					List<string> list = new List<string>();
					IList<IWebElement> web = item.FindElements(By.TagName("td"));
					foreach (var z in web)
					{
						list.Add(z.Text);
					}
					switch (i)
					{
						case 0:
							zm_25.Add(list[0], list[1]);
							break;
						case 1:
							zm_250.Add(list[0], list[1]);
							break;
						case 2:
							zm_750.Add(list[0], list[1]);
							break;
						case 3:
							zm_2500.Add(list[0], list[1]);
							break;
						case 4:
							zm_7500.Add(list[0], list[1]);
							break;
					}
				}
			}
			Dictionary<string, string>[] arts = { zm_25, zm_250, zm_750, zm_2500, zm_7500 };
			driver.Navigate().Back();
			return arts;
		}
		/// <summary>
		/// Закрытие браузера
		/// </summary>
		public void Quit()
		{
			driver.Quit();
		}
		// TODO: Снаряжение (Equipment)
		// TODO: Сокровищница
	}
}
