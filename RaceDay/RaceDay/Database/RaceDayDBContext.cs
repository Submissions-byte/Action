using Microsoft.EntityFrameworkCore;
using RaceDay.Models;
using Results = RaceDay.Models.Results;

namespace RaceDay.Database
{
    public class RaceDayDBContext : DbContext
    {
        public RaceDayDBContext(DbContextOptions<RaceDayDBContext> options) : base(options) { }

        public DbSet<Participant> Participant { get; set; }
        public DbSet<Organiser> Organiser { get; set; }
        public DbSet<Location> Location { get; set; }
        public DbSet<Category> Category { get; set; }
        public DbSet<Event> Event { get; set; }
        public DbSet<Enrolment> Enrolment { get; set; }
        public DbSet<Results> Results { get; set; }
    }
}
