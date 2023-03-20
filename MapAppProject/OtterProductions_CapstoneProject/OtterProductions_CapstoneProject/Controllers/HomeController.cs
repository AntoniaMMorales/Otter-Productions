﻿using System;
using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OtterProductions_CapstoneProject.Areas.Identity.Data;
using OtterProductions_CapstoneProject.DAL.Abstract;
using OtterProductions_CapstoneProject.DAL.Concrete;
using OtterProductions_CapstoneProject.Data;
using OtterProductions_CapstoneProject.Models;

namespace OtterProductions_CapstoneProject.Controllers
{

    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly MapAppDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private IBrowseEventRepository _eventRepository;

        public HomeController(ILogger<HomeController> logger, MapAppDbContext ctx, UserManager<ApplicationUser> userManager)
        {
            _logger = logger;
            _context = ctx;
            _userManager = userManager;
            _eventRepository = new BrowseEventRepository(_context);
        }
        [AllowAnonymous]
        public IActionResult Index()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult Index(Location mapLocation)
        {

            return RedirectToAction("Mappage", "Map", mapLocation);

        }

        [AllowAnonymous]
        public IActionResult Privacy()
        {
            return View();
        }

        [AllowAnonymous]
        public IActionResult FAQ()
        {
            return View();
        }

        [AllowAnonymous]
        public IActionResult Event()
        {
            IEnumerable<Event> events = _context.Events.ToList();
            return View(events);
        }
        
        [AllowAnonymous]
        [HttpGet]
        public IActionResult Browsing()
        {
            //Creates a viewmodel and grabs the events and organizations
            BrowseViewModel browseView = new BrowseViewModel();
            browseView.Events = _eventRepository.GetAllEventsWithinTwoWeeks(DateOnly.FromDateTime(DateTime.Now));
            browseView.Organizations = _context.Organizations.ToList();

            //if (browseView.Events == null)
            //{
            //    return NotFound();
            //}

            return View(browseView);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}