using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using healthcareinsurenc.Pages.PATIENT;
using System.Data.SqlClient;

namespace healthcareinsurenc.Pages.RECORD
{
    public class createModel : PageModel
    {
        public Records rec = new Records();
        public string successMessage = "";
        public string errorMessage = "";

        public List<Patientinfo> patientList = new List<Patientinfo>();
        public void OnGet()
        {
			patientList.Clear();
			try
			{
				string conString = "Data Source=WA-MUTEZINTARE\\SQLEXPRESS;Initial Catalog=healthcare;Integrated Security=True";
				using (SqlConnection con = new SqlConnection(conString))
				{
					con.Open();
					string query = "select id, name from Patients";
					using (SqlCommand conCommand = new SqlCommand(query, con))
					{
						using (SqlDataReader reader = conCommand.ExecuteReader())
						{
							while (reader.Read())
							{
								Patientinfo patient = new Patientinfo();
								patient.id = "" + reader.GetInt32(0);
								patient.name = reader.GetString(1);

								patientList.Add(patient);
							}
						}
					}
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
			}
		}
		public void OnPost()
		{
			rec.record_details = Request.Form["details"];
			rec.patient_id = Request.Form["patient"];
			

			if (rec.record_details.Length == 0 || rec.patient_id.Length == 0)
			{
				errorMessage = "All fields are required";
				OnGet();
				return;
			}

			try
			{
				string conString = "Data Source=WA-MUTEZINTARE\\SQLEXPRESS;Initial Catalog=healthcare;Integrated Security=True";
				using (SqlConnection con = new SqlConnection(conString))
				{
					con.Open();
					string query = "insert into Records (rec_details, patient_id_fk) values (@details, @patient)";

					using (SqlCommand cmd = new SqlCommand(query, con))
					{
						cmd.Parameters.AddWithValue("@details", rec.record_details);
						cmd.Parameters.AddWithValue("@patient", int.Parse(rec.patient_id));
						

						cmd.ExecuteNonQuery();
					}
				}

			}
			catch (Exception ex)
			{
				errorMessage = ex.Message;
				return;
			}

			rec.record_details = "";
			

			successMessage = "New patient added successfully";
			Response.Redirect("/RECORD/Index");
		}
	}
}
