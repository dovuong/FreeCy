using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Models.Framework;
using Models.DAO;
using Models.Common;

namespace FreeCy.Areas.Admin.Controllers
{
    public class UsersController : Controller
    {
        private FreeCyDB db = new FreeCyDB();

        // GET: Admin/Users
        public ActionResult Index(int page = 1, int pageSize = 10)
        {
            var usDAO = new UserDAO();
            var user = new User();
            ViewBag.Userss = usDAO.ListUsers(pageSize, page,user);
            //var model = listproductDAO.ListAllPaging(page, pageSize);
            int totalRecord = usDAO.GetTotalRecords();

            ViewBag.Total = totalRecord;
            ViewBag.Page = page;
            ViewBag.Size = pageSize;
            int maxPage = 5;
            int totalPage = 0;

            totalPage = (int)(totalRecord / pageSize) + 1;
            ViewBag.TotalPage = totalPage;
            ViewBag.MaxPage = maxPage;
            ViewBag.First = 1;
            ViewBag.Last = totalPage;
            ViewBag.Next = page + 1;
            ViewBag.Prev = page - 1;
            return View();

        }


        // GET: Admin/Users/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // GET: Admin/Users/Create
        public ActionResult Create()
        {
            ViewBag.ID_User = new SelectList(db.Conversations, "ID_User", "ID_User");
            return View();
        }

        // POST: Admin/Users/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "UserName,Password,Email,Phone,Birthday,isFreeLancer,isEmployer,ResetPasswordCode,isEmailVerify,Address,WorkTime,CreateAt")] User user)
        {

            bool Status = false;
            string Message = "";
            user.Status = true;
            //Model Validation
            if (ModelState.IsValid)
            {
                //username already exists
                if (UserDAO.Instance.CheckUserName(user.UserName))
                {
                    ModelState.AddModelError("", "*Tên đăng nhập tồn tại!");
                }
                //email already exists
                else if (UserDAO.Instance.CheckUserEmail(user.Email))
                {
                    ModelState.AddModelError("", "*Email đã tồn tại !");
                }
                else
                {

                    user.Password = Encryptor.MD5Hash(user.Password);

                    user.isEmailVerify = false;

                    user.Status = true;
                db.Users.Add(user);
                db.SaveChanges();
                return RedirectToAction("Index");

                }
            }
            ViewBag.Message = Message;
            ViewBag.Status = Status;
            return View();
            
        }

        // GET: Admin/Users/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            ViewBag.ID_User = new SelectList(db.Conversations, "ID_User", "ID_User", user.ID_User);
            return View(user);
        }

        // POST: Admin/Users/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID_User,UserName,Password,Email,Phone,Birthday,Status,isFreeLancer,isEmployer,ResetPasswordCode,isEmailVerify,Address,WorkTime,CreateAt")] User user)
        {
            if (ModelState.IsValid )
            {
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ID_User = new SelectList(db.Conversations, "ID_User", "ID_User", user.ID_User);
            return View(user);
        }

        // GET: Admin/Users/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: Admin/Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            User user = db.Users.Find(id);
            user.Status = false;
            //db.Users.Remove(user);
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
