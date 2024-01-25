using System.ComponentModel.DataAnnotations;

namespace FinalProjectAgency.Areas.Admin.ViewModels
{
    public class RegisterVM
    {
        [Required]
        [MaxLength(30,ErrorMessage ="Uzunlugu 30 dan uzun olmamalidir")]
        [MinLength(3,ErrorMessage ="Uzunlugu 3 dan kicik olmamalidir")]

        public string Name { get; set; }
        [Required]
        [MaxLength(30, ErrorMessage = "Uzunlugu 30 dan uzun olmamalidir")]
        [MinLength(3, ErrorMessage = "Uzunlugu 3 dan kicik olmamalidir")]

        public string UserName { get; set; }
        [Required]
        [MaxLength(30, ErrorMessage = "Uzunlugu 30 dan uzun olmamalidir")]
        [MinLength(4, ErrorMessage = "Uzunlugu 4 dan kicik olmamalidir")]

        public string Surname { get; set; }
        [Required]
        [MaxLength(255, ErrorMessage = "Uzunlugu 255 dan uzun olmamalidir")]
        [MinLength(4, ErrorMessage = "Uzunlugu 4 dan kicik olmamalidir")]
        [DataType(DataType.EmailAddress)]

        public string Email { get; set; }
        [Required]
       
        [DataType(DataType.Password)]

        public string Password { get; set; }
        [Required]

        [DataType(DataType.Password)]
        [Compare(nameof(Password))]

        public string ConfirmPassword { get; set; }

    }
}
