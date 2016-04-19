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
using Microsoft.AspNet.Identity;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Web.Configuration;
using System.IO;

namespace LeagueManagement.Controllers
{
    public class UsersController : Controller
    {
        private SportsSiteContext db = new SportsSiteContext();
        private readonly IUserService _UserService;
        private IUnitOfWork _unitOfWork;
        public UsersController(IUserService UserService, IUnitOfWork unitOfWork)
        {
            _UserService = UserService;
            _unitOfWork = unitOfWork;
        }
        // GET: Users
        public async Task<ActionResult> Index()
        {
            return View(await _UserService.GetAsync());
        }

        // GET: Users/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = await _UserService.FindAsync(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // GET: Users/Create
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            ViewBag.GenderId = new SelectList(db.Genders, "Id", "Name");
            ViewBag.OrganizationId = new SelectList(db.Organizations, "Id", "Name");
            ViewBag.Id = new SelectList(db.Users, "Id", "EmailId");
            ViewBag.UserTypeId = new SelectList(db.UserTypes, "Id", "Name");
            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,OrganizationId,UserTypeId,EmailId,FirstName,LastName,Password,GenderId,ProfilePhoto,CreatedOn,ModifiedOn")] User user)
        {
            if (ModelState.IsValid)
            {
                _UserService.Insert(user);
                _unitOfWork.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.GenderId = new SelectList(db.Genders, "Id", "Name", user.GenderId);
            ViewBag.OrganizationId = new SelectList(db.Organizations, "Id", "Name", user.OrganizationId);
            ViewBag.Id = new SelectList(db.Users, "Id", "EmailId", user.Id);
            ViewBag.UserTypeId = new SelectList(db.UserTypes, "Id", "Name", user.UserTypeId);
            return View(user);
        }

        // GET: Users/Edit/5
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = await _UserService.FindAsync(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            ViewBag.GenderId = new SelectList(db.Genders, "Id", "Name", user.GenderId);
            ViewBag.OrganizationId = new SelectList(db.Organizations, "Id", "Name", user.OrganizationId);
            ViewBag.Id = new SelectList(db.Users, "Id", "EmailId", user.Id);
            ViewBag.UserTypeId = new SelectList(db.UserTypes, "Id", "Name", user.UserTypeId);
            return View(user);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(User user, HttpPostedFileBase file)
        {
              if (ModelState.IsValid)
                {

                if (file != null)
                {
                    string fileName = new Random().Next().ToString();
                    string folderName = WebConfigurationManager.AppSettings["UsersPhotoFolder"];
                    string folderPath = folderName + "\\" + fileName + Path.GetExtension(file.FileName);
                    string savingPath = Request.PhysicalApplicationPath + folderPath;
                    user.ProfilePhoto = Request.ApplicationPath + folderPath;
                    file.SaveAs(savingPath);
                }
                user.ObjectState = Repository.Pattern.Infrastructure.ObjectState.Modified;
                    _UserService.Update(user);
                    _unitOfWork.SaveChanges();
                    return RedirectToAction("Index", "Home");
                }
                ViewBag.GenderId = new SelectList(db.Genders, "Id", "Name", user.GenderId);
                ViewBag.OrganizationId = new SelectList(db.Organizations, "Id", "Name", user.OrganizationId);
                ViewBag.Id = new SelectList(db.Users, "Id", "EmailId", user.Id);
                ViewBag.UserTypeId = new SelectList(db.UserTypes, "Id", "Name", user.UserTypeId);
                return View(user);                              
        }

        // GET: Users/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = await _UserService.FindAsync(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            await _UserService.DeleteAsync(id);
            _unitOfWork.SaveChanges();
            return RedirectToAction("Index");

        }
        public async Task<ActionResult> UpdateProfile()
        {            
            string userId = User.Identity.GetUserId();
            User user = new User();
           
            ViewBag.GenderId = new SelectList(db.Genders, "Id", "Name", user.GenderId);
            ViewBag.OrganizationId = new SelectList(db.Organizations, "Id", "Name", user.OrganizationId);
            ViewBag.Id = new SelectList(db.Users, "Id", "EmailId", user.Id);
            ViewBag.UserTypeId = new SelectList(db.UserTypes, "Id", "Name", user.UserTypeId);           
            ViewBag.SkillSpecialityId = new SelectList(db.SkillSpecialities, "Id", "Name",user.SkillSpecialityId);
            user = db.Users.Where(a => a.AspNetUsersId == userId).SingleOrDefault();
            if (user == null)
            {
                return HttpNotFound();
            }           
            return View("Edit", user);
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        public async Task<ActionResult> Detail(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = await _UserService.FindAsync(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

    }
}
