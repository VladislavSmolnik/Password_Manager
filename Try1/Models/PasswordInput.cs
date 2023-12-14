using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Password_Manager.Models
{
    public class PasswordInput
    {
        public int Id { get; set; }

        [Display(Name ="Имя сайта")]
        public string? Name { get; set; }


        [Display(Name = "UserId")]
        public string? UserId { get; set; }


        [Display(Name = "Описание")]
        public string? Description { get; set; }


        [Display(Name = "Пароль")]
        public string? Password { get; set; }

        [Display(Name = "Время создания")]
        public DateTime CreationTime { get; set; }
        
    }
}
