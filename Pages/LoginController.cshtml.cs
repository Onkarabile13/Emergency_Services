using Emergency_Services.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using System.Data;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using System.Reflection.PortableExecutable;

namespace Emergency_Services.Pages
{
    public class LoginControllerModel : PageModel
    {
        public void OnGet()
        {
        }

        public void OnPost(Userlogin user)
        {
            try
            {
               RegisterControllerModel.sqlConnect = new SqlConnection(RegisterControllerModel.sqlConnection);
               RegisterControllerModel.sqlConnect.Open();

                Change change = new Change();
                RegisterControllerModel.statement = "Select* from users_details where email='" + user.email.Trim() + "' and password='"+ change.getHash(user.psw.Trim()) + "'";
                RegisterControllerModel.sqlCommand = new SqlCommand(RegisterControllerModel.statement, RegisterControllerModel.sqlConnect);
                SqlDataAdapter dabta = new SqlDataAdapter(RegisterControllerModel.sqlCommand);
                DataTable dataTable = new DataTable();
                dabta.Fill(dataTable);
                if (dataTable.Rows.Count > 0)
                {
                    TempData["login"] = "Hello" + " " + user.email.Trim().ToString()+" "+" you have logged in.";
                    RegisterControllerModel.sqlReader = RegisterControllerModel.sqlCommand.ExecuteReader();

                    while (RegisterControllerModel.sqlReader.Read())
                    {
                        HttpContext.Session.SetInt32("user_id", Convert.ToInt32(RegisterControllerModel.sqlReader["user_id"].ToString()));
                    }
                }
                else
                {
                    TempData["login"] = " Username or password is incorrect. Please try again.";
                }
                RegisterControllerModel.sqlConnect.Close();
            }
            catch (Exception exc)
            {
                TempData["login"] = exc.ToString();
            }

        }
    }
}
