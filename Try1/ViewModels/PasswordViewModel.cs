using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Password_Manager.ViewModels
{
    public class PasswordViewModel
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Имя сайта")]
        public string? Name { get; set; }

        [Required]
        [Display(Name = "UserId")]
        public string? UserId { get; set; }


        [Required]
        [Display(Name = "Описание")]
        public string? Description { get; set; }

        [Required]
        [Display(Name = "Пароль")]
        public string? Password { get; set; }


        [Display(Name = "Время создания")]  
        public DateTime CreationTime { get; set; }

    }
}
