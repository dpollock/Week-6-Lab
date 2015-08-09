using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using the_Mike_Ro_Blog.Models;

namespace the_Mike_Ro_Blog.Controllers
{
    public class FollowsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Follows
        [Authorize]
        public ActionResult WhoIFollow()
        {
            var CurrentUser = db.Users.FirstOrDefault(x => x.UserName == User.Identity.Name);
            var myfollows = db.Follows.Where(x => x.Follower_Id == CurrentUser.Id);
            List<WhoIFollowVM> followees = new List<WhoIFollowVM>();

            foreach (Follow u in myfollows)
            {
                WhoIFollowVM f = new WhoIFollowVM();
                var person = db.Users.FirstOrDefault(x => x.Id == u.Followee_Id);
                f.FolloweeName = person.UserName;
               
                f.StillFollowing = true;
                followees.Add(f);
            }

            return View(followees);
        }
        public ActionResult Index()
        {
            List<WhoFollowsWhomVM> pairs = new List<WhoFollowsWhomVM>();
            foreach (Follow f in db.Follows)
            {
                WhoFollowsWhomVM afollowsb = new WhoFollowsWhomVM();
                afollowsb.FollowerName = db.Users.FirstOrDefault(x => x.Id == f.Follower_Id).UserName;
                afollowsb.FolloweeName = db.Users.FirstOrDefault(x => x.Id == f.Followee_Id).UserName;
                pairs.Add(afollowsb);
            }
            

            return View(pairs);
        }

        // GET: Follows/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Follow follow = db.Follows.Find(id);
            if (follow == null)
            {
                return HttpNotFound();
            }
            return View(follow);
        }

        // GET: Follows/Create
        [Authorize]
        public ActionResult Create()
        {


            return View();
        }

        // POST: Follows/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Follow follow)
        {
            var CurrentUser = db.Users.FirstOrDefault(x => x.UserName == User.Identity.Name);
            var IFollowYou = db.Users.FirstOrDefault(x => x.UserName == follow.Followee.UserName);

            if (CurrentUser == null || IFollowYou == null)
            {
                return View(follow);
            }

            var alreadyfollow = CurrentUser.WhoIFollow.Select(x => x.Followee_Id == follow.Followee_Id);

            if (alreadyfollow != null)
            {
               
                return RedirectToAction("WhoIFollow");
            }

            follow.Follower_Id = CurrentUser.Id;
            follow.Followee_Id = IFollowYou.Id;
            
           
            if (ModelState.IsValid)
            {
                db.Follows.Add(follow);
                try
                {
                    db.SaveChanges();
                }
                catch (DbEntityValidationException e)
                {
                    return Content(e.EntityValidationErrors.ToString());
                }

                return RedirectToAction("WhoIFollow");
            }

            return View(follow);
        }

        // GET: Follows/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Follow follow = db.Follows.Find(id);
            if (follow == null)
            {
                return HttpNotFound();
            }
            return View(follow);
        }

        // POST: Follows/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Since")] Follow follow)
        {
            if (ModelState.IsValid)
            {
                db.Entry(follow).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("WhoIFollow");
            }
            return View(follow);
        }

        // GET: Follows/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Follow follow = db.Follows.Find(id);
            if (follow == null)
            {
                return HttpNotFound();
            }
            return View(follow);
        }

        // POST: Follows/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Follow follow = db.Follows.Find(id);
            db.Follows.Remove(follow);
            db.SaveChanges();
            return RedirectToAction("WhoIFollow");
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
