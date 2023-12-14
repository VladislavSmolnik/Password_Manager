using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Password_Manager.Models;

namespace Password_Manager.Context
{
    public class ApplicationContext : IdentityDbContext<User>
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options)
        {
            
        }
        public DbSet<PasswordInput> PasswordInputs { get; set; } = default!;
        public DbSet<User> users { get; set; } = default!;
    }
}
