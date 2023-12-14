using System.ComponentModel.DataAnnotations;

namespace Password_Manager.ViewModels
{
    public class EditViewModel
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Имя сайта")]
        public string? Name { get; set; }
        [Display(Name = "Email")]
        public string? UserEmail { get; set; }

        [Required]
        [Display(Name = "Описание")]
        public string? Description { get; set; }

        [Required]
        [Display(Name = "Пароль")]
        public string? Password { get; set; }
    }
}
