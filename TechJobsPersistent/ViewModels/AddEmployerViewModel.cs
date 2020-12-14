using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TechJobsPersistent.ViewModels
{
    public class AddEmployerViewModel
    {
        [Required(ErrorMessage ="Name is required")]
        public String Name { get; set; }

        [Required(ErrorMessage = "Location is required")]
        public String Location { get; set; }

        public AddEmployerViewModel()
        {

        }
    }
}
