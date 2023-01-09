using WebApplication2.Models;
using System.Data.SqlClient;
using System.Data;

namespace WebApplication2.Repository
{
    public class HomeRepository
    {
        public string Save(SqlConnection con,List<IndexModels> collectdata)
        {
            string result = "";
            con.Open();
            try
            {
                SqlCommand cmd = new SqlCommand("SP_SaveTest", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id", collectdata[0].Id);
                cmd.Parameters.AddWithValue("@Name", collectdata[0].Name);
                cmd.Parameters.AddWithValue("@Date", collectdata[0].Date);
                cmd.Parameters.AddWithValue("@MobileNo", collectdata[0].MobileNo);
                if (collectdata[0].Id == 0)
                {
                    cmd.Parameters.AddWithValue("@Action", "Save");
                }
                else
                {
                    cmd.Parameters.AddWithValue("@Action", "Edit");
                }
                cmd.ExecuteNonQuery();
                result = "Success";
            }
            catch(Exception ex)
            {
                ex.Message.ToString();
                result = "Failure";
            }
            con.Close();
            return result;
        }
        public DataTable IndexDetails(SqlConnection con)
        {
            List<IndexModels> obj = new List<IndexModels>();
            DataTable dt = new DataTable();
            con.Open();
            try
            {
                SqlCommand cmd = new SqlCommand("SP_GetTest", con);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter a = new SqlDataAdapter(cmd);
                a.Fill(dt);
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }
            return dt;
        }
        public DataTable ViewDetailsbyId(SqlConnection con, string Id)
        {
            List<IndexModels> obj = new List<IndexModels>();
            DataTable dt = new DataTable();
            con.Open();
            try
            {
                SqlCommand cmd = new SqlCommand("SP_GetTestDetailsbyId", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id", Convert.ToInt32(Id));
                SqlDataAdapter a = new SqlDataAdapter(cmd);
                a.Fill(dt);
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }
            return dt;
        }
        public string DeleteDetails(SqlConnection con, string Id)
        {
            string result = "";
            con.Open();
            try
            {
                SqlCommand cmd = new SqlCommand("SP_DeleteTestDetailsbyId", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id", Id);
                cmd.ExecuteNonQuery();
                result = "Success";
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
                result = "Failure";
            }
            con.Close();
            return result;
        }
    }
}
