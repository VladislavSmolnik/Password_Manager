using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Reflection.Metadata.Ecma335;
using Password_Manager.Context;
using Password_Manager.Migrations;
using Password_Manager.Models;
using Password_Manager.ViewModels;

namespace Password_Manager.Controllers
{

    public class PasswordController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly ApplicationContext _context;

        public PasswordController(ApplicationContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var passwords = await _context.PasswordInputs.ToListAsync();
            var list = new List<PasswordViewModel>();
            var user = await _userManager.GetUserAsync(HttpContext.User);

            foreach(var password in passwords)
            {
                if (password.UserId == user.Id)
                {
                    var model = new PasswordViewModel
                    {
                        Id = password.Id,
                        Name = password.Name,
                        Description = password.Description,
                        Password = password.Password,
                        UserId = password.UserId,
                        CreationTime = password.CreationTime
                    };
                    list.Add(model);
                };               
            }
            return View(list);
        }

    
        //Детали
        public async Task<IActionResult> Details(int id)
        {
            if (id == null || _context.PasswordInputs == null)
            {
                return NotFound();
            }

            var passwordInputs = await _context.PasswordInputs
                .FirstOrDefaultAsync(m => m.Id == id);
            if (passwordInputs == null)
            {
                return NotFound();
            }

            return View(passwordInputs);
        }

        //Создания нового пароля
        public IActionResult Create()
        {
            return View();
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Description,Password")] CreateViewModel createViewModel)
        {
            var password = new PasswordInput();
            var user = await _userManager.GetUserAsync(HttpContext.User);
            if (ModelState.IsValid)
            {
                password.Name = createViewModel.Name;
                password.Description = createViewModel.Description;
                password.Password = createViewModel.Password;
                password.UserId = user.Id;
                password.CreationTime = DateTime.Now;
                
                _context.Add(password);

                 await _context.SaveChangesAsync();
                 return RedirectToAction(nameof(Index));
            }
            return View();
        }

        //Редактировать

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.PasswordInputs == null)
            {
                return NotFound();
            }

            var passwordInputs = await _context.PasswordInputs.FindAsync(id);
            if (passwordInputs == null)
            {
                return NotFound();
            }
            return View(passwordInputs);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description,Password")] PasswordInput passwordInputs)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);

            if (id != passwordInputs.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    passwordInputs.UserId = user.Id;
                    passwordInputs.CreationTime = DateTime.Now;
                    _context.Update(passwordInputs);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PasswordExists(passwordInputs.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(passwordInputs);
        }

        //Удалить пароль

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.PasswordInputs == null)
            {
                return NotFound();
            }

            var passwordInputs = await _context.PasswordInputs
                .FirstOrDefaultAsync(m => m.Id == id);
            if (passwordInputs == null)
            {
                return NotFound();
            }

            return View(passwordInputs);
        }

        //Подтверждение удаления

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.PasswordInputs == null)
            {
                return Problem("Entity set 'ApplicationContext.PasswordInputs'  is null.");
            }
            var passwordInputs = await _context.PasswordInputs.FindAsync(id);
            if (passwordInputs != null)
            {
                _context.PasswordInputs.Remove(passwordInputs);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PasswordExists(int id)
        {
            return (_context.PasswordInputs?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
