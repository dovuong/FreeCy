using Models.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Models.Framework;
namespace FreeCy.Controllers
{
    public class DetailController : Controller
    {
        
        // GET: Detail
        [HttpPost]
        public ActionResult ProductDetail()
        {
            return View();
        }
        //[HttpPost]

        //[ActionName("ProductDetail")]
        //public ActionResult ProductDetail(int productID)
        //{
        //    var product = new ProductDAO().ViewDetail(productID);
        //    ViewBag.Category = new ProductCategoryDAO().ViewDetail(product.ID_Product);
        //    return View(product);
        //}
        //public ActionResult ProductDetail(int productID)
        //{
        //    if(productID == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

        //    }
        //    var product = new ProductDAO().ViewDetail(productID);
            
        //    if(product == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(product);
        //}
    }
}