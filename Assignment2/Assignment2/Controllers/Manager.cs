using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Assignment2.EntityModels;
using Assignment2.Models;
using Microsoft.ApplicationInsights.Web;

namespace Assignment2.Controllers
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

                cfg.CreateMap<Track, TrackBaseViewModel>();
                cfg.CreateMap<Invoice, InvoiceBaseViewModel>();
                cfg.CreateMap<Invoice, InvoiceWithDetailViewModel>();
                cfg.CreateMap<InvoiceLine, InvoiceLineBaseViewModel>();
                cfg.CreateMap<InvoiceLine, InvoiceLineWithDetailViewModel>();
            });

            mapper = config.CreateMapper();

            // Turn off the Entity Framework (EF) proxy creation features
            // We do NOT want the EF to track changes - we'll do that ourselves
            ds.Configuration.ProxyCreationEnabled = false;

            // Also, turn off lazy loading...
            // We want to retain control over fetching related objects
            ds.Configuration.LazyLoadingEnabled = false;
        }

        internal object InvoiceGetById(object p)
        {
            throw new NotImplementedException();
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

        public IEnumerable<TrackBaseViewModel> TrackGetAll()
        {
            var track = from t in ds.Tracks
                        orderby t.AlbumId, t.Name
                        select t;

            return mapper.Map<IEnumerable<Track>, IEnumerable<TrackBaseViewModel>>(track);
        }

        public IEnumerable<TrackBaseViewModel> TrackGetAllJazz()
        {
            var jazz = from j in ds.Tracks
                       where j.GenreId == 2
                       orderby j.Name
                       select j;

            return mapper.Map<IEnumerable<Track>, IEnumerable<TrackBaseViewModel>>(jazz);
        }

        public IEnumerable<TrackBaseViewModel> TrackGetAllRogerGlover()
        {
            var rogerglover = from r in ds.Tracks
                              where r.Composer.Contains("Roger Glover")
                              orderby r.AlbumId, r.TrackId
                              select r;

            return mapper.Map<IEnumerable<Track>, IEnumerable<TrackBaseViewModel>>(rogerglover);
        }

        public IEnumerable<TrackBaseViewModel> TrackGetAllTop50Longest()
        {
            var top50Long = (from l in ds.Tracks
                             orderby l.Milliseconds descending
                             select l).Take(50);

            return mapper.Map<IEnumerable<Track>, IEnumerable<TrackBaseViewModel>>(top50Long);
        }

        public IEnumerable<InvoiceBaseViewModel> InvoiceGetAll()
        {
            var inv = from i in ds.Invoices
                      orderby i.InvoiceId
                      select i;

            return mapper.Map<IEnumerable<Invoice>, IEnumerable<InvoiceBaseViewModel>>(inv);

        }

        public InvoiceBaseViewModel InvoiceGetById(int? id)
        {
            //Attempt to fetch the object
            var obj = ds.Invoices.Find(id);

            return obj == null ? null : mapper.Map<Invoice, InvoiceBaseViewModel>(obj);
        }

        public InvoiceWithDetailViewModel InvoiceGetByIdWithDetail(int? id)
        {
            var obj = ds.Invoices.Include("Customer.Employee").Include("InvoiceLines.Track.Album.Artist").Include("InvoiceLines.Track.MediaType").SingleOrDefault(i => i.InvoiceId == id);

            return (obj == null) ? null : mapper.Map<Invoice, InvoiceWithDetailViewModel>(obj);
        }


    }
}