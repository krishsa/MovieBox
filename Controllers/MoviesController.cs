using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MovieBox.Models;
using MovieBox.ViewModels;
using MovieBox.Data;
using Microsoft.EntityFrameworkCore;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MovieBox.Controllers
{
   
    public class MoviesController : Controller
    {
        private ApplicationDbContext _context;

        public MoviesController(ApplicationDbContext _appContext)
        {
            _context = _appContext;
        }
        public IActionResult Random()
        {
            var movie = new Movie() { Name = "My Little Pony" };
            var customers = new List<Customer>
            {
                new Customer { Name = "John Smith"},
                new Customer { Name = "Mary Williams"}
            };
            var viewModel = new RandomMovieViewModel()
            {
                Movie = movie,
                Customers  = customers
            };

            return View(viewModel);
        }

        public IActionResult Index()
        {
            var movies = _context.Movies.Include(m => m.Genre).ToList();
            return View(movies);
        }

        public IActionResult Details(int id)
        {
            var movie = _context.Movies.Include(m => m.Genre).SingleOrDefault(m => m.Id == id);
            return View(movie);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Save(Movie movie)
        {
            if(!ModelState.IsValid)
            {
                var viewModel = new MovieFormViewModel(movie)
                {
                    Genres = _context.Genre.ToList()
                };

                return View("MovieForm", viewModel);
            }

            if(movie.Id == 0)
            {
                movie.DateAdded = DateTime.Now;
                _context.Movies.Add(movie);
            }
            else
            {
                var movieInDb = _context.Movies.Single(m => m.Id == movie.Id);

                movieInDb.Name = movie.Name;
                movieInDb.GenreId = movie.GenreId;
                movieInDb.ReleaseDate = movie.ReleaseDate;
                movieInDb.NumberInStock = movie.NumberInStock;
            }

            _context.SaveChanges();

            return RedirectToAction("Index", "Movies");
        }

        public IActionResult New()
        {
            var _genres = _context.Genre.ToList();

            var movie = new Movie();
            var viewModel = new MovieFormViewModel(movie)
            { 
                Genres = _genres
            };

            return View("MovieForm", viewModel);
        }

        public IActionResult Edit(int id)
        {
            var movie = _context.Movies.Single(m => m.Id == id);

            if (movie.Id == 0)
                return NotFound();

            var viewModel = new MovieFormViewModel(movie)
            {
                Id = movie.Id,
                Name = movie.Name,
                ReleaseDate = movie.ReleaseDate,
                NumberInStock = movie.NumberInStock,
                Genres = _context.Genre.ToList()
                
            };

            return View("MovieForm", viewModel);
        }

        // GET: /<controller>/ Sample Code
        //public IActionResult MovieList(int? pageIndex, string sortBy)
        //{
        //    /* Passing values in form data
        //     * ----------------------------*/           
        //    if (!pageIndex.HasValue)
        //        pageIndex = 1;
        //    if (String.IsNullOrWhiteSpace(sortBy))
        //        sortBy = "Name";

        //    return Content(String.Format("pageIndex={0}&sortBy={1}",pageIndex,sortBy));
        //}

        [Route("movies/released/{year}/{month}")]
        public IActionResult ByReleaseDate(int year,int month)
        {
            return Content(year+"/"+month);
        }

        private List<Movie> GetMovies()
        {
            return new List<Movie>
            {
                new Movie { Name = "Jurassic World" },
                new Movie { Name = "My Little Pony" }
            };
        }


    }
}
