using MyFirstWebApp.EntityModels;
using MyFirstWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyFirstWebApp.Controllers
{
    public class EmployeesController : Controller
    {
        private Manager m = new Manager(); //To communicate with Manger class

        // GET: Customers
        public ActionResult Index()
        {
            var c = m.EmployeeGetAll();

            return View(c);
            // return View(m.EmployeeGetAll()); It depends on whether you need to validate, transform, modify, or manipulate the fetched results before passing them to the view.
        }

        // GET: Customers/Details/5
        public ActionResult Details(int? id) //If there is a null value among the bunch of the id, and the parameter is just int instead of int?, it may cause an error.
        {
            // Attempt to get the matching object
            var obj = m.EmployeeGetById(id.GetValueOrDefault()); //The GetValueOrDefault function works if the value matches the type then returns value, otherwise returns 0.
            if (obj == null)
                return HttpNotFound();
            else
                return View(obj);
        }

        // GET: Customers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Customers/Create
        [HttpPost]
        public ActionResult Create(EmployeeAddViewModel newItem)
        {
            // Validate the input
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

        // GET: Customers/Edit/5
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
                var formObj = m.mapper.Map<EmployeeBaseViewModel, EmployeeEditContactFormViewModel>(obj);
                return View(formObj);
            }
        }

        // POST: Customers/Edit/5
        [HttpPost]
        public ActionResult Edit(int? id, EmployeeEditContactViewModel model)
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

        // GET: Customers/Delete/5
        public ActionResult Delete(int? id)
        {
            var itemToDelete = m.EmployeeGetById(id.GetValueOrDefault());
            if (itemToDelete == null)
            {
                // Don't leak info about the delete attempt
                // Simply redirect
                return RedirectToAction("Index");
            }
            else
                return View(itemToDelete);
        }

        // POST: Customers/Delete/5
        [HttpPost]
        public ActionResult Delete(int? id, FormCollection collection)
        {
            var result = m.EmployeeDelete(id.GetValueOrDefault());
            // "result" will be true or false
            // We probably won't do much with the result, because
            // we don't want to leak info about the delete attempt
            // In the end, we should just redirect to the list view
            return RedirectToAction("Index");

        }
    }
}
