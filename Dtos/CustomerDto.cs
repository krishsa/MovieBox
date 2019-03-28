using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using MovieBox.Models;

namespace MovieBox.Dtos
{
    public class CustomerDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Please enter your name")]
        [StringLength(255)]
        public string Name { get; set; }

        public bool IsSubscribedToNewsletter { get; set; }

        [Required(ErrorMessage = "Please select a Membership type.")]
        public byte MembershipTypeId { get; set; }

        //[Min18YearsIfAMember]
        public DateTime? BirthDate { get; set; }

    }
}
