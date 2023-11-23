using Emergency_Services.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;

namespace Emergency_Services.Pages
{
    public class DisasterControllerModel : PageModel
    {
        public List<GetDisaster> getGoods = new List<GetDisaster>();
        public void OnGet()
        {
            try
            {
                RegisterControllerModel.sqlConnect = new SqlConnection(RegisterControllerModel.sqlConnection);
                RegisterControllerModel.sqlConnect.Open();

                int key = Convert.ToInt32(HttpContext.Session.GetInt32("user_id"));
                RegisterControllerModel.statement = "Select* from disaster_details where user_id='" + key + "'";
                RegisterControllerModel.sqlCommand = new SqlCommand(RegisterControllerModel.statement, RegisterControllerModel.sqlConnect);
                RegisterControllerModel.sqlReader = RegisterControllerModel.sqlCommand.ExecuteReader();
                while (RegisterControllerModel.sqlReader.Read())
                {
                    getGoods.Add(new GetDisaster
                    {
                        Id = "DS-" + Convert.ToInt32(RegisterControllerModel.sqlReader["disaster_id"].ToString()),
                        disasterStartDate= RegisterControllerModel.sqlReader["date_start"].ToString(),
                        disasterEndDate= RegisterControllerModel.sqlReader["end_start"].ToString(),
                        disasterLocation= RegisterControllerModel.sqlReader["location_disater"].ToString(),
                        disasterDescription= RegisterControllerModel.sqlReader["disaster_description"].ToString(),
                        goodsSelection= RegisterControllerModel.sqlReader["required_aid"].ToString(),
                        status = RegisterControllerModel.sqlReader["user_status"].ToString()
                    });
                }

                RegisterControllerModel.sqlConnect.Close();
            }
            catch
            {
                TempData["DisasterControll"] = "Kindly sign in first to use this feature.";
            }
        }
    }
}
