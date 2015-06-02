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
    public class RoomsController : Controller
    {
        DBQuery dbWrapper = new DBQuery();
        //
        // GET: /Rooms/

        public ActionResult Index(int? page, string q = null)
        {
            if (checkIfSessionPresent() == false)
            {
                return RedirectToAction("Index", "Main");
            }

            Users userdata = Session["user_data"] as Users;

            ViewBag.fullname = userdata.fullname + " ";
            ViewBag.pagename = "Main";

            List<Rooms> dtRooms = new List<Rooms>();

            dtRooms = (q == null) ? dbWrapper.getRoomsData(userdata.user_id.ToString()) : dbWrapper.searchRoomData(q);
            IQueryable<Rooms> query = dtRooms.AsQueryable();

            ViewBag.query = q;
            var pageNumber = page ?? 1;// if no page was specified in the querystring, default to the first page (1)
            var pagedRooms = query.ToPagedList(pageNumber, 10); // will only contain 10 products max because of the pageSize

            return View(pagedRooms);
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
        // GET: /Rooms/Details/5
     
        public ActionResult Details(string id)
        {
            if (checkIfSessionPresent() == false)
            {
                return RedirectToAction("Index", "Main");
            }

            Rooms resRoom = dbWrapper.getRoomData(id);

            if (resRoom.room_number == "Empty")
            {
                return RedirectToAction("Logout");
            }

            Users userdata = Session["user_data"] as Users;

            ViewBag.fullname = userdata.fullname + " ";
            ViewBag.pagename = "Details";

            return View(resRoom);
        }
         
        //
        // GET: /Rooms/Create

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
        public ActionResult Create(Rooms RoomModel)
        {
            try
            {

                bool resRoomCheck = dbWrapper.verifyRoomNumberBeforeInsert(RoomModel.room_number);

                if (resRoomCheck)
                {
                    ModelState.AddModelError("", "Room Number already taken, Please enter new.");
                }
                else
                {

                    bool resCreateUser = dbWrapper.createNewRoom(RoomModel);

                    if (resCreateUser == false)
                    {
                        ModelState.AddModelError("", "An Error occured in the System, Please try again later");
                    }

                }


                ViewBag.pagename = "Create New Room";
                ViewData["Message"] = "Success";
                return View();

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        //
        // GET: /Rooms/Edit/5
       
        public ActionResult Edit(string id)
        {
            if (checkIfSessionPresent() == false)
            {
                return RedirectToAction("Index", "Main");
            }

            Rooms resRoom = dbWrapper.getRoomData(id);

            if (resRoom.room_number == "Empty")
            {
                return RedirectToAction("Logout");
            }

            Users userdata = Session["user_data"] as Users;

            ViewBag.fullname = userdata.fullname + " ";
            ViewBag.pagename = "Edit Room";

            return View(resRoom);
        }

        //
        // POST: /Rooms/Edit/5

        [HttpPost]
        public ActionResult Edit(Rooms RoomModel, string id)
        {
            try
            {
                bool resUpdateRoom = dbWrapper.editRoomData(RoomModel, id);

                if (resUpdateRoom == false)
                {
                    ModelState.AddModelError("", "An Error occured in the System, Please try again later");
                }


                Rooms res = dbWrapper.getRoomData(id);


                Users ud = Session["user_data"] as Users;

                ViewBag.fullname = ud.fullname + " ";
                ViewBag.pagename = "Edit Room";
                ViewData["Message"] = "Success";
                return View(res);

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
         
        //
        // POST: /Rooms/Delete/5

        [HttpPost]
        public ActionResult Delete(string id)
        {
            try
            {
                bool resDeleteUser = dbWrapper.deleteRoomdata(id);

                if (resDeleteUser == false)
                {
                    TempData["Message"] = "An Error occured in the System, Please try again later";
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
