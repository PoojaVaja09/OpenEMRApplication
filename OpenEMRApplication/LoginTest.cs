
using AutomationWrapper.Base;
using NUnit.Framework;
using OpenEMRApplication.Pages;
using AutomationWrapper.Utilities;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace OpenEMRApplication
{
    class LoginTest:WebDriverWrapper
    {
        public static object[] ValidCredentialSource()
        {    
            //hard coded excel location 
         // object[] main = ExcelUtils.GetSheetIntoObject(@"D:\Sollers\Selenium Concept\OpenEMRApplication\OpenEMRApplication\TestData\OpenEMRData.xlsx", "ValidCredentialSource");
                             
            string path = System.Reflection.Assembly.GetCallingAssembly().CodeBase;
            path = path.Substring(0, path.LastIndexOf("bin"));
            path = new Uri(path).LocalPath;
            path = path + @"TestData\OpenEMRData.xlsx";

            string currentMethodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
            object[] main = ExcelUtils.GetSheetIntoObject(path, currentMethodName);

            return main;
        }

        [Test,Order(1),TestCaseSource("ValidCredentialSource")]
        public void ValidCredentialTest(string usename,string password,string language,string expectedValue)
        {
            LoginPage login = new LoginPage(driver);
            login.SendUsername(usename);
            login.SendPassword(password);
            login.SelectLanguage(language);
            login.ClickonLogin();



            DashboardPage dashboard = new DashboardPage(driver);
            dashboard.WaitForPresenceofBillyText();

            string actualTitle = dashboard.GetCurrentTitle();
             //hard assert
            Assert.AreEqual(expectedValue, actualTitle);
           

            dashboard.MouseHoverBilly();
            //logout
            dashboard.ClickonLogOut();

        }

        public static object[] InvalidCredentialSource()
        {
            object[] temp1 = new object[4];
            temp1[0] = "admin123";
            temp1[1] = "pass123";
            temp1[2] = "English (Indian)";
            temp1[3] = "Invalid username or password";

            object[] temp2 = new object[4];
            temp2[0] = "bala";
            temp2[1] = "bala123";
            temp2[2] = "French (Standard)";
            temp2[3] = "Invalid username or password";

            object[] main = new object[2];
            main[0] = temp1;
            main[1] = temp2;

            return main;
        }


        [Test,Order(2),TestCaseSource("InvalidCredentialSource")]
        public void InvalidCredentialTest(string username, string password, string language, string expectedValue)
        {

            LoginPage login = new LoginPage(driver);
            login.SendUsername(username);
            login.SendPassword(password);
            login.SelectLanguage(language);
            login.ClickonLogin();

            string actualValue =login.GetErrorMessage();

            Assert.AreEqual(expectedValue,actualValue);

        }
        [Test,Order(3)]
        public void AcAcknowledgmentsLicensingAndCertificationTest()
        {
            LoginPage login = new LoginPage(driver);
            login.ClickonAcknowledgementLink();
            login.SwitchtoAcknowlwgement();

            AckCertificationPage ackPage = new AckCertificationPage(driver);

            

            string pageSource = ackPage.GetPageSource();
            Assert.IsTrue(pageSource.Contains("Acknowledgments, Licensing and Certification"));

        }
    }
}
