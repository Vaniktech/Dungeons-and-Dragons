using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.PageObjects;

namespace Dungeons_and_Dragons
{
    class Page
    {
		IWebDriver driver = new ChromeDriver();

		/// <summary>
		/// Получение списка элементов из карточек
		/// </summary>
		/// <returns>IList cards</returns>
		IList<IWebElement> GetCards()
		{
			driver.Url = "https://dungeon.su/articles/inventory/";
			IWebElement center_page = driver.FindElement(By.ClassName("center"));
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
		Dictionary<string, string> bezdelushki()
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

		public void ArmorAndShields()
		{
			Press_button(GetCards(), 1);
			IWebElement center_page1 = driver.FindElement(By.ClassName("card-wrapper"));
			IList<IWebElement> webElements = center_page1.FindElements(By.TagName("table"));
			IList<IWebElement> tables = webElements[0].FindElements(By.ClassName("table_header"));

			DataSet armor = new DataSet("Armor and shields");
			DataTable armorandshields = new DataTable("armorandshields");
			// добавляем таблицу в dataset
			armor.Tables.Add(armorandshields);
			foreach (var item in tables)
			{
				armorandshields.Columns.Add(item.Text);
			}
		}
		static void Main(string[] args)
		{
			Page page = new Page();
			page.ArmorAndShields();
		}
	}
}