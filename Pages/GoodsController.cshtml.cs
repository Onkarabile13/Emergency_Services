using Emergency_Services.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;

namespace Emergency_Services.Pages
{
    public class GoodsControllerModel : PageModel
    {
        public List<GetGoods> getGoods = new List<GetGoods>();
        public void OnGet()
        {
            try { 
            RegisterControllerModel.sqlConnect = new SqlConnection(RegisterControllerModel.sqlConnection);
            RegisterControllerModel.sqlConnect.Open();

            int key = Convert.ToInt32(HttpContext.Session.GetInt32("user_id"));
            RegisterControllerModel.statement = "Select* from goods_details where user_id='" + key + "'";
            RegisterControllerModel.sqlCommand = new SqlCommand(RegisterControllerModel.statement, RegisterControllerModel.sqlConnect);
            RegisterControllerModel.sqlReader = RegisterControllerModel.sqlCommand.ExecuteReader();
            while (RegisterControllerModel.sqlReader.Read())
            {
                getGoods.Add(new GetGoods
                {
                    Id = "GD-" + Convert.ToInt32(RegisterControllerModel.sqlReader["goodId"].ToString()),
                    date = RegisterControllerModel.sqlReader["date_of_goods"].ToString(),
                    totalItem= Convert.ToInt32(RegisterControllerModel.sqlReader["items"].ToString()),
                    goodsSelection= RegisterControllerModel.sqlReader["goods_name"].ToString(),
                    otherGoods= RegisterControllerModel.sqlReader["other_goods"].ToString(),
                    goodDescription= RegisterControllerModel.sqlReader["goods_description"].ToString(),
                    status = RegisterControllerModel.sqlReader["user_status"].ToString()
                });
            }

            RegisterControllerModel.sqlConnect.Close();
        } 
            catch
            {
                TempData["moneyError"] = "Kindly sign in first to use this feature.";
            }
}
    }
}
