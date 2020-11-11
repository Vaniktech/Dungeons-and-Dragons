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

namespace Dungeons_and_Dragons
{
	class Tentaculus:Page
	{
		IWebDriver driver = new ChromeDriver();

		/// <summary>
		/// Получение списка элементов из карточек
		/// </summary>
		/// <returns>IList cards</returns>
		public IList<IWebElement> GetCards()
		{
			IList<IWebElement> cards = driver.FindElements(By.ClassName("cardContainer"));
			return cards;
		}
		/// <summary>
		/// Переход на веб-страницу
		/// </summary>
		private void GoToUrl()
        {
            driver.Url = "https://tentaculus.ru/magic-items/index.html";
            driver.FindElement(By.Id("showAllItems")).Click();
            Thread.Sleep(1000);
        }
		/// <summary>
		/// Получение карточек из разных категорий и запись их в Json
		/// </summary>
		public void Get_Info()
        {
			GoToUrl();
			IList<IWebElement> buttons_down = driver.FindElements(By.ClassName("combo_box_arrow"));
			buttons_down[0].Click();
			IWebElement combo_box = driver.FindElement(By.ClassName("combo_box_content"));
			IList<IWebElement> labels = driver.FindElements(By.TagName("label"));
			Dictionary<string, string> items = new Dictionary<string, string>();
			int count_labels = labels.Count();
			for (int i = 0; i < count_labels; i++)
			{
				try
				{
					labels[i].Click();
				}
				catch (ElementNotInteractableException) { break; }
				Thread.Sleep(1000);
				IList<IWebElement> web = GetCards();
				int count_web = web.Count();
				for (int j = 0; j < count_web; j++)
				{
					try
					{
						items.Add(web[j].FindElement(By.ClassName("header_info")).Text, web[j].FindElement(By.ClassName("coast")).Text);
					}
					catch (NoSuchElementException)
					{
						items.Add(web[j].FindElement(By.ClassName("header_info")).Text, "Unknown");
					}
				}
				labels[i].Click();
				JsWrite(items);
				items.Clear();
			}
		}
		/// <summary>
		/// Закрытие браузера
		/// </summary>
		public void Quit()
		{
			driver.Quit();
		}
	}
}