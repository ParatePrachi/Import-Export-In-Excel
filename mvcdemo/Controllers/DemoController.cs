using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OfficeOpenXml;
using mvcdemo.Data;


//Using EPPlus Import
namespace mvcdemo.Controllers
{
    public class DemoController : Controller
    {
        // GET: Demo
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Upload(FormCollection formCollection)
        {
            var usersList = new List<tbldemo>();
            if (Request != null)
            {
                HttpPostedFileBase file = Request.Files["UploadedFile"];
                if ((file != null) && (file.ContentLength > 0) && !string.IsNullOrEmpty(file.FileName))
                {
                    string fileExtension = System.IO.Path.GetExtension(Request.Files["UploadedFile"].FileName);
                    if (fileExtension == ".xls" || fileExtension == ".xlsx")
                    {
                        string fileLocation = Server.MapPath("~/Upload/") + Request.Files["UploadedFile"].FileName;
                        if (System.IO.File.Exists(fileLocation))
                        {
                            System.IO.File.Delete(fileLocation);
                        }
                        Request.Files["UploadedFile"].SaveAs(fileLocation);
                        string fileName = file.FileName;
                        string fileContentType = file.ContentType;
                        byte[] fileBytes = new byte[file.ContentLength];
                        var data = file.InputStream.Read(fileBytes, 0, Convert.ToInt32(file.ContentLength));
                        using (var package = new ExcelPackage(file.InputStream))
                        {
                            var currentSheet = package.Workbook.Worksheets;
                            var workSheet = currentSheet.First();
                            var noOfCol = workSheet.Dimension.End.Column;
                            var noOfRow = workSheet.Dimension.End.Row;
                            for (int rowIterator = 2; rowIterator <= noOfRow; rowIterator++)
                            {
                                var user = new tbldemo();
                                user.Id = Convert.ToInt32(workSheet.Cells[rowIterator, 1].Value);
                                user.Name = workSheet.Cells[rowIterator, 2].Value.ToString();
                                user.Email = workSheet.Cells[rowIterator, 3].Value.ToString();
                                user.Img = workSheet.Cells[rowIterator, 3].Value.ToString();
                                usersList.Add(user);
                            }
                        }
                    }

                }
            }

            using (mvcdemoEntities excelImportDBEntities = new mvcdemoEntities())
            {
                foreach (var item in usersList)
                {
                    excelImportDBEntities.tbldemoes.Add(item);
                }
                excelImportDBEntities.SaveChanges();
            }
            return View("Index");
        }

    }
}