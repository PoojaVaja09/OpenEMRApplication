using NUnit.Framework;
using AutomationWrapper.Utilities;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using Excel = Microsoft.Office.Interop.Excel;

namespace OpenEMRApplication
{
    
    class ReferenceDemo
    {
        /* public static object[] ProvideName()
         {
             object[] main = new object[4];
             main[0] = "peter";
             main[1] = "john";
             main[2] = "%#*@&&";
             main[3] = "644657";
             return main;
         }


         [Test,TestCaseSource("ProvideName")]
         public void RefMethod(string name)
         {
             Console.WriteLine(name);
         }*/

       /* public static object[] InvalidCredentialSource()
        {
            object[] temp1 = new object[4];
            temp1[0] = "admin123";
            temp1[1] = "wel123";
            temp1[2] = "english";
            temp1[3] = "emr";

            object[] temp2 = new object[4];
            temp2[0] = "doctor";
            temp2[1] = "doc123";
            temp2[2] = "french";
            temp2[3] = "emr";

            object[] main = new object[2];
            main[0] = temp1;
            main[1] = temp2;

            return main;
        }


        
        [Test,TestCaseSource("InvalidCrentialSource")]
        public void InvalidTest(string username,string password,string language,string expectedValue)
        {
            Console.WriteLine(username + password + language + expectedValue);
        }
        */
        [Test]
        public void ExcelRead()
        {
            

            Excel.Application xlApp = new Excel.Application();
            Excel.Workbook xlBook = xlApp.Workbooks.Open(@"D:\Sollers\Selenium Concept\OpenEMRApplication\OpenEMRApplication\TestData\OpenEMR.xlsx");
           
            Excel.Worksheet xlsheet = xlBook.Worksheets["ValidCredentialSorce"];
                Excel.Range xlRange = xlsheet.UsedRange;
            int rowCount = xlRange.Rows.Count;
            int colCount = xlRange.Columns.Count;
            Console.WriteLine(rowCount);
            Console.WriteLine(colCount);

            object[] main = new object[rowCount - 1];


            for (int i = 2; i <= rowCount; i++)
            {
                object[] temp = new object[colCount];
                for (int j = 1; j <= colCount; j++)
                {
                    string value = xlRange.Cells[i, j].value;
                    Console.WriteLine(value);
                    temp[j - 1] = value;
                    

                }
                main[i - 2] = temp;

            }
            xlBook.Close();
            xlApp.Quit();
        }
    }
}
