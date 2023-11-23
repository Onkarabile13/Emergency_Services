using Emergency_Services.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;

namespace Emergency_Services.Pages
{
    public class ItemModel : PageModel
    {
        public void OnGet()
        {
        }
        public void OnPost(PostAssignedItem onPost)
        {
            try
            {
                RegisterControllerModel.sqlConnect = new SqlConnection(RegisterControllerModel.sqlConnection);
                RegisterControllerModel.sqlConnect.Open();

                RegisterControllerModel.statement = "Insert into assign_item values('" + onPost.goodsSelection + "','" + Convert.ToInt32(Request.Query["itemId"]) + "')";
                RegisterControllerModel.sqlCommand = new SqlCommand(RegisterControllerModel.statement, RegisterControllerModel.sqlConnect);
                RegisterControllerModel.sqlCommand.ExecuteNonQuery();

                TempData["assignItem"] = "Item of "+ onPost.goodsSelection +" is assigned to active disaster.";
                RegisterControllerModel.sqlConnect.Close();
            }
            catch
            {
                TempData["assignItem"] = "Kindly sign in first to use this feature.";
            }
        }
    }
}
