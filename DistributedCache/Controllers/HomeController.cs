using Microsoft.AspNetCore.Mvc;
using DistributedCache.Services;

namespace DistributedCache.Controllers;

public class HomeController(
    IUserService userService,
    ICacheUserService cacheUserService) : Controller
{
    private readonly IUserService _userService = userService;
    private readonly ICacheUserService _cacheUserService = cacheUserService;

    public async Task<IActionResult> Index()
    {
        var user = (await _cacheUserService.GetCachedUserAsync()).FirstOrDefault();
        return View(user);
    }

    public async Task<IActionResult> CacheUserAsync()
    {
        var users = await _userService.GetUsersAsync();
        return View(nameof(Index), users.FirstOrDefault());
    }

    public async Task<IActionResult> CacheRemoveAsync()
    {
        await _cacheUserService.ClearCacheAsync();
        return RedirectToAction(nameof(Index));
    }
}
