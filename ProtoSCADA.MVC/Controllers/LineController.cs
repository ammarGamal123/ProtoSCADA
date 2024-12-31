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
    public class LineController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<LineController> _logger;

        public LineController(HttpClient httpClient, ILogger<LineController> logger)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _httpClient.BaseAddress = new Uri("http://protoscada.runasp.net/api/");
        }

        // GET: Line/Index
        public async Task<IActionResult> Index(int pageNumber = 1, int pageSize = 10)
        {
            try
            {
                var response = await _httpClient.GetFromJsonAsync<ProcessResult<List<LineDto>>>($"Line?pageNumber={pageNumber}&pageSize={pageSize}");
                if (response?.IsSuccess == true && response.Data != null)
                {
                    ViewBag.PageNumber = pageNumber;
                    ViewBag.PageSize = pageSize;
                    ViewBag.TotalPages = (int)Math.Ceiling((double)response.Data.Count / pageSize);
                    return View(response.Data);
                }

                ViewData["Error"] = response?.ErrorMessage ?? "Failed to fetch line data.";
            }
            catch (Exception ex)
            {
                ViewData["Error"] = $"An error occurred: {ex.Message}";
                _logger.LogError(ex, "Error while fetching lines.");
            }

            return View(new List<LineDto>());
        }

        // GET: Line/Details/{id}
        public async Task<IActionResult> Details(int id)
        {
            try
            {
                var response = await _httpClient.GetFromJsonAsync<ProcessResult<LineDto>>($"Line/{id}");
                if (response?.IsSuccess == true && response.Data != null)
                {
                    ViewData["Title"] = $"Line Details - {response.Data.Name}";
                    return View(response.Data);
                }

                ViewData["Error"] = response?.ErrorMessage ?? "Failed to fetch line details.";
            }
            catch (Exception ex)
            {
                ViewData["Error"] = $"An error occurred: {ex.Message}";
                _logger.LogError(ex, $"Error while fetching line details for ID {id}.");
            }

            return RedirectToAction(nameof(Index));
        }

        // GET: Line/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Line/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(LineDto lineDto)
        {
            if (!ModelState.IsValid)
            {
                return View(lineDto);
            }

            try
            {
                var response = await _httpClient.PostAsJsonAsync("Line", lineDto);
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }

                ModelState.AddModelError("", "Failed to create line.");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"An error occurred: {ex.Message}");
                _logger.LogError(ex, "Error while creating line.");
            }

            return View(lineDto);
        }

        // GET: Line/Edit/{id}
        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                var response = await _httpClient.GetFromJsonAsync<ProcessResult<LineDto>>($"Line/{id}");
                if (response?.IsSuccess == true && response.Data != null)
                {
                    return View(response.Data);
                }

                ViewData["Error"] = response?.ErrorMessage ?? "Line not found.";
            }
            catch (Exception ex)
            {
                ViewData["Error"] = $"An error occurred: {ex.Message}";
                _logger.LogError(ex, $"Error while fetching line for editing with ID {id}.");
            }

            return RedirectToAction(nameof(Index));
        }

        // POST: Line/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(LineDto lineDto)
        {
            if (!ModelState.IsValid)
            {
                return View(lineDto);
            }

            try
            {
                var response = await _httpClient.PutAsJsonAsync($"Line/{lineDto.ID}", lineDto);
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }

                ModelState.AddModelError("", "Failed to update line.");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"An error occurred: {ex.Message}");
                _logger.LogError(ex, $"Error while updating line with ID {lineDto.ID}.");
            }

            return View(lineDto);
        }

        // GET: Line/Delete/{id}
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var response = await _httpClient.GetFromJsonAsync<ProcessResult<LineDto>>($"Line/{id}");
                if (response?.IsSuccess == true && response.Data != null)
                {
                    return View(response.Data);
                }

                ViewData["Error"] = response?.ErrorMessage ?? "Line not found.";
            }
            catch (Exception ex)
            {
                ViewData["Error"] = $"An error occurred: {ex.Message}";
                _logger.LogError(ex, $"Error while fetching line for deletion with ID {id}.");
            }

            return RedirectToAction(nameof(Index));
        }

        // POST: Line/Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ConfirmDelete(int id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"Line/{id}");
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }

                ModelState.AddModelError("", "Failed to delete line.");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"An error occurred: {ex.Message}");
                _logger.LogError(ex, $"Error while deleting line with ID {id}.");
            }

            return RedirectToAction(nameof(Delete), new { id });
        }
    }
}