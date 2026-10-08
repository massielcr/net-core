using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PSGettingStartedSWWeb.DataContext;
using PSGettingStartedSWWeb.Entities;
using PSGettingStartedSWWeb.Models;
using System.Diagnostics;

namespace PSGettingStartedSWWeb.Controllers
{
    public class HomeController : Controller
    {
        private FilmDbContext _filmDbContext;

        public HomeController(FilmDbContext filmDbContext)
        {
            _filmDbContext = filmDbContext;
        }

        public async Task<IActionResult> Index()
        {
            IEnumerable<Film> films = await _filmDbContext.Films.OrderBy(f => f.RatingScore).Select(f => f).ToListAsync();

            return View(films);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
