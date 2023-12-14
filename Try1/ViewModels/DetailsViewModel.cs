using System.ComponentModel.DataAnnotations;

namespace Password_Manager.ViewModels
{
    public class DetailsViewModel
    {
        public int Id { get; set; }
        [Required]
        [Display(Name = "Имя сайта")]
        public string? Name { get; set; }

        [Display(Name = "UserId")]
        public string? UserID { get; set; }

        [Required]
        [Display(Name = "Описание")]
        public string? Description { get; set; }

        [Required]
        [Display(Name = "Пароль")]
        public string? Password { get; set; }
    }
}
