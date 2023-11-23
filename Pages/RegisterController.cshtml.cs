using Emergency_Services.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using System.Data;
using System.Reflection.PortableExecutable;
using System;
using System.Security.Principal;

namespace Emergency_Services.Pages
{
    public class RegisterControllerModel : PageModel
    {

        public static SqlCommand sqlCommand;
        public static SqlConnection sqlConnect;
        public static SqlDataReader sqlReader;
        public static string statement = "";
        public static string sqlConnection = @"Data Source=THE-SMITH\SQLEXPRESS;Initial Catalog=management;Integrated Security=True";

        public void OnGet()
        {
        }

        public void OnPost(UserDetais user)
        {
            try
            {
                sqlConnect = new SqlConnection(sqlConnection);
                sqlConnect.Open();

                statement = "Select* from users_details where email='" + user.email.Trim()+"'";
                sqlCommand = new SqlCommand(statement, sqlConnect);
                SqlDataAdapter dabta = new SqlDataAdapter(sqlCommand);
                DataTable dataTable = new DataTable();
                dabta.Fill(dataTable);
                if (dataTable.Rows.Count > 0)
                {
                    TempData["reg"] = "Account already exists: " + " " + user.email.Trim().ToString();
                }
                else
                {
                    Change change = new Change();
                    statement = "insert into users_details values('" + user.username.Trim() + "','"+user.email.Trim()+"','"+ change.getHash(user.psw.Trim())+"')";
                    sqlCommand = new SqlCommand(statement, sqlConnect);
                    sqlCommand.ExecuteNonQuery();
                    TempData["reg"] = "Hello " + " " + user.email.Trim().ToString()+"  "+"you are registered.";
                }
                sqlConnect.Close();
            }
            catch (Exception exc)
            {
                TempData["reg"] = exc.ToString();
            }
        }
    }
}
