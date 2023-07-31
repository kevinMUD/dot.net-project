using healthcareinsurenc.Pages.PATIENT;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace healthcareinsurenc.Pages.PATIENT
{
    public class IndexModel : PageModel
    {
        public List<Patientinfo> listpatients = new List<Patientinfo>();
        public void OnGet()
        {
            listpatients.Clear();
            try
            {
                string constring = "Data Source=WA-MUTEZINTARE\\SQLEXPRESS;Initial Catalog=healthcare;Integrated Security=True";
                using (SqlConnection con = new SqlConnection(constring))
                {
                    con.Open();
                    string querry = "SELECT * FROM Patients ";
                    using (SqlCommand cmd = new SqlCommand(querry, con))
                    {
                        using (SqlDataReader rd = cmd.ExecuteReader())
                        {
                            while (rd.Read())
                            {
                                Patientinfo info = new Patientinfo();
                                info.id = "" + rd.GetInt32(0);
                                info.name = rd.GetString(1);
                                info.email = rd.GetString(2);
                                info.phone = rd.GetString(3);
                                info.address = rd.GetString(4);
                                info.createdadat = rd.GetDateTime(5).ToString();

                                listpatients.Add(info);
                            }
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception:" + ex.Message);
            }
        }
    }
    public class Patientinfo
    {
        public string id;
        public string name;
        public string email;
        public string phone;
        public string address;
        public string createdadat;
    }
}


