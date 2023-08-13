﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using YTP.AddToCart.Models;
using YTP.AddToCart.ViewModels;

namespace YTP.AddToCart.Controllers {
    public class HomeController : Controller {
        CONN dbaccess = new CONN();
        public ActionResult Index() {
            return View(dbaccess.tbl_product.ToList());
        }

        [HttpPost]
        public JsonResult AddToCart(int id) {

            if (Session["VMCart"] != null) {

                List<VMCart> cartList = (List<VMCart>)Session["VMCart"];
                int check = 0;
                foreach (var item in cartList) {

                    if (item.pid == id) {

                        item.qty++;
                        check = 0;
                        break;

                    } else {

                        check = 1;
                    }
                }

                if (check == 1) {

                    VMCart cart = new VMCart();
                    cart.pid = id;
                    cart.qty = 1;
                    cartList.Add(cart);
                }

                Session["VMCart"] = (List<VMCart>)cartList;

            } else {

                List<VMCart> newList = new List<VMCart>();
                VMCart cart = new VMCart();
                cart.pid = id;
                cart.qty = 1;
                newList.Add(cart);

                Session["VMCart"] = (List<VMCart>)newList;
            }

            return Json(Session["VMCart"], JsonRequestBehavior.AllowGet);
        }

        public ActionResult About() {
            return View();
        }

        public ActionResult Contact() {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}