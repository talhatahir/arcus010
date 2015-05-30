using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Arcus10.DAL;
using Arcus10.Models;
using System.Data;
using System.Data.SqlClient;
using PagedList.Mvc;
using PagedList;


namespace Arcus10.Controllers
{

    public class UsersController : Controller
    {
        DBQuery dbWrapper = new DBQuery();
        //
        // GET: /Users/

        public ActionResult Index(int? page,string q=null)
        {
            if (checkIfSessionPresent() == false)
            {
                return RedirectToAction("Index", "Main");
            }

            Users userdata = Session["user_data"] as Users;

            ViewBag.fullname = userdata.fullname + " ";
            ViewBag.pagename = "Main";

            List<Users> dtUsers = new List<Users>();

            dtUsers = (q==null) ? dbWrapper.getUsersData(userdata.user_id.ToString()) : dbWrapper.searchUserData(q,userdata.user_id.ToString());
                IQueryable<Users> query = dtUsers.AsQueryable();

            ViewBag.query = q;
            var pageNumber = page ?? 1;// if no page was specified in the querystring, default to the first page (1)
            var pagedUsers = query.ToPagedList(pageNumber, 4); // will only contain 10 products max because of the pageSize

            return View(pagedUsers);
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

        //
        // GET: /Users/Details/5

        public ActionResult Details(string id)
        {
            if (checkIfSessionPresent() == false)
            {
                return RedirectToAction("Index", "Main");
            }

            Users resUser = dbWrapper.getUserData(id);

            if (resUser.username == "Empty")
            {
                return RedirectToAction("Logout");
            }

            Users userdata = Session["user_data"] as Users;

            ViewBag.fullname = userdata.fullname + " ";
            ViewBag.pagename = "Details";

            return View(resUser);
        }

        //
        // GET: /Users/Create

        public ActionResult Create()
        {
            if (checkIfSessionPresent() == false)
            {
                return RedirectToAction("Index", "Main");
            }

            Users userdata = Session["user_data"] as Users;

            ViewBag.fullname = userdata.fullname + " ";
            ViewBag.pagename = "Create";

            return View();
        }

        [HttpPost]
        public ActionResult Create(Users UserModel)
        {
            try
            {

                bool resEmailCheck = dbWrapper.verifyUserEmailBeforeInsert(UserModel.username);

                if (resEmailCheck)
                {
                    ModelState.AddModelError("", "Email/username already taken, Please enter new.");
                }
                else
                {

                    bool resCreateUser = dbWrapper.createNewUser(UserModel);

                    if (resCreateUser == false)
                    {
                        ModelState.AddModelError("", "An Error occured in the System, Please try again later");
                    }

                }


                ViewBag.pagename = "Create New User";
                ViewData["Message"] = "Success";
                return View();

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        //
        // GET: /Users/Edit/5

        public ActionResult Edit(string id)
        {
            if (checkIfSessionPresent() == false)
            {
                return RedirectToAction("Index", "Main");
            }

            Users resUser = dbWrapper.getUserData(id);

            if (resUser.username == "Empty")
            {
                return RedirectToAction("Logout");
            }

            Users userdata = Session["user_data"] as Users;

            ViewBag.fullname = userdata.fullname + " ";
            ViewBag.pagename = "Edit User";

            return View(resUser);
        }

        //
        // POST: /Users/Edit/5

        [HttpPost]
        public ActionResult Edit(Users ProfileModel, string id)
        {
            try
            {
                bool resUpdateUser = dbWrapper.editUserData(ProfileModel,id);

                if (resUpdateUser == false)
                {
                    ModelState.AddModelError("", "An Error occured in the System, Please try again later");
                }


                Users resUser = dbWrapper.getUserData(id);


                Users ud = Session["user_data"] as Users;

                ViewBag.fullname = ud.fullname + " ";
                ViewBag.pagename = "Edit User";
                ViewData["Message"] = "Success";
                return View(resUser);

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

       
        //
        // POST: /Users/Delete/5

        [HttpPost]
        public ActionResult Delete(string id)
        {
            try
            {
                bool resDeleteUser = dbWrapper.deleteUserdata(id);

                if (resDeleteUser == false)
                {
                    TempData["Message"]= "An Error occured in the System, Please try again later";
                }

                
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return RedirectToAction("Index");

        }
    }
}
