using Microsoft.AspNetCore.Mvc;
using ProtoSCADA.Entities.DTOs;
using ProtoSCADA.Service.Utilities;
using System.Net.Http.Json;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;

namespace ProtoSCADA.MVC.Controllers
{
    public class FactoryController : Controller
    {
        private readonly HttpClient _httpClient;

        public FactoryController(HttpClient httpClient)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
            _httpClient.BaseAddress = new Uri("http://protoscada.runasp.net/api/");
        }

        // GET: Factory/Index
        public async Task<IActionResult> Index(int pageNumber = 1, int pageSize = 20)
        {
            try
            {
                var response = await _httpClient.GetFromJsonAsync<ProcessResult<List<FactoryDto>>>($"Factory?pageNumber={pageNumber}&pageSize={pageSize}");
                if (response?.IsSuccess == true && response.Data != null)
                {
                    return View(response.Data);
                }

                ViewData["Error"] = response?.ErrorMessage ?? "Failed to fetch factory data.";
            }
            catch (Exception ex)
            {
                ViewData["Error"] = $"An error occurred: {ex.Message}";
            }

            return View(new List<FactoryDto>());
        }

        // GET: Factory/Details/{id}
        public async Task<IActionResult> Details(int id)
        {
            try
            {
                var response = await _httpClient.GetFromJsonAsync<ProcessResult<FactoryDto>>($"Factory/{id}");
                if (response?.IsSuccess == true && response.Data != null)
                {
                    ViewData["Title"] = $"Factory Details - {response.Data.Name}";
                    return View(response.Data);
                }

                ViewData["Error"] = response?.ErrorMessage ?? "Failed to fetch factory details.";
            }
            catch (Exception ex)
            {
                ViewData["Error"] = $"An error occurred: {ex.Message}";
            }

            // Handle the case where response.Data is null
            return RedirectToAction(nameof(Index)); // Redirect to the index page or show an error view
        }

        // GET: Factory/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Factory/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(FactoryDto factoryDto)
        {
            if (!ModelState.IsValid)
            {
                return View(factoryDto);
            }

            try
            {
                var response = await _httpClient.PostAsJsonAsync("Factory", factoryDto);
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }

                ModelState.AddModelError("", "Failed to create factory.");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"An error occurred: {ex.Message}");
            }

            return View(factoryDto);
        }

        // GET: Factory/Edit/{id}
        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                var response = await _httpClient.GetFromJsonAsync<ProcessResult<FactoryDto>>($"Factory/{id}");
                if (response?.IsSuccess == true && response.Data != null)
                {
                    return View(response.Data);
                }

                ViewData["Error"] = response?.ErrorMessage ?? "Factory not found.";
            }
            catch (Exception ex)
            {
                ViewData["Error"] = $"An error occurred: {ex.Message}";
            }

            return RedirectToAction(nameof(Index));
        }

        // POST: Factory/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(FactoryDto factoryDto)
        {
            if (!ModelState.IsValid)
            {
                return View(factoryDto);
            }

            try
            {
                var response = await _httpClient.PutAsJsonAsync($"Factory/{factoryDto.ID}", factoryDto);
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }

                ModelState.AddModelError("", "Failed to update factory.");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"An error occurred: {ex.Message}");
            }

            return View(factoryDto);
        }

        // GET: Factory/Delete/{id}
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var response = await _httpClient.GetFromJsonAsync<ProcessResult<FactoryDto>>($"Factory/{id}");
                if (response?.IsSuccess == true && response.Data != null)
                {
                    return View(response.Data);
                }

                ViewData["Error"] = response?.ErrorMessage ?? "Factory not found.";
            }
            catch (Exception ex)
            {
                ViewData["Error"] = $"An error occurred: {ex.Message}";
            }

            return RedirectToAction(nameof(Index));
        }

        // POST: Factory/Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ConfirmDelete(int id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"Factory/{id}");
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }

                ModelState.AddModelError("", "Failed to delete factory.");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"An error occurred: {ex.Message}");
            }

            return RedirectToAction(nameof(Delete), new { id });
        }
    }
}