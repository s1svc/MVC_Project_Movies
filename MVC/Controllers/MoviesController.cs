#nullable disable
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using CORE.Services;
using APP.Models;

namespace MVC.Controllers
{
    public class MoviesController : Controller
    {
        private readonly IService<MovieRequest, MovieResponse> _movieService;
        private readonly IService<DirectorRequest, DirectorResponse> _directorService;
        private readonly IService<GenreRequest, GenreResponse> _genreService;

        public MoviesController(
			IService<MovieRequest, MovieResponse> movieService
            , IService<DirectorRequest, DirectorResponse> directorService
            , IService<GenreRequest, GenreResponse> genreService
        )
        {
            _movieService = movieService;
            _directorService = directorService;
            _genreService = genreService;
        }

        private void SetViewData()
        {
            ViewData["DirectorId"] = new SelectList(_directorService.List(), "Id", "FullNameR");
            ViewBag.GenreIds = new MultiSelectList(_genreService.List(), "Id", "Name");
        }

        private void SetTempData(string message, string key = "Message")
        {
            TempData[key] = message;
        }

        public IActionResult Index(string sortBy)
        {
            var list = _movieService.List();
            if (sortBy == "Name")
                list = list.OrderBy(m => m.Name).ToList();
            else if (sortBy == "TotalRevenue")
                list = list.OrderByDescending(m => m.TotalRevenue).ToList();
            else if (sortBy == "Director")
                list = list.OrderBy(m => m.DirectorNameR).ToList();
            ViewBag.SortBy = new SelectList(new Dictionary<string, string>
            {
                { "ReleaseDate", "Release Date" },
                { "Name", "Name" },
                { "TotalRevenue", "Total Revenue" },
                { "Director", "Director" }
            }, "Key", "Value", sortBy);
            return View(list);
        }

        public IActionResult Details(int id)
        {
            var item = _movieService.Single(id);
            return View(item);
        }

        public IActionResult Create()
        {
            SetViewData();
            return View();
        }

        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Create(MovieRequest movie)
        {
            if (ModelState.IsValid)
            {
                var response = _movieService.Add(movie);
                if (response.IsSuccessful)
                {
                    SetTempData(response.Message);
                    return RedirectToAction(nameof(Details), new { id = response.Id });
                }
                ModelState.AddModelError("", response.Message);
            }
            SetViewData();
            return View(movie);
        }

        public IActionResult Edit(int id)
        {
            var item = _movieService.Edit(id);
            SetViewData();
            return View(item);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Edit(MovieRequest movie)
        {
            if (ModelState.IsValid)
            {
                var response = _movieService.Update(movie);
                if (response.IsSuccessful)
                {
                    SetTempData(response.Message);
                    return RedirectToAction(nameof(Details), new { id = response.Id });
                }
                ModelState.AddModelError("", response.Message);
            }
            SetViewData();
            return View(movie);
        }

        public IActionResult Delete(int id)
        {
            var item = _movieService.Single(id);
            return View(item);
        }

        [HttpPost, ValidateAntiForgeryToken, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            var response = _movieService.Remove(id);
            SetTempData(response.Message);
            return RedirectToAction(nameof(Index));
        }
    }
}
