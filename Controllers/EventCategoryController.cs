using CodingEventsDemo.Data;
using CodingEventsDemo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodingEventsDemo.Controllers
{
    public class EventCategoryController : Controller
    {
        //public DbSet<EventCategory> Categories { get; set; }
        private EventDbContext _context;
        public EventCategoryController(EventDbContext dbContext)
        {
            _context = dbContext;
        }
        public IActionResult Index()
        {
            List<EventCategory> category = _context.Category.ToList();
            return View(category);
        }
    }
}
