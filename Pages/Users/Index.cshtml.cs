using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace EnockHealthCare.Pages.Users
{
    public class IndexModel : PageModel
    {
        public Users user = new Users();
        public string errorMessage = "";
        public string successMessage = "";
        public void OnGet()
        {
        }

        public void OnPost()
        {
            user.username = Request.Form["name"];
            user.userpassword = Request.Form["password"];

            if (user.username.Length == 0 || user.userpassword.Length == 0)
            {
                errorMessage = "All fields are required";
                return;
            }

            try
            {
                string conString = "Data Source=WA-MUTEZINTARE\\SQLEXPRESS;Initial Catalog=healthcare;Integrated Security=True";
                using (SqlConnection con = new SqlConnection(conString))
                {
                    con.Open();
                    string query = "select * from Users where username=@name and userpassword=@pwd";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@name", user.username);
                        cmd.Parameters.AddWithValue("@pwd", user.userpassword);
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                user.userRole = reader.GetString(3);
                                user.role_id = "" + reader.GetInt32(4);
                                Response.Redirect("/Users/LoggedIn?role=" + user.userRole + "&id=" + user.role_id);
                                return;
                            }
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
            }
        }
    }

    public class Users
    {
        public string? u_id;
        public string? username;
        public string? userpassword;
        public string? userRole;
        public string? role_id;
    }
}
