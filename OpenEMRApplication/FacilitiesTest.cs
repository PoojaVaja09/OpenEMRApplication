
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

namespace OpenEMRApplication
{
    class FacilitiesTest : WebDriverWrapper
    {
        public static object[] AddFacilitySource()
        {
            string path = System.Reflection.Assembly.GetCallingAssembly().CodeBase;
            path = path.Substring(0, path.LastIndexOf("bin"));
            path = new Uri(path).LocalPath;
            path = path + @"TestData\OpenEMRData.xlsx";
            string currentMethodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
            object[] main = ExcelUtils.GetSheetIntoObject(path, currentMethodName);
            return main;
        }



        [Test, TestCaseSource("AddFacilitySource")]

        public void AddFacility(string username, string password, string language, string facilityName, string color,string ExpectedValue)
        {
            LoginPage login = new LoginPage(driver);
            login.SendUsername(username);
            login.SendPassword(password);
            login.SelectLanguage(language);
            login.ClickonLogin();

            DashboardPage dashboardPage = new DashboardPage(driver);
            dashboardPage.HoverandClickFacility();

            //switch into frame
            Thread.Sleep(3000);
            FacilityPage facilityPage = new FacilityPage(driver);
            facilityPage.SwitchToADMFrame();

            Thread.Sleep(3000);
            facilityPage.WaitForAddFacilityandClick();
            //comeout from frame
            facilityPage.SwitchToDefaultContent();

            //switch to frame
            Thread.Sleep(3000);
            facilityPage.SwitchToModalFrame();

            facilityPage.SendFacilityName(facilityName);
            facilityPage.SendColor(color);
            facilityPage.ClickOnSave();

            //comeout 
            facilityPage.SwitchToDefaultContent();

            Thread.Sleep(3000);
            facilityPage.SwitchToADMFrame();

            //string pageSource = driver.PageSource;
            //Assert


            bool check = facilityPage.CheckForGivenName("Paul");
            Assert.IsTrue(check, "Assertion on Add Facility");

        }
    }
}
