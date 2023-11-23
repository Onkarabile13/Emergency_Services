using Emergency_Services.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;

namespace Emergency_Services.Pages
{
    public class AuthHomeModel : PageModel
    {
        public double available, buy, average, goods, money= 0.0;
        public List<GetDisaster> getGoods = new List<GetDisaster>();
        public void OnGet()
        {
            try
            {
                RegisterControllerModel.sqlConnect = new SqlConnection(RegisterControllerModel.sqlConnection);
                RegisterControllerModel.sqlConnect.Open();

                RegisterControllerModel.statement = "Select* from disaster_details";
                RegisterControllerModel.sqlCommand = new SqlCommand(RegisterControllerModel.statement, RegisterControllerModel.sqlConnect);
                RegisterControllerModel.sqlReader = RegisterControllerModel.sqlCommand.ExecuteReader();
                while (RegisterControllerModel.sqlReader.Read())
                {
                    getGoods.Add(new GetDisaster
                    {
                        Id =""+Convert.ToInt32(RegisterControllerModel.sqlReader["disaster_id"].ToString()),
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

                RegisterControllerModel.statement = "select sum(donated_money) as available from money_details";
                RegisterControllerModel.sqlCommand = new SqlCommand(RegisterControllerModel.statement, RegisterControllerModel.sqlConnect);
                RegisterControllerModel.sqlReader = RegisterControllerModel.sqlCommand.ExecuteReader();
                while (RegisterControllerModel.sqlReader.Read())
                {
                    available = Convert.ToDouble(RegisterControllerModel.sqlReader["available"].ToString());
                }

                RegisterControllerModel.sqlConnect.Close();

                RegisterControllerModel.sqlConnect = new SqlConnection(RegisterControllerModel.sqlConnection);
                RegisterControllerModel.sqlConnect.Open();

                RegisterControllerModel.statement = "select sum(amount) as available from Buy";
                RegisterControllerModel.sqlCommand = new SqlCommand(RegisterControllerModel.statement, RegisterControllerModel.sqlConnect);
                RegisterControllerModel.sqlReader = RegisterControllerModel.sqlCommand.ExecuteReader();
                while (RegisterControllerModel.sqlReader.Read())
                {
                    buy = Convert.ToDouble(RegisterControllerModel.sqlReader["available"].ToString());
                }
                RegisterControllerModel.sqlConnect.Close();

                RegisterControllerModel.sqlConnect = new SqlConnection(RegisterControllerModel.sqlConnection);
                RegisterControllerModel.sqlConnect.Open();

                RegisterControllerModel.statement = "select sum(moneys) as available from assign_money";
                RegisterControllerModel.sqlCommand = new SqlCommand(RegisterControllerModel.statement, RegisterControllerModel.sqlConnect);
                RegisterControllerModel.sqlReader = RegisterControllerModel.sqlCommand.ExecuteReader();
                while (RegisterControllerModel.sqlReader.Read())
                {
                    money = Convert.ToDouble(RegisterControllerModel.sqlReader["available"].ToString());
                }
          
                RegisterControllerModel.sqlConnect.Close();



                average = available - (buy+ money);
                TempData["available"] = average;
            }
            catch
            {
                TempData["DisasterControll"] = "Kindly sign in first to use this feature.";
            }
        }
    }
}
