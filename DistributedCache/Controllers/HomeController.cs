using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using DistributedCache.Services;

namespace DistributedCache.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUserService _userService;
        private readonly ICacheUserService _cacheUserService;

        public HomeController(ILogger<HomeController> logger, IUserService userService, ICacheUserService cacheUserService)
        {
            _logger = logger;
            _userService = userService;
            _cacheUserService = cacheUserService;
        }

        public async Task<IActionResult> Index()
        {
            var users = (await _cacheUserService.GetCachedUser())?.FirstOrDefault();
            return View(users);
        }

        public async Task<IActionResult> CacheUserAsync()
        {
            var users = await _userService.GetUsersAsync();
            var cachedEntry = users.First();
            return View(nameof(Index), cachedEntry);
        }

        public IActionResult CacheRemoveAsync()
        {
            _cacheUserService.ClearCache();
            return RedirectToAction(nameof(Index));
        }
    }
}
