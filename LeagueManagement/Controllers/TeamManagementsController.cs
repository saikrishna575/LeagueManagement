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
    public class TeamManagementsController : Controller
    {
        private SportsSiteContext db = new SportsSiteContext();
        private readonly ITeamManagementService _teamManagementService;
        private IUnitOfWork _unitOfWork;
        public TeamManagementsController(ITeamManagementService teamManagementService, IUnitOfWork unitOfWork)
        {
            _teamManagementService = teamManagementService;
            _unitOfWork = unitOfWork;
        }
        // GET: TeamManagements
        public async Task<ActionResult> Index()
        {

            return View(await _teamManagementService.GetAsync());
        }

        // GET: TeamManagements/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TeamManagement teamManagement = await _teamManagementService.FindAsync(id);
            if (teamManagement == null)
            {
                return HttpNotFound();
            }
            return View(teamManagement);
        }

        // GET: TeamManagements/Create
        public ActionResult Create()
        {
            ViewBag.OrganizationId = new SelectList(db.Organizations, "Id", "Name");
            ViewBag.TeamId = new SelectList(db.Teams, "Id", "Name");
            ViewBag.TeamMemberId = new SelectList(db.TeamMembers, "ID", "Name");
            ViewBag.TitleId = new SelectList(db.TeamTitles, "Id", "Name");
            return View();
        }

        // POST: TeamManagements/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,OrganizationId,TeamId,TitleId,TeamMemberId,CreatedBy,CreatedOn,ModifiedBy,ModifiedOn")] TeamManagement teamManagement)
        {
            if (ModelState.IsValid)
            {
                _teamManagementService.Insert(teamManagement);
                _unitOfWork.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.OrganizationId = new SelectList(db.Organizations, "Id", "Name", teamManagement.OrganizationId);
            ViewBag.TeamId = new SelectList(db.Teams, "Id", "Name", teamManagement.TeamId);
            ViewBag.TeamMemberId = new SelectList(db.TeamMembers, "ID", "Name", teamManagement.TeamMemberId);
            ViewBag.TitleId = new SelectList(db.TeamTitles, "Id", "Name", teamManagement.TitleId);
            return View(teamManagement);
        }

        // GET: TeamManagements/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TeamManagement teamManagement = await _teamManagementService.FindAsync(id);
            if (teamManagement == null)
            {
                return HttpNotFound();
            }
            ViewBag.OrganizationId = new SelectList(db.Organizations, "Id", "Name", teamManagement.OrganizationId);
            ViewBag.TeamId = new SelectList(db.Teams, "Id", "Name", teamManagement.TeamId);
            ViewBag.TeamMemberId = new SelectList(db.TeamMembers, "ID", "Name", teamManagement.TeamMemberId);
            ViewBag.TitleId = new SelectList(db.TeamTitles, "Id", "Name", teamManagement.TitleId);
            return View(teamManagement);
        }

        // POST: TeamManagements/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,OrganizationId,TeamId,TitleId,TeamMemberId,CreatedBy,CreatedOn,ModifiedBy,ModifiedOn")] TeamManagement teamManagement)
        {
            if (ModelState.IsValid)
            {
                teamManagement.ObjectState = Repository.Pattern.Infrastructure.ObjectState.Modified;
                _teamManagementService.Update(teamManagement);
                _unitOfWork.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.OrganizationId = new SelectList(db.Organizations, "Id", "Name", teamManagement.OrganizationId);
            ViewBag.TeamId = new SelectList(db.Teams, "Id", "Name", teamManagement.TeamId);
            ViewBag.TeamMemberId = new SelectList(db.TeamMembers, "ID", "Name", teamManagement.TeamMemberId);
            ViewBag.TitleId = new SelectList(db.TeamTitles, "Id", "Name", teamManagement.TitleId);
            return View(teamManagement);
        }

        // GET: TeamManagements/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TeamManagement teamManagement = await _teamManagementService.FindAsync(id);
            if (teamManagement == null)
            {
                return HttpNotFound();
            }
            return View(teamManagement);
        }

        // POST: TeamManagements/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            TeamManagement teamManagement = await _teamManagementService.FindAsync(id);
            _teamManagementService.Delete(teamManagement);
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
