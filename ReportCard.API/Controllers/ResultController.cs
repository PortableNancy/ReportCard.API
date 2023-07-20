using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ReportCard.Core.Models;
using System.Data;
using System.Data.SqlClient;

namespace ReportCard.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ResultController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private SqlConnection con;
        public ResultController(IConfiguration configuration)
        {
            _configuration = configuration;
            con = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));

        }

        Result result = new Result();
        [HttpPost("Upload-result")]
        public string UploadNewResult(Result res)
        {
            string response = " ";
            if (res != null)
            {
                SqlCommand cmd = new SqlCommand("Result_New", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@StudentID", res.studentId);
                cmd.Parameters.AddWithValue("@studentName", res.studentName);
                cmd.Parameters.AddWithValue("@Mathematics", res.Mathematics);
                cmd.Parameters.AddWithValue("@English", res.English);
                cmd.Parameters.AddWithValue("@Chemistry", res.Chemistry);
                cmd.Parameters.AddWithValue("@Biology", res.Biology);
                cmd.Parameters.AddWithValue("@Economics", res.Economics);
                cmd.Parameters.AddWithValue("@History", res.History);
                cmd.Parameters.AddWithValue("@StudentTotal", res.studentTotal);
                cmd.Parameters.AddWithValue("@StudentAverage", res.studentAverage);
                cmd.Parameters.AddWithValue("@Grade", res.Grade);
                cmd.Parameters.AddWithValue("@Remark", res.Remark);

                con.Open();
                int x = cmd.ExecuteNonQuery();
                con.Close();
                if (x > 0)
                {
                    response = "Result sucessfully uploaded";
                }
                else
                {
                    response = "Failed";
                }
            }
            return response;

        }

        [HttpGet("Get-Results")]
        public List<Result> GetAllResults()
        {
            SqlDataAdapter adapter = new SqlDataAdapter("Get_All", con);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            DataTable table = new DataTable();
            adapter.Fill(table);
            List<Result> results = new List<Result>();
            if (table.Rows.Count > 0)
            {
                for (int i = 0; i < table.Rows.Count; i++)
                {
                    Result result = new Result()
                    {
                        studentId = Convert.ToInt32(table.Rows[i]["StudentID"]),
                        studentName = table.Rows[i]["StudentName"].ToString(),
                        Mathematics = Convert.ToInt32(table.Rows[i]["Mathematics"]),
                        English = Convert.ToInt32(table.Rows[i]["English"]),
                        Chemistry = Convert.ToInt32(table.Rows[i]["Chemistry"]),
                        Biology = Convert.ToInt32(table.Rows[i]["Biology"]),
                        Economics = Convert.ToInt32(table.Rows[i]["Economics"]),
                        History = Convert.ToInt32(table.Rows[i]["History"]),
                        studentTotal = Convert.ToInt32(table.Rows[i]["StudentTotal"]),


                    };
                    results.Add(result);


                }
            }
            if (results.Count > 0)
            {
                return results;
            }
            else
            {
                return null;
            }

        }
        [HttpGet("Get_By_Id")]
        public object GetById(int id)
        {
            SqlDataAdapter adapter = new SqlDataAdapter("sp_RetrieveByID", con);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.AddWithValue("@StudentID", id);
            DataTable table = new DataTable();
            adapter.Fill(table);


            Result result = new Result();
            if (table.Rows.Count > 0)
            {
                result.studentName = table.Rows[0]["StudentName"].ToString();
                result.Mathematics = Convert.ToInt32(table.Rows[0]["Mathematics"]);
                result.English = Convert.ToInt32(table.Rows[0]["English"]);
                result.Chemistry = Convert.ToInt32(table.Rows[0]["Chemistry"]);
                result.Biology = Convert.ToInt32(table.Rows[0]["Biology"]);
                result.Economics = Convert.ToInt32(table.Rows[0]["Economics"]);
                result.History = Convert.ToInt32(table.Rows[0]["History"]);
                result.studentTotal = Convert.ToInt32(table.Rows[0]["StudentTotal"]);
            }
            if (table.Rows.Count > 0)
            {
                return result;
            }
            else
            {
                return null;
            }
        }

        [HttpPut("Edit_Result")]
        public string UpdateResult(int id, Result res)
        {
            string response = " ";
            if (res != null)
            {
                SqlCommand cmd = new SqlCommand("sp_UpdateResult", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@StudentID", id);
                cmd.Parameters.AddWithValue("@studentName", res.studentName);
                cmd.Parameters.AddWithValue("@Mathematics", res.Mathematics);
                cmd.Parameters.AddWithValue("@English", res.English);
                cmd.Parameters.AddWithValue("@Chemistry", res.Chemistry);
                cmd.Parameters.AddWithValue("@Biology", res.Biology);
                cmd.Parameters.AddWithValue("@Economics", res.Economics);
                cmd.Parameters.AddWithValue("@History", res.History);
                cmd.Parameters.AddWithValue("@StudentTotal", res.studentTotal);
                cmd.Parameters.AddWithValue("@StudentAverage", res.studentAverage);
                cmd.Parameters.AddWithValue("@Grade", res.Grade);
                cmd.Parameters.AddWithValue("@Remark", res.Remark);

                con.Open();
                int x = cmd.ExecuteNonQuery();
                con.Close();
                if (x > 0)
                {
                    response = "Result sucessfully updated";
                }
                else
                {
                    response = "Failed";
                }
            }
            return response;
        }

        [HttpDelete("Delete_Result_By_ID")]
        public string DeleteResult(int id)
        {
            string response = " ";
             
                SqlCommand cmd = new SqlCommand("sp_DeleteByID", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@StudentID", id);
               

                con.Open();
                int x = cmd.ExecuteNonQuery();
                con.Close();
                if (x > 0)
                {
                    response = "Result sucessfully Deleted";
                }
                else
                {
                    response = "Failed";
                }
            
            return response;
        }
    }
}
