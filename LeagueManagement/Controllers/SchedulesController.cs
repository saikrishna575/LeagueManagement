using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using LMEntities.Models;
using Repository.Pattern.UnitOfWork;
using LMService;

namespace LeagueManagement.Controllers
{
    public class SchedulesController : Controller
    {
        private SportsSiteContext db = new SportsSiteContext();
        private readonly IScheduleService _scheduleService;
        private IUnitOfWork _unitOfWork;
        public SchedulesController(IScheduleService ScheduleService, IUnitOfWork unitOfWork)
        {
            _scheduleService = ScheduleService;
            _unitOfWork = unitOfWork;
        }
        // GET: Schedules
        public async Task<ActionResult> Index()
        {
            return View(await _scheduleService.GetAsync());
        }

        // GET: Schedules/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Schedule schedule = await _scheduleService.FindAsync(id);
            if (schedule == null)
            {
                return HttpNotFound();
            }
            schedule.Team = (Team)(from a in db.Teams where a.Id == schedule.HomeTeamId select a).SingleOrDefault();
            schedule.Team1 = (Team)(from a in db.Teams where a.Id == schedule.VisitorTeamId select a).SingleOrDefault();
            schedule.Ground = (Ground)(from a in db.Grounds where a.Id == schedule.GroundId select a).SingleOrDefault();
            schedule.Season = (Season)(from a in db.Seasons where a.Id == schedule.SeasonId select a).SingleOrDefault();
            schedule.Umpire = (User)(from a in db.Users where a.Id == schedule.UmpireId select a).SingleOrDefault();
            return View(schedule);
        }

        // GET: Schedules/Create
        public ActionResult Create()
        {
            ViewBag.HomeTeamId = new SelectList(db.Teams, "Id", "Name");
            ViewBag.VisitorTeamId = new SelectList(db.Teams, "Id", "Name");
            ViewBag.GroundId = new SelectList(db.Grounds, "Id", "Name");
            ViewBag.SeasonId = new SelectList(db.Seasons, "Id", "Name");
           // ViewBag.YearId = new SelectList(db.Years, "Id", "YearNumber");
            ViewBag.Umpires = new SelectList(db.Users.Where(a => a.UserTypeId == 2), "Id ", "UserName");
            return View();
        }

        // POST: Schedules/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Schedule schedule)
        {
            if (ModelState.IsValid)
            {
                _scheduleService.Insert(schedule);
                _unitOfWork.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.HomeTeamId = new SelectList(db.Teams, "Id", "Name");
            ViewBag.VisitorTeamId = new SelectList(db.Teams, "Id", "Name");
            ViewBag.GroundId = new SelectList(db.Grounds, "Id", "Name");
            ViewBag.SeasonId = new SelectList(db.Seasons, "Id", "Name", schedule.SeasonId);
           
            return View(schedule);
        }

        // GET: Schedules/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Schedule schedule = await _scheduleService.FindAsync(id);
            if (schedule == null)
            {
                return HttpNotFound();
            }
            ViewBag.HomeTeamId = new SelectList(db.Teams, "Id", "Name");
            ViewBag.VisitorTeamId = new SelectList(db.Teams, "Id", "Name");
            ViewBag.GroundId = new SelectList(db.Grounds, "Id", "Name");
            ViewBag.SeasonId = new SelectList(db.Seasons, "Id", "Name", schedule.SeasonId);
           // ViewBag.YearId = new SelectList(db.Years, "Id", "Id", schedule.YearId);
            return View(schedule);
        }

        // POST: Schedules/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,SeasonId,YearId,HomeTeamId,VisitorTeamId,UmpireId,GroundId,ScheduleDate,StartTime,EndTime,CreatedOn,CreatedBy,ModifiedOn,ModfiedBy")] Schedule schedule)
        {
            if (ModelState.IsValid)
            {
                schedule.ObjectState = Repository.Pattern.Infrastructure.ObjectState.Modified;
                _scheduleService.Update(schedule);
                _unitOfWork.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.HomeTeamId = new SelectList(db.Teams, "Id", "Name");
            ViewBag.VisitorTeamId = new SelectList(db.Teams, "Id", "Name");
            ViewBag.GroundId = new SelectList(db.Grounds, "Id", "Name");
            ViewBag.SeasonId = new SelectList(db.Seasons, "Id", "Name", schedule.SeasonId);
           // ViewBag.YearId = new SelectList(db.Years, "Id", "Id", schedule.YearId);
            return View(schedule);
        }

        // GET: Schedules/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Schedule schedule = await _scheduleService.FindAsync(id);
            if (schedule == null)
            {
                return HttpNotFound();
            }
            schedule.Team = (Team)(from a in db.Teams where a.Id == schedule.HomeTeamId select a).SingleOrDefault();
            schedule.Team1 = (Team)(from a in db.Teams where a.Id == schedule.VisitorTeamId select a).SingleOrDefault();
            schedule.Ground = (Ground)(from a in db.Grounds where a.Id == schedule.GroundId select a).SingleOrDefault();
            schedule.Season = (Season)(from a in db.Seasons where a.Id == schedule.SeasonId select a).SingleOrDefault();
            schedule.Umpire = (User)(from a in db.Users where a.Id == schedule.UmpireId select a).SingleOrDefault();
            return View(schedule);
        }

        // POST: Schedules/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Schedule schedule = await _scheduleService.FindAsync(id);
            _scheduleService.Delete(schedule);
            _unitOfWork.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
