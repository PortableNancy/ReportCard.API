using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ReportCard.Core.Models;
using System.Data;
using System.Data.SqlClient;

namespace ReportCard.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private  SqlConnection Connect;
        public UserController(IConfiguration configuration)
        {
            _configuration = configuration;
            Connect = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
        }
        [HttpPost]
        public string Registration(User user)
        {
            string response = " ";
            if (user != null)
            {
                SqlCommand cmd = new SqlCommand("sp_RegisterUser", Connect);
                cmd.CommandType = CommandType.StoredProcedure;
              
                cmd.Parameters.AddWithValue("@UserName",user.userName);
                cmd.Parameters.AddWithValue("@Password", user.password);
                cmd.Parameters.AddWithValue("@Email", user.email);
                cmd.Parameters.AddWithValue("@IsActive", user.Isactive);
      

                Connect.Open();
                int x = cmd.ExecuteNonQuery();
                Connect.Close();
                if (x > 0)
                {
                    response = "User sucessfully Created";
                }
                else
                {
                    response = "Failed";
                }
            }
            return response;
        }

        [HttpPost("Login")]
        public string Login(string password, string email, int IsActive)
        {
            SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM UserRegistration WHERE Password = '"+password+"' AND Email = '"+email+"' AND IsActive = 1", Connect);
           
            DataTable table = new DataTable();
            adapter.Fill(table);
            if(table.Rows.Count > 0)
            {
                return "User Exist";
            }
            else
            {
                return "User does not Exist";
            }

        }
    }
}
