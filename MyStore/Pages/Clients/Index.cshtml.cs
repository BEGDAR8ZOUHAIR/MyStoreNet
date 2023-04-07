using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
using System.Data.SqlTypes;

namespace MyStore.Pages.Clients
{
    public class IndexModel : PageModel
    {
        public List<CLientInfo> listClients = new List<CLientInfo>();       
        public void OnGet()
        {
            try
            {
                String connectionString = "Data Source=.\\;Initial Catalog=mystore;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString)) 
                {
                    connection.Open();
                    String sql = "SELECT * FROM clients";
                    using (SqlCommand command = new SqlCommand(sql, connection)) 
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                CLientInfo clientinfo = new CLientInfo();
                                // id is integer but  clientinfo is string "" to convert in to string 
                                clientinfo.id = "" + reader.GetInt32(0);
                                clientinfo.name = reader.GetString(1);
                                clientinfo.email = reader.GetString(2);
                                clientinfo.phone = reader.GetString(3);
                                clientinfo.adress = reader.GetString(4);
                                clientinfo.created_at = reader.GetDateTime(5).ToString();

                                listClients.Add(clientinfo);
                            }
                        }
                    }

                }

            }
            catch (Exception ex)
            { 

                Console.WriteLine("Exeption: " + ex.ToString());
            }
        }
    }
    public class  CLientInfo
    {
        public string id;
        public string name;
        public string email;
        public string phone;
        public string adress;
        public string created_at;

    }
}
