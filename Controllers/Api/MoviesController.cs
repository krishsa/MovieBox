using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MovieBox.Dtos;
using MovieBox.Models;
using MovieBox.Data;
using MovieBox.Areas.Mapper;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MovieBox.Controllers.Api
{
    [Route("api/[controller]")]
    public class MoviesController : Controller
    {
        private readonly ApplicationDbContext _context;

        private readonly IMapper _mapper;
        
        public MoviesController(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        // GET: api/movies
        [HttpGet]
        public IEnumerable<MovieDto> GetMovies()
        {
            return _context.Movies.ToList().Select(_mapper.Map<Movie, MovieDto>);
        }

        // GET api/movies/5term
        [HttpGet("{id}")]
        public MovieDto GetMovie(int id)
        {
            var movie = _context.Movies.Include(m => m.Genre).SingleOrDefault(m => m.Id == id);

            if (movie == null)
                throw new Exception();

            return _mapper.Map<Movie, MovieDto>(movie);
        }

        // POST api/movies
        [HttpPost]
        public IActionResult CreatetMovie([FromBody]MovieDto movieDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var movie = _mapper.Map<MovieDto, Movie>(movieDto);
            _context.Add(movie);
            _context.SaveChanges();

            movieDto.Id = movie.Id;

            var requestUrl = HttpContext.Request.Path;

            return Created(requestUrl + "/" + movieDto.Id, movieDto);

        }

        // PUT api/movies/5
        [HttpPut("{id}")]
        public void UpdateMovie(int id, [FromBody]MovieDto movieDto)
        {
            if (!ModelState.IsValid)
                throw new Exception();

            var movieInDb = _context.Movies.SingleOrDefault(m => m.Id == id);

            _mapper.Map(movieDto, movieInDb);
            _context.SaveChanges();

        }


        // DELETE api/movies/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var movieInDb = _context.Movies.SingleOrDefault(m => m.Id == id);

            if (movieInDb == null)
                throw new Exception();
             
            _context.Movies.Remove(movieInDb);
            _context.SaveChanges();
        }
    }
}
