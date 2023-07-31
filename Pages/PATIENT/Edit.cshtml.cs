using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
using System.Security.Cryptography.X509Certificates;

namespace healthcareinsurenc.Pages.PATIENT
{
    public class EditModel : PageModel
    {
        public Patientinfo pinfo = new Patientinfo();
        public string errormessage = "";
        public string successmessage = "";
        public void OnGet()
        {
            string id = Request.Query["id"];
            try
            {
                string constring = "Data Source=WA-MUTEZINTARE\\SQLEXPRESS;Initial Catalog=healthcare;Integrated Security=True";
                using (SqlConnection con = new SqlConnection(constring))
                {
                    con.Open();
                    string querry = "SELECT * FROM Patients where id=@id ";
                    using (SqlCommand cmd = new SqlCommand(querry, con))
                    {
                        cmd.Parameters.AddWithValue("@id", id);
                        using (SqlDataReader rd = cmd.ExecuteReader())
                        {
                            while (rd.Read())
                            {
                               
                                pinfo.id = "" + rd.GetInt32(0);
                                pinfo.name = rd.GetString(1);
                                pinfo.email = rd.GetString(2);
                                pinfo.phone = rd.GetString(3);
                                pinfo.address = rd.GetString(4);

                                
                            }
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                errormessage = ex.Message;
                return;
            }
        }
        public void OnPost() 
        {
            pinfo.id= Request.Form["id"];
            pinfo.name = Request.Form["name"];
            pinfo.email = Request.Form["email"];
            pinfo.phone = Request.Form["phone"];
            pinfo.address = Request.Form["address"];

            if (pinfo.name.Length == 0 || pinfo.email.Length == 0 ||
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
                    string querry = "UPDATE Patients SET name=@name,email=@email,phone=@phone,address=@address WHERE id=@id";
                    using (SqlCommand cmd = new SqlCommand(querry, con))
                    {
                        cmd.Parameters.AddWithValue("@id", pinfo.id);
                        cmd.Parameters.AddWithValue("@name", pinfo.name);
                        cmd.Parameters.AddWithValue("@email", pinfo.email);
                        cmd.Parameters.AddWithValue("@phone", pinfo.phone);
                        cmd.Parameters.AddWithValue("@address", pinfo.address);
                        cmd.ExecuteNonQuery();
                    }

                }
            }
            catch (Exception ex)
            {
                errormessage = ex.Message;
                return;
            }
            pinfo.name = ""; pinfo.email = ""; pinfo.id = "";
            pinfo.address = ""; pinfo.address = "";
            successmessage = " patient successfully added";
            Response.Redirect("/PATIENT/Index");
        }
    }
}
