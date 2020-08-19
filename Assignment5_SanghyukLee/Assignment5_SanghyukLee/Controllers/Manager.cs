using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
// new...
using AutoMapper;
using System.Security.Claims;
using Assignment5_SanghyukLee.Models;
using Assignment5_SanghyukLee.EntityModels;

namespace Assignment5_SanghyukLee.Controllers
{
    public class Manager
    {
        // Reference to the data context
        private ApplicationDbContext ds = new ApplicationDbContext();

        // AutoMapper instance
        public IMapper mapper;

        // Request user property...

        // Backing field for the property
        private RequestUser _user;

        // Getter only, no setter
        public RequestUser User
        {
            get
            {
                // On first use, it will be null, so set its value
                if (_user == null)
                {
                    _user = new RequestUser(HttpContext.Current.User as ClaimsPrincipal);
                }
                return _user;
            }
        }

        // Default constructor...
        public Manager()
        {
            // If necessary, add constructor code here

            // Configure the AutoMapper components
            var config = new MapperConfiguration(cfg =>
            {
                // Define the mappings below, for example...
                // cfg.CreateMap<SourceType, DestinationType>();
                // cfg.CreateMap<Employee, EmployeeBase>();

                // Object mapper definitions

                cfg.CreateMap<Models.RegisterViewModel, Models.RegisterViewModelForm>();

                cfg.CreateMap<Genre, GenreBaseViewModel>();

                cfg.CreateMap<Album, AlbumBaseViewModel>();
                cfg.CreateMap<Album, AlbumWithDetailViewModel>();
                cfg.CreateMap<AlbumBaseViewModel, AlbumAddFormViewModel>();
                cfg.CreateMap<AlbumAddViewModel, Album>();

                cfg.CreateMap<Artist, ArtistBaseViewModel>();
                cfg.CreateMap<Artist, ArtistWithDetailViewModel>();
                cfg.CreateMap<ArtistBaseViewModel, ArtistAddFormViewModel>();
                cfg.CreateMap<ArtistAddViewModel, Artist>();
                cfg.CreateMap<Artist, ArtistWithMediaInfoViewModel>();

                cfg.CreateMap<Track, TrackBaseViewModel>();
                cfg.CreateMap<Track, TrackWithDetailViewModel>();
                cfg.CreateMap<TrackBaseViewModel, TrackAddFormViewModel>();
                cfg.CreateMap<Track, TrackAudioViewModel>();
                cfg.CreateMap<TrackAddViewModel, Track>();
                cfg.CreateMap<TrackWithDetailViewModel, TrackAddFormViewModel>();
                cfg.CreateMap<TrackWithDetailViewModel, TrackEditFormViewModel>();

                cfg.CreateMap<Genre, GenreBaseViewModel>();

                cfg.CreateMap<MediaItem, MediaItemBaseViewModel>();
                cfg.CreateMap<MediaItem, MediaContentViewModel>();
                cfg.CreateMap<MediaItemAddViewModel, MediaItem>();
                cfg.CreateMap<MediaItemBaseViewModel, MediaItemAddFormViewModel>();

            });

            mapper = config.CreateMapper();

            // Turn off the Entity Framework (EF) proxy creation features
            // We do NOT want the EF to track changes - we'll do that ourselves
            ds.Configuration.ProxyCreationEnabled = false;

            // Also, turn off lazy loading...
            // We want to retain control over fetching related objects
            ds.Configuration.LazyLoadingEnabled = false;
        }

        // ############################################################
        // RoleClaim

