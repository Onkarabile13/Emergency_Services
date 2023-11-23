using Emergency_Services.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;

namespace Emergency_Services.Pages
{
    public class BuyModel : PageModel
    {
        public double available,buy,average = 0.0;
        public void OnGet()
        {
            try
            {
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
                average = available - buy;
                TempData["available"] = average;
                RegisterControllerModel.sqlConnect.Close();
            }
            catch
            {
                TempData["assignedTotal"] = "Kindly sign in first to use this feature.";
            }

        }
        public void OnPost(PostBuy onPost)
        {
            try
            {
                RegisterControllerModel.sqlConnect = new SqlConnection(RegisterControllerModel.sqlConnection);
                RegisterControllerModel.sqlConnect.Open();

                RegisterControllerModel.statement = "Insert into Buy values('" + onPost.goodsSelection + "','" + onPost.totalDonation+ "')";
                RegisterControllerModel.sqlCommand = new SqlCommand(RegisterControllerModel.statement, RegisterControllerModel.sqlConnect);
                RegisterControllerModel.sqlCommand.ExecuteNonQuery();

                TempData["buy"] = "Item of the name: "+ onPost.goodsSelection+" "+"which cost: R"+ Convert.ToDouble(onPost.totalDonation)+" "+"will be bought.";
                RegisterControllerModel.sqlConnect.Close();
            }
            catch
            {
                TempData["buy"] = "Kindly sign in first to use this feature.";
            }

        }
    }
}
