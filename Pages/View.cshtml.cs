using Emergency_Services.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using System.Reflection.PortableExecutable;
using System;

namespace Emergency_Services.Pages
{
    public class ViewModel : PageModel
    {
        public List<GetDisaster> getAll = new List<GetDisaster>();
        public double money, money2, money3 = 0.0;
        public void OnGet()
        {
            try
            {
                RegisterControllerModel.sqlConnect = new SqlConnection(RegisterControllerModel.sqlConnection);
                RegisterControllerModel.sqlConnect.Open();

                int key = Convert.ToInt32(HttpContext.Session.GetInt32("user_id"));
                RegisterControllerModel.statement = "Select* from disaster_details";
                RegisterControllerModel.sqlCommand = new SqlCommand(RegisterControllerModel.statement, RegisterControllerModel.sqlConnect);
                RegisterControllerModel.sqlReader = RegisterControllerModel.sqlCommand.ExecuteReader();
                while (RegisterControllerModel.sqlReader.Read())
                {
                    getAll.Add(new GetDisaster
                    {
                        Id = "DS-" + Convert.ToInt32(RegisterControllerModel.sqlReader["disaster_id"].ToString()),
                        disasterStartDate = RegisterControllerModel.sqlReader["date_start"].ToString(),
                        disasterEndDate = RegisterControllerModel.sqlReader["end_start"].ToString(),
                        disasterLocation = RegisterControllerModel.sqlReader["location_disater"].ToString(),
                        disasterDescription = RegisterControllerModel.sqlReader["disaster_description"].ToString(),
                        goodsSelection = RegisterControllerModel.sqlReader["required_aid"].ToString(),
                        status = RegisterControllerModel.sqlReader["user_status"].ToString()
                    });
                }

                RegisterControllerModel.sqlConnect.Close();


                RegisterControllerModel.sqlConnect = new SqlConnection(RegisterControllerModel.sqlConnection);
                RegisterControllerModel.sqlConnect.Open();
                RegisterControllerModel.statement = "select sum(donated_money) as Amount_Sum from money_details";
                RegisterControllerModel.sqlCommand = new SqlCommand(RegisterControllerModel.statement, RegisterControllerModel.sqlConnect);
                RegisterControllerModel.sqlReader = RegisterControllerModel.sqlCommand.ExecuteReader();
                while (RegisterControllerModel.sqlReader.Read())
                {
                    money = Convert.ToDouble(RegisterControllerModel.sqlReader["Amount_Sum"].ToString());
                }
                RegisterControllerModel.sqlConnect.Close();

                TempData["tempMoney"] = money;

                RegisterControllerModel.sqlConnect.Close();


                RegisterControllerModel.sqlConnect = new SqlConnection(RegisterControllerModel.sqlConnection);
                RegisterControllerModel.sqlConnect.Open();
                RegisterControllerModel.statement = "select COUNT(*) as amount_sum from goods_details";
                RegisterControllerModel.sqlCommand = new SqlCommand(RegisterControllerModel.statement, RegisterControllerModel.sqlConnect);
                RegisterControllerModel.sqlReader = RegisterControllerModel.sqlCommand.ExecuteReader();
                while (RegisterControllerModel.sqlReader.Read())
                {
                    money2 = Convert.ToDouble(RegisterControllerModel.sqlReader["amount_sum"].ToString());
                }
                RegisterControllerModel.sqlConnect.Close();

                TempData["tempMoney2"] = money2;


                RegisterControllerModel.sqlConnect = new SqlConnection(RegisterControllerModel.sqlConnection);
                RegisterControllerModel.sqlConnect.Open();
                RegisterControllerModel.statement = "select COUNT(*) as amount_sum from disaster_details";
                RegisterControllerModel.sqlCommand = new SqlCommand(RegisterControllerModel.statement, RegisterControllerModel.sqlConnect);
                RegisterControllerModel.sqlReader = RegisterControllerModel.sqlCommand.ExecuteReader();
                while (RegisterControllerModel.sqlReader.Read())
                {
                    money3 = Convert.ToDouble(RegisterControllerModel.sqlReader["amount_sum"].ToString());
                }
                RegisterControllerModel.sqlConnect.Close();

                TempData["tempMoney3"] = money3;
            }
            catch
            {
                TempData["getAll"] = "Kindly sign in first to use this feature.";
            }
        }
    }
}
