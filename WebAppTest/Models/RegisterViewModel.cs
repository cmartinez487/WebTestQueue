using System.ComponentModel.DataAnnotations;

namespace WebAppTest.Models
{
    public class RegisterViewModel
    {
        [Required]
        [Display(Name = "Dni")]
        public int Dni { get; set; }

        [Required]
        [Display(Name = "Nombre")]
        public string Name { get; set; }
    }
}
