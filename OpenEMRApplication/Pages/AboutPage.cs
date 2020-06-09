using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenEMRApplication.Pages
{
    class AboutPage
    {

        private IWebDriver driver;
        private By versionLocator = By.XPath("//h4[contains(text(),'Version Number')]");

        public AboutPage(IWebDriver driver)
        {
            this.driver = driver;
        }

        public void SwitchtoFrame()
        {
            driver.SwitchTo().Frame("msc");

        }
        public void WaituntilXpath()
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(40));
            wait.IgnoreExceptionTypes(typeof(NoSuchWindowException));
            wait.Until(x => x.FindElement(versionLocator));
        }
        public string GetVersionTitle()
        {
            string actualVersion = driver.FindElement(versionLocator).Text;

            return actualVersion;

        }
        public void SwitchtoDefaultContent()
        {
            driver.SwitchTo().DefaultContent();
        }
    }
}
