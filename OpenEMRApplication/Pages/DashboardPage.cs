using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenEMRApplication.Pages
{
    class DashboardPage
    {
        private By billyLocator = By.XPath("//span[text()='Billy']");
        private By logoutLocator = By.XPath("//li[text()='Logout']");
        private By aboutLocator = By.XPath("//div[contains(text(),'About')]");
        private By administrationLocator = By.XPath("//div[text()='Administration']");
        private By facilityLocator = By.XPath("//div[text()='Facilities']");
        private By patientLocator = By.XPath("//div[text()='Patient/Client']");
        private By newPatientLocator = By.XPath("//body/div[@id='mainBox']/div[@id='body_top_div']/div[@id='menu_items']/span[@id='menu_logo']/div/div[@class='appMenu']/span[5]/div[1]/ul[1]/li[2]/div[1]");




        private IWebDriver driver;

        public DashboardPage(IWebDriver driver)
        {
            this.driver = driver;
        }

        public void WaitForPresenceofBillyText()
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(40));
            //wait.Until(ExpectedConditions.PresenceOfAllElementsLocatedBy(By.XPath("//span[text()='Billy']")));
            wait.IgnoreExceptionTypes(typeof(NoSuchElementException));

            wait.Until(x => x.FindElement(billyLocator));
        }

        public string GetCurrentTitle()
        {
            return driver.Title;
        }

        public void MouseHoverBilly()
        {
            IWebElement billyEle = driver.FindElement(billyLocator);
            Actions actions = new Actions(driver);
            actions.MoveToElement(billyEle).Build().Perform();

        }

        public void ClickonLogOut()
        {
            IWebElement logoutEle = driver.FindElement(logoutLocator);
            logoutEle.Click();
        }

        public void ClickonAbout()
        {
            driver.FindElement(aboutLocator).Click();
        }

        public void HoverandClickFacility()
        {
            Actions actions = new Actions(driver);
            IWebElement administrationEle = driver.FindElement(administrationLocator);
            actions.MoveToElement(administrationEle).Build().Perform();

            IWebElement facilityEle = driver.FindElement(facilityLocator);
            actions.MoveToElement(facilityEle).Click().Build().Perform();
        }

        public void HoverAndClickPatient()
        {
            Actions actions = new Actions(driver);
            IWebElement patientEle = driver.FindElement(patientLocator);
            actions.MoveToElement(patientEle).Build().Perform();

            driver.FindElement(newPatientLocator).Click();
        }


    }
}
