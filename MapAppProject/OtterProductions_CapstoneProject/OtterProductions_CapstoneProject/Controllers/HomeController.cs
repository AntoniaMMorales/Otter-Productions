﻿using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OtterProductions_CapstoneProject.Areas.Identity.Data;
using OtterProductions_CapstoneProject.Data;
using OtterProductions_CapstoneProject.Models;

namespace OtterProductions_CapstoneProject.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly MapAppDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

<<<<<<< HEAD
        public HomeController(ILogger<HomeController> logger, MapAppDbContext ctx, UserManager<ApplicationUser> userManager)
        {
            _logger = logger;
            _context = ctx;
            _userManager = userManager;
        }
=======
    public IActionResult Index()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Index(Location mapLocation)
    {

        return RedirectToAction("Mappage", "Map", mapLocation);

    }
   
    [Authorize]
    public IActionResult Privacy()
    {
        return View();
    }
>>>>>>> dev

        public IActionResult Index()
        {
            return View();
        }

        //public async Task<IActionResult> Index()
        //{
        //    //// Information straight from the Controller (does not need to go to the database)
        //    //bool isAdmin = User.IsInRole("Admin");
        //    //bool isAuthenticated = User.Identity.IsAuthenticated;
        //    //string name = User.Identity.Name;
        //    //string authType = User.Identity.AuthenticationType;

        //    //// Information from Identity through the user manager
        //    //string id = _userManager.GetUserId(User);       // reportedly does not need to hit the db
        //    //ApplicationUser user = await _userManager.GetUserAsync(User);  // does go to the db
        //    //string email = user?.Email ?? "no email";
        //    //string phone = user?.PhoneNumber ?? "no phone number";
        //    //ViewBag.Message = $"User {name} is authenticated? {isAuthenticated} using type {authType} and is an Admin? {isAdmin}.  ID from Identity is {id}, email is {email} and phone is {phone}";

        //    //MapAppUser maUser = null;
        //    //if (id != null)
        //    //{
        //    //    maUser = _context.MapAppUsers.Where(u => u.AspnetIdentityId == id).FirstOrDefault();
        //    //}

        //    //MainPageVM vm = new MainPageVM { TheIdentityUser = user, TheUser = maUser};

        //    //// Read cooki
        //    //string cookie = Request.Cookies["MapApp-app"];
        //    //_logger.LogInformation($"Read cookie: {cookie}");

        //    return View();
        //}

        [Authorize]
        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult FAQ()
        {
            return View();
        }

        public IActionResult Greeting()
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