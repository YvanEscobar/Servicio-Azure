using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApiRest.Models;
using System.Data.SqlClient;
using System.Collections;
using System.Configuration;

namespace WebApiRest
{
    public class PersonPersistence
    {
        // Se comenta para preparar servicio para Azure y se copia en servicio: getPersons
        // private SqlConnection conn;

        //public PersonPersistence()
        //{
        //    // string myConnectionString;
        //    string myConnectionString = "server=127.0.0.1;uid=root;pwd=1234;database=employeedb";
        //    try
        //    {
        //        conn = new SqlConnection();
        //        conn.ConnectionString = myConnectionString;
        //        conn.Open();
        //    }
        //    catch (Exception ex)
        //    {

        //    }
        //}

        public ArrayList Persons
        {
            get
            {

                // Se agrega
                SqlConnection conn;
                string myConnectionString = ConfigurationManager.ConnectionStrings["localDB"].ConnectionString;
                conn = new SqlConnection();
                try
                {
                    conn.ConnectionString = myConnectionString;
                    conn.Open();

                    ArrayList personArray = new ArrayList();
                    SqlDataReader mySQLReader = null;
                    String sqlString = "SELECT * FROM tblpersonnel";
                    SqlCommand cmd = new SqlCommand(sqlString, conn);

                    mySQLReader = cmd.ExecuteReader();
                    while (mySQLReader.Read())
                    {
                        Person p = new Person();
                        p.ID = mySQLReader.GetInt32(0);
                        p.FirstName = mySQLReader.GetString(1);
                        p.LastName = mySQLReader.GetString(2);
                        p.PayRate = mySQLReader.GetDouble(3);
                        p.StartDate = mySQLReader.GetDateTime(4);
                        p.EndDate = mySQLReader.GetDateTime(5);
                        personArray.Add(p);
                    }
                    return personArray;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    conn.Close();
                }

            }
        }

        public Person getPerson(long ID)
        {
            //Person p = new Person();
            SqlConnection conn;
            string myConnectionString = ConfigurationManager.ConnectionStrings["localDB"].ConnectionString;
            conn = new SqlConnection();
            try
            {
                conn.ConnectionString = myConnectionString;
                conn.Open();

                //ArrayList personArray = new ArrayList();
                Person p = new Person();
                SqlDataReader mySQLReader = null;

                String sqlString = "SELECT * FROM tblpersonnel WHERE ID = " + ID.ToString();
                SqlCommand cmd = new SqlCommand(sqlString, conn);

                mySQLReader = cmd.ExecuteReader();
                if (mySQLReader.Read())
                {
                    p.ID = mySQLReader.GetInt32(0);
                    p.FirstName = mySQLReader.GetString(1);
                    p.LastName = mySQLReader.GetString(2);
                    p.PayRate = mySQLReader.GetDouble(3);
                    p.StartDate = mySQLReader.GetDateTime(4);
                    p.EndDate = mySQLReader.GetDateTime(5);

                    return p;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }
        }


        public Person getPerson(String firstName, String lastName)
        {
            //Person p = new Person();
            SqlConnection conn;
            string myConnectionString = ConfigurationManager.ConnectionStrings["localDB"].ConnectionString;
            conn = new SqlConnection();
            try
            {
                conn.ConnectionString = myConnectionString;
                conn.Open();

                //ArrayList personArray = new ArrayList();
                Person p = new Person();
                SqlDataReader mySQLReader = null;

                String sqlString = "SELECT * FROM tblpersonnel WHERE FirstName = '" + firstName + "' AND LastName = '" + lastName + "'";
                SqlCommand cmd = new SqlCommand(sqlString, conn);

                mySQLReader = cmd.ExecuteReader();
                if (mySQLReader.Read())
                {
                    p.ID = mySQLReader.GetInt32(0);
                    p.FirstName = mySQLReader.GetString(1);
                    p.LastName = mySQLReader.GetString(2);
                    p.PayRate = mySQLReader.GetDouble(3);
                    p.StartDate = mySQLReader.GetDateTime(4);
                    p.EndDate = mySQLReader.GetDateTime(5);

                    return p;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }
        }

        public Boolean deletePerson(long ID)
        {
            // Person p = new Person();
            // SqlDataReader mySQLReader = null;

            SqlConnection conn;
            string myConnectionString = ConfigurationManager.ConnectionStrings["localDB"].ConnectionString;
            conn = new SqlConnection();
            try
            {
                // ArrayList personArray = new ArrayList();
                conn.ConnectionString = myConnectionString;
                conn.Open();

                Person p = new Person();
                SqlDataReader mySQLReader = null;

                String sqlString = "SELECT * FROM tblpersonnel WHERE ID = " + ID.ToString();
                SqlCommand cmd = new SqlCommand(sqlString, conn);

                mySQLReader = cmd.ExecuteReader();
                if (mySQLReader.Read())
                {
                    mySQLReader.Close();
                    sqlString = "DELETE FROM tblpersonnel WHERE ID = " + ID.ToString();
                    cmd = new SqlCommand(sqlString, conn);

                    cmd.ExecuteNonQuery();
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
            finally
            {
                conn.Close();
            }
        }

        public bool updatePerson(long ID, Person personToSave)
        {
            // Se agrega

            SqlConnection conn;
            string myConnectionString = ConfigurationManager.ConnectionStrings["localDB"].ConnectionString;
            conn = new SqlConnection();
            try
            {
                //ArrayList personArray = new ArrayList();
                conn.ConnectionString = myConnectionString;
                conn.Open();

                SqlDataReader mySQLReader = null;

                String sqlString = "SELECT * FROM tblpersonnel WHERE ID = " + ID.ToString();
                SqlCommand cmd = new SqlCommand(sqlString, conn);

                mySQLReader = cmd.ExecuteReader();
                if (mySQLReader.Read())
                {
                    mySQLReader.Close();
                    sqlString = "UPDATE tblPersonnel SET FirstName='" + personToSave.FirstName + "', LastName='" + personToSave.LastName + "', PayRate='" + personToSave.PayRate + "', StartDate='" + personToSave.StartDate.ToString("yyyy-MM-dd HH:mm:ss") + "', EndDate='" + personToSave.EndDate.ToString("yyyy-MM-dd HH:mm:ss") + "' WHERE ID = " + ID.ToString();
                    cmd = new SqlCommand(sqlString, conn);

                    cmd.ExecuteNonQuery();
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
            finally
            {
                conn.Close();
            }
        }
        public long savePerson(Person personToSave)
        {
            // Se agrega
            SqlConnection conn;
            string myConnectionString = ConfigurationManager.ConnectionStrings["localDB"].ConnectionString;
            conn = new SqlConnection();
            try
            {
                conn.ConnectionString = myConnectionString;
                conn.Open();

                //ArrayList personArray = new ArrayList();

                String sqlString = "INSERT INTO tblPersonnel (FirstName, LastName, PayRate, StartDate, EndDate) VALUES ('" + personToSave.FirstName + "','" + personToSave.LastName + "','" + personToSave.PayRate + "','" + personToSave.StartDate.ToString("yyyy-MM-dd HH:mm:ss") + "','" + personToSave.EndDate.ToString("yyyy-MM-dd HH:mm:ss") + "'); SELECT SCOPE_IDENTITY();";
                SqlCommand cmd = new SqlCommand(sqlString, conn);
                decimal id = (decimal)cmd.ExecuteScalar();
                return (long)id;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }
        }
    }
}