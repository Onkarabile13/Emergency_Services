using Emergency_Services.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;

namespace Emergency_Services.Pages
{
    public class ItemsPlayModel : PageModel
    {

        public List<GetAssignedItem> getGoods = new List<GetAssignedItem>();
        public void OnGet()
        {
            try
            {
                RegisterControllerModel.sqlConnect = new SqlConnection(RegisterControllerModel.sqlConnection);
                RegisterControllerModel.sqlConnect.Open();

                RegisterControllerModel.statement = "select* from assign_item";
                RegisterControllerModel.sqlCommand = new SqlCommand(RegisterControllerModel.statement, RegisterControllerModel.sqlConnect);
                RegisterControllerModel.sqlReader = RegisterControllerModel.sqlCommand.ExecuteReader();
                while (RegisterControllerModel.sqlReader.Read())
                {
                    getGoods.Add(new GetAssignedItem
                    {
                        Id = "GD" + Convert.ToInt32(RegisterControllerModel.sqlReader["asign_id"].ToString()),
                        goodsSelection = RegisterControllerModel.sqlReader["item"].ToString()
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
