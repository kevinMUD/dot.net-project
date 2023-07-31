using healthcareinsurenc.Pages.PATIENT;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace healthcareinsurenc.Pages.specialist
{
    public class IndexModel : PageModel
    {
		public List<specialistinfo> listspecialist = new List<specialistinfo>();
		public void OnGet()
        {
			listspecialist.Clear();
			
			try
			{
				string constring = "Data Source=WA-MUTEZINTARE\\SQLEXPRESS;Initial Catalog=healthcare;Integrated Security=True";
				using (SqlConnection con = new SqlConnection(constring))
				{
					con.Open();
					string querry = "SELECT * FROM Specialist ";
					using (SqlCommand cmd = new SqlCommand(querry, con))
					{
						using (SqlDataReader rd = cmd.ExecuteReader())
						{
							while (rd.Read())
							{
								specialistinfo info = new specialistinfo();
								info.id = "" + rd.GetInt32(0);
								info.name = rd.GetString(1);
								info.email = rd.GetString(2);
								info.phone = rd.GetString(3);
								info.address = rd.GetString(4);
								info.specialization = rd.GetString(5);

								listspecialist.Add(info);
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
	public class specialistinfo
	{
		public string id;
		public string name;
		public string email;
		public string phone;
		public string address;
		public string specialization;
	}
}
