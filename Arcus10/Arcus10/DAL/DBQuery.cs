using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Arcus10.Models;
using System.Data.Entity;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Web.Providers.Entities;

namespace Arcus10.DAL
{
    public class DBQuery
    {

        public DBQuery()
        {

        }

        public List<Users> getUsersData(string id)
        {
            List<Users> lstUsers = new List<Users>();
            DBContext db = new DBContext();

            string str = "select * from Users where user_id!=@id order by user_id asc";

            SqlConnection connection = new SqlConnection(db.getConnection());
            SqlCommand cmd = new SqlCommand(str, connection);
            cmd.Parameters.Add(new SqlParameter("id", id));

            try
            {
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }

                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                sda.Fill(ds, "UsersTable");

                foreach (DataRow row in ds.Tables["UsersTable"].Rows)
                {
                    Users temp = new Users();

                    temp.username = row["username"].ToString();
                    temp.role = row["role"].ToString();
                    temp.fullname = row["fullname"].ToString();

                    lstUsers.Add(temp);
                }

                connection.Close();

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return lstUsers;

        }

        public List<Rooms> getRoomsData(string id)
        {
            List<Rooms> lstRooms = new List<Rooms>();
            DBContext db = new DBContext();

            string str = "select * from Rooms  order by room_id asc";

            SqlConnection connection = new SqlConnection(db.getConnection());
            SqlCommand cmd = new SqlCommand(str, connection);

            try
            {
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }

                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                sda.Fill(ds, "RoomsTable");

                foreach (DataRow row in ds.Tables["RoomsTable"].Rows)
                {
                    Rooms temp = new Rooms();

                    temp.room_number = row["room_number"].ToString();
                    temp.room_status = row["room_status"].ToString();
                    temp.room_type = row["room_type"].ToString();
                    temp.room_cost = row["room_cost"].ToString();
                    temp.room_capacity = row["room_capacity"].ToString();
                    temp.room_name = row["room_name"].ToString();
                    temp.room_availability = row["room_availability"].ToString(); 

                    lstRooms.Add(temp);
                }

                connection.Close();

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return lstRooms;

        }

        public bool deleteRoomdata(string roomnumber)
        {
            DBContext db = new DBContext();

            string str = "delete from Rooms where room_number=@roomnumber";


            SqlConnection connection = new SqlConnection(db.getConnection());
            SqlCommand cmd = new SqlCommand(str, connection);
            cmd.Parameters.Add(new SqlParameter("roomnumber", roomnumber));

            try
            {
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }


                if (cmd.ExecuteNonQuery() > 0)
                {
                    connection.Close();
                    return true;
                }
                else
                {
                    return false;
                }


            }
            catch (Exception ex)
            {
                throw ex;
            }



        }


        public List<Users> searchUserData(string query, string id)
        {
            List<Users> lstUsers = new List<Users>();
            DBContext db = new DBContext();

            string str = "select * from Users where user_id!=@id AND fullname LIKE '%' + @query1 + '%' order by fullname asc";

            SqlConnection connection = new SqlConnection(db.getConnection());
            SqlCommand cmd = new SqlCommand(str, connection);
            cmd.Parameters.Add(new SqlParameter("id", id));
            cmd.Parameters.Add(new SqlParameter("query1", query));

            try
            {
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }

                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                sda.Fill(ds, "UsersTable");

                foreach (DataRow row in ds.Tables["UsersTable"].Rows)
                {
                    Users temp = new Users();

                    temp.username = row["username"].ToString();
                    temp.role = row["role"].ToString();
                    temp.fullname = row["fullname"].ToString();

                    lstUsers.Add(temp);
                }

                connection.Close();

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return lstUsers;

        }


