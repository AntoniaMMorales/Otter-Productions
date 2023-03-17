using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OtterProductions_CapstoneProject.Data;
using OtterProductions_CapstoneProject.Models;
using Microsoft.AspNetCore.Authorization;

namespace OtterProductions_CapstoneProject.Controllers
{
    [Authorize(Roles = "admin")]
    public class MapAppUsersController : Controller
    {
        private readonly MapAppDbContext _context;

        public MapAppUsersController(MapAppDbContext context)
        {
            _context = context;
        }

        // GET: MapAppUsers

        public async Task<IActionResult> Index()
        {
            return View(await _context.MapAppUsers.ToListAsync());
        }

    }
}