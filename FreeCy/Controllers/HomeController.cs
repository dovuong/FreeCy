using FreeCy.Code;
using FreeCy.Models;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Models.DAO;
using Models.Framework;
using System.Net;

namespace FreeCy.Controllers
{
    public class HomeController : Controller
    {
        
        public ActionResult Index()
        {
            var productDao = new ProductDAO();
            User user = new User();
            //ViewBag.Products = productDao.ListProduct(5,5, user);

            return View();
        }
       
        public ActionResult ListProduct(int page=1, int pageSize = 10, string sort="")
        {
            var listproductDAO = new ProductDAO();
            User user = new User();
            ViewBag.Products = listproductDAO.ListProduct(pageSize, page, user,sort);
            //var model = listproductDAO.ListAllPaging(page, pageSize);
            int totalRecord = listproductDAO.GetTotalRecords() ;
            ViewBag.sort = sort;
            ViewBag.Total = totalRecord;
            ViewBag.Page = page;
            ViewBag.Size = pageSize;
            int maxPage = 5;
            int totalPage = 0;

            totalPage = (int)(totalRecord / pageSize)+1;
            ViewBag.TotalPage = totalPage;
            ViewBag.MaxPage = maxPage;
            ViewBag.First = 1;
            ViewBag.Last = totalPage;
            ViewBag.Next = page + 1;
            ViewBag.Prev = page - 1;
            return View();
        }

        public ActionResult ListProduct2(int page = 1, int pageSize = 7, string sort = "")
        {
            var listproductDAO = new ProductDAO();
            User user = new User();
            ViewBag.Products = listproductDAO.ListProduct(pageSize, page, user, sort);
            //var model = listproductDAO.ListAllPaging(page, pageSize);
            int totalRecord = listproductDAO.GetTotalRecords();
            ViewBag.sort = sort;
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

        public ActionResult Detail(int productID)
        {

            if (productID == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            }
            var listproductDAO = new ProductDAO();
            var product = new ProductDAO().ViewDetail(productID);

            if (product == null)
            {
                return HttpNotFound();
            }
            ViewBag.Category = listproductDAO.ViewDetail(product.ID_Product);
            return View(product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(ViewModel model)
        {
            if (model.LoginModel != null)
            {
                var result = new UserModel().Login(model.LoginModel.Username, model.LoginModel.Password);
                if (result && ModelState.IsValid)
                {
                    SessionHelper.SetSession(new UserSession() { Username = model.LoginModel.Username });
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Nhập sai cmnr");
                }
                return View(model);
            }
            else
            {
                var result = new UserModel().SignUp(model.SignUpModel.Username, model.SignUpModel.Password, model.SignUpModel.Email);
                if (result && ModelState.IsValid)
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    //ModelState.AddModelError("", "Nhập sai cmnr");
                    ViewBag.Notification = "Nhập sai cmnr";

                }
                return View(model);
            }
        }

    }
}