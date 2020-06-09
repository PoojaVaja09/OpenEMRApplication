
using AutomationWrapper.Base;
using NUnit.Framework;
using OpenEMRApplication.Pages;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace OpenEMRApplication
{
    class AboutTest:WebDriverWrapper
    {
        [Test]
        public void VersionNumberTest()
        {
            LoginPage login = new LoginPage(driver);
            login.SendUsername("admin");
            login.SendPassword("pass");
            login.SelectLanguage("English (Indian)");
            login.ClickonLogin();

            test.Info("finished with login");

            DashboardPage dashpage = new DashboardPage(driver);
            dashpage.ClickonAbout();

            AboutPage aboutPage = new AboutPage(driver);
            aboutPage.SwitchtoFrame();

            aboutPage.WaituntilXpath();

            string actualVersion = aboutPage.GetVersionTitle();

            Assert.AreEqual("Version Number: v5.0.2 (1)", actualVersion);
            aboutPage.SwitchtoDefaultContent();
        }

       
    }
}
