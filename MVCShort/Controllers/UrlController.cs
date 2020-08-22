using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.SqlClient;
using MVCShort.Models;

namespace MVCShort.Controllers
{
    public class UrlController : Controller
    {
        string connectionString = @"Data Source=(localdb)\MVCShortDB;Initial Catalog=UrlTable;Integrated Security=True";
        [HttpGet]
        public ActionResult Index()
        {
            DataTable dtblUrl = new DataTable();
            using (SqlConnection sqlCon = new SqlConnection(connectionString))
            {
                sqlCon.Open();
                SqlDataAdapter sqlDa = new SqlDataAdapter("SELECT *FROM URL", sqlCon);
                sqlDa.Fill(dtblUrl);
            }
                return View(dtblUrl);
        }


        [HttpGet]
        public ActionResult Create()
        {
            return View(new UrlModel());
        }

        // POST: Url/Create
        [HttpPost]
        public ActionResult Create(UrlModel urlModel)
        {
            using (SqlConnection sqlCon = new SqlConnection(connectionString))
            {
                try
                {
                    sqlCon.Open();
                    string query = "INSERT INTO URL Values (@Url,@Surl,@Date,@Count)";
                    SqlCommand sqlCmd = new SqlCommand(query, sqlCon);
                    sqlCmd.Parameters.AddWithValue("@Url", urlModel.Url);
                    sqlCmd.Parameters.AddWithValue("@Surl", urlModel.Surl);
                    sqlCmd.Parameters.AddWithValue("@Date", urlModel.Date);
                    sqlCmd.Parameters.AddWithValue("@Count", urlModel.Count);
                    sqlCmd.ExecuteNonQuery();
                }catch{}
                
            }
            return RedirectToAction("Index");
           
        }

        // GET: Url/Edit/5
        public ActionResult Edit(int id)
        {
            UrlModel urlModel = new UrlModel();
            DataTable dtblUrl = new DataTable();
            using (SqlConnection sqlCon = new SqlConnection(connectionString))
            {
                sqlCon.Open();
                string query = "SELECT* FROM URL WHERE Id = @Id";
                SqlDataAdapter sqlDa = new SqlDataAdapter(query, sqlCon);
                sqlDa.SelectCommand.Parameters.AddWithValue("@Id", id);
                sqlDa.Fill(dtblUrl);
            }
            if(dtblUrl.Rows.Count ==1)
            {
                urlModel.Id = Convert.ToInt32(dtblUrl.Rows[0][0].ToString());
                urlModel.Surl = Convert.ToString(dtblUrl.Rows[0][1].ToString());
                urlModel.Url = Convert.ToString(dtblUrl.Rows[0][2].ToString());
                urlModel.Date = Convert.ToDateTime(dtblUrl.Rows[0][3].ToString());
                urlModel.Count = Convert.ToInt32(dtblUrl.Rows[0][4].ToString());
                return View(urlModel);

            }
            else
                return RedirectToAction("Index");
        }

        // POST: Url/Edit/5
        [HttpPost]
        public ActionResult Edit(UrlModel urlModel)
        {
            using (SqlConnection sqlCon = new SqlConnection(connectionString))
            {
                try
                {
                    sqlCon.Open();
                    string query = "UPDATE URL SET Url=@Url,Surl=@Surl,Date=@Date,Count=@Count Where Id=@Id";
                    SqlCommand sqlCmd = new SqlCommand(query, sqlCon);
                    sqlCmd.Parameters.AddWithValue("@Id", urlModel.Id);
                    sqlCmd.Parameters.AddWithValue("@Url", urlModel.Url);
                    sqlCmd.Parameters.AddWithValue("@Surl", urlModel.Surl);
                    sqlCmd.Parameters.AddWithValue("@Date", urlModel.Date);
                    sqlCmd.Parameters.AddWithValue("@Count", urlModel.Count);
                    sqlCmd.ExecuteNonQuery();
                }
                catch { }
            }
            return RedirectToAction("Index");
        }

        // GET: Url/Delete/5
        public ActionResult Delete(int id)
        {
            using (SqlConnection sqlCon = new SqlConnection(connectionString))
            {
                sqlCon.Open();
                string query = "DELETE FROM URL Where Id=@Id";
                SqlCommand sqlCmd = new SqlCommand(query, sqlCon);
                sqlCmd.Parameters.AddWithValue("@Id", id);
              
                sqlCmd.ExecuteNonQuery();
            }
            return RedirectToAction("Index");
        }
    }
}
