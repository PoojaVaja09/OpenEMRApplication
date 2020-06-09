using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace AutomationWrapper.Utilities
{
    public class DBUtils
    {
        //update
        //delete
        //insert
        //select
        //static string connectionString = "Data Source=balaji;Initial Catalog=MagentoDB;User ID=sa;Password=123456;Integrated Security=True";
        static string connectionString = ConfigurationManager.ConnectionStrings["emrdb"].ToString();
        public static string GetFirstCellValue(string query)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand command = new SqlCommand(query, connection);
            connection.Open();
            string cellValue = command.ExecuteScalar().ToString();
            connection.Close();

            return cellValue;
        }

        public static int UpdateDeleteInsertQuery(string query)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand command = new SqlCommand(query, connection);
            connection.Open();
            int noOfAffected = command.ExecuteNonQuery();
            connection.Close();

            return noOfAffected;
        }

        public static DataTable SelectQueryToDatatable(string query)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand command = new SqlCommand(query, connection);
            DataTable dt = new DataTable();

            //automatically open and close the connection
            SqlDataAdapter adapter = new SqlDataAdapter(command);

            adapter.Fill(dt);

            return dt;
        }
    }
}
//string connectionString = "Server=127.0.0.1; Port=5432; User Id=postgres; Password=123456; DataBase=SkypeDB";





