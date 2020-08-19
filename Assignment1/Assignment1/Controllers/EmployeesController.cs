using Assignment1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Assignment1.Controllers
{
    public class EmployeesController : Controller
    {
        private Manager m = new Manager();

        // GET: Employees
        public ActionResult Index()
        {
            var c = m.EmployeeGetAll();

            return View(c);
        }

        // GET: Employees/Details/5
        public ActionResult Details(int? id)
        {
            // Attempt to get the matching object
            var obj = m.EmployeeGetById(id.GetValueOrDefault()); //The GetValueOrDefault function works if the value matches the type then returns value, otherwise returns 0.
            if (obj == null)
                return HttpNotFound();
            else
                return View(obj);
        }

        // GET: Employees/Create
        public ActionResult Create()
        {
            var emp = new EmployeeAddViewModel();

            return View(emp);
        }

        // POST: Employees/Create
        [HttpPost]
        public ActionResult Create(EmployeeAddViewModel newItem)
        {
            if (!ModelState.IsValid)
                return View(newItem);
            try
            {
                // Process the input
                var addedItem = m.EmployeeAdd(newItem);
                // If the item was not added, return the user to the Create page
                // otherwise redirect them to the Details page.
                if (addedItem == null)
                    return View(newItem);
                else
                    return RedirectToAction("Details", new { id = addedItem.EmployeeId });
            }
            catch
            {
                return View(newItem);
            }
        }

        // GET: Employees/Edit/5
        public ActionResult Edit(int? id)
        {
            // Attempt to fetch the matching object
            var obj = m.EmployeeGetById(id.GetValueOrDefault());
            if (obj == null)
                return HttpNotFound();
            else
            {
                // Create and configure an "edit form"
                // Notice that obj is a CustomerBaseViewModel object so
                // we must map it to a CustomerEditContactFormViewModel object
                // Notice that we can use AutoMapper anywhere,
                // and not just in the Manager class.
                var formObj = m.mapper.Map<EmployeeBaseViewModel, EmployeeEditFormViewModel>(obj);
                return View(formObj);
            }
        }

        // POST: Employees/Edit/5
        [HttpPost]
        public ActionResult Edit(int? id, EmployeeEditViewModel model)
        {
            // Validate the input
            if (!ModelState.IsValid)
            {
                // Our "version 1" approach is to display the "edit form" again
                return RedirectToAction("Edit", new { id = model.EmployeeId });
            }
            if (id.GetValueOrDefault() != model.EmployeeId) //Retrieves the value of the current Nullable<T> object, or the default value of the underlying type.
            {
                // This appears to be data tampering, so redirect the user away
                return RedirectToAction("Index");
            }
            // Attempt to do the update
            var editedItem = m.EmployeeEditContactInfo(model);

            if (editedItem == null)
            {
                // There was a problem updating the object
                // Our "version 1" approach is to display the "edit form" again
                return RedirectToAction("Edit", new { id = model.EmployeeId });
            }
            else
            {
                // Show the details view, which will show the updated data
                return RedirectToAction("Details", new { id = model.EmployeeId });
            }
        }

        // GET: Employees/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Employees/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
