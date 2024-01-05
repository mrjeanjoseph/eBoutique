using ExcelDataReader;
using ImportExcelFile.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace ImportExcelFile.Controllers {
    public class EmployeeController : Controller
    {
        private readonly EmployeeDbContext _dbContext;

        public EmployeeController() {
            _dbContext = new EmployeeDbContext();
        }

        // GET: Student  
        public ActionResult Index() {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> ImportFile() {
            return View("Index");
        }

        private List<Employee> GetDataFromCSVFile(Stream stream) {
            var empList = new List<Employee>();
            try {
                using (var reader = ExcelReaderFactory.CreateCsvReader(stream)) {
                    var dataSet = reader.AsDataSet(new ExcelDataSetConfiguration {
                        ConfigureDataTable = _ => new ExcelDataTableConfiguration {
                            UseHeaderRow = true // To set First Row As Column Names    
                        }
                    });

                    if (dataSet.Tables.Count > 0) {
                        var dataTable = dataSet.Tables[0];
                        foreach (DataRow objDataRow in dataTable.Rows) {
                            if (objDataRow.ItemArray.All(x => string.IsNullOrEmpty(x?.ToString()))) continue;
                            empList.Add(new Employee() {
                                Id = Convert.ToInt32(objDataRow["ID"].ToString()),
                                EmpName = objDataRow["Name"].ToString(),
                                Position = objDataRow["Position"].ToString(),
                                Location = objDataRow["Location"].ToString(),
                                Age = Convert.ToInt32(objDataRow["Age"].ToString()),
                                Salary = Convert.ToInt32(objDataRow["Salary"].ToString()),
                            });
                        }
                    }
                }
            } catch (Exception) {
                throw;
            }
            return empList;
        }

        [HttpPost]
        public async Task<ActionResult> ImportFile(HttpPostedFileBase importFile) {
            if (importFile == null) return Json(new { Status = 0, Message = "No File Selected" });

            try {
                var fileData = GetDataFromCSVFile(importFile.InputStream);

                var dtEmployee = fileData.ToDataTable();
                var tblEmployeeParameter = new SqlParameter("tblEmployeeTableType", SqlDbType.Structured) {
                    TypeName = "dbo.tblTypeEmployee",
                    Value = dtEmployee
                };
                await _dbContext.Database.ExecuteSqlCommandAsync("EXEC spBulkImportEmployee @tblEmployeeTableType", tblEmployeeParameter);
                return Json(new { Status = 1, Message = "File Imported Successfully " });
            } catch (Exception ex) {
                return Json(new { Status = 0, Message = ex.Message });
            }
        }


    }
}