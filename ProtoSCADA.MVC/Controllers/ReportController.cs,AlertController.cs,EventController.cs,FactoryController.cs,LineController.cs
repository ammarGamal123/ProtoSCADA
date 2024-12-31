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
    public class ReportController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<ReportController> _logger;

        public ReportController(HttpClient httpClient, ILogger<ReportController> logger)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _httpClient.BaseAddress = new Uri("http://protoscada.runasp.net/api/");
        }

        // GET: Report/Index
        public async Task<IActionResult> Index(int pageNumber = 1, int pageSize = 10)
        {
            try
            {
                var response = await _httpClient.GetFromJsonAsync<ProcessResult<List<ReportDto>>>($"Report?pageNumber={pageNumber}&pageSize={pageSize}");
                if (response?.IsSuccess == true && response.Data != null)
                {
                    ViewBag.PageNumber = pageNumber;
                    ViewBag.PageSize = pageSize;
                    ViewBag.TotalPages = (int)Math.Ceiling((double)response.Data.Count / pageSize);
                    return View(response.Data); // Pass a list of ReportDto to the Index view
                }

                ViewData["Error"] = response?.ErrorMessage ?? "Failed to fetch report data.";
            }
            catch (Exception ex)
            {
                ViewData["Error"] = $"An error occurred: {ex.Message}";
                _logger.LogError(ex, "Error while fetching reports.");
            }

            return View(new List<ReportDto>());
        }

        // GET: Report/Details/{id}
        public async Task<IActionResult> Details(int id)
        {
            try
            {
                var response = await _httpClient.GetFromJsonAsync<ProcessResult<ReportDto>>($"Report/{id}");
                if (response?.IsSuccess == true && response.Data != null)
                {
                    ViewData["Title"] = $"Report Details - {response.Data.Name}";
                    return View(response.Data); // Pass a single ReportDto to the Details view
                }

                ViewData["Error"] = response?.ErrorMessage ?? "Failed to fetch report details.";
            }
            catch (Exception ex)
            {
                ViewData["Error"] = $"An error occurred: {ex.Message}";
                _logger.LogError(ex, $"Error while fetching report details for ID {id}.");
            }

            return RedirectToAction(nameof(Index));
        }

        // GET: Report/Create
        public IActionResult Create()
        {
            return View(); // Return the Create view
        }

        // POST: Report/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ReportDto reportDto)
        {
            if (!ModelState.IsValid)
            {
                return View(reportDto); // Return the view with validation errors
            }

            try
            {
                var response = await _httpClient.PostAsJsonAsync("Report", reportDto);
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index)); // Redirect to Index after successful creation
                }

                ModelState.AddModelError("", "Failed to create report.");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"An error occurred: {ex.Message}");
                _logger.LogError(ex, "Error while creating report.");
            }

            return View(reportDto); // Return the view with errors
        }

        // GET: Report/Edit/{id}
        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                var response = await _httpClient.GetFromJsonAsync<ProcessResult<ReportDto>>($"Report/{id}");
                if (response?.IsSuccess == true && response.Data != null)
                {
                    return View(response.Data); // Pass a single ReportDto to the Edit view
                }

                ViewData["Error"] = response?.ErrorMessage ?? "Report not found.";
            }
            catch (Exception ex)
            {
                ViewData["Error"] = $"An error occurred: {ex.Message}";
                _logger.LogError(ex, $"Error while fetching report for editing with ID {id}.");
            }

            return RedirectToAction(nameof(Index));
        }

        // POST: Report/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ReportDto reportDto)
        {
            if (!ModelState.IsValid)
            {
                return View(reportDto); // Return the view with validation errors
            }

            try
            {
                var response = await _httpClient.PutAsJsonAsync($"Report/{reportDto.ID}", reportDto);
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index)); // Redirect to Index after successful update
                }

                ModelState.AddModelError("", "Failed to update report.");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"An error occurred: {ex.Message}");
                _logger.LogError(ex, $"Error while updating report with ID {reportDto.ID}.");
            }

            return View(reportDto); // Return the view with errors
        }

        // GET: Report/Delete/{id}
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var response = await _httpClient.GetFromJsonAsync<ProcessResult<ReportDto>>($"Report/{id}");
                if (response?.IsSuccess == true && response.Data != null)
                {
                    return View(response.Data); // Pass a single ReportDto to the Delete view
                }

                ViewData["Error"] = response?.ErrorMessage ?? "Report not found.";
            }
            catch (Exception ex)
            {
                ViewData["Error"] = $"An error occurred: {ex.Message}";
                _logger.LogError(ex, $"Error while fetching report for deletion with ID {id}.");
            }

            return RedirectToAction(nameof(Index));
        }

        // POST: Report/Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ConfirmDelete(int id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"Report/{id}");
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index)); // Redirect to Index after successful deletion
                }

                ModelState.AddModelError("", "Failed to delete report.");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"An error occurred: {ex.Message}");
                _logger.LogError(ex, $"Error while deleting report with ID {id}.");
            }

            return RedirectToAction(nameof(Delete), new { id }); // Redirect back to Delete view if deletion fails
        }
    }
}