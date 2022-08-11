using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using mvcdemo.Models;
using System.IO;
using System.Data;
using System.Data.SqlClient;
using ClosedXML.Excel;

//Export Using DataTable

namespace mvcdemo.Controllers
{
    public class EmployeeController : Controller
    {
        // GET: Employee
        public ActionResult Index()
        {
            DataSet ds = this.GetCustomers();
            return View(ds);
        }

        public ActionResult Export()
        {
            using (XLWorkbook wb = new XLWorkbook())
            {
                DataTable dt = this.GetCustomers().Tables[0];
                wb.Worksheets.Add(dt);
                using (MemoryStream stream = new MemoryStream())
                {
                    wb.SaveAs(stream);
                    return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "EmployeeExcel.xlsx");
                }
            }
        }

        private DataSet GetCustomers()
        {
            DataSet ds = new DataSet();
            string constr =@"data source=DESKTOP-28MCLDS;initial catalog=mvcdemo;integrated security=True";
            using (SqlConnection con = new SqlConnection(constr))
            {
                string query = "SELECT * FROM tblEmployee";
                using (SqlCommand cmd = new SqlCommand(query))
                {
                    cmd.Connection = con;
                    using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                    {
                        sda.Fill(ds);
                    }
                }
            }

            return ds;
        }
    

    //public ActionResult GetEmpList()
    //    {
    //        try
    //        {
    //            return Json(new { model = (new EmployeeModel().GetEmpList()) }, JsonRequestBehavior.AllowGet);
    //        }
    //        catch (Exception Ex)
    //        {
    //            return Json(new { Ex.Message }, JsonRequestBehavior.AllowGet);
    //        }

    //    }
        //public ActionResult ExpoertToExcel()
        //{
        //    int i = 0;
        //    int j = 0;
        //    string sql = null;
        //    string data = null;
        //    Excel.Application xlApp;
        //    Excel.Workbook xlWorkBook;
        //    Excel.Worksheet xlWorkSheet;
        //    object misValue = Missing.Value;
        //    xlApp = new Excel.Application();
        //    xlApp.Visible = false;
        //    xlWorkBook = (Excel.Workbook)(xlApp.Workbooks.Add(Missing.Value));
        //    xlWorkSheet = (Excel.Worksheet)xlWorkBook.ActiveSheet;
        //    //string conn = ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString;
        //    SqlConnection con = new SqlConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties =\"Excel 12.0 Xml;HDR=YES;IMEX=1\";");
        //    con.Open();
        //    var cmd = new SqlCommand("SELECT * FROM tblProduct", con);
        //    var reader = cmd.ExecuteReader();
        //    int k = 0;
        //    for (i = 0; i < reader.FieldCount; i++)
        //    {
        //        data = (reader.GetName(i));
        //        xlWorkSheet.Cells[1, k + 1] = data;
        //        k++;
        //    }
        //    char lastColumn = (char)(65 + reader.FieldCount - 1);
        //    xlWorkSheet.get_Range("A1", lastColumn + "1").Font.Bold = true;
        //    xlWorkSheet.get_Range("A1", lastColumn + "1").VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
        //    reader.Close();
        //    sql = "SELECT * FROM tblProduct";
        //    SqlDataAdapter dscmd = new SqlDataAdapter(sql, con);
        //    DataSet ds = new DataSet();
        //    dscmd.Fill(ds);
        //    for (i = 0; i <= ds.Tables[0].Rows.Count - 1; i++)
        //    {
        //        var newj = 0;
        //        for (j = 0; j <= ds.Tables[0].Columns.Count - 1; j++)
        //        {
        //            data = ds.Tables[0].Rows[i].ItemArray[j].ToString();
        //            xlWorkSheet.Cells[i + 2, newj + 1] = data;
        //            newj++;
        //        }
        //    }
        //    xlWorkBook.Close(true, misValue, misValue);
        //    xlApp.Quit();
        //    releaseObject(xlWorkSheet);
        //    releaseObject(xlWorkBook);
        //    releaseObject(xlApp);
        //    return RedirectToAction("Index", "ExpoertToExcel");
        //}
        //private void releaseObject(object obj)
        //{
        //    try
        //    {
        //        System.Runtime.InteropServices.Marshal.ReleaseComObject(obj);
        //        obj = null;
        //    }
        //    catch
        //    {
        //        obj = null;
        //        //MessageBox.Show("Exception Occured while releasing object " + ex.ToString());  
        //    }
        //    finally
        //    {
        //        GC.Collect();
        //    }
        //}
    }
}