using healthcareinsurenc.Pages.PATIENT;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace healthcareinsurenc.Pages.specialist
{
    public class createModel : PageModel
    {
        public specialistinfo piinfo = new specialistinfo();
        public string errormessage = "";
        public string successmessage = "";
        public void OnGet()
        {
        }
        public void OnPost()
        {
            piinfo.name = Request.Form["name"];
            piinfo.email = Request.Form["email"];
            piinfo.phone = Request.Form["phone"];
            piinfo.address = Request.Form["address"];
            piinfo.specialization = Request.Form["specialization"];

            if (piinfo.name.Length == 0 || piinfo.email.Length == 0 ||
            piinfo.phone.Length == 0 || piinfo.address.Length == 0 || piinfo.specialization.Length == 0)
            {
                errormessage = "All field are required";
                return;
            }
            try
            {
                string constring = "Data Source=WA-MUTEZINTARE\\SQLEXPRESS;Initial Catalog=healthcare;Integrated Security=True";
                using (SqlConnection con = new SqlConnection(constring))
                {
                    con.Open();
                    string querry = "INSERT INTO Specialist(name,email,phone,address,specialization) VALUES (@name,@email,@phone,@address,@specialization)";
                    using (SqlCommand cmd = new SqlCommand(querry, con))
                    {
                        cmd.Parameters.AddWithValue("@name", piinfo.name);
                        cmd.Parameters.AddWithValue("@email", piinfo.email);
                        cmd.Parameters.AddWithValue("@phone", piinfo.phone);
                        cmd.Parameters.AddWithValue("@address", piinfo.address);
                        cmd.Parameters.AddWithValue("@specialization", piinfo.specialization);
                        cmd.ExecuteNonQuery();
                    }

                }
            }
            catch (Exception ex)
            {
                errormessage = ex.Message;
                return;
            }
            piinfo.name = ""; piinfo.email = "";
            piinfo.address = ""; piinfo.address = ""; piinfo.specialization = "";
            successmessage = " specialist successfully added";
            Response.Redirect("/specialist/Index");
        }
    }
}
