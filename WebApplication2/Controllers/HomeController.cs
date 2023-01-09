using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WebApplication2.Models;
using WebApplication2.Repository;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Data;

namespace WebApplication2.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private IConfiguration Configuration;
        HomeRepository objRep = new HomeRepository();
        public HomeController(ILogger<HomeController> logger, IConfiguration _configuration)
        {
            _logger = logger;
            Configuration = _configuration;
        }

        public IActionResult Index()
        {
            string connString = this.Configuration.GetConnectionString("DefaultConnection");
            //SqlConnection con = new SqlConnection(connstring);
            //con.Open();
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [Route("/api/Home/Save")]
        [HttpPost]
        public List<IndexModels> Save([FromBody] List<IndexModels> collectdata)
        {
            string connString = this.Configuration.GetConnectionString("DefaultConnection");
            SqlConnection con = new SqlConnection(connString);
            List<IndexModels> obj = new List<IndexModels>();
            string result = "";
            try
            {
                result = objRep.Save(con,collectdata);
                IndexModels objlist = new IndexModels();
                objlist.Result = result;
                obj.Add(objlist);
            }
            catch(Exception ex)
            {
                IndexModels objlist = new IndexModels();
                objlist.Result = result;
                obj.Add(objlist);
            }
            return obj;
        }
        [Route("/api/Home/IndexDetails")]
        [HttpGet]

        public List<IndexModels> IndexDetails()
        {
            string connString = this.Configuration.GetConnectionString("DefaultConnection");
            SqlConnection con = new SqlConnection(connString);
            List<IndexModels> obj = new List<IndexModels>();
            DataTable dt = new DataTable();
            try
            {
                dt = objRep.IndexDetails(con);
                foreach (DataRow row in dt.Rows)
                {
                    IndexModels objlist = new IndexModels();
                    objlist.Id = Convert.ToInt32(row["Id"]);
                    objlist.Name = row["Name"].ToString();
                    objlist.Date = Convert.ToDateTime(row["Date"]).ToString("MM/dd/yyyy");
                    objlist.MobileNo = row["MobileNo"].ToString();
                    obj.Add(objlist);
                }
            }
            catch(Exception ex)
            {
                ex.Message.ToString();
            }
            return obj;
        }
        [Route("/api/Home/ViewDetailsbyId")]
        [HttpGet]

        public List<IndexModels> ViewDetailsbyId(string Id)
        {
            string connString = this.Configuration.GetConnectionString("DefaultConnection");
            SqlConnection con = new SqlConnection(connString);
            List<IndexModels> obj = new List<IndexModels>();
            DataTable dt = new DataTable();
            try
            {
                dt = objRep.ViewDetailsbyId(con,Id);
                foreach (DataRow row in dt.Rows)
                {
                    IndexModels objlist = new IndexModels();
                    objlist.Id = Convert.ToInt32(row["Id"]);
                    objlist.Name = row["Name"].ToString();
                    objlist.Date = Convert.ToDateTime(row["Date"]).ToString("yyyy-MM-dd");
                    objlist.MobileNo = row["MobileNo"].ToString();
                    obj.Add(objlist);
                }
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }
            return obj;
        }
        [Route("/api/Home/DeleteDetails")]
        [HttpGet]
        public List<IndexModels> DeleteDetails(string Id)
        {
            string connString = this.Configuration.GetConnectionString("DefaultConnection");
            SqlConnection con = new SqlConnection(connString);
            List<IndexModels> obj = new List<IndexModels>();
            string result = "";
            try
            {
                result = objRep.DeleteDetails(con, Id);
                IndexModels objlist = new IndexModels();
                objlist.Result = result;
                obj.Add(objlist);
            }
            catch (Exception ex)
            {
                IndexModels objlist = new IndexModels();
                objlist.Result = result;
                obj.Add(objlist);
            }
            return obj;
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}