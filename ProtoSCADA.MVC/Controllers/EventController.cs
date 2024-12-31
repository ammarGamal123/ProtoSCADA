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
    public class EventController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<EventController> _logger;

        public EventController(HttpClient httpClient, ILogger<EventController> logger)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _httpClient.BaseAddress = new Uri("http://protoscada.runasp.net/api/");
        }

        // GET: Event/Index
        public async Task<IActionResult> Index(int pageNumber = 1, int pageSize = 10)
        {
            try
            {
                var response = await _httpClient.GetFromJsonAsync<ProcessResult<List<EventDto>>>($"Event?pageNumber={pageNumber}&pageSize={pageSize}");
                if (response?.IsSuccess == true && response.Data != null)
                {
                    ViewBag.PageNumber = pageNumber;
                    ViewBag.PageSize = pageSize;
                    ViewBag.TotalPages = (int)Math.Ceiling((double)response.Data.Count / pageSize);
                    return View(response.Data);
                }

                ViewData["Error"] = response?.ErrorMessage ?? "Failed to fetch event data.";
            }
            catch (Exception ex)
            {
                ViewData["Error"] = $"An error occurred: {ex.Message}";
                _logger.LogError(ex, "Error while fetching events.");
            }

            return View(new List<EventDto>());
        }

        // GET: Event/Details/{id}
        public async Task<IActionResult> Details(int id)
        {
            try
            {
                var response = await _httpClient.GetFromJsonAsync<ProcessResult<EventDto>>($"Event/{id}");
                if (response?.IsSuccess == true && response.Data != null)
                {
                    ViewData["Title"] = $"Event Details - {response.Data.ID}";
                    return View(response.Data);
                }

                ViewData["Error"] = response?.ErrorMessage ?? "Failed to fetch event details.";
            }
            catch (Exception ex)
            {
                ViewData["Error"] = $"An error occurred: {ex.Message}";
                _logger.LogError(ex, $"Error while fetching event details for ID {id}.");
            }

            return RedirectToAction(nameof(Index));
        }

        // GET: Event/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Event/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(EventDto eventDto)
        {
            if (!ModelState.IsValid)
            {
                return View(eventDto);
            }

            try
            {
                var response = await _httpClient.PostAsJsonAsync("Event", eventDto);
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }

                ModelState.AddModelError("", "Failed to create event.");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"An error occurred: {ex.Message}");
                _logger.LogError(ex, "Error while creating event.");
            }

            return View(eventDto);
        }

        // GET: Event/Edit/{id}
        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                var response = await _httpClient.GetFromJsonAsync<ProcessResult<EventDto>>($"Event/{id}");
                if (response?.IsSuccess == true && response.Data != null)
                {
                    return View(response.Data);
                }

                ViewData["Error"] = response?.ErrorMessage ?? "Event not found.";
            }
            catch (Exception ex)
            {
                ViewData["Error"] = $"An error occurred: {ex.Message}";
                _logger.LogError(ex, $"Error while fetching event for editing with ID {id}.");
            }

            return RedirectToAction(nameof(Index));
        }

        // POST: Event/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EventDto eventDto)
        {
            if (!ModelState.IsValid)
            {
                return View(eventDto);
            }

            try
            {
                var response = await _httpClient.PutAsJsonAsync($"Event/{eventDto.ID}", eventDto);
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }

                ModelState.AddModelError("", "Failed to update event.");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"An error occurred: {ex.Message}");
                _logger.LogError(ex, $"Error while updating event with ID {eventDto.ID}.");
            }

            return View(eventDto);
        }

        // GET: Event/Delete/{id}
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var response = await _httpClient.GetFromJsonAsync<ProcessResult<EventDto>>($"Event/{id}");
                if (response?.IsSuccess == true && response.Data != null)
                {
                    return View(response.Data);
                }

                ViewData["Error"] = response?.ErrorMessage ?? "Event not found.";
            }
            catch (Exception ex)
            {
                ViewData["Error"] = $"An error occurred: {ex.Message}";
                _logger.LogError(ex, $"Error while fetching event for deletion with ID {id}.");
            }

            return RedirectToAction(nameof(Index));
        }

        // POST: Event/Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ConfirmDelete(int id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"Event/{id}");
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }

                ModelState.AddModelError("", "Failed to delete event.");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"An error occurred: {ex.Message}");
                _logger.LogError(ex, $"Error while deleting event with ID {id}.");
            }

            return RedirectToAction(nameof(Delete), new { id });
        }
    }
}