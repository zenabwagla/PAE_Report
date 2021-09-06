﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using AngularKendo.Models;

namespace AngularKendo.Controllers
{
    public class EmployeeCRUDController : Controller
    {
        private EmployeeEntities db = new EmployeeEntities();
        // GET: EmployeeCRUD
        public ActionResult Index()
        {
            return View();
        }
        // GET: Employee  
        public ActionResult GetAllEmployee([DataSourceRequest]DataSourceRequest request)
        {
            try
            {

                var employee = db.Employees.ToList();

                return Json(employee.ToDataSourceResult(request));
            }
            catch (Exception ex)
            {
                return Json(ex.Message);
            }

        }

        // UPDATE: Employee  

        public ActionResult UpdateEmployee([DataSourceRequest]DataSourceRequest request, Employee_Table emp)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Entry(emp).State = EntityState.Modified;
                    db.SaveChanges();
                    return Json(new[] { emp }.ToDataSourceResult(request, ModelState));

                }
                else
                {
                    return Json(db.Employees.ToList());
                }
            }
            catch (Exception ex)
            {
                return Json(ex.Message);
            }
        }

        // ADD: Employee  

        public ActionResult AddEmployee([DataSourceRequest]DataSourceRequest request, Employee_Table emp)
        {
            try
            {
                if (ModelState.IsValid)
                {

                    db.Employees.Add(emp);
                    db.SaveChanges();
                    var _emplist = db.Employees.ToList();
                    return Json(new[] { emp }.ToDataSourceResult(request, ModelState));
                }

                else
                {
                    return Json(db.Employees.ToList());
                }
            }
            catch (Exception ex)
            {
                return Json(ex.Message);
            }
        }

        // DELETE: Employee  

        public ActionResult DeleteEmployee([DataSourceRequest]DataSourceRequest request, Employee_Table emp)
        {
            try
            {
                Employee_Table employee = db.Employees.Find(emp.EmployeeId);
                if (employee == null)
                {
                    return Json("Employee Not Found");
                }

                db.Employees.Remove(employee);
                db.SaveChanges();
                return Json(db.Employees.ToList());
            }
            catch (Exception ex)
            {
                return Json(ex.Message);
            }
        }
    }
}