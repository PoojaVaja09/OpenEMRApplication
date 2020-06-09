
using AutomationWrapper.Base;
using NUnit.Framework;
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
    class ProceduresTest:WebDriverWrapper
    {
        [Test,Order(1)]
        public void AddProcedureTest()
        {
            driver.FindElement(By.Id("authUser")).SendKeys("admin");
            driver.FindElement(By.Id("clearPass")).SendKeys("pass");

            IWebElement languageEle = driver.FindElement(By.Name("languageChoice"));
            SelectElement selectLanguage = new SelectElement(languageEle);
            selectLanguage.SelectByText("English (Standard)");

            driver.FindElement(By.XPath("//button[@type='submit']")).Click();

            //mouse action

            Actions actions = new Actions(driver);
            IWebElement procedureEle = driver.FindElement(By.XPath("(//div[text()='Procedures'])[1]"));
            actions.MoveToElement(procedureEle).Build().Perform();

            driver.FindElement(By.XPath("//div[contains(text(),'Configuration')]")).Click();

            //switch to frame Pat
            //Thread.Sleep(5000);                       
            driver.SwitchTo().Frame("pat");

           WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(40));
            wait.IgnoreExceptionTypes(typeof(NoSuchWindowException));
            wait.Until(x => x.FindElement(By.Id("add_node_button"))).Click();



            //driver.FindElement(By.Id("add_node_button")).Click();
            //come out from frame
            driver.SwitchTo().DefaultContent();

            //switch to frame modalFrame
            //Thread.Sleep(5000);                       
            driver.SwitchTo().Frame("modalframe");
                                       
            IWebElement procedureTierEle = driver.FindElement(By.XPath("//select[@id='form_procedure_type']"));
            SelectElement selectProcedureTier = new SelectElement(procedureTierEle);
            selectProcedureTier.SelectByText("Group");

            driver.FindElement(By.XPath("//input[@id='form_name ']")).SendKeys("Sollers1");
            driver.FindElement(By.Id("form_description")).SendKeys("SDET");
            driver.FindElement(By.Name("form_save")).Click();
            //comeout from frame
            driver.SwitchTo().DefaultContent();


            //switch to frame
            driver.SwitchTo().Frame("pat");

            
            //asser
            var rowsEle = driver.FindElements(By.XPath("//div[@id='con0']/table/tbody/tr"));
            int rowCount = rowsEle.Count;

            bool check = false;
            for (int i = 1; i <= rowCount; i++)
            {
                string name = driver.FindElement(By.XPath("//div[@id='con0']/table/tbody/tr[" + i + "]/td[1]")).Text;
                if (name.Contains("Sollers1"))
                {
                    check = true;
                }
            }
            Assert.IsTrue(check, "Assertion on Add Procedure");
            

        }
        [Test,Order(2)]
        public void EditProcedureTest()
        {
            driver.FindElement(By.Id("authUser")).SendKeys("admin");
            driver.FindElement(By.Id("clearPass")).SendKeys("pass");

            IWebElement languageEle = driver.FindElement(By.Name("languageChoice"));
            SelectElement selectLanguage = new SelectElement(languageEle);
            selectLanguage.SelectByText("English (Standard)");

            driver.FindElement(By.XPath("//button[@type='submit']")).Click();

            //mouse action

            Actions actions = new Actions(driver);
            IWebElement procedureEle = driver.FindElement(By.XPath("(//div[text()='Procedures'])[1]"));
            actions.MoveToElement(procedureEle).Build().Perform();

            driver.FindElement(By.XPath("//div[contains(text(),'Configuration')]")).Click();

            //switch to frame Pat
           // Thread.Sleep(5000);                       
            driver.SwitchTo().Frame("pat");

            driver.FindElement(By.XPath("//tr[1]//td[6]//span[1]")).Click();
            
            //come out from frame
            driver.SwitchTo().DefaultContent();
            //switch to Frame
            //Thread.Sleep(3000);
            driver.SwitchTo().Frame("modalframe");

            driver.FindElement(By.XPath("//button[@name='form_delete']")).Click();

            driver.SwitchTo().DefaultContent();




            //on deleting
            //Assert.IsFalse(check);//expect false

        }
    }
}
