using RecordKeeping.Projects.Models;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Linq;
using System;

namespace RecordKeeping.Projects.Controllers {
    public class ProjectController : Controller
    {
        AdventureWorksEntities _dptObj;
        
        // GET: Project
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult GetDepartmentDetails() {
            _dptObj = new AdventureWorksEntities();

            List<DepartmentModel> deptList = 
                (from obj in _dptObj.Departments
                select new DepartmentModel {
                    DepartmentID = obj.DepartmentID,
                    Name = obj.Name,
                    GroupName = obj.GroupName,
                    ModifiedDate = obj.ModifiedDate
                }).ToList();

            return Json(deptList, JsonRequestBehavior.AllowGet);
        }
    }
}