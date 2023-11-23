using Emergency_Services.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;

namespace Emergency_Services.Pages
{
    public class ShowDisasterRecordsModel : PageModel
    {
        public void OnGet()
        {
        }
        public void OnPost(PostDisaster onPost)
        {
            try
            {
                RegisterControllerModel.sqlConnect = new SqlConnection(RegisterControllerModel.sqlConnection);
                RegisterControllerModel.sqlConnect.Open();

                int key = Convert.ToInt32(HttpContext.Session.GetInt32("user_id"));
                //date_start, enddate, location, description, required aid, status, key
                RegisterControllerModel.statement = "Insert into disaster_details values('"+onPost.disasterStartDate.Trim().ToString()+"', '"+ onPost.disasterEndDate.Trim().ToString() + "','"+ onPost.disasterLocation.Trim().ToString() + "','"+ onPost.disasterDescription.Trim().ToString() + "','"+ onPost.goodsSelection.Trim().ToString() + "','"+ onPost.status.Trim().ToString() + "','"+key+"')";
                RegisterControllerModel.sqlCommand = new SqlCommand(RegisterControllerModel.statement, RegisterControllerModel.sqlConnect);
                RegisterControllerModel.sqlCommand.ExecuteNonQuery();

                TempData["disaster"] = "Your data is being saved.";
                RegisterControllerModel.sqlConnect.Close();
            }
            catch
            {
                TempData["disaster"] = "Kindly sign in first to use this feature.";
            }
        }
    }
}
