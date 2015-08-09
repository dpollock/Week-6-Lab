using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using the_Mike_Ro_Blog.Models;

namespace the_Mike_Ro_Blog.Controllers
{
    public class PostsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Posts
        [Authorize]
        public ActionResult Index()
        {
            var CurrentUser = db.Users.FirstOrDefault(x => x.UserName == User.Identity.Name);
            var postsIfollow = from p in db.Posts
                               join u in db.Users
                               on p.Poster equals u
                               join f in db.Follows
                               on u.Id equals f.Followee_Id
                               where f.Follower_Id == CurrentUser.Id.ToString()
                               select new PostVM()  { Poster = u.UserName, PostedOn = p.PostedOn, Text = p.Text, Post_Id = p.Id};
           
            List<PostVM> postsIsee = postsIfollow.ToList();

            foreach (Post p in CurrentUser.MyPosts)
            {
                PostVM mypost = new PostVM();
                mypost.Poster = CurrentUser.UserName;
                mypost.PostedOn = p.PostedOn;
                mypost.Text = p.Text;
                mypost.Post_Id = p.Id;
                mypost.IsMine = true;
                postsIsee.Add(mypost);
            }


            return View(postsIsee.OrderByDescending(x => x.PostedOn));
        }

        // GET: Posts/Create
        [Authorize]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Posts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Post post)
        {
            var CurrentUser = db.Users.FirstOrDefault(x => x.UserName == User.Identity.Name);

            post.Poster = CurrentUser;
            post.PostedOn = DateTime.Now;
            if (ModelState.IsValid)
            {
                db.Posts.Add(post);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            var errors = ModelState.Select(x => x.Value.Errors)
                          .Where(y => y.Count > 0)
                          .ToList();
            return View(post);
        }

        // GET: Posts/Edit/5
        [Authorize]
        public ActionResult Edit(int id)
        {
            var CurrentUser = db.Users.FirstOrDefault(x => x.UserName == User.Identity.Name);
            Post post = db.Posts.Find(id);
            if (post == null || post.Poster != CurrentUser)
            {
                return HttpNotFound();
            }
            return View(post);
        }

        // POST: Posts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Post post)
        {
            if (ModelState.IsValid)
            {
                Post edited = db.Posts.Find(post.Id);
                edited.Text = post.Text;
                
                db.Entry(edited).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(post);
        }

        // GET: Posts/Delete/5
        [Authorize]
        public ActionResult Delete(int id)
        {
            var CurrentUser = db.Users.FirstOrDefault(x => x.UserName == User.Identity.Name);
            Post post = db.Posts.Find(id);
            if (post == null || post.Poster != CurrentUser)
            {
                return HttpNotFound();
            }

          
            return View(post);
        }

        // POST: Posts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Post post = db.Posts.Find(id);
            db.Posts.Remove(post);
            db.SaveChanges();
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
