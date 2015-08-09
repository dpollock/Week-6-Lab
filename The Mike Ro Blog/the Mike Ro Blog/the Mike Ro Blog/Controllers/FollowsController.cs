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
                f.Follow_Id = u.Id;
                followees.Add(f);
            }

            return View(followees);
        }
        [Authorize]
        public ActionResult WhoFollowsMe()
        {
            var CurrentUser = db.Users.FirstOrDefault(x => x.UserName == User.Identity.Name);
            var followingme = db.Follows.Where(x => x.Followee_Id == CurrentUser.Id);
            List<WhoIFollowVM> followers = new List<WhoIFollowVM>();

            foreach (Follow u in followingme)
            {
                WhoIFollowVM f = new WhoIFollowVM();
                var person = db.Users.FirstOrDefault(x => x.Id == u.Follower_Id);
                f.FolloweeName = person.UserName;

                f.StillFollowing = true;
                f.Follow_Id = u.Id;
                followers.Add(f);
            }

            return View(followers);
        }

        //public ActionResult Index()
        //{
            //List<WhoFollowsWhomVM> pairs = new List<WhoFollowsWhomVM>();
            //foreach (Follow f in db.Follows)
            //{
                //WhoFollowsWhomVM afollowsb = new WhoFollowsWhomVM();
                //afollowsb.FollowerName = db.Users.FirstOrDefault(x => x.Id == f.Follower_Id).UserName;
                //afollowsb.FolloweeName = db.Users.FirstOrDefault(x => x.Id == f.Followee_Id).UserName;
                //pairs.Add(afollowsb);
            //}
            

            //return View(pairs);
        //}

        // GET: Follows/Details/5

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
        public ActionResult Create(Follow id)
          
        {
            ApplicationUser userToFollow = new ApplicationUser();
            userToFollow.UserName = id.Followee.UserName;
            var CurrentUser = db.Users.FirstOrDefault(x => x.UserName == User.Identity.Name);
            var IFollowYou = db.Users.FirstOrDefault(x => x.UserName == userToFollow.UserName);

            if (CurrentUser == null || IFollowYou == null)
            {
                return View(userToFollow);
            }

            var alreadyfollows = from f in db.Follows
                                 where f.Follower_Id == CurrentUser.Id
                                 where f.Followee_Id == IFollowYou.Id
                                 select f;

            var amIfollowing = alreadyfollows.ToList();
            if (amIfollowing.Count > 0)
            {
               
                return RedirectToAction("WhoIFollow");
            }
            Follow follow = new Follow();

            follow.Follower_Id = CurrentUser.Id;
            follow.Followee_Id = IFollowYou.Id;
            
           
            if (ModelState.IsValid)
            {
                db.Follows.Add(follow);
                db.SaveChanges();
               

                return RedirectToAction("WhoIFollow");
            }

            return View(follow);
        }
                                                                                                     
        // GET: Follows/Delete/5
        [Authorize]
        public ActionResult Delete(int id)
        {
            var CurrentUser = db.Users.FirstOrDefault(x => x.UserName == User.Identity.Name);

            Follow unfollow = db.Follows.FirstOrDefault(x => x.Id == id);
            if (unfollow == null)
            {
                return HttpNotFound();
            }
            WhoFollowsWhomVM showUnfollow = new WhoFollowsWhomVM();
            showUnfollow.FollowerName = CurrentUser.UserName;
            showUnfollow.FolloweeName = db.Users.FirstOrDefault(x => x.Id == unfollow.Followee_Id).UserName;
            showUnfollow.Follow_Id = unfollow.Id;
           
            return  View(showUnfollow);
        }

        // POST: Follows/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Follow follow = db.Follows.FirstOrDefault(x=>x.Id == id);
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
