using Microsoft.AspNetCore.Mvc;
using ProtoSCADA.Entities.DTOs;
using ProtoSCADA.Service.Utilities;
using System.Net.Http.Json;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using Microsoft.Extensions.Logging;

namespace ProtoSCADA.MVC.Controllers
{
    public class AlertController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<AlertController> _logger;

        public AlertController(HttpClient httpClient, ILogger<AlertController> logger)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _httpClient.BaseAddress = new Uri("http://protoscada.runasp.net/api/");
        }

        // GET: Alert/Index
        public async Task<IActionResult> Index(int pageNumber = 1, int pageSize = 10)
        {
            try
            {
                var response = await _httpClient.GetFromJsonAsync<ProcessResult<List<AlertDto>>>($"Alert?pageNumber={pageNumber}&pageSize={pageSize}");
                if (response?.IsSuccess == true && response.Data != null)
                {
                    ViewBag.PageNumber = pageNumber;
                    ViewBag.PageSize = pageSize;
                    ViewBag.TotalPages = (int)Math.Ceiling((double)response.Data.Count / pageSize);
                    return View(response.Data);
                }

                ViewData["Error"] = response?.ErrorMessage ?? "Failed to fetch alert data.";
            }
            catch (Exception ex)
            {
                ViewData["Error"] = $"An error occurred: {ex.Message}";
                _logger.LogError(ex, "Error while fetching alerts.");
            }

            return View(new List<AlertDto>());
        }

        // GET: Alert/Details/{id}
        public async Task<IActionResult> Details(int id)
        {
            try
            {
                var response = await _httpClient.GetFromJsonAsync<ProcessResult<AlertDto>>($"Alert/{id}");
                if (response?.IsSuccess == true && response.Data != null)
                {
                    ViewData["Title"] = $"Alert Details - {response.Data.ID}";
                    return View(response.Data);
                }

                ViewData["Error"] = response?.ErrorMessage ?? "Failed to fetch alert details.";
            }
            catch (Exception ex)
            {
                ViewData["Error"] = $"An error occurred: {ex.Message}";
                _logger.LogError(ex, $"Error while fetching alert details for ID {id}.");
            }

            return RedirectToAction(nameof(Index));
        }

        // GET: Alert/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Alert/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AlertDto alertDto)
        {
            if (!ModelState.IsValid)
            {
                return View(alertDto);
            }

            try
            {
                var response = await _httpClient.PostAsJsonAsync("Alert", alertDto);
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }

                ModelState.AddModelError("", "Failed to create alert.");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"An error occurred: {ex.Message}");
                _logger.LogError(ex, "Error while creating alert.");
            }

            return View(alertDto);
        }

        // GET: Alert/Edit/{id}
        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                var response = await _httpClient.GetFromJsonAsync<ProcessResult<AlertDto>>($"Alert/{id}");
                if (response?.IsSuccess == true && response.Data != null)
                {
                    return View(response.Data);
                }

                ViewData["Error"] = response?.ErrorMessage ?? "Alert not found.";
            }
            catch (Exception ex)
            {
                ViewData["Error"] = $"An error occurred: {ex.Message}";
                _logger.LogError(ex, $"Error while fetching alert for editing with ID {id}.");
            }

            return RedirectToAction(nameof(Index));
        }

        // POST: Alert/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(AlertDto alertDto)
        {
            if (!ModelState.IsValid)
            {
                return View(alertDto);
            }

            try
            {
                var response = await _httpClient.PutAsJsonAsync($"Alert/{alertDto.ID}", alertDto);
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }

                ModelState.AddModelError("", "Failed to update alert.");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"An error occurred: {ex.Message}");
                _logger.LogError(ex, $"Error while updating alert with ID {alertDto.ID}.");
            }

            return View(alertDto);
        }

        // GET: Alert/Delete/{id}
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var response = await _httpClient.GetFromJsonAsync<ProcessResult<AlertDto>>($"Alert/{id}");
                if (response?.IsSuccess == true && response.Data != null)
                {
                    return View(response.Data);
                }

                ViewData["Error"] = response?.ErrorMessage ?? "Alert not found.";
            }
            catch (Exception ex)
            {
                ViewData["Error"] = $"An error occurred: {ex.Message}";
                _logger.LogError(ex, $"Error while fetching alert for deletion with ID {id}.");
            }

            return RedirectToAction(nameof(Index));
        }

        // POST: Alert/Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ConfirmDelete(int id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"Alert/{id}");
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }

                ModelState.AddModelError("", "Failed to delete alert.");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"An error occurred: {ex.Message}");
                _logger.LogError(ex, $"Error while deleting alert with ID {id}.");
            }

            return RedirectToAction(nameof(Delete), new { id });
        }
    }
}