using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Assignment1.EntityModels;
using Assignment1.Models;

namespace Assignment1.Controllers
{
    public class Manager
    {
        // Reference to the data context
        private DataContext ds = new DataContext();

        // AutoMapper instance
        public IMapper mapper;

        public Manager()
        {
            // If necessary, add more constructor code here...

            // Configure the AutoMapper components
            var config = new MapperConfiguration(cfg =>
            {
                // Define the mappings below, for example...
                // cfg.CreateMap<SourceType, DestinationType>();
                // cfg.CreateMap<Employee, EmployeeBase>();

                cfg.CreateMap<Employee, EmployeeBaseViewModel>();

                cfg.CreateMap<EmployeeAddViewModel, Employee>();

                cfg.CreateMap<EmployeeBaseViewModel, EmployeeEditFormViewModel>();

            });

            mapper = config.CreateMapper();

            // Turn off the Entity Framework (EF) proxy creation features
            // We do NOT want the EF to track changes - we'll do that ourselves
            ds.Configuration.ProxyCreationEnabled = false;

            // Also, turn off lazy loading...
            // We want to retain control over fetching related objects
            ds.Configuration.LazyLoadingEnabled = false;
        }

        // Add methods below
        // Controllers will call these methods
        // Ensure that the methods accept and deliver ONLY view model objects and collections
        // The collection return type is almost always IEnumerable<T>

        // Suggested naming convention: Entity + task/action
        // For example:
        // ProductGetAll()
        // ProductGetById()
        // ProductAdd()
        // ProductEdit()
        // ProductDelete()

        public IEnumerable<EmployeeBaseViewModel> EmployeeGetAll()
        {

            var emp = from e in ds.Employees
                      orderby e.LastName, e.FirstName
                      select e;
     
            return mapper.Map<IEnumerable<Employee>, IEnumerable<EmployeeBaseViewModel>>(emp);
            //                 Source object type                 Target object type     Source object
        }

        internal object EmployeeGetById(object p)
        {
            throw new NotImplementedException();
        }

        public EmployeeBaseViewModel EmployeeGetById(int? id)
        {
            //Attempt to fetch the object
            var obj = ds.Employees.Find(id);

            //Return the result (or null if not found).
            return obj == null ? null : mapper.Map<Employee, EmployeeBaseViewModel>(obj);

        }

        public EmployeeBaseViewModel EmployeeAdd(EmployeeAddViewModel newEmployee)
        {
            // Attempt to add the new item.
            // Notice how we map the incoming data to the Customer design model class.
            var addedItem = ds.Employees.Add(mapper.Map<EmployeeAddViewModel, Employee>(newEmployee));
            ds.SaveChanges();
            // If successful, return the added item (mapped to a view model class).
            return addedItem == null ? null : mapper.Map<Employee, EmployeeBaseViewModel>(addedItem);
        }

        public EmployeeBaseViewModel EmployeeEditContactInfo(EmployeeEditViewModel employee) //As the automapper is just assigning the properties between the objects, to edit, we need to use the function.
        {
            // Attempt to fetch the object.
            var obj = ds.Employees.Find(employee.EmployeeId);

            if (obj == null)
            {
                // Employee was not found, return null.
                return null;
            }
            else
            {
                // Employee was found. Update the entity object
                // with the incoming values then save the changes.
                ds.Entry(obj).CurrentValues.SetValues(employee);
                //ds.Entry(obj) returns an object which provides access to information about an entity (obj in this situation) and allows control of the properties.
                //The Entry objest has a CurrentValues property, which gets the current values for the entity's properties as a collection of DbPropertyValues.
                //The CurrentValues property has a SetValues() method. This sets (changes) the CurrentValues properties to those in a passed-in-object (employee)
                ds.SaveChanges();

                // Prepare and return the object.
                return mapper.Map<Employee, EmployeeBaseViewModel>(obj);

            }

        }
    }
}