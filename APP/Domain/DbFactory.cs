using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APP.Domain
{
    public class DbFactory : IDesignTimeDbContextFactory<Db>
    {
        const string CONNECTIONSTRING = "Data Source=DB.db";
        public Db CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<Db>();
            optionsBuilder.UseSqlite(CONNECTIONSTRING);
            return new Db(optionsBuilder.Options);
        }

        public void SeedDb()
        {
            var db = CreateDbContext(null);

            var movieGenres = db.MovieGenres.ToList();
            db.MovieGenres.RemoveRange(movieGenres);
            var movies = db.Movies.ToList();
            db.Movies.RemoveRange(movies);
            var genres = db.Genres.ToList();
            db.Genres.RemoveRange(genres);
            var directors = db.Directors.ToList();
            db.Directors.RemoveRange(directors);

            db.Database.ExecuteSqlRaw("UPDATE SQLITE_SEQUENCE SET SEQ=0 WHERE NAME='MovieGenres'");
            db.Database.ExecuteSqlRaw("UPDATE SQLITE_SEQUENCE SET SEQ=0 WHERE NAME='Movies'");
            db.Database.ExecuteSqlRaw("UPDATE SQLITE_SEQUENCE SET SEQ=0 WHERE NAME='Genres'");
            db.Database.ExecuteSqlRaw("UPDATE SQLITE_SEQUENCE SET SEQ=0 WHERE NAME='Directors'");

            var genreList = new List<Genre> {
                new Genre { Name = "Action" },
                new Genre { Name = "Comedy" },
                new Genre { Name = "Drama" },
                new Genre { Name = "Horror" },
                new Genre { Name = "Sci-Fi" },
                new Genre { Name = "Thriller" },
                new Genre { Name = "Crime" },
                new Genre { Name = "Documentary" },
            };
            db.Genres.AddRange(genreList);
            db.SaveChanges();
            db.Directors.Add(new Director
            {
                FirstName = "Wes",
                LastName = "Anderson",
                isRetired = false,
                Movies = new List<Movie> {
                new Movie {
                    Name = "The Grand Budapest Hotel",
                    ReleaseDate = new DateTime(2014, 4, 11),
                    TotalRevenue = 175900000,
                    GenreIds = new List<int> { 2,3 }
                },
                new Movie {
                    Name = "Fantastic Mr. Fox",
                    ReleaseDate = new DateTime(2009, 11, 13),
                    TotalRevenue = 58000000,
                    GenreIds = new List<int> { 2,3 }
                },
                new Movie {
                    Name = "Isle of Dogs",
                    ReleaseDate = new DateTime(2018, 04, 20),
                    TotalRevenue = 73000000,
                    GenreIds = new List<int> { 2,3 }
                },
                new Movie {
                    Name = "The Royal Tenenbaums",
                    ReleaseDate = new DateTime(2002, 03, 22),
                    TotalRevenue = 71400000,
                    GenreIds = new List<int> { 2,3 }
                }
            }
            });

            db.Directors.Add(new Director
            {
                FirstName = "Quentin",
                LastName = "Tarantino",
                isRetired = false,
                Movies = new List<Movie>
                {
                    new Movie
                    {
                        Name = "Pulp Fiction",
                        ReleaseDate = new DateTime(1994, 10, 14),
                        TotalRevenue = 213900000,
                        GenreIds = new List<int> { 1, 2, 3, 7 }
                    },
                    new Movie
                    {
                        Name = "Inglourious Basterds",
                        ReleaseDate = new DateTime(2009, 08, 21),
                        TotalRevenue = 321000000,
                        GenreIds = new List<int> { 1, 2, 3, 6 }
                    },
                    new Movie
                    {
                        Name = "Django Unchained",
                        ReleaseDate = new DateTime(2012, 12, 25),
                        TotalRevenue = 425400000,
                        GenreIds = new List<int> { 1, 2, 3, 6 }
                    },
                    new Movie
                    {
                        Name = "Once Upon a Time in Hollywood",
                        ReleaseDate = new DateTime(2019, 07, 26),
                        TotalRevenue = 374600000,
                        GenreIds = new List<int> { 1, 2, 3, 6 }
                    }
                }
            });
            db.SaveChanges();
        }
    }
}
