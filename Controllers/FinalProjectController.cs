using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace 期末作業.Controllers
{
    public class FinalProjectController : Controller
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["AdventureWorksLT2022ConnectionString"].ConnectionString;

        // GET: FinalProject
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(int salesOrderId)
        {
            ViewBag.OrderHeader = GetOrderHeader(salesOrderId);
            ViewBag.OrderDetail = GetOrderDetail(salesOrderId);
            ViewBag.CustomerList = GetCustomerList();
            ViewBag.InputOrderId = salesOrderId;
            return View();
        }

        private DataTable GetOrderHeader(int id)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string sql = "SELECT * FROM SalesLT.SalesOrderHeader WHERE SalesOrderID = @id";
                SqlDataAdapter da = new SqlDataAdapter(sql, conn);
                da.SelectCommand.Parameters.AddWithValue("@id", id);
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
        }

        private DataTable GetOrderDetail(int id)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string sql = "SELECT * FROM SalesLT.SalesOrderDetail WHERE SalesOrderID = @id";
                SqlDataAdapter da = new SqlDataAdapter(sql, conn);
                da.SelectCommand.Parameters.AddWithValue("@id", id);
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
        }

        private DataTable GetCustomerList()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string sql = "SELECT Customer.LastName + '_' + Customer.FirstName AS FullName FROM SalesLT.Customer";
                SqlDataAdapter da = new SqlDataAdapter(sql, conn);
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
        }
    }
}