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
    public class ExcelColumnType
    {
        public string ColumnName { get; set; }
        public string DataType { get; set; }
    }

    public class ExcelController : ApiController
    {
        // POST api/values
        public IHttpActionResult Post(HttpRequestMessage request)
        {

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
            var columnList = new List<ExcelColumnType>();
            
            for (int i = 0; i < finalRecords.Columns.Count; i++)
            {
                var item = new ExcelColumnType 
                {
                    ColumnName = Convert.ToString(finalRecords.Rows[0][i]) ,
                    DataType = DataTypeHelper.TypeChecker(finalRecords.Rows[1][i])
                };
                columnList.Add(item);
            }

            return Ok(columnList);
        }
    }
}
