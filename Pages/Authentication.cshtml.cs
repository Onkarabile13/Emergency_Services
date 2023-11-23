using Emergency_Services.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Emergency_Services.Pages
{
    public class AuthenticationModel : PageModel
    {
        public void OnGet()
        {
        }
        public void OnPost(Admin admin)
        {
            if (admin.email.Trim() == "admin" && admin.psw.Trim() == "12345")
            {
                TempData["admin"] = "Hello Admin, Welcome";
            }
            else 
            {
                TempData["admin"] = "Username or password is incorrect. Please try again.";
            }
        }
    }
}
