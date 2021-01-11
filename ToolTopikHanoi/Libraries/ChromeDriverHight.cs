using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zu.AsyncWebDriver.Remote;
using Zu.Chrome;

namespace ToolTopikHanoi.Libraries
{
   public class ChromeDriverHight
    {

        public static string check { get; set; }
        public static int countTask2 { get; set; }
        public static int sumTask2 { get; set; }
        public static string[] selectIDs { get; set; }
        private static AsyncChromeDriver chromeDriver;
        private static WebDriver driver;
        public static WebDriver _driver
        {
            get
            {
                if (driver != null && check != null && check != "countinue")
                {
                    chromeDriver.Close();
                    chromeDriver = null;
                    driver.Close();
                    driver.Quit();
                    driver = null;
                    check = null;
                }
                else if (driver != null && check == "countinue")
                {
                    return driver;
                }
                else if(driver == null && check == "End")
                {
                    return driver;
                }
                else
                {
                    chromeDriver = new AsyncChromeDriver();
                    driver = new WebDriver(chromeDriver);

                }
                return driver;
            }
        }
    }
}
