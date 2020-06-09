using MongoDB.Driver;
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
    class PatientPage
    {
        string patFrame = "pat";
        private By nameLocator = By.XPath("//input[@id='form_fname']");
        private By patientFNameLocator = By.XPath("//input[@id='form_fname']");
        private By patientLNameLocator = By.Id("form_lname");
        private By dobLocator = By.Id("form_DOB");
        private By genderLocator = By.Id("form_sex");
        private By createPatientBtnLocator = By.Id("create");
        private By confirmBtnLocator = By.XPath("//input[@value='Confirm Create New Patient']");
        private By closehappybirthdaylocator = By.XPath("//div[@class='closeDlgIframe']");
        private By confirmationTextLocator = By.XPath("//h2[contains(text(),'Medical Record Dashboard')]");

        private IWebDriver driver;

        public PatientPage(IWebDriver driver)
        {
            this.driver = driver;
        }

        public void SwitchToPatFrame()
        {
            driver.SwitchTo().Frame(patFrame);
        }

        public void WaitForNameLocator()
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(40));
            wait.IgnoreExceptionTypes(typeof(NoSuchWindowException));
            wait.Until(x => x.FindElement(nameLocator));

        }

        public void SendFirstName(string fName)
        {
            driver.FindElement(patientFNameLocator).SendKeys(fName);
        }

       

        public void SendLastName(string lName)
        {
            driver.FindElement(patientLNameLocator).SendKeys(lName);
        }

        public void SendDOB(string dob)
        {

            IWebElement dateEle = driver.FindElement(dobLocator);
            dateEle.Clear();
            dateEle.SendKeys(dob);

        }
        public void SendGender(string gender)
        {
            IWebElement genderEle = driver.FindElement(genderLocator);
            SelectElement selectGender = new SelectElement(genderEle);
            selectGender.SelectByText(gender);

        }
        public void ClickCreateNewPatient()
        {
            driver.FindElement(createPatientBtnLocator).Click();
        }
        public void SwitchToDefaultContent()
        {
            driver.SwitchTo().DefaultContent();
        }

        public void SwitchToModalFrame(string modalFrame)
        {
            driver.SwitchTo().Frame(modalFrame);
        }

        public void ClickConfirmNewPatientButton()
        {
            driver.FindElement(confirmBtnLocator).Click();
        }

        public void WaitForAlertAndAccept()
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(40));
            wait.IgnoreExceptionTypes(typeof(NoAlertPresentException));
            wait.Until(x => x.SwitchTo().Alert()).Accept();
        }

        public void CloseHappyBirthDayMessage()
        {
            driver.FindElement(closehappybirthdaylocator).Click();
        }

        public string GetConfirmationText()
        {
            string actualConfirmationText = driver.FindElement(confirmationTextLocator).Text;
            return actualConfirmationText;
        }
    }
}
