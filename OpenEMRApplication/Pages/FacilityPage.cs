using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenEMRApplication.Pages
{
    class FacilityPage
    {
        private By addFacilityLocator = By.XPath("//span[text()='Add Facility']");
        private string admFrame = "adm";
        private string modalFrame = "modalframe";
        private By nameLocator = By.XPath("//input[@name='facility']");

        private By colorLocator = By.Id("ncolor");

        private By savebtnLocator = By.XPath("//span[text()='Save']");


        



        private IWebDriver driver;

        public FacilityPage(IWebDriver driver)
        {
            this.driver = driver;
        }

        public void SwitchToADMFrame()
        {
            driver.SwitchTo().Frame(admFrame);
        }
        public void WaitForAddFacilityandClick()
        {
            driver.FindElement(addFacilityLocator).Click();
        }
        public void SwitchToDefaultContent()
        {
            driver.SwitchTo().DefaultContent();
        }

        public void SwitchToModalFrame()
        {
            driver.SwitchTo().Frame(modalFrame);
        }

        public void SendFacilityName(string name)
        {
            driver.FindElement(nameLocator).SendKeys(name);
        }

        public void SendColor(string color)
        {
            driver.FindElement(colorLocator).SendKeys(color);
        }
        public bool CheckForGivenName(string inputname)
        {
            var rowsEle = driver.FindElements(By.XPath("//table[@class='table table-striped']/tbody/tr"));
            int rowCount = rowsEle.Count;

            bool check = false;

            for (int i = 1; i <= rowCount; i++)
            {
                string name = driver.FindElement(By.XPath("//table[@class='table table-striped']/tbody/tr[" + i + "]/td[1]")).Text;
                if (name.Trim().Equals(inputname))
                {
                    check = true;
                }
            }

            return check;

        }
        public void ClickOnSave()
        {
            driver.FindElement(savebtnLocator).Click();
        }
        
        
        
    }
}
