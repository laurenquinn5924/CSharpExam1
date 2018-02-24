using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CSharpExam1.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace CSharpExam1.Controllers
{
    public class ActivityController : Controller
    {
        private Exam1Context _context;

        public ActivityController(Exam1Context context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("/logout")]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }

        private User ActiveUser
        {
            get { return _context.users.Where(u => u.id == HttpContext.Session.GetInt32("id")).FirstOrDefault(); }
        }

        [HttpGet]
        [Route("Dashboard")]
        public IActionResult Dashboard()
        {
            if (ActiveUser == null)
            {
                return RedirectToAction("Index", "Home");
            }
            User user = _context.users.Where(u => u.id == HttpContext.Session.GetInt32("id")).FirstOrDefault();
            // FirtsOrdDefault means pull first value that matches what I'm asking for or the default value
            ViewBag.UserInfo = user;


            ViewBag.AllActivities = _context.Activities.Include(r => r.RSVPS).ToList(); 
            //u in this case is a wedding, populate my weddings RSVP
            return View("Dashboard");
        }

        [HttpGet]
        [Route("/newactivity")]
        public IActionResult NewActivity()
        {
            if (ActiveUser == null)
                return RedirectToAction("Index", "Home");

            ViewBag.UserInfo = ActiveUser;
            return View("NewActivity");
        }

        [HttpPost]
        [Route("AddActivity")]
        public IActionResult AddActivity(AddActivity activity)
        {
            if (ActiveUser == null)
                return RedirectToAction("Index", "Home");

            if (ModelState.IsValid)
            {
                Activity Activity = new Activity
                {
                    UserId = ActiveUser.id,
                    ActivityTitle = activity.ActivityTitle,
                    Time = activity.Time,
                    Date = activity.Date,
                    Duration = activity.Duration,
                    Description = activity.Description,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now

                };

                _context.Activities.Add(Activity);
                _context.SaveChanges();
                return RedirectToAction("Dashboard");
            }


            ViewBag.UserInfo = ActiveUser;
            return View("NewActivity");
        }

        [HttpGet]
        [Route("activity/{ActivityId}")]
        public IActionResult ShowActivity(int ActivityId) // once the user successfully logs in
        {
            if (ActiveUser == null)
                return RedirectToAction("Index", "Home");

            ViewBag.ShowOne = _context.Activities.Where(o => o.ActivityId == ActivityId).Include(g => g.RSVPS).ThenInclude(u => u.User).SingleOrDefault();
            // ViewBag.ShowOne = _context.users.Where(c => c.id == id).Include(z => z.id).ThenInclude(n => n.FirstName);
            return View("ShowActivity");
        }

        [HttpPost]
        [Route("Delete")]
        public IActionResult Delete(int ActivityId)
        {
            if (ActiveUser == null)
                return RedirectToAction("Index", "Home");

            Activity ToDelete = _context.Activities.SingleOrDefault(ShowOne => ShowOne.ActivityId == ActivityId);
            _context.Activities.Remove(ToDelete);
            _context.SaveChanges();

            ViewBag.UserInfo = ActiveUser;
            System.Console.WriteLine("In delete finction");
            List<Activity> Activities = _context.Activities.ToList();
            return RedirectToAction("Dashboard");
        }


        [HttpPost]
        [Route("rsvpToActivity")]
        public IActionResult rsvpToActivity(int ActivityId)
        {

            RSVP addRSVP = new RSVP
            {
                UserId = (int)HttpContext.Session.GetInt32("id"),
                ParticipantActivityId = ActivityId

            };

            _context.RSVPS.Add(addRSVP);
            _context.SaveChanges();
            return RedirectToAction("Dashboard");
        }

        [HttpPost]
        [Route("UnrsvpToActivity")]
        public IActionResult UnrsvpToActivity(int ActivityId)
        {
            RSVP RemoveRsvp = _context.RSVPS.SingleOrDefault(x => x.ParticipantActivityId == ActivityId && x.UserId == (int)HttpContext.Session.GetInt32("id"));
            _context.RSVPS.Remove(RemoveRsvp);
            _context.SaveChanges();
            return RedirectToAction("Dashboard");
        }

    }
}