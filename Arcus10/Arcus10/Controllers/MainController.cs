using Arcus10.DAL;
using Arcus10.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Arcus10.Controllers
{
    public class MainController : Controller
    {


        DBQuery dbWrapper = new DBQuery();
        //
        // GET: /Main/

        public ActionResult Index()
        {
           
            if (checkIfSessionPresent())
            {
               
                Users userdata = Session["user_data"] as Users;

                if (userdata.role == "Admin")
                {
                    return RedirectToAction("Index", "Admin");
                }

                else if (userdata.role == "Manager")
                {
                    return RedirectToAction("Index", "Manager", "");
                }

                else if (userdata.role == "Other")
                {
                    return RedirectToAction("Index", "Other", "");
                }

                else
                {
                    return RedirectToAction("Index");
                }
            }
            else
            {
                return RedirectToAction("Login");               
            }

           
        }



        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(Users loginModel)
        {
            try
            {
                Users resVal = dbWrapper.loginUser(loginModel);

                if (resVal.username != "Empty")
                {
                    Session["user_data"] = resVal;

                  
                    Users userdata = Session["user_data"] as Users;

                    if (userdata.role == "Admin")
                    {
                        return RedirectToAction("Index", "Admin");
                    }

                    else if (userdata.role == "Manager")
                    {
                        return RedirectToAction("Index", "Manager", "");
                    }

                    else if (userdata.role == "Other")
                    {
                        return RedirectToAction("Index", "Other", "");
                    }

                    else
                    {
                        return RedirectToAction("Index");
                    }
                }

                else
                {
                    ModelState.AddModelError("", "Email or Password is incorrect!");
                }               

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return View();
        }

        public bool checkIfSessionPresent()
        {

            if (Session["user_data"] != null)
            {
                return true;
            }

            return false;

        }

        //
        // GET: /Main/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Main/Create

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

        //
        // GET: /Main/Edit/5

        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /Main/Edit/5

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

        //
        // GET: /Main/Delete/5

        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Main/Delete/5

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
    }
}
