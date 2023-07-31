using healthcareinsurenc.Pages.PATIENT;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace healthcareinsurenc.Pages.specialist
{
    public class editModel : PageModel
    {
        public specialistinfo piinfo = new specialistinfo();
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
                    string querry = "SELECT * FROM Specialist where id=@id ";
                    using (SqlCommand cmd = new SqlCommand(querry, con))
                    {
                        cmd.Parameters.AddWithValue("@id", id);
                        using (SqlDataReader rd = cmd.ExecuteReader())
                        {
                            while (rd.Read())
                            {

                                piinfo.id = "" + rd.GetInt32(0);
                                piinfo.name = rd.GetString(1);
                                piinfo.email = rd.GetString(2);
                                piinfo.phone = rd.GetString(3);
                                piinfo.address = rd.GetString(4);
                                piinfo.specialization = rd.GetString(5);


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
            piinfo.id = Request.Form["id"];
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
                    string querry = "UPDATE Specialist SET name=@name,email=@email,phone=@phone,address=@address,specialization=@specialization WHERE id=@id";
                    using (SqlCommand cmd = new SqlCommand(querry, con))
                    {
                        cmd.Parameters.AddWithValue("@id", piinfo.id);
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
            piinfo.name = ""; piinfo.email = ""; piinfo.id = "";
            piinfo.address = ""; piinfo.address = ""; piinfo.specialization = "";
            successmessage = " patient successfully added";
            Response.Redirect("/specialist/Index");
        }
    }
}
