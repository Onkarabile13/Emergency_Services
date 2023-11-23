using Emergency_Services.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;

namespace Emergency_Services.Pages
{
    public class AssignedTotalModel : PageModel
    {
        public List<GetAssignMoney> getGoods = new List<GetAssignMoney>();
        public void OnGet()
        {
            try
            {
                RegisterControllerModel.sqlConnect = new SqlConnection(RegisterControllerModel.sqlConnection);
                RegisterControllerModel.sqlConnect.Open();

                RegisterControllerModel.statement = "select*, sum(moneys) as m from assign_money group by asign_id,moneys,disaster_id";
                RegisterControllerModel.sqlCommand = new SqlCommand(RegisterControllerModel.statement, RegisterControllerModel.sqlConnect);
                RegisterControllerModel.sqlReader = RegisterControllerModel.sqlCommand.ExecuteReader();
                while (RegisterControllerModel.sqlReader.Read())
                {
                    getGoods.Add(new GetAssignMoney
                    {
                        Id = "MD" + Convert.ToInt32(RegisterControllerModel.sqlReader["asign_id"].ToString()),
                        totalDonation =Convert.ToDouble(RegisterControllerModel.sqlReader["moneys"].ToString()),
                        tot= Convert.ToDouble(RegisterControllerModel.sqlReader["m"].ToString())
                    });
                }

                RegisterControllerModel.sqlConnect.Close();
            }
            catch
            {
                TempData["assignedTotal"] = "Kindly sign in first to use this feature.";
            }
        }
    }
}
