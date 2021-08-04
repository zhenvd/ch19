using CodingEventsDemo.Data;
using CodingEventsDemo.Models;
using CodingEventsDemo.ViewModels;
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

        public IActionResult Create()
        {
            AddEventCategoryViewModel addEventCategoryViewModel = new AddEventCategoryViewModel();
            return View(addEventCategoryViewModel);
        }

        [HttpPost]
        [Route("/List")]
        public IActionResult ProcessCreateEventCategoryForm(AddEventCategoryViewModel addEventCategoryViewModel)
        {
            if (ModelState.IsValid)
            {
                EventCategory newEventCategory = new EventCategory(addEventCategoryViewModel.Name);
                _context.Category.Add(newEventCategory); // This stages the data
                _context.SaveChanges(); // This actually saves the data in the DB

                return Redirect("/EventCategory");
            }

            return View("Create", addEventCategoryViewModel);
        }
    }
}
