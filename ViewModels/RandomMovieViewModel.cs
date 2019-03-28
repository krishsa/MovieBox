using System;
using System.Collections.Generic;
using MovieBox.Models;

namespace MovieBox.ViewModels
{
    public class RandomMovieViewModel
    {
        public Movie Movie { get; set; }
        public List<Customer> Customers { get; set; }
    }
}
