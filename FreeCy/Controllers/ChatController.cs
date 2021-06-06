using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FreeCy.Models;
using Models.DAO;
using Models.Framework;

namespace FreeCy.Controllers
{
    public class ChatController : Controller
    {


        // GET: Chat
        public ActionResult Index()
        {

            
            var MesDao = new MessRoomsDAO();
            string a = MesDao.GetIDRoom(16, 17);
            var msss = MesDao.ListAllMess(a);
            ViewBag.IdRoom = a;

            ViewBag.Total = MesDao.TotalMess(a);

            //ViewBag.Room = MesDao.ListAll(16);
            return View(msss);
        }



        [HttpPost]
        public ActionResult UpdateIMG(PictureModel obj)
        {
            FreeCyDB db = new FreeCyDB();
            var userld = "16";
            // if user is logged in
            var file =obj.Picture;
            Message u = db.Messages.Find(16);
            if (file != null)
            {
                // Now lets Update the image
                var extension = Path.GetExtension(file.FileName);
                string id_and_extension = userld + extension;
                string imgUrl = "~/Profile Images/" + id_and_extension;
                u.Contents = imgUrl;
                db.Entry(u).State = EntityState.Modified; 
                db.SaveChanges();
                var path = Server.MapPath("~/Profile Images/"); 
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                if ((System.IO.File.Exists(path + id_and_extension)))
                System.IO.File.Delete(path + id_and_extension);
                file.SaveAs((path + id_and_extension));

            }
            

                return RedirectToAction("Picture");
        }


        public ActionResult Picture()
        {

            FreeCyDB db = new FreeCyDB();
            Message u = db.Messages.Find(16);


            return View(u);
        }


        [HttpPost]
        public ActionResult PostMessage(Message obj, int Iduser)// thay bằng id room
        {
            var MesDao = new MessRoomsDAO();
            obj.CreatedAt = DateTime.Now;
            obj.ID_User = 17;//// lấy ra session.id_user
            MesDao.AddMess(obj);
            MesDao.Update(obj.ID_MessRoom);
            var Iduserr = MesDao.GetIdByRoom(obj.ID_MessRoom, obj.ID_User);

            // tìm iduser của room khác với id của session

            string ac = "Details/" + Iduser.ToString();

            return RedirectToAction(ac);
        }

        // GET: Chat/Details/5
        public ActionResult Details(int iduser=0)
        {
            var MesDao = new MessRoomsDAO();

            var UsDao = new UserDAO();

            //ViewBag.listMess = 
            if (UsDao.CheckId(iduser) && iduser != 17)// check khác session
            {
                string a = MesDao.GetIDRoom(17, iduser);
                var msss = MesDao.ListAllMess(a);
                List<MessList> l1 = (MesDao.ListMess(17));  
                l1.Sort((x, y) => y.UpdateTime.CompareTo(x.UpdateTime));
                ViewBag.ListMess = l1;
                ViewBag.IdRoom = a;
                ViewBag.idus = iduser;
                ViewBag.Name = UsDao.GetNameById(iduser);
                ViewBag.Total = MesDao.TotalMess(a);
                return View(msss);
            }
            return HttpNotFound();



        }

        public ActionResult Erorr()
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Chat/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Chat/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Chat/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Chat/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Chat/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Chat/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Test()
        {
            return View();
        }
    }
}
