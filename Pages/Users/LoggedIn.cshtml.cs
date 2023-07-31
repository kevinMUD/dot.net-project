using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EnockHealthCare.Pages.Users
{
    public class LoggedInModel : PageModel
    {
        public Users user = new Users();
        public void OnGet()
        {
            string role = Request.Query["role"];
            string roleId = Request.Query["id"];

            user.userRole = role;
            user.role_id = roleId;
        }
    }
}