        public List<string> RoleClaimGetAllStrings()
        {
            return ds.RoleClaims.OrderBy(r => r.Name).Select(r => r.Name).ToList();
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

        public IEnumerable<GenreBaseViewModel> GenreGetAll()
        {
            var genre = from t in ds.Genres
                        orderby t.Name
                        select t;

            return mapper.Map<IEnumerable<Genre>, IEnumerable<GenreBaseViewModel>>(genre);
        }


        public IEnumerable<ArtistBaseViewModel> ArtistGetAll()
        {
            var artist = from t in ds.Artists
                         orderby t.Name
                         select t;

            return mapper.Map<IEnumerable<Artist>, IEnumerable<ArtistBaseViewModel>>(artist);
        }

        public ArtistWithDetailViewModel ArtistGetById(int? id)
        {


            //Attempt to fetch the object
            var obj = ds.Artists.Include("MediaItems").SingleOrDefault(a => a.ArtistId == id);


            if (obj == null)
            {
                return null;
            }
            else
            {
                var result = mapper.Map<Artist, ArtistWithMediaInfoViewModel>(obj);
                result.AlbumNames = obj.Albums.Select(a => a.Name);
                return result;
            }


            // return (obj == null) ? null : mapper.Map<Artist, ArtistWithDetailViewModel>(obj);


        }

        public ArtistWithDetailViewModel ArtistAdd(ArtistAddViewModel newArtist)
        {
            newArtist.Executive = HttpContext.Current.User.Identity.Name;
            var addedItem = ds.Artists.Add(mapper.Map<ArtistAddViewModel, Artist>(newArtist));
            ds.SaveChanges();
            return addedItem == null ? null : mapper.Map<Artist, ArtistWithDetailViewModel>(addedItem);
        }


        public IEnumerable<AlbumBaseViewModel> AlbumGetAll()
        {
            var album = from t in ds.Albums
                        orderby t.Name
                        select t;

            return mapper.Map<IEnumerable<Album>, IEnumerable<AlbumBaseViewModel>>(album);
        }

        public AlbumWithDetailViewModel AlbumGetById(int? id)
        {
            //Attempt to fetch the object
            var obj = ds.Albums.Include("Artists").Include("Tracks").SingleOrDefault(a => a.AlbumId == id);

            if (obj == null)
            {
                return null;
            }
            else
            {

                var result = mapper.Map<Album, AlbumWithDetailViewModel>(obj);
                result.ArtistNames = obj.Artists.Select(a => a.Name);
                return result;

            }
        }

        public AlbumWithDetailViewModel AlbumAdd(AlbumAddViewModel newAlbum)
        {
            newAlbum.Coordinator = HttpContext.Current.User.Identity.Name;

            var addedAlbum = ds.Albums.Add(mapper.Map<AlbumAddViewModel, Album>(newAlbum));
            ds.SaveChanges();
            return (addedAlbum != null) ? mapper.Map<Album, AlbumWithDetailViewModel>(addedAlbum) : null;

        }

        /* public AlbumWithDetailViewModel AlbumAdd(AlbumAddViewModel newAlbum)
        {
            var temp = newAlbum.ArtistIds.ToList(); //get the list of artists' id
            temp.Add(newAlbum.ArtistId); //Inside the list, add the new id of newly added album
            newAlbum.ArtistIds = temp; //As you have added a new id, initialize the original file with updated list

            var selectedArtists = new List<Artist>(); //Artist type of selected artist list

            foreach (var selectedArtistId in newAlbum.ArtistIds) //Loop through the newly initialized artistsIds property
            {
                var artist = ds.Artists.Find(selectedArtistId); //find if there is a matching id inside the database
                if (artist != null)
                {
                    selectedArtists.Add(artist); //If it is not null then it adds inside
                }
            }

            if (selectedArtists.Count() > 0)
            {
                if (newAlbum.TrackIds.Count() > 0)
                {
                    var selectedTracks = new List<Track>();

                    foreach (var selectedTrackId in newAlbum.TrackIds)
                    {
                        var track = ds.Tracks.Find(selectedTrackId);
                        if (track != null)
                        {
                            selectedTracks.Add(track);
                        }
                    }

                    newAlbum.Tracks = selectedTracks;
                }

                newAlbum.Artists = selectedArtists;

                newAlbum.Artists = selectedArtists;

                newAlbum.Coordinator = HttpContext.Current.User.Identity.Name;
                var addedAlbum = ds.Albums.Add(mapper.Map<AlbumAddViewModel, Album>(newAlbum));
                ds.SaveChanges();

                return (addedAlbum != null) ? mapper.Map<Album, AlbumWithDetailViewModel>(addedAlbum) : null;
            }

            return null;

        } */

        public IEnumerable<TrackBaseViewModel> TrackGetAll()
        {
            var track = from t in ds.Tracks
                        orderby t.Name
                        select t;

            return mapper.Map<IEnumerable<Track>, IEnumerable<TrackBaseViewModel>>(track);
        }

        public IEnumerable<TrackBaseViewModel> TrackGetAllByArtistId(int? id)
        {
            //fetch the artist
            var artist = ds.Artists.Include("Albums.Tracks").SingleOrDefault(a => a.ArtistId == id);

            //Continue?
            if (artist == null)
            {
                return null;
            }

            //Create a collection to hold the results
            var tracks = new List<Track>();

            //Go through
            foreach (var album in artist.Albums)
            {
                tracks.AddRange(album.Tracks);
            }

            tracks = tracks.Distinct().ToList();

            return mapper.Map<IEnumerable<Track>, IEnumerable<TrackBaseViewModel>>(tracks.OrderBy(t => t.Name));
        }

        public TrackWithDetailViewModel TrackGetById(int? id)
        {
            //Attempt to fetch the object
            var obj = ds.Tracks.Include("Albums.Artists").SingleOrDefault(t => t.TrackId == id);

            if (obj == null)
            {
                return null;
            }
            else
            {

                var result = mapper.Map<Track, TrackWithDetailViewModel>(obj);
                result.AlbumNames = obj.Albums.Select(a => a.Name);
                return result;
            }
        }

        public TrackBaseViewModel TrackAdd(TrackAddViewModel newTrack)
        {
            /* var album = ds.Albums.Find(newTrack.AlbumId);

            if (album != null)
            {
                newTrack.Albums = new List<Album> { album };
            } */

            newTrack.Clerk = HttpContext.Current.User.Identity.Name;
            var addedTrack = ds.Tracks.Add(mapper.Map<TrackAddViewModel, Track>(newTrack));

            byte[] audioBytes = new byte[newTrack.AudioUpload.ContentLength];
            newTrack.AudioUpload.InputStream.Read(audioBytes, 0, newTrack.AudioUpload.ContentLength);

            addedTrack.Audio = audioBytes;
            addedTrack.AudioContentType = newTrack.AudioUpload.ContentType;

            ds.SaveChanges();
            return (addedTrack != null) ? mapper.Map<Track, TrackWithDetailViewModel>(addedTrack) : null;
        }

        public TrackAudioViewModel TrackAudioGetById(int? id)
        {

            var trackAudio = ds.Tracks.Find(id);

            if (trackAudio == null)
            {
                return null;
            }
            else
            {
                return mapper.Map<Track, TrackAudioViewModel>(trackAudio);
            }

        }

        public TrackWithDetailViewModel TrackEdit(TrackEditViewModel editedTrack)
        {
            var track = ds.Tracks.Find(editedTrack.Id);

            if (track == null)
            {
                return null;
            }else
            {

                byte[] audioBytes = new byte[editedTrack.AudioUpload.ContentLength];
                editedTrack.AudioUpload.InputStream.Read(audioBytes, 0, editedTrack.AudioUpload.ContentLength);

                track.Audio = audioBytes;
                track.AudioContentType = editedTrack.AudioUpload.ContentType;

                ds.SaveChanges();

                return mapper.Map<Track, TrackWithDetailViewModel>(track);
            }
        }

        public bool TrackDelete(int? id)
        {
            var track = ds.Tracks.Find(id);

            if (track == null)
            {
                return false;
            }else
            {
                ds.Tracks.Remove(track);
                ds.SaveChanges();
                return true;
            }
        }

        public ArtistWithMediaInfoViewModel ArtistMediaAdd(MediaItemAddViewModel newMedia)
        {

            var artist = ds.Artists.Find(newMedia.ArtistId);

            if (artist == null)
            {
                return null;
            }
            else
            {
                var addedMedia = new MediaItem();

                ds.MediaItems.Add(addedMedia);

                addedMedia.Caption = newMedia.Caption;
                addedMedia.Artist = artist;

                byte[] mediaBytes = new byte[newMedia.MediaUpload.ContentLength];
                newMedia.MediaUpload.InputStream.Read(mediaBytes, 0, newMedia.MediaUpload.ContentLength);

                addedMedia.Content = mediaBytes;
                addedMedia.ContentType = newMedia.MediaUpload.ContentType;

                ds.SaveChanges();

                return (addedMedia == null) ? null : mapper.Map<Artist, ArtistWithMediaInfoViewModel>(artist);
            }
        }

        public MediaContentViewModel MediaGetById(string stringId)
        {
            var media = ds.MediaItems.SingleOrDefault(mi => mi.StringId == stringId);

            return (media == null) ? null : mapper.Map<MediaItem, MediaContentViewModel>(media);
        }






        // Add some programmatically-generated objects to the data store
        // Can write one method, or many methods - your decision
        // The important idea is that you check for existing data first
        // Call this method from a controller action/method

        public bool LoadData()
        {
            // User name
            var user = HttpContext.Current.User.Identity.Name;

            // Monitor the progress
            bool done = false;

            // ############################################################
            // Role claims

            if (ds.RoleClaims.Count() == 0)
            {
                ds.RoleClaims.Add(new EntityModels.RoleClaim { Name = "Executive" });
                ds.RoleClaims.Add(new EntityModels.RoleClaim { Name = "Coordinator" });
                ds.RoleClaims.Add(new EntityModels.RoleClaim { Name = "Clerk" });
                ds.RoleClaims.Add(new EntityModels.RoleClaim { Name = "Staff" });

                ds.SaveChanges();
                done = true;
            }

            // ############################################################
            // Genre

            if (ds.Genres.Count() == 0)
            {
                // Add genres

                ds.Genres.Add(new Genre { Name = "Alternative" });
                ds.Genres.Add(new Genre { Name = "Classical" });
                ds.Genres.Add(new Genre { Name = "Country" });
                ds.Genres.Add(new Genre { Name = "Easy Listening" });
                ds.Genres.Add(new Genre { Name = "Hip-Hop/Rap" });
                ds.Genres.Add(new Genre { Name = "Jazz" });
                ds.Genres.Add(new Genre { Name = "Pop" });
                ds.Genres.Add(new Genre { Name = "R&B" });
                ds.Genres.Add(new Genre { Name = "Rock" });
                ds.Genres.Add(new Genre { Name = "Soundtrack" });

                ds.SaveChanges();
                done = true;
            }

            // ############################################################
            // Artist

            if (ds.Artists.Count() == 0)
            {
                // Add artists

                ds.Artists.Add(new Artist
                {
                    Name = "The Beatles",
                    BirthOrStartDate = new DateTime(1962, 8, 15),
                    Executive = user,
                    Genre = "Pop",
                    UrlArtist = "https://upload.wikimedia.org/wikipedia/commons/9/9f/Beatles_ad_1965_just_the_beatles_crop.jpg"
                });

                ds.Artists.Add(new Artist
                {
                    Name = "Adele",
                    BirthName = "Adele Adkins",
                    BirthOrStartDate = new DateTime(1988, 5, 5),
                    Executive = user,
                    Genre = "Pop",
                    UrlArtist = "http://www.billboard.com/files/styles/article_main_image/public/media/Adele-2015-close-up-XL_Columbia-billboard-650.jpg"
                });

                ds.Artists.Add(new Artist
                {
                    Name = "Bryan Adams",
                    BirthOrStartDate = new DateTime(1959, 11, 5),
                    Executive = user,
                    Genre = "Rock",
                    UrlArtist = "https://upload.wikimedia.org/wikipedia/commons/7/7e/Bryan_Adams_Hamburg_MG_0631_flickr.jpg"
                });

                ds.SaveChanges();
                done = true;
            }

            // ############################################################
            // Album

            if (ds.Albums.Count() == 0)
            {
                // Add albums

                // For Bryan Adams
                var bryan = ds.Artists.SingleOrDefault(a => a.Name == "Bryan Adams");

                ds.Albums.Add(new Album
                {
                    Artists = new List<Artist> { bryan },
                    Name = "Reckless",
                    ReleaseDate = new DateTime(1984, 11, 5),
                    Coordinator = user,
                    Genre = "Rock",
                    UrlAlbum = "https://upload.wikimedia.org/wikipedia/en/5/56/Bryan_Adams_-_Reckless.jpg"
                });

                ds.Albums.Add(new Album
                {
                    Artists = new List<Artist> { bryan },
                    Name = "So Far So Good",
                    ReleaseDate = new DateTime(1993, 11, 2),
                    Coordinator = user,
                    Genre = "Rock",
                    UrlAlbum = "https://upload.wikimedia.org/wikipedia/pt/a/ab/So_Far_so_Good_capa.jpg"
                });

                ds.SaveChanges();
                done = true;
            }

            // ############################################################
            // Track

            if (ds.Tracks.Count() == 0)
            {
                // Add tracks

                // For Reckless
                var reck = ds.Albums.SingleOrDefault(a => a.Name == "Reckless");

                ds.Tracks.Add(new Track
                {
                    Albums = new List<Album> { reck },
                    Name = "Run To You",
                    Composers = "Bryan Adams, Jim Vallance",
                    Clerk = user,
                    Genre = "Rock"
                });

                ds.Tracks.Add(new Track
                {
                    Albums = new List<Album> { reck },
                    Name = "Heaven",
                    Composers = "Bryan Adams, Jim Vallance",
                    Clerk = user,
                    Genre = "Rock"
                });

                ds.Tracks.Add(new Track
                {
                    Albums = new List<Album> { reck },
                    Name = "Somebody",
                    Composers = "Bryan Adams, Jim Vallance",
                    Clerk = user,
                    Genre = "Rock"
                });

                ds.Tracks.Add(new Track
                {
                    Albums = new List<Album> { reck },
                    Name = "Summer of '69",
                    Composers = "Bryan Adams, Jim Vallance",
                    Clerk = user,
                    Genre = "Rock"
                });

                ds.Tracks.Add(new Track
                {
                    Albums = new List<Album> { reck },
                    Name = "Kids Wanna Rock",
                    Composers = "Bryan Adams, Jim Vallance",
                    Clerk = user,
                    Genre = "Rock"
                });

                // For Reckless
                var so = ds.Albums.SingleOrDefault(a => a.Name == "So Far So Good");

                ds.Tracks.Add(new Track
                {
                    Albums = new List<Album> { so },
                    Name = "Straight from the Heart",
                    Composers = "Bryan Adams, Eric Kagna",
                    Clerk = user,
                    Genre = "Rock"
                });

                ds.Tracks.Add(new Track
                {
                    Albums = new List<Album> { so },
                    Name = "It's Only Love",
                    Composers = "Bryan Adams, Jim Vallance",
                    Clerk = user,
                    Genre = "Rock"
                });

                ds.Tracks.Add(new Track
                {
                    Albums = new List<Album> { so },
                    Name = "This Time",
                    Composers = "Bryan Adams, Jim Vallance",
                    Clerk = user,
                    Genre = "Rock"
                });

                ds.Tracks.Add(new Track
                {
                    Albums = new List<Album> { so },
                    Name = "(Everything I Do) I Do It for You",
                    Composers = "Bryan Adams, Jim Vallance",
                    Clerk = user,
                    Genre = "Rock"
                });

                ds.Tracks.Add(new Track
                {
                    Albums = new List<Album> { so },
                    Name = "Heat of the Night",
                    Composers = "Bryan Adams, Jim Vallance",
                    Clerk = user,
                    Genre = "Rock"
                });

                ds.SaveChanges();
                done = true;
            }

            return done;
        }

        public bool RemoveData()
        {
            try
            {
                foreach (var e in ds.RoleClaims)
                {
                    ds.Entry(e).State = System.Data.Entity.EntityState.Deleted;
                }
                ds.SaveChanges();

                foreach (var e in ds.Tracks)
                {
                    ds.Entry(e).State = System.Data.Entity.EntityState.Deleted;
                }
                ds.SaveChanges();

                foreach (var e in ds.Albums)
                {
                    ds.Entry(e).State = System.Data.Entity.EntityState.Deleted;
                }
                ds.SaveChanges();

                foreach (var e in ds.Artists)
                {
                    ds.Entry(e).State = System.Data.Entity.EntityState.Deleted;
                }
                ds.SaveChanges();

                foreach (var e in ds.Genres)
                {
                    ds.Entry(e).State = System.Data.Entity.EntityState.Deleted;
                }
                ds.SaveChanges();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool RemoveDatabase()
        {
            try
            {
                return ds.Database.Delete();
            }
            catch (Exception)
            {
                return false;
            }
        }

    }

    // New "RequestUser" class for the authenticated user
    // Includes many convenient members to make it easier to render user account info
    // Study the properties and methods, and think about how you could use it

    // How to use...

    // In the Manager class, declare a new property named User
    //public RequestUser User { get; private set; }

    // Then in the constructor of the Manager class, initialize its value
    //User = new RequestUser(HttpContext.Current.User as ClaimsPrincipal);

    public class RequestUser
    {
        // Constructor, pass in the security principal
        public RequestUser(ClaimsPrincipal user)
        {
            if (HttpContext.Current.Request.IsAuthenticated)
            {
                Principal = user;

                // Extract the role claims
                RoleClaims = user.Claims.Where(c => c.Type == ClaimTypes.Role).Select(c => c.Value);

                // User name
                Name = user.Identity.Name;

                // Extract the given name(s); if null or empty, then set an initial value
                string gn = user.Claims.SingleOrDefault(c => c.Type == ClaimTypes.GivenName).Value;
                if (string.IsNullOrEmpty(gn)) { gn = "(empty given name)"; }
                GivenName = gn;

                // Extract the surname; if null or empty, then set an initial value
                string sn = user.Claims.SingleOrDefault(c => c.Type == ClaimTypes.Surname).Value;
                if (string.IsNullOrEmpty(sn)) { sn = "(empty surname)"; }
                Surname = sn;

                IsAuthenticated = true;
                // You can change the string value in your app to match your app domain logic
                IsAdmin = user.HasClaim(ClaimTypes.Role, "Admin") ? true : false;
            }
            else
            {
                RoleClaims = new List<string>();
                Name = "anonymous";
                GivenName = "Unauthenticated";
                Surname = "Anonymous";
                IsAuthenticated = false;
                IsAdmin = false;
            }

            // Compose the nicely-formatted full names
            NamesFirstLast = $"{GivenName} {Surname}";
            NamesLastFirst = $"{Surname}, {GivenName}";
        }

        // Public properties
        public ClaimsPrincipal Principal { get; private set; }
        public IEnumerable<string> RoleClaims { get; private set; }

        public string Name { get; set; }

        public string GivenName { get; private set; }
        public string Surname { get; private set; }

        public string NamesFirstLast { get; private set; }
        public string NamesLastFirst { get; private set; }

        public bool IsAuthenticated { get; private set; }

        public bool IsAdmin { get; private set; }

        public bool HasRoleClaim(string value)
        {
            if (!IsAuthenticated) { return false; }
            return Principal.HasClaim(ClaimTypes.Role, value) ? true : false;
        }

        public bool HasClaim(string type, string value)
        {
            if (!IsAuthenticated) { return false; }
            return Principal.HasClaim(type, value) ? true : false;
        }
    }

}