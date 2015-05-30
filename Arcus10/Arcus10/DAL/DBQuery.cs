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