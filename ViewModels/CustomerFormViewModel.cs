using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using MovieBox.Models;

namespace MovieBox.ViewModels
{
    public class CustomerFormViewModel
    {
        public IEnumerable<MembershipType> MembershipTypes { get; set; }

        public Customer Customer { get; set; }

    }
}
