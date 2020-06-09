
using AutomationWrapper.Base;
using NUnit.Framework;
using OpenEMRApplication.Pages;
using AutomationWrapper.Utilities;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Office.Core;
using OpenQA.Selenium.Support.Extensions;

namespace OpenEMRApplication
{
    class PatientTest:WebDriverWrapper
    {
        public static object[] NewPatientSource()
        {
            //object[] main = ExcelUtils.GetSheetIntoObject(@"D:\Sollers\Selenium Concept\OpenEMRApplication\OpenEMRApplication\TestData\OpenEMRData.xlsx", "NewPatientSource");
            //return main;

            string path = System.Reflection.Assembly.GetCallingAssembly().CodeBase;
            path = path.Substring(0, path.LastIndexOf("bin"));
            path = new Uri(path).LocalPath;
            path = path + @"TestData\OpenEMRData.xlsx";

            string currentMethodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
            object[] main = ExcelUtils.GetSheetIntoObject(path, currentMethodName);

            return main;
        }


        [Test,TestCaseSource("NewPatientSource")]
        public void NewPatientTest(string username,string password, string language,string pFirsrName,string pLastName,string DOB,string gender)
        {
            LoginPage login = new LoginPage(driver);
            login.SendUsername(username);
            login.SendPassword(password);
            login.SelectLanguage(language);
            login.ClickonLogin();
                     
            DashboardPage dashboard = new DashboardPage(driver);
            dashboard.HoverAndClickPatient();

            PatientPage patientPage = new PatientPage(driver);
            patientPage.SwitchToPatFrame();

            patientPage.WaitForNameLocator();

            patientPage.SendFirstName(pFirsrName);
            patientPage.SendLastName(pLastName);
            patientPage.SendDOB(DOB);
            patientPage.SendGender(gender);

            patientPage.ClickCreateNewPatient();

            patientPage.SwitchToDefaultContent();

            patientPage.SwitchToModalFrame("modalframe");
            

            patientPage.ClickConfirmNewPatientButton();

            patientPage.SwitchToDefaultContent();

            
            patientPage.WaitForAlertAndAccept();

            //close happybirthday message
            patientPage.CloseHappyBirthDayMessage();

            patientPage.SwitchToPatFrame();

            string actualText = patientPage.GetConfirmationText();
            Assert.IsTrue(actualText.Contains("Medical Record Dashboard"), "Assertion on add Patient");

        }

        public static object[] RecallBoardSource()
        {
            //object[] main = ExcelUtils.GetSheetIntoObject(@"D:\Sollers\Selenium Concept\OpenEMRApplication\OpenEMRApplication\TestData\OpenEMRData.xlsx", "RecallBoardSource");
            //return main;

            string path = System.Reflection.Assembly.GetCallingAssembly().CodeBase;
            path = path.Substring(0, path.LastIndexOf("bin"));
            path = new Uri(path).LocalPath;
            path = path + @"TestData\OpenEMRData.xlsx";

            string currentMethodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
            object[] main = ExcelUtils.GetSheetIntoObject(path, currentMethodName);

            return main;
        }



        [Test,TestCaseSource("RecallBoardSource")]
        public void RecallBoardTest(string username, string password, string language, string pFirsrName, string pLastName, string DOB, string gender)
        {
            
            LoginPage login = new LoginPage(driver);
            login.SendUsername(username);
            login.SendPassword(password);
            login.SelectLanguage(language);
            login.ClickonLogin();

            DashboardPage dashboard = new DashboardPage(driver);
            dashboard.HoverAndClickPatient();
            Thread.Sleep(3000);

            PatientPage patientPage = new PatientPage(driver);
            patientPage.SwitchToPatFrame();
            Thread.Sleep(3000);
            

            patientPage.SendFirstName(pFirsrName);
            patientPage.SendLastName(pLastName);


            
            patientPage.SendDOB(DOB);
            patientPage.SendGender(gender);

            patientPage.ClickCreateNewPatient();

            patientPage.SwitchToDefaultContent();

            patientPage.SwitchToModalFrame("modalframe");


            patientPage.ClickConfirmNewPatientButton();

            patientPage.SwitchToDefaultContent();

            Thread.Sleep(3000);
            //patientPage.WaitForAlertAndAccept();
            driver.SwitchTo().Alert().Accept();

            
            Thread.Sleep(3000);
                        

            driver.FindElement(By.XPath("//div[text()='Recall Board']")).Click();

            Thread.Sleep(3000);
            driver.SwitchTo().Frame("rcb");
            driver.FindElement(By.XPath("//button[@class='btn btn-default btn-add']")).Click();
            driver.SwitchTo().DefaultContent();

            Thread.Sleep(3000);
            driver.SwitchTo().Frame("recall");
            driver.FindElement(By.XPath("//label[contains(text(),'plus 1 year')]")).Click();

            SelectElement selectProvider = new SelectElement(driver.FindElement(By.Id("new_provider")));
            selectProvider.SelectByText("Lee, Donna");

            SelectElement selectFacility = new SelectElement(driver.FindElement(By.Id("new_facility")));
            selectFacility.SelectByText("Great Clinic");

            driver.FindElement(By.XPath("//button[@id='add_new']")).Click();
            string responseText=driver.FindElement(By.Id("div_response")).Text;

            if(responseText.Equals("Persons needing a recall, no appt scheduled yet."))
            {
                Console.WriteLine("Appointment is not scheduled");
            }
            else
            {
                Console.WriteLine("Appointment is scheduled");
            }

            SelectElement selectSameProvider = new SelectElement(driver.FindElement(By.Id("form_provider")));
            selectSameProvider.SelectByText("Lee, Donna");

            IWebElement dateEle=driver.FindElement(By.Id("form_to_date"));
            dateEle.Clear();
            dateEle.SendKeys("2021-06-08");
            Thread.Sleep(3000);

            //JAVA script for"Filter" Button 
            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            js.ExecuteScript("document.getElementById('filter_submit').click()");
            
            Thread.Sleep(3000);
            IWebElement patientEle = driver.FindElement(By.XPath("//div[@class='divTableRow ALL whitish']/div[1]"));
            Console.WriteLine(patientEle);
            string patientText = patientEle.Text;
            Console.WriteLine(patientText);
            
            Thread.Sleep(3000);
            driver.FindElement(By.XPath("//i[@class='top_right_corner fa fa-times']")).Click();
           

            Thread.Sleep(3000);

            IAlert alertBox = driver.SwitchTo().Alert();
            string alertText = alertBox.Text;
            Console.WriteLine(alertText);

            Thread.Sleep(3000);
            alertBox.Accept();

            //IWebElement checkpatientEle = driver.FindElement(By.XPath("//div[@class='divTableRow ALL whitish']/div[1]"));
            //Assert.IsFalse(checkpatientEle.Displayed);
            driver.SwitchTo().DefaultContent();

                      

        }
    }
}
    