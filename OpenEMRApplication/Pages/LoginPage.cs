using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenEMRApplication.Pages
{
    class LoginPage
    {
        private  By userLocator = By.Id("authUser");
        private  By passLocator = By.Name("clearPass");
        private By languageLocator = By.Name("languageChoice");
        private By loginLocator = By.XPath("//button[@type='submit']");

        private By errorLocator = By.XPath("//div[@class='alert alert-danger login-failure m-1']");

        private By acknowlwgementLocator = By.XPath("//a[contains(text(),'Acknowledgments, Licensing and Certification')]");



        private IWebDriver driver;

        public LoginPage(IWebDriver driver)
        {
            this.driver = driver;
        }

        public  void SendUsername(string username)
        {
            driver.FindElement(userLocator).SendKeys(username);
        }

        public void SendPassword(string password)
        {
            driver.FindElement(passLocator).SendKeys(password);
        }

        public void SelectLanguage(string language)
        { 
            SelectElement selectLanguage = new SelectElement(driver.FindElement(languageLocator));
            selectLanguage.SelectByText(language);
        }

        public void ClickonLogin()
        {
            driver.FindElement(loginLocator).Click();
        }

        public string GetErrorMessage()
        {
            string messageText = driver.FindElement(errorLocator).Text;
            return messageText;
        }


        public void ClickonAcknowledgementLink()
        {
            driver.FindElement(acknowlwgementLocator).Click();
        }

        public void SwitchtoAcknowlwgement()
        {
            var windows = driver.WindowHandles;

            driver.SwitchTo().Window(windows[1]);

        }
    }
}
