using Microsoft.AspNetCore.Mvc;
using ProtoSCADA.Entities.Entities;
using ProtoSCADA.Data.Context;
using System.Net.Http.Json;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Collections.Generic;
using System;
using ProtoSCADA.Service.Utilities;
using ProtoSCADA.Entities.DTOs;

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

        // GET: User/Index
        public async Task<IActionResult> Index(int pageNumber = 1, int pageSize = 10)
        {
            try
            {
                var response = await _httpClient.GetFromJsonAsync<ProcessResult<List<UserDto>>>($"api/User?pageNumber={pageNumber}&pageSize={pageSize}");
                if (response?.IsSuccess == true && response.Data != null)
                {
                    return View(response.Data);
                }
                ViewData["Error"] = response?.ErrorMessage ?? "Failed to fetch user data.";
            }
            catch (Exception ex)
            {
                ViewData["Error"] = $"An error occurred: {ex.Message}";
            }
            return View(new List<User>());
        }

        // GET: User/Details/{id}
        public async Task<IActionResult> Details(int id)
        {
            try
            {
                var response = await _httpClient.GetFromJsonAsync<ProcessResult<User>>($"api/User/{id}");
                if (response?.IsSuccess == true && response.Data != null)
                {
                    ViewData["Title"] = $"User Details - {response.Data.Name}";
                    return View(response.Data);
                }

                ViewData["Error"] = response?.ErrorMessage ?? "Failed to fetch user details.";
            }
            catch (Exception ex)
            {
                ViewData["Error"] = $"An error occurred: {ex.Message}";
            }

            return View("Error");
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
            if (!ModelState.IsValid)
            {
                return View(user);
            }

            try
            {
                user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);
                _context.Users.Add(user);
                await _context.SaveChangesAsync();

                var response = await _httpClient.PostAsJsonAsync("api/User", user);
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }

                ModelState.AddModelError("", "Failed to create user.");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"An error occurred: {ex.Message}");
            }

            return View(user);
        }

        // GET: User/Edit/{id}
        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                var response = await _httpClient.GetFromJsonAsync<ProcessResult<User>>($"api/User/{id}");
                if (response?.IsSuccess == true && response.Data != null)
                {
                    return View(response.Data);
                }

                ViewData["Error"] = response?.ErrorMessage ?? "User not found.";
            }
            catch (Exception ex)
            {
                ViewData["Error"] = $"An error occurred: {ex.Message}";
            }

            return RedirectToAction(nameof(Index));
        }

        // POST: User/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(User user)
        {
            if (!ModelState.IsValid)
            {
                return View(user);
            }

            try
            {
                var response = await _httpClient.PutAsJsonAsync($"api/User/{user.ID}", user);
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }

                ModelState.AddModelError("", "Failed to update user.");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"An error occurred: {ex.Message}");
            }

            return View(user);
        }

        // GET: User/Delete/{id}
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var response = await _httpClient.GetFromJsonAsync<ProcessResult<User>>($"api/User/{id}");
                if (response?.IsSuccess == true && response.Data != null)
                {
                    return View(response.Data);
                }

                ViewData["Error"] = response?.ErrorMessage ?? "User not found.";
            }
            catch (Exception ex)
            {
                ViewData["Error"] = $"An error occurred: {ex.Message}";
            }

            return RedirectToAction(nameof(Index));
        }

        // POST: User/Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ConfirmDelete(int id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"api/User/{id}");
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }

                ModelState.AddModelError("", "Failed to delete user.");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"An error occurred: {ex.Message}");
            }

            return RedirectToAction(nameof(Delete), new { id });
        }

        // GET: User/Login
        public IActionResult Login() => View();

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

        #region Private Helper Methods

        private async Task<List<User>> FetchUsersDataAsync()
        {
            try
            {
                var response = await _httpClient.GetFromJsonAsync<ProcessResult<List<User>>>("api/User");
                return response?.IsSuccess == true && response.Data != null ? response.Data : null;
            }
            catch (Exception ex)
            {
                ViewData["Error"] = $"Error fetching user data: {ex.Message}";
                return null;
            }
        }

        private async Task<User> FetchUserDataAsync(int id)
        {
            try
            {
                var response = await _httpClient.GetFromJsonAsync<ProcessResult<User>>($"api/User/{id}");
                return response?.IsSuccess == true && response.Data != null ? response.Data : null;
            }
            catch (Exception ex)
            {
                ViewData["Error"] = $"Error loading user data: {ex.Message}";
                return null;
            }
        }

        #endregion
    }
}

