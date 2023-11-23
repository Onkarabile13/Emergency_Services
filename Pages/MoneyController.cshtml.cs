using Emergency_Services.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using System;
using System.Reflection.PortableExecutable;

namespace Emergency_Services.Pages
{
    public class MoneyControllerModel : PageModel
    {
        public List<GetMoney> getMoney = new List<GetMoney>();
        public void OnGet()
        {
            try
            {

                RegisterControllerModel.sqlConnect = new SqlConnection(RegisterControllerModel.sqlConnection);
                RegisterControllerModel.sqlConnect.Open();

                int key = Convert.ToInt32(HttpContext.Session.GetInt32("user_id"));
                RegisterControllerModel.statement = "Select* from money_details where user_id='" + key + "'";
                RegisterControllerModel.sqlCommand= new SqlCommand(RegisterControllerModel.statement, RegisterControllerModel.sqlConnect);
                RegisterControllerModel.sqlReader = RegisterControllerModel.sqlCommand.ExecuteReader();
                while (RegisterControllerModel.sqlReader.Read())
                {
                    getMoney.Add(new GetMoney
                    {
                        id="MN-"+Convert.ToInt32(RegisterControllerModel.sqlReader["money_id"].ToString()),
                        date = RegisterControllerModel.sqlReader["donateion_date"].ToString(),
                        totalDonation = Convert.ToDouble(RegisterControllerModel.sqlReader["donated_money"].ToString()),
                        status = RegisterControllerModel.sqlReader["user_status"].ToString()
                    });
                }

                RegisterControllerModel.sqlConnect.Close();
            }
            catch
            {
                TempData["moneyError"] = "Kindly sign in first to use this feature.";
            }
        }
        public void OnPost()
        {
            
        }
    }
}
