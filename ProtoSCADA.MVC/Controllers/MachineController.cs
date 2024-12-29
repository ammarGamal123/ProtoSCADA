using Microsoft.AspNetCore.Mvc;
using ProtoSCADA.Entities.Entities;
using ProtoSCADA.Service.Utilities;
using System.Net.Http.Json;
using ProtoSCADA.Entities.DTOs;

namespace ProtoSCADA.MVC.Controllers
{
    public class MachineController : Controller
    {
        private readonly HttpClient _httpClient;

        public MachineController(HttpClient httpClient)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
            _httpClient.BaseAddress = new Uri("http://protoscada.runasp.net/api/");
        }

        // GET: Machine/Index
        public async Task<IActionResult> Index()
        {
            try
            {
                var response = await _httpClient.GetFromJsonAsync<ProcessResult<List<MachineDto>>>("Machine");
                if (response?.IsSuccess == true && response.Data != null)
                {
                    return View(response.Data);
                }

                ViewData["Error"] = response?.ErrorMessage ?? "Failed to fetch machine data.";
            }
            catch (Exception ex)
            {
                ViewData["Error"] = $"An error occurred: {ex.Message}";
            }

            return View(new List<MachineDto>());
        }

        // GET: Machine/Details/{id}
        public async Task<IActionResult> Details(int id)
        {
            try
            {
                var response = await _httpClient.GetFromJsonAsync<ProcessResult<MachineDto>>($"Machine/{id}");
                if (response?.IsSuccess == true && response.Data != null)
                {
                    ViewData["Title"] = $"Machine Details - {response.Data.MachineType}";
                    return View(response.Data);
                }

                ViewData["Error"] = response?.ErrorMessage ?? "Failed to fetch machine details.";
            }
            catch (Exception ex)
            {
                ViewData["Error"] = $"An error occurred: {ex.Message}";
            }

            return View("Error");
        }

        // GET: Machine/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Machine/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(MachineDto machineDto)
        {
            if (!ModelState.IsValid)
            {
                return View(machineDto);
            }

            try
            {
                var response = await _httpClient.PostAsJsonAsync("Machine", machineDto);
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }

                ModelState.AddModelError("", "Failed to create machine.");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"An error occurred: {ex.Message}");
            }

            return View(machineDto);
        }

        // GET: Machine/Edit/{id}
        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                var response = await _httpClient.GetFromJsonAsync<ProcessResult<MachineDto>>($"Machine/{id}");
                if (response?.IsSuccess == true && response.Data != null)
                {
                    return View(response.Data);
                }

                ViewData["Error"] = response?.ErrorMessage ?? "Machine not found.";
            }
            catch (Exception ex)
            {
                ViewData["Error"] = $"An error occurred: {ex.Message}";
            }

            return RedirectToAction(nameof(Index));
        }

        // POST: Machine/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(MachineDto machineDto)
        {
            if (!ModelState.IsValid)
            {
                return View(machineDto);
            }

            try
            {
                var response = await _httpClient.PutAsJsonAsync($"Machine/{machineDto.MachineID}", machineDto);
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }

                ModelState.AddModelError("", "Failed to update machine.");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"An error occurred: {ex.Message}");
            }

            return View(machineDto);
        }

        // GET: Machine/Delete/{id}
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var response = await _httpClient.GetFromJsonAsync<ProcessResult<MachineDto>>($"Machine/{id}");
                if (response?.IsSuccess == true && response.Data != null)
                {
                    return View(response.Data);
                }

                ViewData["Error"] = response?.ErrorMessage ?? "Machine not found.";
            }
            catch (Exception ex)
            {
                ViewData["Error"] = $"An error occurred: {ex.Message}";
            }

            return RedirectToAction(nameof(Index));
        }

        // POST: Machine/Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ConfirmDelete(int id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"Machine/{id}");
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }

                ModelState.AddModelError("", "Failed to delete machine.");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"An error occurred: {ex.Message}");
            }

            return RedirectToAction(nameof(Delete), new { id });
        }
    }
}