using System.Data.SqlClient;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.Common;
using System.Net.Security;

namespace MyStore.Pages.Product
{

    public class EditModel : PageModel
    {
        public ProductInfo productInfo = new ProductInfo();
        public String errorMessage = "";
        public String successMessage = "";

        public void OnGet()
        {
            String id = Request.Query["id"];

            try
            {
                String comnectionString = "Data Source=.\\sqlexpress;Initial Catalog=mystore;Integrated Security=True";
                using(SqlConnection connection = new SqlConnection(comnectionString)) 
                {
                 connection.Open();
                    String sql = "SELECT * FROM product WHERE id=@id";
                    using (SqlCommand command = new SqlCommand(sql, connection)) 
                    {
                        command.Parameters.AddWithValue("@id", id);
                        using(SqlDataReader reader = command.ExecuteReader()) 
                        {
                         if(reader.Read())
                            {
                                productInfo.id = "" + reader.GetInt32(0);
                                productInfo.name=reader.GetString(1);
                                productInfo.email = reader.GetString(2);
                                productInfo.phone = reader.GetString(3);
                                productInfo.address = reader.GetString(4);
                            }
                        }
                    }
                }


			}
            catch(Exception ex) 
            {
                errorMessage = ex.Message;
            }
        }
        public void OnPost() 
        {
            productInfo.id = Request.Form["id"];
            productInfo.name = Request.Form["name"];
            productInfo.email = Request.Form["email"];
            productInfo.phone = Request.Form["phone"];
            productInfo.address = Request.Form["address"];

            if(productInfo.id.Length == 0 || productInfo.name.Length ==0 ||
                productInfo.email.Length == 0 ||
                productInfo.address.Length == 0)
                    {
                errorMessage = "All field are Required ";
                return;

                     
            }

            try
            {

				String connectionString  = "Data Source=.\\sqlexpress;Initial Catalog=mystore;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "UPDATE product" +
                            "SET name=@name, email=@email, phone=@phone, address=@addre " +
                           " WHERE id = @id";






                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@name", productInfo.name);

                        command.Parameters.AddWithValue("@email", productInfo.email);

                        command.Parameters.AddWithValue("@phone", productInfo.phone);
                        command.Parameters.AddWithValue("@address", productInfo.address);

                        command.Parameters.AddWithValue("@id", productInfo.id);




                        command.ExecuteNonQuery();

                    }


                }





			}
			catch (Exception ex)
            {
                errorMessage = ex.Message;
            }
            Response.Redirect("/Product/Index");
        }
    }
}
