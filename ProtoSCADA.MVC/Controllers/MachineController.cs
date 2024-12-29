using Microsoft.AspNetCore.Mvc;
using ProtoSCADA.Entities.Entities;
using ProtoSCADA.Service.Utilities;
using System.Net.Http.Json;

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

        // GET: Machine/Index (List all machines)
        public async Task<IActionResult> Index()
        {
            try
            {
                // Fetch data from the API
                var response = await _httpClient.GetFromJsonAsync<ProcessResult<List<Machine>>>("Machine");

                // Check if the response is successful and data exists
                if (response is { IsSuccess: true, Data: not null })
                {
                    return View(response.Data); // Pass the data to the view
                }

                // Handle API errors or missing data
                ViewData["Error"] = response?.ErrorMessage ?? "Failed to fetch machine data.";
            }
            catch (HttpRequestException httpEx)
            {
                // Handle HTTP-specific exceptions (e.g., network issues)
                ViewData["Error"] = $"Network error occurred while loading machines: {httpEx.Message}";
            }
            catch (Exception ex)
            {
                // Handle other exceptions
                ViewData["Error"] = $"An unexpected error occurred: {ex.Message}";
            }

            // Return an empty list if an error occurs
            return View(new ProcessResult<List<Machine>>());
        }


        // GET: Machine/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Machine/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Machine machine)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var response = await _httpClient.PostAsJsonAsync("Machine", machine);
                    if (response.IsSuccessStatusCode)
                        return RedirectToAction(nameof(Index));

                    ModelState.AddModelError("", "Failed to create machine.");
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"Error creating machine: {ex.Message}");
            }
            return View(machine);
        }

        // GET: Machine/Edit/{id}
        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                var machine = await _httpClient.GetFromJsonAsync<ProcessResult<Machine>>($"Machine/{id}");
                return machine != null ? View(machine) : NotFound();
            }
            catch (Exception ex)
            {
                ViewData["Error"] = $"Error loading machine: {ex.Message}";
                return RedirectToAction(nameof(Index));
            }
        }

        // POST: Machine/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Machine machine)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var response = await _httpClient.PutAsJsonAsync<Machine>($"Machine/{machine.ID}", machine);
                    if (response.IsSuccessStatusCode)
                        return RedirectToAction(nameof(Index));

                    ModelState.AddModelError("", "Failed to update machine.");
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"Error updating machine: {ex.Message}");
            }
            return View(machine);
        }

        // GET: Machine/Delete/{id}
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var machine = await _httpClient.GetFromJsonAsync<Machine>($"Machine/{id}");
                return machine != null ? View(machine) : NotFound();
            }
            catch (Exception ex)
            {
                ViewData["Error"] = $"Error loading machine: {ex.Message}";
                return RedirectToAction(nameof(Index));
            }
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
                    return RedirectToAction(nameof(Index));

                ModelState.AddModelError("", "Failed to delete machine.");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"Error deleting machine: {ex.Message}");
            }
            return RedirectToAction(nameof(Delete), new { id });
        }

        // GET: Machine/Details/{id}
        public async Task<IActionResult> Details(int id)
        {
            try
            {
                // Fetch machine details from the API
                var response = await _httpClient.GetFromJsonAsync<ProcessResult<Machine>>($"Machine/{id}");

                // Check if the response is successful and data exists
                if (response is { IsSuccess: true, Data: not null })
                {
                    // Set ViewData to provide the title dynamically
                    ViewData["Title"] = $"Machine Details - {response.Data.Name}"; // Dynamic title using machine name
                    return View(response.Data); // Pass the machine data to the view
                }

                // Handle API errors or missing data
                ViewData["Error"] = response?.ErrorMessage ?? "Failed to fetch machine details.";
            }
            catch (HttpRequestException httpEx)
            {
                // Handle HTTP-specific exceptions (e.g., network issues)
                ViewData["Error"] = $"Network error occurred while loading machine details: {httpEx.Message}";
            }
            catch (Exception ex)
            {
                // Handle other exceptions
                ViewData["Error"] = $"An unexpected error occurred: {ex.Message}";
            }

            // Return a NotFound or error message if an error occurs
            return View("Error");
        }


    }
}
