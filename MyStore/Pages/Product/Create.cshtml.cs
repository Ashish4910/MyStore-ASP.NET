using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.Common;
using System.Data.SqlClient;
using System.Net.Security;

namespace MyStore.Pages.Product
{
    public class CreateModel : PageModel
    {
        public ProductInfo productInfo = new ProductInfo();
        public String errorMessage = "";
        public String successMessage = "";
		public DateTime? Cre_DtTm { get; set; }


		public void OnGet()
        {
        }
        public void OnPost()
        { 
         productInfo.name= Request.Form["name"];
			productInfo.email = Request.Form["email"];
			productInfo.phone = Request.Form["phone "];
			productInfo.address = Request.Form["address"];


            if (productInfo.name.Length == 0 || productInfo.email.Length == 0 ||
                productInfo.phone.Length == 0 || productInfo.address.Length == 0)
            {
                errorMessage = "All the field are required";
                return;
            }
            // save the  new Client into the database
            
				try
				{

					String connectionString = "Data Source=.\\sqlexpress;Initial Catalog=mystore;Integrated Security=True";
					using (SqlConnection connection = new SqlConnection(connectionString))
					{
						connection.Open();
						String sql = "INSERT INTO product " +
							"(name,email,phone,address) VALUES" +
							"(@name,@email,@phone,@address);";
						using (SqlCommand command = new SqlCommand(sql, connection))
						{
							command.Parameters.AddWithValue("@name", productInfo.name);
							command.Parameters.AddWithValue("@email", productInfo.email);
							command.Parameters.AddWithValue("@phone", productInfo.phone);
							command.Parameters.AddWithValue("@address", productInfo.address);

							command.ExecuteNonQuery();


						}
					}
				}
				catch (Exception ex)
				{
					errorMessage = ex.Message;
					return;
				}
				productInfo.name = ""; productInfo.email = "";productInfo.phone = ""; productInfo.address = "";
				successMessage = " New Client Added Correctly";


				Response.Redirect("/Product/Index");

			}
           

		}
    }


