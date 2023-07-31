using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace healthcareinsurenc.Pages.RECORD
{
    public class IndexModel : PageModel
    {
        public List<Records> recordsList = new List<Records>();
        public void OnGet()
        {
            try
            {
                string conString = "Data Source=WA-MUTEZINTARE\\SQLEXPRESS;Initial Catalog=healthcare;Integrated Security=True";
                using (SqlConnection con = new SqlConnection(conString))
                {
                    con.Open();
                    string query = "select * from Records";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Records record = new Records();
                                record.record_id = reader.GetInt32(0);
                                record.record_details = reader.GetString(1);
                                record.patient_id = "" + reader.GetInt32(2);
                                record.recorded_date = reader.GetDateTime(3).ToString();

                                recordsList.Add(record);
                            }
                        }
                    }
                }
            }catch (Exception ex) 
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
    public class Records
    {
        public int record_id;
        public string? record_details;
        public string? patient_id;
        public string? recorded_date;
    }
}
