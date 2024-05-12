using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace OjasMart.Models
{
    public class dbHelper
    {
        public string consStringNew = ConfigurationManager.ConnectionStrings["DefaultNewCon"].ConnectionString;
        string consString = ConfigurationManager.ConnectionStrings["DefaultNewCon"].ConnectionString;
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultNewCon"].ConnectionString);
        public int ExecuteNonQueryProc(string cmdText, SqlParameter[] prms)
        {
            int r = 0;
            try
            {
                //string str1 = System.Configuration.ConfigurationManager.ConnectionStrings["conStr"].ToString();
                using (SqlConnection conn = new SqlConnection(consString))
                {
                    using (SqlCommand cmd = new SqlCommand(cmdText, conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Clear();
                        if (prms != null)
                        {
                            foreach (SqlParameter p in prms)
                            {
                                cmd.Parameters.Add(p);
                            }
                        }
                        conn.Open();
                        try
                        {
                            r = cmd.ExecuteNonQuery();
                        }
                        catch (Exception ex)
                        {
                            r = 0;
                        }
                        finally
                        {
                            conn.Close();
                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return r;

        }
        public DataTable ExecProcDataTable(string ProName, SqlParameter[] Param)
        {
            DataTable dt = new DataTable();
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand(ProName, con);
                cmd.CommandType = CommandType.StoredProcedure;
                foreach (SqlParameter prm in Param)
                {
                    cmd.Parameters.Add(prm);
                }
                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                adp.Fill(dt);
            }
            catch (Exception ex)
            {

            }
            finally
            {
                con.Close();
            }
            return dt;
        }
        public DataSet ExecProcDataSet(string ProName, SqlParameter[] Param)
        {
            DataSet dt = new DataSet();
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand(ProName, con);
                cmd.CommandType = CommandType.StoredProcedure;
                foreach (SqlParameter prm in Param)
                {
                    cmd.Parameters.Add(prm);
                }
                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                adp.Fill(dt);
            }
            catch (Exception ex)
            {

            }
            finally
            {
                con.Close();
            }
            return dt;
        }
        public DataTable ExecAdaptorDataTable(string Query)
        {
            DataTable dt = new DataTable();
            try
            {
                SqlCommand cmd = new SqlCommand(Query, con);
                cmd.CommandType = CommandType.Text;
                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                adp.Fill(dt);
            }
            catch (Exception ex)
            {

            }
            return dt;
        }
    }
}