        public List<Rooms> searchRoomData(string query)
        {
            List<Rooms> lstRooms = new List<Rooms>();
            DBContext db = new DBContext();

            string str = "select * from Rooms where room_name LIKE '%' + @query1 + '%' order by room_name asc";

            SqlConnection connection = new SqlConnection(db.getConnection());
            SqlCommand cmd = new SqlCommand(str, connection);
            cmd.Parameters.Add(new SqlParameter("query1", query));

            try
            {
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }

                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                sda.Fill(ds, "RoomsTable");

                foreach (DataRow row in ds.Tables["RoomsTable"].Rows)
                {
                    Rooms temp = new Rooms();

                    temp.room_number = row["room_number"].ToString();
                    temp.room_status = row["room_status"].ToString();
                    temp.room_type = row["room_type"].ToString();
                    temp.room_cost = row["room_cost"].ToString();
                    temp.room_capacity = row["room_capacity"].ToString();
                    temp.room_name = row["room_name"].ToString();
                    temp.room_availability = row["room_availability"].ToString(); 


                    lstRooms.Add(temp);
                }

                connection.Close();

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return lstRooms;

        }



        public bool verifyUserEmailBeforeInsert(string username)
        {
            DBContext db = new DBContext();

            string str = "select username from Users where username=@username";

            SqlConnection connection = new SqlConnection(db.getConnection());
            SqlCommand cmd = new SqlCommand(str, connection);
            cmd.Parameters.Add(new SqlParameter("username", username));

            try
            {
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }


                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                sda.Fill(ds, "UsersTable");

                if (ds.Tables["UsersTable"].Rows.Count > 0)
                {
                    connection.Close();
                    return true;
                }
                else
                {
                    return false;
                }


            }
            catch (Exception ex)
            {
                throw ex;
            }

        }


        public bool verifyRoomNumberBeforeInsert(string roomnumber)
        {
            DBContext db = new DBContext();

            string str = "select room_number from Rooms where room_number=@roomnumber";

            SqlConnection connection = new SqlConnection(db.getConnection());
            SqlCommand cmd = new SqlCommand(str, connection);
            cmd.Parameters.Add(new SqlParameter("roomnumber", roomnumber));

            try
            {
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }

                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                sda.Fill(ds, "RoomsTable");

                if (ds.Tables["RoomsTable"].Rows.Count > 0)
                {
                    connection.Close();
                    return true;
                }

                else
                {
                    return false;
                }


            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public bool createNewUser(Users model)
        {
            DBContext db = new DBContext();

            string str = "INSERT INTO Users (username, password, role, fullname) VALUES (@username,@password,@role,@fullname)";

            SqlConnection connection = new SqlConnection(db.getConnection());
            SqlCommand cmd = new SqlCommand(str, connection);
            cmd.Parameters.Add(new SqlParameter("username", model.username));
            cmd.Parameters.Add(new SqlParameter("password", model.password));
            cmd.Parameters.Add(new SqlParameter("role", model.role));
            cmd.Parameters.Add(new SqlParameter("fullname", model.fullname));

            try
            {
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }


                if (cmd.ExecuteNonQuery() > 0)
                {
                    connection.Close();
                    return true;
                }
                else
                {
                    return false;
                }


            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public bool createNewRoom(Rooms model)
        {
            DBContext db = new DBContext();

            string str = "INSERT INTO Rooms (room_number, room_type, room_name, room_capacity,room_cost,room_status,room_availability) VALUES (@room_number,@room_type,@room_name,@room_capacity,@room_cost,@room_status,@room_availability)";

            SqlConnection connection = new SqlConnection(db.getConnection());
            SqlCommand cmd = new SqlCommand(str, connection);
            cmd.Parameters.Add(new SqlParameter("room_number", model.room_number));
            cmd.Parameters.Add(new SqlParameter("room_type", model.room_type));
            cmd.Parameters.Add(new SqlParameter("room_name", model.room_name));
            cmd.Parameters.Add(new SqlParameter("room_capacity", model.room_capacity));
            cmd.Parameters.Add(new SqlParameter("room_cost", model.room_cost));
            cmd.Parameters.Add(new SqlParameter("room_status", model.room_status));
            cmd.Parameters.Add(new SqlParameter("room_availability", model.room_availability));

            try
            {
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }


                if (cmd.ExecuteNonQuery() > 0)
                {
                    connection.Close();
                    return true;
                }
                else
                {
                    return false;
                }


            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public bool deleteUserdata(string username)
        {
            DBContext db = new DBContext();

            string str = "delete from Users where username=@username";


            SqlConnection connection = new SqlConnection(db.getConnection());
            SqlCommand cmd = new SqlCommand(str, connection);
            cmd.Parameters.Add(new SqlParameter("username", username));

            try
            {
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }


                if (cmd.ExecuteNonQuery() > 0)
                {
                    connection.Close();
                    return true;
                }
                else
                {
                    return false;
                }


            }
            catch (Exception ex)
            {
                throw ex;
            }



        }

        public Users getUserData(int id)
        {

            Users userData = new Users();

            userData.username = "Empty";

            DBContext db = new DBContext();

            string str = "select * from Users where user_id=@uid";

            SqlConnection connection = new SqlConnection(db.getConnection());
            SqlCommand cmd = new SqlCommand(str, connection);
            cmd.Parameters.Add(new SqlParameter("uid", id));


            try
            {
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }

                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                sda.Fill(ds, "UsersTable");

                foreach (DataRow row in ds.Tables["UsersTable"].Rows)
                {

                    userData.username = row["username"].ToString();
                    userData.fullname = row["fullname"].ToString();
                    userData.password = row["password"].ToString();
                    userData.user_id = Convert.ToInt32(row["user_id"]);
                    userData.role = row["role"].ToString();



                }

                connection.Close();

                return userData;

            }
            catch (Exception ex)
            {
                throw ex;
            }



        }

        public Users getUserData(string username)
        {

            Users userData = new Users();

            userData.username = "Empty";

            DBContext db = new DBContext();

            string str = "select * from Users where username=@username";

            SqlConnection connection = new SqlConnection(db.getConnection());
            SqlCommand cmd = new SqlCommand(str, connection);
            cmd.Parameters.Add(new SqlParameter("username", username));


            try
            {
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }

                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                sda.Fill(ds, "UsersTable");

                foreach (DataRow row in ds.Tables["UsersTable"].Rows)
                {
                    userData.username = row["username"].ToString();
                    userData.fullname = row["fullname"].ToString();
                    userData.role = row["role"].ToString();

                }

                connection.Close();

                return userData;

            }
            catch (Exception ex)
            {
                throw ex;
            }



        }

        public Rooms getRoomData(string roomnumber)
        {

            Rooms roomData = new Rooms();

            roomData.room_number = "Empty";

            DBContext db = new DBContext();

            string str = "select * from Rooms where room_number=@roomnumber";

            SqlConnection connection = new SqlConnection(db.getConnection());
            SqlCommand cmd = new SqlCommand(str, connection);
            cmd.Parameters.Add(new SqlParameter("roomnumber", roomnumber));


            try
            {
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }

                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                sda.Fill(ds, "RoomsTable");

               foreach (DataRow row in ds.Tables["RoomsTable"].Rows)
                {
                    roomData.room_number = row["room_number"].ToString();
                    roomData.room_status = row["room_status"].ToString();
                    roomData.room_type = row["room_type"].ToString();
                    roomData.room_cost = row["room_cost"].ToString();
                    roomData.room_capacity = row["room_capacity"].ToString();
                    roomData.room_name = row["room_name"].ToString();
                    roomData.room_availability = row["room_availability"].ToString(); 
                }

                connection.Close();

                return roomData;

            }
            catch (Exception ex)
            {
                throw ex;
            }



        }

        public Users loginUser(Users model)
        {
            DBContext db = new DBContext();

            Users userData = new Users();

            userData.username = "Empty";

            string str = "select * from Users where username=@username and password=@password";

            SqlConnection connection = new SqlConnection(db.getConnection());
            SqlCommand cmd = new SqlCommand(str, connection);
            cmd.Parameters.Add(new SqlParameter("username", model.username));
            cmd.Parameters.Add(new SqlParameter("password", model.password));

            try
            {
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }

                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                sda.Fill(ds, "LoginTable");
                connection.Close();

                if (ds.Tables["LoginTable"].Rows.Count == 1)
                {
                    foreach (DataRow row in ds.Tables["LoginTable"].Rows)
                    {
                        userData.user_id = Convert.ToInt32(row["user_id"]);
                        userData.fullname = row["fullname"].ToString();
                        userData.role = row["role"].ToString();
                        userData.username = row["username"].ToString();
                    }



                }
                connection.Close();

                return userData;

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }



        public bool editUserData(Users model, string id)
        {
            DBContext db = new DBContext();

            string str = "update Users set fullname=@fullname,role=@role where username=@username";

            if (model.password != null)
            {
                str = "update Users set fullname=@fullname,password=@password,role=@role where username=@username";
            }


            SqlConnection connection = new SqlConnection(db.getConnection());
            SqlCommand cmd = new SqlCommand(str, connection);
            cmd.Parameters.Add(new SqlParameter("fullname", model.fullname));
            cmd.Parameters.Add(new SqlParameter("role", model.role));
            if (model.password != null)
            {
                cmd.Parameters.Add(new SqlParameter("password", model.password));
            }

            cmd.Parameters.Add(new SqlParameter("username", id));

            try
            {
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }


                if (cmd.ExecuteNonQuery() > 0)
                {
                    connection.Close();
                    return true;
                }
                else
                {
                    return false;
                }


            }
            catch (Exception ex)
            {
                throw ex;
            }



        }


        public bool editRoomData(Rooms model, string id)
        {
            DBContext db = new DBContext();

            string str = "update Rooms set room_type=@room_type, room_name=@room_name, room_capacity=@room_capacity,room_cost=@room_cost,room_status=@room_status,room_availability=@room_availability where room_number=@room_number";

           

            SqlConnection connection = new SqlConnection(db.getConnection());
            SqlCommand cmd = new SqlCommand(str, connection);
            cmd.Parameters.Add(new SqlParameter("room_type", model.room_type));
            cmd.Parameters.Add(new SqlParameter("room_name", model.room_name));
            cmd.Parameters.Add(new SqlParameter("room_capacity", model.room_capacity));
            cmd.Parameters.Add(new SqlParameter("room_cost", model.room_cost));
            cmd.Parameters.Add(new SqlParameter("room_status", model.room_status));
            cmd.Parameters.Add(new SqlParameter("room_availability", model.room_availability));
            cmd.Parameters.Add(new SqlParameter("room_number", id));

            try
            {
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }


                if (cmd.ExecuteNonQuery() > 0)
                {
                    connection.Close();
                    return true;
                }
                else
                {
                    return false;
                }


            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool updateUser(Users model)
        {
            DBContext db = new DBContext();

            string str = "update Users set fullname=@fullname where user_id=@user_id";

            if (model.password != null)
            {
                str = "update Users set password=@password , fullname=@fullname where user_id=@user_id";
            }


            SqlConnection connection = new SqlConnection(db.getConnection());
            SqlCommand cmd = new SqlCommand(str, connection);
            cmd.Parameters.Add(new SqlParameter("fullname", model.fullname));
            if (model.password != null)
            {
                cmd.Parameters.Add(new SqlParameter("password", model.password));
            }

            cmd.Parameters.Add(new SqlParameter("user_id", model.user_id));

            try
            {
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }


                if (cmd.ExecuteNonQuery() > 0)
                {
                    connection.Close();
                    return true;
                }
                else
                {
                    return false;
                }


            }
            catch (Exception ex)
            {
                throw ex;
            }



        }

        public List<Users> getStaffData()
        {
            List<Users> lstUsers = new List<Users>();
            DBContext db = new DBContext();

            string str = "select * from Staff";

            SqlConnection connection = new SqlConnection(db.getConnection());
            SqlCommand cmd = new SqlCommand(str, connection);

            try
            {
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }

                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                sda.Fill(ds, "StaffTable");

                foreach (DataRow row in ds.Tables["StaffTable"].Rows)
                {
                    Users temp = new Users();

                    temp.username = row["username"].ToString();
                    temp.role = row["role"].ToString();
                    temp.fullname = row["fullname"].ToString();

                    lstUsers.Add(temp);
                }

                connection.Close();

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return lstUsers;

        }


    }
}