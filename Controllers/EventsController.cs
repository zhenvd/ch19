using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CodingEventsDemo.Data;
using CodingEventsDemo.Models;
using CodingEventsDemo.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace coding_events_practice.Controllers
{
    public class EventsController : Controller
    {
        private EventDbContext _context;
        public EventsController(EventDbContext dbContext)
        {
            _context = dbContext;
        }
        // GET: /<controller>/
        public IActionResult Index()
        {
            //List<Event> events = _context.Events.ToList();
            List<Event> events = _context.Events
            .Include(e => e.Category)
            .ToList();
            return View(events);
        }

        public IActionResult Add()
        {
            //AddEventViewModel addEventViewModel = new AddEventViewModel();
            List<EventCategory> categories = _context.Categories.ToList();
            AddEventViewModel addEventViewModel = new AddEventViewModel(categories);
            return View(addEventViewModel);
        }

        [HttpPost]
        public IActionResult Add(AddEventViewModel addEventViewModel)
        {
            if (ModelState.IsValid)
            {
                EventCategory theCategory = _context.Categories.Find(addEventViewModel.CategoryId);
                Event newEvent = new Event
                {
                    Name = addEventViewModel.Name,
                    Description = addEventViewModel.Description,
                    ContactEmail = addEventViewModel.ContactEmail,
                    Category = theCategory
                };

                _context.Events.Add(newEvent); // This stages the data
                _context.SaveChanges(); // This actually saves the data in the DB

                return Redirect("/Events");
            }

            return View(addEventViewModel);
        }

        public IActionResult Delete()
        {
            ViewBag.events = _context.Events.ToList();

            return View();
        }

        [HttpPost]
        public IActionResult Delete(int[] eventIds)
        {
            foreach (int eventId in eventIds)
            {
                Event theEvent = _context.Events.Find(eventId);
                _context.Events.Remove(theEvent);
            }

            _context.SaveChanges();

            return Redirect("/Events");
        }

        [HttpGet]
        public IActionResult Detail(int id)
        {
            Event theEvent = _context.Events
               .Include(e => e.Category)
               .FirstOrDefault(e => e.Id == id);

            EventDetailViewModel viewModel = new EventDetailViewModel(theEvent);


            return View(viewModel);
        }
    }
}
