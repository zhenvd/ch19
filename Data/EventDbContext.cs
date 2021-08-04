using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using CodingEventsDemo.Models;

namespace CodingEventsDemo.Data
{
    public class EventDbContext : DbContext
    {
        public DbSet<Event> Events { get; set; }
        public DbSet<EventCategory> Category { get; set; }

        public EventDbContext(DbContextOptions<EventDbContext> options) : base(options)
        {
        }
    }
}
