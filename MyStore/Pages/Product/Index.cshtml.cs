using System.Data.SqlClient;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MyStore.Pages.Product
{
    public class IndexModel : PageModel
    {

        public List<ProductInfo>listProduct = new List<ProductInfo>();
        public void OnGet()
        {
            try
            {
                String connectionString = "Data Source=.\\sqlexpress;Initial Catalog=mystore;Integrated Security=True";
             using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "SELECT * FROM product";
                    using(SqlCommand command = new SqlCommand(sql,connection))
                    {
                       using(SqlDataReader reader = command.ExecuteReader())
                        {
                            while(reader.Read())
                            {
                                ProductInfo productInfo = new ProductInfo();
                                productInfo.id = "" + reader.GetInt32(0);
								productInfo.name = reader.GetString(1);
								productInfo.email= reader.GetString(2);
								productInfo.phone = reader.GetString(3);
								productInfo.address= reader.GetString(4);
								productInfo.created_at= reader.GetDateTime(5).ToString();  


                                listProduct.Add(productInfo);

							}
                        }
                    }
                }
			}
            catch(Exception ex) 
            {
                           
            }
        }
    }
    public class ProductInfo
    {
        public String id;
        public String name;
        public String email;
        public String phone;
        public String address;
        public String created_at;
    }
}
