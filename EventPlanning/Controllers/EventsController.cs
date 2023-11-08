using EventPlanning.Data;
using EventPlanning.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace EventPlanning.Controllers
{
    [Authorize]
    public class EventsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        public EventsController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        public async Task<ViewResult> IndexAsync()
        {
            ViewData["userId"] = _userManager.GetUserId(User);
            ViewBag.IsAdmin = await _userManager.IsInRoleAsync(await _userManager.GetUserAsync(User), "Admin");
            return View(_context.Events.Include(e => e.Properties).Include(e => e.RegisteredUsers));
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public ViewResult Create()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ViewResult Create(Event @event)
        {
            _context.Events.Add(@event);
            _context.SaveChanges();
            return View("EventCreated", @event);
        }

        [HttpPost]
        public ViewResult Register(Guid eventId, Guid userId)
        {
            var @event = _context.Events.Include(e => e.RegisteredUsers).Include(e => e.Properties).Single(e => e.Id == eventId);
            if (@event.MaxRegisteredUsers > @event.RegisteredUsers.Count)
            {
                var user = _context.UsersDetails.SingleOrDefault(u => u.Id == userId);
                if (user == null)
                {
                    return View("NoUserDetails");
                }
                @event.RegisteredUsers.Add(user);
                _context.SaveChanges();
                return View("RegisteredOnEvent", @event);
            }
            else
            {
                return View("MaxRegisteredUsers", @event.MaxRegisteredUsers);
            }
        }
    }
}
