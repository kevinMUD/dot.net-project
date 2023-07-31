using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace healthcareinsurenc.Pages.PATIENT
{
    public class createModel : PageModel
    {
        public Patientinfo pinfo = new Patientinfo();
        public string errormessage = "";
        public string successmessage = "";
        public void OnGet()
        {
        }
        public void OnPost() 
        {
            pinfo.name = Request.Form["name"];  
            pinfo.email = Request.Form["email"];  
            pinfo.phone = Request.Form["phone"];  
            pinfo.address = Request.Form["address"];  
        
        if(pinfo.name.Length == 0 || pinfo.email.Length == 0 ||
        pinfo.phone.Length == 0 || pinfo.address.Length == 0)
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
                    string querry = "INSERT INTO Patients(name,email,phone,address) VALUES (@name,@email,@phone,@address)";
                    using (SqlCommand cmd = new SqlCommand(querry, con))
                    {
                       cmd.Parameters.AddWithValue("@name", pinfo.name);
                       cmd.Parameters.AddWithValue("@email",pinfo.email); 
                       cmd.Parameters.AddWithValue("@phone",pinfo.phone); 
                       cmd.Parameters.AddWithValue("@address",pinfo.address); 
                        cmd.ExecuteNonQuery();
                    }

                }
            } catch (Exception ex)
            {
                errormessage = ex.Message;
                return;
            }
            pinfo.name = "";pinfo.email = "";
            pinfo.address = "";pinfo.address = "";
            successmessage = " patient successfully added";
            Response.Redirect("/PATIENT/Index");
        }

        
    }
}
