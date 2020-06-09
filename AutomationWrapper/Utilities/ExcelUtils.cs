using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Excel = Microsoft.Office.Interop.Excel;

namespace AutomationWrapper.Utilities
{
    public class ExcelUtils
    {

        public static object[] GetSheetIntoObject(string fileDetails,string sheetName)
        {
            Excel.Application xlApp = null;
            Excel.Workbook xlBook = null;
            object[] main = null;
            try
            {
               xlApp  = new Excel.Application();
                xlBook = xlApp.Workbooks.Open(fileDetails);
                Excel.Worksheet xlsheet =(Excel.Worksheet) xlBook.Worksheets[sheetName];
                Excel.Range xlRange = xlsheet.UsedRange;
                int rowCount = xlRange.Rows.Count;
                int colCount = xlRange.Columns.Count;
                Console.WriteLine(rowCount);
                Console.WriteLine(colCount);

                main = new object[rowCount - 1];


                for (int i = 2; i <= rowCount; i++)
                {
                    object[] temp = new object[colCount];
                    for (int j = 1; j <= colCount; j++)
                    {
                        string value =Convert.ToString(xlRange.Cells[i, j].value);
                        Console.WriteLine(value);
                        temp[j - 1] = value;


                    }
                    main[i - 2] = temp;

                }

            }
            finally
            { // always run
                xlBook.Close();
                xlApp.Quit();
            }
            
           
            return main;
        
        }
    }
}
