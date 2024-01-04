
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web;
using System.Web.Mvc;

namespace swaransoft_Assessment.Models
{
    public class Student
    {
        public int StudentId { get; set; }

        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Mobile is required")]
        public string Mobile { get; set; }

        [Required(ErrorMessage = "State is required")]
        public int StateId { get; set; }

        [Required(ErrorMessage = "City is required")]
        public int CityId { get; set; }

        [Required(ErrorMessage = "About Yourself is required")]
        [MaxLength(256, ErrorMessage = "About Yourself cannot exceed 256 characters")]
        public string AboutYourSelf { get; set; }

        public byte[] PhotoData { get; set; }

        [NotMapped]
        public HttpPostedFileBase PhotoFile { get; set; }

        [NotMapped]
        public CascadingModel CascadingModel { get; set; }
    }
}