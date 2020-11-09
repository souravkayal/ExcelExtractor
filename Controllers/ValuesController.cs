using ExcelDataReader;
using ExcelReader.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace ExcelReader.Controllers
{
    public class CustomerDataType
    {
        public string CustomerName { get; set; }
        public string CustomerId { get; set; }
        public string CustomerSalary { get; set; }
        public string CustomerDesignation { get; set; }
    }

    public class ExcelController : ApiController
    {
        // POST api/values
        public IHttpActionResult Post(HttpRequestMessage request)
        {
            object aa = 123;

            bool rea = (aa.GetType() == typeof(int));


            HttpContext context = HttpContext.Current;
            HttpPostedFile postedFile = context.Request.Files["file"];
            Stream stream = postedFile.InputStream;

            IExcelDataReader reader = null;
            if (postedFile.FileName.EndsWith(".xls"))
            {
                reader = ExcelReaderFactory.CreateBinaryReader(stream);
            }
            else if (postedFile.FileName.EndsWith(".xlsx"))
            {
                reader = ExcelReaderFactory.CreateOpenXmlReader(stream);
            }

            DataSet excelRecords = reader.AsDataSet();
            reader.Close();

            var finalRecords = excelRecords.Tables[0];
            //for (int i = 1; i < finalRecords.Rows.Count; i++)
            //{
            var result = new CustomerDataType
            {
                    CustomerName = DataTypeHelper.TypeChecker(finalRecords.Rows[1][0]),
                    CustomerId = DataTypeHelper.TypeChecker(finalRecords.Rows[1][1]),
                    CustomerSalary = DataTypeHelper.TypeChecker(finalRecords.Rows[1][2]),
                    CustomerDesignation = DataTypeHelper.TypeChecker(finalRecords.Rows[1][3])
            };
            //}
            return Ok(result);
        }
    }
}
