using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace InformationProtection.Models
{
    public class Person
    {
        public int PersonId { get; set; }

        // Id of a City in Cities
        [Required]
        public int CityId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string EmailAddress { get; set; }

        [Required]
        public string Password { get; set; }

        // "M" for mail, "F" for female.
        [Required]
        public string Gender { get; set; }

        [Required]
        public DateTime BirthDate { get; set; }

        public string About { get; set; }

        // 0: Unselected, 1: Primary school,
        // 2: High school 3: University
        [Required]
        public int Education { get; set; }

        //true: Active, false: Passive
        [Required]
        public bool IsActive { get; set; }

        [Required]
        public DateTime RecordDate { get; set; }

        public Person()
        {
            RecordDate = DateTime.Now;
            Password = "123";
            About = "";
        }
    }
    public class City
    {
        public int CityId { get; set; }

        [Required]
        public string CityName { get; set; }
    }
}