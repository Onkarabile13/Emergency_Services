using Emergency_Services.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;

namespace Emergency_Services.Pages
{
    public class ShowGoodsRecordsModel : PageModel
    {
        public void OnGet()
        {
        }
        public void OnPost(PostGoods onPost)
        {
            try
            {
                RegisterControllerModel.sqlConnect = new SqlConnection(RegisterControllerModel.sqlConnection);
                RegisterControllerModel.sqlConnect.Open();

                int key = Convert.ToInt32(HttpContext.Session.GetInt32("user_id"));
                //date, items, name,other,description,status, key
                RegisterControllerModel.statement = "Insert into goods_details values('"+onPost.date.Trim()+"','"+Convert.ToInt32(onPost.totalItem)+"','"+onPost.goodsSelection+"','"+onPost.otherGoods+"','"+onPost.goodDescription+"','"+onPost.status+"','"+key+"')";
                RegisterControllerModel.sqlCommand = new SqlCommand(RegisterControllerModel.statement, RegisterControllerModel.sqlConnect);
                RegisterControllerModel.sqlCommand.ExecuteNonQuery();

                TempData["goods"] = "Your data is being saved.";
                RegisterControllerModel.sqlConnect.Close();
            }
            catch
            {
                TempData["goods"] = "Kindly sign in first to use this feature.";
            }

        }
    }
}
