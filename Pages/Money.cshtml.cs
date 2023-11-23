using Emergency_Services.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;

namespace Emergency_Services.Pages
{
    public class MoneyModel : PageModel
    {
        public void OnGet()
        {
        }
        public void OnPost(PostAssignMoney onPost)
        {

            try
            {
                RegisterControllerModel.sqlConnect = new SqlConnection(RegisterControllerModel.sqlConnection);
                RegisterControllerModel.sqlConnect.Open();

                RegisterControllerModel.statement = "Insert into assign_money values('"+Convert.ToDouble(onPost.totalDonation) +"','"+ Convert.ToInt32(Request.Query["moneyId"]) + "')";
                RegisterControllerModel.sqlCommand = new SqlCommand(RegisterControllerModel.statement, RegisterControllerModel.sqlConnect);
                RegisterControllerModel.sqlCommand.ExecuteNonQuery();

                TempData["assignMoney"] = "total of" +" R" + Convert.ToDouble(onPost.totalDonation)+"  is assigned to active disaster." ;
                RegisterControllerModel.sqlConnect.Close();
            }
            catch
            {
                TempData["assignMoney"] = "Kindly sign in first to use this feature.";
            }
        }
    }
}
