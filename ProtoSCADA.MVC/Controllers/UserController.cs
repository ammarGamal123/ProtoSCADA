using Microsoft.AspNetCore.Mvc;
using ProtoSCADA.Entities.Entities;
using ProtoSCADA.Data.Context;
using System.Net.Http.Json;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Http;
using Org.BouncyCastle.Crypto.Generators;
using Microsoft.EntityFrameworkCore;

namespace ProtoSCADA.MVC.Controllers
{
    public class UserController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly ApplicationDbContext _context;

        public UserController(HttpClient httpClient, IConfiguration configuration, ApplicationDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
            _httpClient.BaseAddress = new Uri(configuration["ApiBaseUrl"] ?? "http://protoscada.runasp.net/");
        }

        // GET: User/Register
        public IActionResult Register()
        {
            return View();
        }

        // POST: User/Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(User user)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);
                    _context.Users.Add(user);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Login));
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"Error registering user: {ex.Message}");
            }
            return View(user);
        }

        // GET: User/Login
        public IActionResult Login()
        {
            return View();
        }

        // POST: User/Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(string username, string password)
        {
            try
            {
                var user = await _context.Users.FirstOrDefaultAsync(n => n.Name == username);
                if (user != null && BCrypt.Net.BCrypt.Verify(password, user.Password))
                {
                    HttpContext.Session.SetString("Username", user.Name);
                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError("", "Invalid login attempt.");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"Error during login: {ex.Message}");
            }
            return View();
        }

        // GET: User/Logout
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction(nameof(Login));
        }

        // GET: User/Index
        public async Task<IActionResult> Index()
        {
            try
            {
                var users = await _httpClient.GetFromJsonAsync<List<User>>("api/user");
                return View(users);
            }
            catch (Exception ex)
            {
                ViewData["Error"] = $"Error loading users: {ex.Message}";
                return View(new List<User>());
            }
        }

        // GET: User/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: User/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(User user)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var response = await _httpClient.PostAsJsonAsync("api/user", user);
                    if (response.IsSuccessStatusCode)
                        return RedirectToAction(nameof(Index));

                    ModelState.AddModelError("", "Failed to create user.");
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"Error creating user: {ex.Message}");
            }
            return View(user);
        }

        // GET: User/Edit/{id}
        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                var user = await _httpClient.GetFromJsonAsync<User>($"api/user/{id}");
                return user != null ? View(user) : NotFound();
            }
            catch (Exception ex)
            {
                ViewData["Error"] = $"Error loading user: {ex.Message}";
                return RedirectToAction(nameof(Index));
            }
        }

        // POST: User/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(User user)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var response = await _httpClient.PutAsJsonAsync($"api/user/{user.ID}", user);
                    if (response.IsSuccessStatusCode)
                        return RedirectToAction(nameof(Index));

                    ModelState.AddModelError("", "Failed to update user.");
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"Error updating user: {ex.Message}");
            }
            return View(user);
        }

        // GET: User/Delete/{id}
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var user = await _httpClient.GetFromJsonAsync<User>($"api/user/{id}");
                return user != null ? View(user) : NotFound();
            }
            catch (Exception ex)
            {
                ViewData["Error"] = $"Error loading user: {ex.Message}";
                return RedirectToAction(nameof(Index));
            }
        }

        // POST: User/Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ConfirmDelete(int id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"api/user/{id}");
                if (response.IsSuccessStatusCode)
                    return RedirectToAction(nameof(Index));

                ModelState.AddModelError("", "Failed to delete user.");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"Error deleting user: {ex.Message}");
            }
            return RedirectToAction(nameof(Delete), new { id });
        }
    }
}
