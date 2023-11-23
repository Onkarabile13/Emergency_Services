using Emergency_Services.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;

namespace Emergency_Services.Pages
{
    public class ShowMoneyRecordsModel : PageModel
    {
        public void OnGet()
        {
        }
        public void OnPost(PostMoney onPost) 
        {
            try
            {
                RegisterControllerModel.sqlConnect = new SqlConnection(RegisterControllerModel.sqlConnection);
                RegisterControllerModel.sqlConnect.Open();

                int key = Convert.ToInt32(HttpContext.Session.GetInt32("user_id"));
                RegisterControllerModel.statement = "Insert into money_details values('" + onPost.date.Trim().ToString() + "','" + Convert.ToDouble(onPost.totalDonation) + "','" + onPost.status.Trim() + "','" + key + "')";
                RegisterControllerModel.sqlCommand = new SqlCommand(RegisterControllerModel.statement, RegisterControllerModel.sqlConnect);
                RegisterControllerModel.sqlCommand.ExecuteNonQuery();

                TempData["money"] = "Your data is being saved.";
                RegisterControllerModel.sqlConnect.Close();
            }
            catch
            {
                TempData["money"] = "Kindly sign in first to use this feature.";
            }
        }
    }
}
