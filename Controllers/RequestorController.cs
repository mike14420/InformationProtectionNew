using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using InformationProtection.Models;
using IpModelData;
using WMC.Core.Employees.BusObj2008;

namespace InformationProtection.Controllers
{
    [HandleError]
    public class RequestorController : BaseController
    {


        public ActionResult Index()    
        {

            return View();
        }

        public ActionResult Details(String EmpID)
        {
            IpRequestorViewData requestor = new IpRequestorViewData();
            IpRequestorView Model = new IpRequestorView();
            if (String.IsNullOrEmpty(EmpID))
            {
                return RedirectToAction("Index");
            }
            else
            {
                requestor = Model.GetRequestor(EmpID);
            }
            if (requestor == null)
            {
                return RedirectToAction("Index");
            }
            return View(requestor);
        }


        /// <summary>
        /// Create allows you to create a IP request for different employees
        /// </summary>
        /// <returns></returns>

        public ActionResult Create()
        {

            String LoginUserName = HttpContext.Request.LogonUserIdentity.Name;
            //LoginUserName = "wmcdomain\\29783";// fir testing
            EmployeeView model = new EmployeeView();
            IpRequestorViewData thisEmployee = model.GetUserInfo(LoginUserName);
            if (thisEmployee == null)
            {
                return RedirectToAction("Index");
            }
            SelectList employees = model.GetEmployees();
            ViewBag.AllUsers = employees;
            return View(thisEmployee);
        }

        //
        // POST: /Requestor/Create

        [HttpPost]
        public ActionResult Create(IpRequestorViewData thisEmployee)
        {
            if (ModelState.IsValid)
            {
                IpRequestorView ourModel = new IpRequestorView();
                int retValue = ourModel.CreateRequestor(thisEmployee, Roles.RoleNameEnum.user.ToString());
                return RedirectToAction("Details", new { EmpID = thisEmployee.EmpID });
            }
            EmployeeView Mdl = new EmployeeView();
            ViewBag.AllUsers = Mdl.GetEmployees();
            return View(thisEmployee);
        }


        public ActionResult Edit(String EmpID)
        {
            IpRequestorViewData Data = new IpRequestorViewData();
            IpRequestorView Model = new IpRequestorView();
            Data = Model.GetRequestor(EmpID);
            return View(Data);
        }

        [HttpPost]
        public ActionResult Edit(String EmpID, IpRequestorViewData data)
        {
            if (ModelState.IsValid)
            {
                IpRequestorView ourModel = new IpRequestorView();
                bool retValue = ourModel.Update(data);
                if (retValue)
                {
                    return RedirectToAction("Index", "UsersView", new { EmpID = EmpID });
                }
            }
            return View(data);
        }


   
    }
}
