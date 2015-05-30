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
    public class AdminController : Controller
    {
        //
        // GET: /Admin/
        DBQuery dbWrapper = new DBQuery();

        public ActionResult Index()
        {

            if (checkIfSessionPresent() == false)
            {
                return RedirectToAction("Index", "Main");
            }

            Users userdata = Session["user_data"] as Users;

            ViewBag.fullname = userdata.fullname + " ";
            ViewBag.pagename = "Main";
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

        public void removeSession()
        {
            Session["user_data"] = null;

        }


        public ActionResult Logout()
        {
            removeSession();
            return RedirectToAction("Index");
        }

        //
        // GET: /Admin/Create

        public ActionResult Profile()
        {
            if (checkIfSessionPresent() == false)
            {
                return RedirectToAction("Index", "Main");
            }

            Users userdata = Session["user_data"] as Users;

            Users resUser = dbWrapper.getUserData(userdata.user_id);

            if (resUser.username == "Empty")
            {
                return RedirectToAction("Logout");
            }



            ViewBag.fullname = userdata.fullname + " ";
            ViewBag.pagename = "Profile";

            return View(resUser);
        }

        [HttpPost]
        public ActionResult Profile(Users ProfileModel)
        {
            try
            {
                Users userdata = Session["user_data"] as Users;
                bool setError = true;

                Users updateUserData = dbWrapper.getUserData(userdata.user_id);

                if (ProfileModel.confirmpassword != null && updateUserData.password != ProfileModel.confirmpassword)
                {
                    ModelState.AddModelError("", "Please Enter your correct Old Password to continue");
                    setError = false;
                }
                else
                {
                    ProfileModel.user_id = userdata.user_id;

                    bool resUpdateUser = dbWrapper.updateUser(ProfileModel);

                    if (resUpdateUser == false)
                    {
                        ModelState.AddModelError("", "An Error occured in the System, Please try again later");
                        setError = false;
                    }

                }

                Users resUser = dbWrapper.getUserData(userdata.user_id);

                updateUserSession(resUser);

                Users ud = Session["user_data"] as Users;

                ViewBag.fullname = ud.fullname + " ";
                ViewBag.pagename = "Profile";
                if (setError)
                {
                    ViewData["Message"] = "Success";
                }
                return View(resUser);

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public void updateUserSession(Users u)
        {
            Users userdata = Session["user_data"] as Users;
            userdata.fullname = u.fullname;
            userdata.user_id = u.user_id;
            userdata.role = u.role;
            userdata.username = u.username;

            Session["user_data"] = userdata;

        }




    }
}
