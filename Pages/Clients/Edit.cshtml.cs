using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace ClientsManager.Pages.Clients
{
    public class EditModel : PageModel
    {
        public ClientInfo clientInfo = new ClientInfo();
        public String errorMessage = "";
        public String successMessage = "";
        public void OnGet()
        {
            String id = Request.Query["id"];

            try
            {
                String connectionString = "Data Source=LUONG-LENOVO\\MSSQLSERVER07;Initial Catalog=mystore;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {

                    connection.Open();
                    String queryString = "SELECT * FROM clients WHERE id=@id";
                    using (SqlCommand command = new SqlCommand(queryString, connection))
                    {
                        command.Parameters.AddWithValue("@id", id);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {

                            clientInfo.id = "" + reader.GetInt32(0);
                            clientInfo.name = reader.GetString(1);
                            clientInfo.email = reader.GetString(2);
                            clientInfo.phone = reader.GetString(3);
                            clientInfo.address = reader.GetString(4);
                            clientInfo.created_at = reader.GetDateTime(5).ToString();
                            }
                        }

                    }
                }
            }
            catch (Exception ex)
            {

                errorMessage = ex.Message;
                return;
            }
        }

        public void OnPost()
        {
            clientInfo.name = Request.Form["name"];
            clientInfo.email = Request.Form["email"];
            clientInfo.phone = Request.Form["phone"];
            clientInfo.address = Request.Form["address"];
            clientInfo.id = Request.Form["id"];

            if (clientInfo.address.Length == 0 || clientInfo.name.Length == 0 || clientInfo.phone.Length == 0
                || clientInfo.email.Length == 0)
            {
                errorMessage = "All the fields are required";
                return;
            }

            try
            {
                String connectionString = "Data Source=LUONG-LENOVO\\MSSQLSERVER07;Initial Catalog=mystore;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sqlQuery = "UPDATE clients " +
                                      "SET name=@name, email=@email, phone=@phone, address=@address " +
                                      "WHERE (id=@id);";
                    using (SqlCommand command = new SqlCommand(sqlQuery, connection))
                    {
                        command.Parameters.AddWithValue("@name", clientInfo.name);
                        command.Parameters.AddWithValue("@email", clientInfo.email);
                        command.Parameters.AddWithValue("@phone", clientInfo.phone);
                        command.Parameters.AddWithValue("@address", clientInfo.address);
                        command.Parameters.AddWithValue("@id", clientInfo.id);

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {

                errorMessage = ex.Message;
                return;
            }

            clientInfo.name = ""; clientInfo.email = ""; clientInfo.phone = ""; clientInfo.address = "";
            successMessage = "Client edited successfully";

            Response.Redirect("/Clients/Index");
        }
    }
}
