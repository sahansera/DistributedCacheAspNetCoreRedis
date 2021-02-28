using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using DistributedCache.Models;
using DistributedCache.Services;

namespace DistributedCache.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUsersService _usersService;
        private readonly ICacheService _cacheService;

        public HomeController(ILogger<HomeController> logger, IUsersService usersService, ICacheService cacheService)
        {
            _logger = logger;
            _usersService = usersService;
            _cacheService = cacheService;
        }

        public async Task<IActionResult> Index()
        {
            var users = (await _cacheService.GetCachedUser())?.FirstOrDefault();
            return View(users);
        }

        public async Task<IActionResult> CacheUserAsync()
        {
            var users = await _usersService.GetUsersAsync();
            var cacheEntry = users.First();
            return View(nameof(Index), cacheEntry);
        }

        public IActionResult CacheRemoveAsync()
        {
            _cacheService.ClearCache();
            return RedirectToAction(nameof(Index));
        }
    }
}
