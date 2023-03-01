using Meetup.DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Meetup.DataAccess.DataContext
{
    public class MeetupContext:DbContext
    {
        public MeetupContext(DbContextOptions<MeetupContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
             modelBuilder.Entity<User>()
                .HasMany<Event>(x => x.Events)
                .WithOne(x => x.User)
                .HasForeignKey(x => x.UserId);

             modelBuilder.Entity<Event>()
                 .HasMany<BookedEvent>(r => r.BookedEvents)
                 .WithOne(r => r.Event)
                 .HasForeignKey(r => r.EventId);
              
            base.OnModelCreating(modelBuilder);

        }
        public DbSet<User> Users { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<BookedEvent> BookedEvents { get; set; }
    }
}
