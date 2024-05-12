using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Web;

namespace OjasMart.Models
{
    public class ConnectionClass
    {
        dbHelper db = new dbHelper();
        SendSMS sm = new SendSMS();
        public ConnectionClass()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public string str = System.Configuration.ConfigurationManager.ConnectionStrings["ozasmartEntities"].ToString();
        public SqlConnection con;
        public void Connection()
        {
            con = new SqlConnection(str);
        }
        public DataTable GetCompanyDet()
        {
            Connection();
            DataTable dt = new DataTable();
            con.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "select *  from CompanyMaster";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = cmd;
            da.Fill(dt);
            con.Close();
            return dt;
        }
        public DataTable GetServiceDet(int ServiceId)
        {
            Connection();
            DataTable dt = new DataTable();
            con.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "select *  from Service_Master where serviceID='" + ServiceId + "'";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = cmd;
            da.Fill(dt);
            con.Close();
            return dt;
        }
        public DataTable GetPlanByservicEID(int ServiceId, string MobileNo)
        {
            Connection();
            DataTable dt = new DataTable();
            con.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "USP_S_PlanByServiceID";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ServiceID", ServiceId);
            cmd.Parameters.AddWithValue("@MobileNo", MobileNo);
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = cmd;
            da.Fill(dt);
            con.Close();
            return dt;
        }
        public DataTable GetPlanByservicEIDandPlan(int PlanId, int ServiceId)
        {
            Connection();
            DataTable dt = new DataTable();
            con.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "USP_S_GetPlans";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@PlanID", PlanId);
            cmd.Parameters.AddWithValue("@ServiceID", ServiceId);
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = cmd;
            da.Fill(dt);
            con.Close();
            return dt;
        }
        public DataTable GetUserDet(string MobileNo)
        {
            Connection();
            DataTable dt = new DataTable();
            con.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "USP_S_UserProfile_Det";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@MobileNo", MobileNo);
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = cmd;
            da.Fill(dt);
            con.Close();
            return dt;
        }
        public DataTable GetUserBookings(string MobileNo)
        {
            Connection();
            DataTable dt = new DataTable();
            con.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "USP_S_MyBookings";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@MobileNo", MobileNo);
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = cmd;
            da.Fill(dt);
            con.Close();
            return dt;
        }
        public DataTable GetUserPlans(string MobileNo)
        {
            Connection();
            DataTable dt = new DataTable();
            con.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "USP_S_MyPlans";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@MobileNo", MobileNo);
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = cmd;
            da.Fill(dt);
            con.Close();
            return dt;
        }
        public DataTable GetUserDetPlan(string MobileNo, int ServiceId, int PlanId)
        {
            Connection();
            DataTable dt = new DataTable();
            con.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "select * from CustomerPlans where MobileNo=@MobileNo and serviceid=@serviceid and PlanId=@PlanId";
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@MobileNo", MobileNo);
            cmd.Parameters.AddWithValue("@serviceid", ServiceId);
            cmd.Parameters.AddWithValue("@PlanId", PlanId);
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = cmd;
            da.Fill(dt);
            con.Close();
            return dt;
        }

        public DataTable Getuserproduct(string Customerid)
        {
            Connection();
            DataTable dt = new DataTable();
            con.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "select * from CustomerPlans where MobileNo=@custID";
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@custID", Customerid);
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = cmd;
            da.Fill(dt);
            con.Close();
            return dt;
        }
        public DataTable Reportbooking(string Customerid)
        {
            Connection();
            DataTable dt = new DataTable();
            con.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "USP_S_Customerplan";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Mobile", Customerid);
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = cmd;
            da.Fill(dt);
            con.Close();
            return dt;
        }

        public DataTable Reportcomplaint(string Customerid)
        {
            Connection();
            DataTable dt = new DataTable();
            con.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "USP_S_Customercomplain";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Mobile", Customerid);
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = cmd;
            da.Fill(dt);
            con.Close();
            return dt;
        }

        public DataTable Getengineerproduct(string MobileNo)
        {
            Connection();
            DataTable dt = new DataTable();
            con.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "USP_S_EngAllotedProduct";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Mobile", MobileNo);
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = cmd;
            da.Fill(dt);
            con.Close();
            return dt;
        }


        public DataTable Engineerlead(string MobileNo)
        {
            Connection();
            DataTable dt = new DataTable();
            con.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "USP_S_EngComplaint";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Email", MobileNo);
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = cmd;
            da.Fill(dt);
            con.Close();
            return dt;
        }

        public string Veryfyhappycode(string allotementid, string complaintid, string happycode, string remark)
        {
            string str = "";
            Connection();
            con.Open();
            SqlTransaction trans = con.BeginTransaction();
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "dbo.[USP_U_Status]";
                cmd.Parameters.AddWithValue("@AID", allotementid);
                cmd.Parameters.AddWithValue("@CID", complaintid);
                cmd.Parameters.AddWithValue("@HappyCode", happycode);
                cmd.Parameters.AddWithValue("@OutRemark", remark);
                cmd.Parameters["@OutRemark"].Direction = ParameterDirection.InputOutput;
                cmd.Parameters["@OutRemark"].Size = 256;
                cmd.Transaction = trans;
                cmd.ExecuteNonQuery();
                str = cmd.Parameters["@OutRemark"].Value.ToString();
                trans.Commit();
                con.Close();
            }
            catch (Exception ex)
            {
                str = ex.Message;
                trans.Rollback();
            }
            con.Close();
            return str;
        }

        //checkaddress

        public string Checkaddress(string Mobile, string Count)
        {
            string str = "";
            Connection();
            con.Open();
            SqlTransaction trans = con.BeginTransaction();
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "dbo.[USP_S_checkaddress]";
                cmd.Parameters.AddWithValue("@MobileNo", Mobile);
                cmd.Parameters.AddWithValue("@Count", "");
                cmd.Parameters["@Count"].Direction = ParameterDirection.InputOutput;
                cmd.Parameters["@Count"].Size = 256;
                cmd.Transaction = trans;
                cmd.ExecuteNonQuery();
                str = cmd.Parameters["@Count"].Value.ToString();
                trans.Commit();
                con.Close();
            }
            catch (Exception ex)
            {
                str = ex.Message;
                trans.Rollback();
            }
            con.Close();
            return str;
        }

        /* public string Updateddress(string Mobile,string Address,string Area,string  PinCode, string Count)
         {
             string str = "";
             Connection();
             con.Open();
             SqlTransaction trans = con.BeginTransaction();
             try
             {
                 SqlCommand cmd = new SqlCommand();
                 cmd.Connection = con;
                 cmd.CommandType = CommandType.StoredProcedure;
                 cmd.CommandText = "dbo.[USP_I_Updateaddress]";
                 cmd.Parameters.AddWithValue("@Moible", Mobile);
                 cmd.Parameters.AddWithValue("@Address", Address);
                 cmd.Parameters.AddWithValue("@Area", Area);
                 cmd.Parameters.AddWithValue("@PinCode", PinCode);
                 cmd.Parameters.AddWithValue("@Count", "");
                 cmd.Parameters["@Count"].Direction = ParameterDirection.InputOutput;
                 cmd.Parameters["@Count"].Size = 256;
                 cmd.Transaction = trans;
                 cmd.ExecuteNonQuery();
                 str = cmd.Parameters["@Count"].Value.ToString();
                 trans.Commit();
                 con.Close();
             }
             catch (Exception ex)
             {
                 str = ex.Message;
                 trans.Rollback();
             }
             con.Close();
             return str;
         } 

         public string Updateaddress(string Mobile, string Address, string Area, string Pin, string Count)
         {
             string str = "";
             Connection();
             con.Open();
             SqlTransaction trans = con.BeginTransaction();
             try
             {
                 SqlCommand cmd = new SqlCommand();
                 cmd.Connection = con;
                 cmd.CommandType = CommandType.StoredProcedure;
                 cmd.CommandText = "dbo.[USP_U_Updateaddress]";
                 cmd.Parameters.AddWithValue("@MobileNo", Mobile);
                 cmd.Parameters.AddWithValue("@Address", Address);
                 cmd.Parameters.AddWithValue("@Area", Area);
                 cmd.Parameters.AddWithValue("@Pin", Pin);
                 cmd.Parameters.AddWithValue("@Count", "");
                 cmd.Parameters["@Count"].Direction = ParameterDirection.InputOutput;
                 cmd.Parameters["@Count"].Size = 256;
                 cmd.Transaction = trans;
                 cmd.ExecuteNonQuery();
                 str = cmd.Parameters["@Count"].Value.ToString();
                 trans.Commit();
                 con.Close();
             }
             catch (Exception ex)
             {
                 str = ex.Message;
                 trans.Rollback();
             }
             con.Close();
             return str;
         }*/

        public string Updateprofile(string Mobile, string Address, string Pin, string Email, string Count)
        {
            string str = "";
            Connection();
            con.Open();
            SqlTransaction trans = con.BeginTransaction();
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "dbo.[USP_I_Updateprofile]";
                cmd.Parameters.AddWithValue("@Mobile", Mobile);
                cmd.Parameters.AddWithValue("@Address", Address);
                cmd.Parameters.AddWithValue("@PinCode", Pin);
                cmd.Parameters.AddWithValue("@Email", Email);
                cmd.Parameters.AddWithValue("@Count", "");
                cmd.Parameters["@Count"].Direction = ParameterDirection.InputOutput;
                cmd.Parameters["@Count"].Size = 256;
                cmd.Transaction = trans;
                cmd.ExecuteNonQuery();
                str = cmd.Parameters["@Count"].Value.ToString();
                trans.Commit();
                con.Close();
            }
            catch (Exception ex)
            {
                str = ex.Message;
                trans.Rollback();
            }
            con.Close();
            return str;
        }

        public string Ratingservice(string MobileNo, string Rating)
        {
            string str = "";
            Connection();
            con.Open();
            SqlTransaction trans = con.BeginTransaction();
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "dbo.[USP_I_Rating]";
                cmd.Parameters.AddWithValue("@customer", MobileNo);
                cmd.Parameters.AddWithValue("@rate", Rating);
                str = "rated";
                cmd.Transaction = trans;
                cmd.ExecuteNonQuery();
                trans.Commit();
                con.Close();
            }
            catch (Exception ex)
            {
                str = ex.Message;
                trans.Rollback();
            }
            con.Close();
            return str;
        }

        public string Ratingservice(string MobileNo, string Address, string Area, string Pincode, string Rating)
        {
            string str = "";
            Connection();
            con.Open();
            SqlTransaction trans = con.BeginTransaction();
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "dbo.[USP_I_Rating]";
                cmd.Parameters.AddWithValue("@customer", MobileNo);
                cmd.Parameters.AddWithValue("@rate", Rating);
                str = "rated";
                cmd.Transaction = trans;
                cmd.ExecuteNonQuery();
                trans.Commit();
                con.Close();
            }
            catch (Exception ex)
            {
                str = ex.Message;
                trans.Rollback();
            }
            con.Close();
            return str;
        }

        public string reneratepassword(int length)
        {
            string alphabets = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            string numbers = "1234567890";
            string characters = numbers;
            characters += alphabets + numbers;
            string otp = string.Empty;
            for (int i = 0; i < length; i++)
            {
                string character = string.Empty;
                do
                {
                    int index = new Random().Next(0, characters.Length);
                    character = characters.ToCharArray()[index].ToString();
                } while (otp.IndexOf(character) != -1);
                otp += character;
            }
            return otp;
        }

        public void sendotp(string Mob, string Msg)
        {

            //string URL = "http://sms.sigmasoftwares.org/http-api.php?username=amcondoor&password=123456&senderid=AMCDOR&route=2&number=" +Mob +"&message="+ Msg;
            string URL = "http://mysmsshop.in/http-api.php?username=&password=Admin123$&senderid=&route=1&number=" + Mob + "&message=" + Msg + "";
            //string URL = "http://bhashsms.com/api/sendmsg.php?user=martizaa&pass=123456&sender=MRTIZA&phone=" + Mob + "&text=" + Msg + "&priority=ndnd&stype=normal";
            HttpWebRequest myReq = (HttpWebRequest)WebRequest.Create(URL);
            HttpWebResponse myResp = (HttpWebResponse)myReq.GetResponse();
            System.IO.StreamReader respStreamReader = new System.IO.StreamReader(myResp.GetResponseStream());
            string responseString = respStreamReader.ReadToEnd();
            respStreamReader.Close();
            myResp.Close();
        }
        public string InsertUpdateMobileNo(string MobileNo)
        {
            string str = "";
            Connection();
            con.Open();
            SqlTransaction trans = con.BeginTransaction();
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "dbo.[USP_I_Mobile_Master]";
                cmd.Parameters.AddWithValue("MobileNo", MobileNo);
                cmd.Parameters.AddWithValue("Msg", "");
                cmd.Parameters["Msg"].Direction = ParameterDirection.InputOutput;
                cmd.Parameters["Msg"].Size = 256;
                cmd.Transaction = trans;
                cmd.ExecuteNonQuery();
                str = cmd.Parameters["Msg"].Value.ToString();
                trans.Commit();
                con.Close();
            }
            catch (Exception ex)
            {
                str = ex.Message;
                trans.Rollback();
            }
            con.Close();
            return str;
        }
        public string InsertOTPMobileNo(string MobileNo, string OTP)
        {
            string str = "";
            Connection();
            con.Open();
            SqlTransaction trans = con.BeginTransaction();
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "dbo.[USP_InsertOTPLog]";
                cmd.Parameters.AddWithValue("MobileNo", MobileNo);
                cmd.Parameters.AddWithValue("OTP", OTP);
                cmd.Parameters.AddWithValue("Msg", "");
                cmd.Parameters["Msg"].Direction = ParameterDirection.InputOutput;
                cmd.Parameters["Msg"].Size = 256;
                cmd.Transaction = trans;
                cmd.ExecuteNonQuery();
                str = cmd.Parameters["Msg"].Value.ToString();
                trans.Commit();
                con.Close();
            }
            catch (Exception ex)
            {
                str = ex.Message;
                trans.Rollback();
            }
            con.Close();
            return str;
        }
        public string UpdateOTPMobileNo(string MobileNo, string OTP)
        {
            string str = "";
            Connection();
            con.Open();
            SqlTransaction trans = con.BeginTransaction();
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "dbo.[USP_UpdateOTPLog]";
                cmd.Parameters.AddWithValue("MobileNo", MobileNo);
                cmd.Parameters.AddWithValue("OTP", OTP);
                cmd.Parameters.AddWithValue("Msg", "");
                cmd.Parameters["Msg"].Direction = ParameterDirection.InputOutput;
                cmd.Parameters["Msg"].Size = 256;
                cmd.Transaction = trans;
                cmd.ExecuteNonQuery();
                str = cmd.Parameters["Msg"].Value.ToString();
                trans.Commit();
                con.Close();
            }
            catch (Exception ex)
            {
                str = ex.Message;
                trans.Rollback();
            }
            con.Close();
            return str;
        }
        public string UpdateRegistrationNo(string MobileNo, string Name, string Email, string Address, string PinCode)
        {
            string str = "";
            Connection();
            con.Open();
            SqlTransaction trans = con.BeginTransaction();
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                string refcode = reneratepassword(4);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "dbo.[USP_UpdateUserRegistration]";
                cmd.Parameters.AddWithValue("Name", Name);
                cmd.Parameters.AddWithValue("Email", Email);
                cmd.Parameters.AddWithValue("MobileNo", MobileNo);
                cmd.Parameters.AddWithValue("Address", Address);
                cmd.Parameters.AddWithValue("PinCode", PinCode);
                cmd.Parameters.AddWithValue("Ref", refcode);
                cmd.Parameters.AddWithValue("Msg", "");
                cmd.Parameters["Msg"].Direction = ParameterDirection.InputOutput;
                cmd.Parameters["Msg"].Size = 256;
                cmd.Transaction = trans;
                cmd.ExecuteNonQuery();
                str = cmd.Parameters["Msg"].Value.ToString();
                trans.Commit();
                con.Close();
            }
            catch (Exception ex)
            {
                str = ex.Message;
                trans.Rollback();
            }
            con.Close();
            return str;
        }

        public string Customercomplaint(string MobileNo, string serviceid, string planid, string complaint, string status, string count)
        {
            string str = "";
            Connection();
            con.Open();
            SqlTransaction trans = con.BeginTransaction();
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                string refcode = reneratepassword(4);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "dbo.[USP_I_Customercomplaint]";
                cmd.Parameters.AddWithValue("Mobile", MobileNo);
                cmd.Parameters.AddWithValue("serviceid", serviceid);
                cmd.Parameters.AddWithValue("planid", planid);
                cmd.Parameters.AddWithValue("Complaint", complaint);
                cmd.Parameters.AddWithValue("Status", status);
                cmd.Parameters.AddWithValue("Count", "");
                cmd.Parameters["Count"].Direction = ParameterDirection.InputOutput;
                cmd.Parameters["Count"].Size = 256;
                cmd.Transaction = trans;
                cmd.ExecuteNonQuery();
                str = cmd.Parameters["Count"].Value.ToString();
                trans.Commit();
                con.Close();
            }
            catch (Exception ex)
            {
                str = ex.Message;
                trans.Rollback();
            }
            con.Close();
            return str;
        }

        public DataTable BindRegistrationNo(string MobileNo, string UserType)
        {
            Connection();
            DataTable dt = new DataTable();
            con.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "USP_BindUserRegistration";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter();
            cmd.Parameters.AddWithValue("MobileNo", MobileNo);
            cmd.Parameters.AddWithValue("UserType", UserType);
            da.SelectCommand = cmd;
            da.Fill(dt);
            con.Close();
            return dt;
        }
        public DataTable BindService()
        {

            Connection();
            DataTable dt = new DataTable();
            con.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "USP_BindService";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = cmd;
            da.Fill(dt);
            con.Close();
            return dt;
        }

        public DataTable BindUpcommingService()
        {

            Connection();
            DataTable dt = new DataTable();
            con.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "USP_BindUpcomingService";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter();

            da.SelectCommand = cmd;
            da.Fill(dt);
            con.Close();
            return dt;
        }

        public DataTable BindServiceaccordinguser(string Mobile)
        {
            Connection();
            DataTable dt = new DataTable();
            con.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "USP_S_ServiceMasterapp";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter();
            cmd.Parameters.AddWithValue("Mobile", Mobile);
            da.SelectCommand = cmd;
            da.Fill(dt);
            con.Close();
            return dt;
        }

        public DataTable Bindplanaccordinguserandservice(string Mobile, int serviceid)
        {
            Connection();
            DataTable dt = new DataTable();
            con.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "USP_S_PlanMasterapp";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter();
            cmd.Parameters.AddWithValue("Mobile", Mobile);
            cmd.Parameters.AddWithValue("serviceid", serviceid);
            da.SelectCommand = cmd;
            da.Fill(dt);
            con.Close();
            return dt;
        }

        public string InsertUpdateComplaintMaster(string Complaint, string MobileNo, int service_id, int Plan_id)
        {
            string str = "";
            Connection();
            con.Open();
            SqlTransaction trans = con.BeginTransaction();
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "USP_I_Complaint_Master";
                cmd.Parameters.AddWithValue("Complaint", Complaint);
                cmd.Parameters.AddWithValue("User_ID", MobileNo);
                cmd.Parameters.AddWithValue("Service_Id", service_id);
                cmd.Parameters.AddWithValue("Plan_ID", Plan_id);
                cmd.Parameters.AddWithValue("Msg", "");
                cmd.Parameters["Msg"].Direction = ParameterDirection.InputOutput;
                cmd.Parameters["Msg"].Size = 256;
                cmd.Transaction = trans;
                cmd.ExecuteNonQuery();
                str = cmd.Parameters["Msg"].Value.ToString();
                trans.Commit();
                con.Close();
            }
            catch (Exception ex)
            {
                str = ex.Message;
                trans.Rollback();
            }
            con.Close();
            return str;
        }
        public DataTable GetFeaturesPlannser(int ServiceId, int PlanId)
        {
            Connection();
            DataTable dt = new DataTable();
            con.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "select Serviceid,Planid,Features from PlanFeatures where ServiceId=@ServiceId and PlanId=@PlanId";
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@ServiceID", ServiceId);
            cmd.Parameters.AddWithValue("@PlanId", PlanId);
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = cmd;
            da.Fill(dt);
            con.Close();
            return dt;
        }
        public DataTable GetServicPlan(int ServiceId)
        {
            Connection();
            DataTable dt = new DataTable();
            con.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "select * from Plan_Master where ServiceId=@ServiceId";
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@ServiceID", ServiceId);
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = cmd;
            da.Fill(dt);
            con.Close();
            return dt;
        }

        public DataTable GetServicPlan1(int ServiceId)
        {
            Connection();
            DataTable dt = new DataTable();
            con.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SELECT pm.PlanID AS PlanID,pm.PlanName AS PlanName,pm.PlanPrice AS PlanPrice,pm.ServiceID AS ServiceID,pt.PlanDescription AS PlanDescription,pt.Plantype AS PlanStatus,pm.Planimage,pt.PlanDuration,(SELECT count(PlanName) FROM Planvstype WHERE PlanName=pm.PlanName AND ServiceID=@ServiceId) AS Count FROM Plan_Master pm JOIN Planvstype pt ON pm.PlanID = pt.PlanID AND pm.ServiceID = pt.ServiceID where pm.ServiceId=@ServiceId ORDER BY PlanID ,(CASE WHEN pt.Plantype='PLATINUM' THEN 10  WHEN pt.Plantype='GOLD' THEN 9 WHEN pt.Plantype='SILVER' THEN 8 ELSE 12 END) DESC";
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@ServiceID", ServiceId);
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = cmd;
            da.Fill(dt);
            con.Close();
            return dt;
        }


        public DataTable BindServices()
        {
            Connection();
            DataTable dt = new DataTable();
            con.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "USP_S_Services4Registration";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = cmd;
            da.Fill(dt);
            con.Close();
            return dt;
        }
        public DataTable BindPlans(int serviceid)
        {
            Connection();
            DataTable dt = new DataTable();
            con.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "USP_S_Plan_ServiceMaster4P";
            cmd.Parameters.AddWithValue("@serviceID", serviceid);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = cmd;
            da.Fill(dt);
            con.Close();
            return dt;
        }
        public string InsertUpdatePlanBycustomers(string RecieptNo, string MobileNo, DataTable MainTable, string paymode)
        {
            string str = "";
            Connection();
            con.Open();
            SqlTransaction trans = con.BeginTransaction();
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "USP_I_Plan_Purchased";
                cmd.Parameters.AddWithValue("RecieptNo", RecieptNo);
                cmd.Parameters.AddWithValue("MobileNo", MobileNo);
                cmd.Parameters.AddWithValue("CustomerPlans", MainTable);
                cmd.Parameters.AddWithValue("PayMode", paymode);
                cmd.Parameters.AddWithValue("Msg", "");
                cmd.Parameters["Msg"].Direction = ParameterDirection.InputOutput;
                cmd.Parameters["Msg"].Size = 256;
                cmd.Transaction = trans;
                cmd.ExecuteNonQuery();
                str = cmd.Parameters["Msg"].Value.ToString();
                trans.Commit();
                con.Close();
            }
            catch (Exception ex)
            {
                str = ex.Message;
                trans.Rollback();
            }
            con.Close();
            return str;
        }
        public string CheckMobile(string MobileNo)
        {
            string str = "";
            Connection();
            con.Open();
            SqlTransaction trans = con.BeginTransaction();
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "USP_S_CheckMobile";
                cmd.Parameters.AddWithValue("MobileNo", MobileNo);
                cmd.Parameters.AddWithValue("Remark", "");
                cmd.Parameters["Remark"].Direction = ParameterDirection.InputOutput;
                cmd.Parameters["Remark"].Size = 256;
                cmd.Transaction = trans;
                cmd.ExecuteNonQuery();
                str = cmd.Parameters["Remark"].Value.ToString();
                trans.Commit();
                con.Close();
            }
            catch (Exception ex)
            {
                str = ex.Message;
                trans.Rollback();
            }
            con.Close();
            return str;
        }

        public DataTable GetCustomer(string userid, string password)
        {
            string msg = "";
            Connection();
            con.Open();
            DataTable dt = new DataTable();
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "Proc_GetLoginDetails";
                cmd.Parameters.AddWithValue("@Action", "5");
                cmd.Parameters.AddWithValue("@UserName", userid);
                cmd.Parameters.AddWithValue("@Password", password);

                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                sda.Fill(dt);

            }
            catch (Exception ex)
            {
                dt = null;
            }
            finally
            {
                con.Close();
            }
            return dt;
        }

        public DataTable GetState()
        {
            string msg = "";
            Connection();
            con.Open();
            DataTable dt = new DataTable();
            try
            {
                string qry = "SELECT '0' AS State_id,'Select State' AS State_name UNION ALL SELECT State_id,State_name+' ('+State_Abr+')' State_name FROM State_Master ";
                SqlCommand cmd = new SqlCommand(qry, con);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                sda.Fill(dt);
            }
            catch (Exception ex)
            {
                dt = null;
            }
            finally
            {
                con.Close();
            }
            return dt;
        }

        public DataTable getCity(int id)
        {
            Connection(); con.Open();
            DataTable dt = new DataTable();
            try
            {
                string qry = "SELECT ID, CityName FROM CityMaster WHERE StateId='" + id + "' ";
                SqlCommand cmd = new SqlCommand(qry, con);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                sda.Fill(dt);
            }
            catch (Exception ex)
            {
                dt = null;
            }
            finally
            {
                con.Close();
            }
            return dt;
        }

        //public string InsertCustomer(string Name, string email, string mobile, string Password)
        //{
        //    string msg = "";
        //    Connection();
        //    con.Open();
        //    //SqlTransaction trans = con.BeginTransaction();
        //    try
        //    {
        //        SqlCommand cmd = new SqlCommand();
        //        cmd = new SqlCommand();
        //        cmd.Connection = con;
        //        cmd.CommandType = CommandType.StoredProcedure;
        //        cmd.CommandText = "CustomerApiRegistration";
        //        //cmd.Parameters.AddWithValue("@Id", null);
        //        //cmd.Parameters.AddWithValue("@CustomerId", null);
        //        cmd.Parameters.AddWithValue("@Name", Name);
        //        cmd.Parameters.AddWithValue("@Email", email);
        //        cmd.Parameters.AddWithValue("@mobile", mobile);
        //        cmd.Parameters.AddWithValue("@Pass", Password);
        //        cmd.Parameters.AddWithValue("@Branch", "123");
        //        cmd.Parameters.AddWithValue("@gender", "123");

        //        //cmd.Parameters.AddWithValue("@MSG", "");
        //        //cmd.Parameters["@MSG"].Direction = ParameterDirection.Output;
        //        //cmd.Parameters["@MSG"].Size = 256;
        //        //cmd.Parameters.AddWithValue("@MSG1", "");
        //        //cmd.Parameters["@MSG1"].Direction = ParameterDirection.Output;
        //        //cmd.Parameters["@MSG1"].Size = 50;
        //        //cmd.Transaction = trans;
        //        int i = cmd.ExecuteNonQuery();
        //        str = cmd.Parameters["@custid"].Value.ToString();
        //        //msg1 = cmd.Parameters["@MSG1"].Value.ToString();
        //        //trans.Commit();
        //        if (i > 0)
        //        {

        //            SendSMS sms = new SendSMS();
        //            sms.sendsms1(mobile, "Thank you for Registration with .");

        //            sms.sendsms1("12312413", "A new Customer having EmailId:" + email + " and mobile:" + mobile + " has been registered on gososcart.");
        //        }
        //        else
        //        {
        //            str = "Mobile No. Already Registered";
        //        }
        //        con.Close();
        //    }
        //    catch (Exception ex)
        //    {
        //        str = "0";

        //        //trans.Rollback();
        //    }
        //    finally
        //    {
        //        con.Close();
        //    }
        //    return str;
        //}



        public DataTable InsertCustomer(string Name, string Email, string Mobile, string Password, string RefferCode, int action)
        {
            DataTable dt = new DataTable();


            SqlParameter[] parameter = new SqlParameter[] {
                new SqlParameter("@Action",action),
                new SqlParameter("@name",Mobile),
                new SqlParameter("@EmailId",Email),
                new SqlParameter("@Password",Password),
                new SqlParameter("@mobileno",Name),
                new SqlParameter("@UsedReferal",RefferCode),
            };
            return db.ExecProcDataTable("Proc_InserCustomerAccountWeb", parameter);

        }

        //public string InsertCustomer(string Name, string email, string mobile, string Password, out string msg1)
        //{
        //    string msg = "";
        //    Connection();
        //    con.Open();
        //    DataTable dt = new DataTable();
        //    try
        //    {
        //        SqlCommand cmd = new SqlCommand();
        //        cmd = new SqlCommand();
        //        cmd.Connection = con;
        //        cmd.CommandType = CommandType.StoredProcedure;
        //        cmd.CommandText = "dbo.Api_Payment";

        //        cmd.Parameters.AddWithValue("@OrderNo", OrderId);
        //        cmd.Parameters.AddWithValue("@PayStatus", PaymentStatus);
        //        cmd.Parameters.AddWithValue("@TransId", TransactionId);
        //        cmd.Parameters.AddWithValue("@TotalPrice", TotalAmount);
        //        cmd.Parameters.AddWithValue("@PaidPrice", PaidAmount == "" ? null : PaidAmount);
        //        cmd.Parameters.AddWithValue("@DiscountAmount", DiscountAmount == "" ? null : DiscountAmount);
        //        cmd.Parameters.AddWithValue("@PaymentMode", paymentMode);
        //        cmd.Parameters.AddWithValue("@DeliveryType", DeliveryType);
        //        cmd.Parameters.AddWithValue("@CustomerCode", CustomerCode);

        //        cmd.Parameters.AddWithValue("@Myhpayid", Myhpayid);
        //        cmd.Parameters.AddWithValue("@CardType", CardType);
        //        cmd.Parameters.AddWithValue("@CardNum", CardNum);


        //        //cmd.Parameters.AddWithValue("@Error_msg", "");
        //        //cmd.Parameters["@Error_msg"].Direction = ParameterDirection.Output;
        //        //cmd.Parameters["@Error_msg"].Size = 256;
        //        int i = cmd.ExecuteNonQuery();
        //        //str = cmd.Parameters["@Error_msg"].Value.ToString();
        //        if (i > 0)
        //        {
        //            msg = "True";
        //            decimal price = Convert.ToDecimal(TotalAmount) / 100;
        //            //decimal price = Convert.ToDecimal(PaidAmount);
        //            string msg1 = "Your order Id " + OrderId + " amounting to Rs." + price.ToString() + " has been received.Thank You for shopping with us";


        //            SendSMS sms = new SendSMS();
        //            sms.sendsms1(contact, msg1);
        //        }
        //        else
        //        {
        //            msg = "False";
        //        }

        //        con.Close();
        //    }
        //    catch (Exception ex)
        //    {
        //        msg = "False";
        //    }
        //    return msg;


        //}
        public string GetPassword(string userid)
        {
            string msg = "";
            Connection();
            try
            {
                con.Open();
                string qry = "SELECT tbm.Password FROM tblLogin_Master tbm join Customers cm ON cm.CustomerID=tbm.MCode WHERE 1=1 AND (cm.Email='" + userid + "' OR cm.Phone1='" + userid + "') ";
                SqlCommand com = new SqlCommand(qry, con);
                SqlDataAdapter sda = new SqlDataAdapter(com);
                DataTable dt = new DataTable();
                sda.Fill(dt);

            }
            catch (Exception ae)
            {
                msg = "";
            }
            finally
            {
                con.Close();
            }
            return msg;
        }

        public DataTable GetMainCateGory()
        {
            Connection();
            DataTable dt = new DataTable();
            try
            {
                con.Open();
                string qry = "EXECUTE proc_GetMainDetail";
                SqlCommand cmd = new SqlCommand(qry, con);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                sda.Fill(dt);
            }
            catch (Exception ex)
            {
                dt = null;
            }
            finally
            {
                con.Close();
            }
            return dt;
        }

        public DataTable GetCateGory()
        {
            Connection();
            DataTable dt = new DataTable();
            try
            {
                con.Open();
                string qry = "EXEC proc_GetCategory";
                SqlCommand cmd = new SqlCommand(qry, con);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                sda.Fill(dt);
            }
            catch (Exception ex)
            {
                dt = null;
            }
            finally
            {
                con.Close();
            }
            return dt;
        }

        public DataTable GetSubCateGory(int cat)
        {
            Connection();
            DataTable dt = new DataTable();
            try
            {
                con.Open();
                string qry = "EXEC proc_GetSubCategory " + cat + "";
                SqlCommand cmd = new SqlCommand(qry, con);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                sda.Fill(dt);
            }
            catch (Exception ex)
            {
                dt = null;
            }
            finally
            {
                con.Close();
            }
            return dt;
        }
        public DataTable getBanner(string LocationCode)
        {
            Connection();
            DataTable dt = new DataTable();
            try
            {
                con.Open();
                string qry = "EXEC proc_GetDashBoardProducts '4', '" + LocationCode.Trim() + "'";
                SqlCommand cmd = new SqlCommand(qry, con);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                sda.Fill(dt);
            }
            catch (Exception ex)
            {
                dt = null;
            }
            finally
            {
                con.Close();
            }
            return dt;
        }


        public DataTable getBannerbottome()
        {
            Connection();
            DataTable dt = new DataTable();
            try
            //7
            {
                con.Open();
                string qry = "EXEC proc_GetDashBoardProducts '61', 'null'";
                SqlCommand cmd = new SqlCommand(qry, con);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                sda.Fill(dt);
            }
            catch (Exception ex)
            {
                dt = null;
            }
            finally
            {
                con.Close();
            }
            return dt;
        }


        public DataSet GetProductdetail(string Productid)
        {
            Connection();
            DataSet dt = new DataSet();
            try
            {
                con.Open();
                string qry = "EXEC proc_getproduct '" + Productid + "'";
                SqlCommand cmd = new SqlCommand(qry, con);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                sda.Fill(dt);
            }
            catch (Exception ex)
            {
                dt = null;
            }
            finally
            {
                con.Close();
            }
            return dt;
        }
        public DataTable GetProduct(string SubCatid, string ProductName)
        {
            Connection();
            DataTable dt = new DataTable();
            try
            {
                con.Open();
                if (ProductName == "")
                {
                    ProductName = null;
                }
                if (SubCatid == "")
                {
                    SubCatid = null;
                }
                string qry = "EXEC proc_getproductList '" + SubCatid + "','" + ProductName + "'";
                SqlCommand cmd = new SqlCommand(qry, con);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                sda.Fill(dt);
            }
            catch (Exception ex)
            {
                dt = null;
            }
            finally
            {
                con.Close();
            }
            return dt;
        }
        public DataTable GetSimilarProduct(string subCateid, string Productid)
        {
            Connection();
            DataTable dt = new DataTable();
            try
            {
                con.Open();
                string qry = "EXEC proc_getSimilarProduct '" + subCateid + "','" + Productid + "'";
                SqlCommand cmd = new SqlCommand(qry, con);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                sda.Fill(dt);
            }
            catch (Exception ex)
            {
                dt = null;
            }
            finally
            {
                con.Close();
            }
            return dt;
        }
        //public string AddToCart(string CustomerID, string ProductID, int Quantity,string Color,string size,string colorImage)
        //{
        //    string msg = "";
        //    Connection();
        //    con.Open();
        //    SqlTransaction trans = con.BeginTransaction();
        //    try
        //    {
        //        SqlCommand cmd = new SqlCommand();
        //        cmd = new SqlCommand();
        //        cmd.Connection = con;
        //        cmd.CommandType = CommandType.StoredProcedure;
        //        cmd.CommandText = "Proc_AddCart";

        //        cmd.Parameters.AddWithValue("@CustomerID", CustomerID);
        //        cmd.Parameters.AddWithValue("@ProductID", ProductID);
        //        cmd.Parameters.AddWithValue("@Quantity", Quantity);
        //        cmd.Parameters.AddWithValue("@Color", Color);
        //        cmd.Parameters.AddWithValue("@size", size);
        //        cmd.Parameters.AddWithValue("@colorimage", colorImage);

        //        cmd.Parameters.AddWithValue("@MSG", "");
        //        cmd.Parameters["@MSG"].Direction = ParameterDirection.Output;
        //        cmd.Parameters["@MSG"].Size = 256;

        //        cmd.Transaction = trans;
        //        cmd.ExecuteNonQuery();
        //        str = cmd.Parameters["@MSG"].Value.ToString();

        //        trans.Commit();
        //        con.Close();
        //    }
        //    catch (Exception ex)
        //    {
        //        str = "Item not added to cart";
        //        trans.Rollback();
        //    }
        //    finally
        //    {
        //        con.Close();
        //    }
        //    return str;
        //}




        public DataTable AddToCart(string CustomerID, string ProductID, string Quantity, string colorname, string sizename, string AttrId = null, string varId = null)
        {

            Connection();
            con.Open();
            DataTable dt = new DataTable();

            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "proc_InsertUpdateCartDetailsApp";
                cmd.Parameters.AddWithValue("@Action", "1");
                cmd.Parameters.AddWithValue("@CustomerId", CustomerID);
                cmd.Parameters.AddWithValue("@IpAddress", null);
                cmd.Parameters.AddWithValue("@ProductId", ProductID);
                cmd.Parameters.AddWithValue("@Quantity", Quantity);
                cmd.Parameters.AddWithValue("@varId", null);
                cmd.Parameters.AddWithValue("@SizeId", sizename);
                cmd.Parameters.AddWithValue("@ColorId", colorname);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                sda.Fill(dt);

            }
            catch (Exception ex)
            {
                dt = null;

            }
            finally
            {
                con.Close();
            }
            return dt;
        }



        public DataTable getCartlist(string CustomerID)
        {

            Connection();
            con.Open();
            DataTable dt = new DataTable();

            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "proc_getCartDetails";
                cmd.Parameters.AddWithValue("@Action", "1");
                cmd.Parameters.AddWithValue("@CustomerCode", CustomerID);

                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                sda.Fill(dt);

            }
            catch (Exception ex)
            {
                dt = null;

            }
            finally
            {
                con.Close();
            }
            return dt;
        }

        public DataTable GetComboOffer(string CustomerID)
        {

            Connection();
            con.Open();
            DataTable dt = new DataTable();

            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "Proc_GetComboOffer";
                cmd.Parameters.AddWithValue("@Action", "1");
                cmd.Parameters.AddWithValue("@CustomerID", CustomerID);

                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                sda.Fill(dt);

            }
            catch (Exception ex)
            {
                dt = null;

            }
            finally
            {
                con.Close();
            }
            return dt;
        }







        //public DataTable getCartlist(string CustomerId, string Action, string AreaCode)
        //{
        //    Connection();
        //    DataTable dt = new DataTable();
        //    try
        //    {
        //        con.Open();
        //        string qry = "EXEC [proc_getCartDetailsApi] '" + Action + "','" + CustomerId + "','" + AreaCode + "'";
        //        SqlCommand cmd = new SqlCommand(qry, con);
        //        SqlDataAdapter sda = new SqlDataAdapter(cmd);
        //        sda.Fill(dt);
        //    }
        //    catch (Exception ex)
        //    {
        //        dt = null;
        //    }
        //    finally
        //    {
        //        con.Close();
        //    }
        //    return dt;
        //}



        //public string UpdateCart(string CustomerID, string ProductID, int Quantity)
        //{
        //    string msg = "";
        //    Connection();
        //    con.Open();
        //    SqlTransaction trans = con.BeginTransaction();
        //    try
        //    {
        //        SqlCommand cmd = new SqlCommand();
        //        cmd = new SqlCommand();
        //        cmd.Connection = con;
        //        cmd.CommandType = CommandType.StoredProcedure;
        //        cmd.CommandText = "Proc_UpdateCart";

        //        cmd.Parameters.AddWithValue("@CustomerID", CustomerID);
        //        cmd.Parameters.AddWithValue("@ProductID", ProductID);
        //        cmd.Parameters.AddWithValue("@Quantity", Quantity);

        //        cmd.Parameters.AddWithValue("@MSG", "");
        //        cmd.Parameters["@MSG"].Direction = ParameterDirection.Output;
        //        cmd.Parameters["@MSG"].Size = 256;

        //        cmd.Transaction = trans;
        //        cmd.ExecuteNonQuery();
        //        str = cmd.Parameters["@MSG"].Value.ToString();

        //        trans.Commit();
        //        con.Close();
        //    }
        //    catch (Exception ex)
        //    {
        //        str = "Item not added to cart";
        //        trans.Rollback();
        //    }
        //    finally
        //    {
        //        con.Close();
        //    }
        //    return str;
        //}



        public DataTable UpdateCartUser(string CustomerID, string ProductId, string Quantity, string VarId)
        {

            Connection();
            con.Open();
            DataTable dt = new DataTable();

            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "proc_InsertUpdateCartDetails";
                cmd.Parameters.AddWithValue("@Action", "1");
                cmd.Parameters.AddWithValue("@CustomerId", CustomerID);
                cmd.Parameters.AddWithValue("@ProductId", ProductId);
                cmd.Parameters.AddWithValue("@Quantity", Quantity);
                cmd.Parameters.AddWithValue("@varId", VarId);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                sda.Fill(dt);

            }
            catch (Exception ex)
            {
                dt = null;

            }
            finally
            {
                con.Close();
            }
            return dt;
        }


        public DataTable DeleteCart(string CustomerID, string cartlistid)
        {

            Connection();
            con.Open();
            DataTable dt = new DataTable();

            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "proc_RemoveFromCart";
                cmd.Parameters.AddWithValue("@Action", "1");
                cmd.Parameters.AddWithValue("@UserCode", CustomerID);
                cmd.Parameters.AddWithValue("@ProductID", cartlistid);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                sda.Fill(dt);

            }
            catch (Exception ex)
            {
                dt = null;

            }
            finally
            {
                con.Close();
            }
            return dt;
        }


        //public string DeleteCart(string CustomerID, string ProductID, int Quantity, string Rateid)
        //{
        //    string msg = "";
        //    Connection();
        //    con.Open();
        //    SqlTransaction trans = con.BeginTransaction();
        //    try
        //    {
        //        SqlCommand cmd = new SqlCommand();
        //        cmd = new SqlCommand();
        //        cmd.Connection = con;
        //        cmd.CommandType = CommandType.StoredProcedure;
        //        cmd.CommandText = "Proc_DeleteCart";

        //        cmd.Parameters.AddWithValue("@CustomerID", CustomerID);
        //        cmd.Parameters.AddWithValue("@ProductID", ProductID);
        //        cmd.Parameters.AddWithValue("@Quantity", Quantity);
        //        cmd.Parameters.AddWithValue("@rateId", Rateid);

        //        cmd.Parameters.AddWithValue("@MSG", "");
        //        cmd.Parameters["@MSG"].Direction = ParameterDirection.Output;
        //        cmd.Parameters["@MSG"].Size = 256;

        //        cmd.Transaction = trans;
        //        cmd.ExecuteNonQuery();
        //        str = cmd.Parameters["@MSG"].Value.ToString();

        //        trans.Commit();
        //        con.Close();
        //    }
        //    catch (Exception ex)
        //    {
        //        str = "False";
        //        trans.Rollback();
        //    }
        //    finally
        //    {
        //        con.Close();
        //    }
        //    return str;
        //}

        public string AddAddress(string CustomerId, string Name, string MobileNo, string PinCode, string Locality, string Address, string StateId, string CityId, string LanMark)
        {
            string msg = "";
            Connection();
            con.Open();
            SqlTransaction trans = con.BeginTransaction();
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "Proc_addAddress";

                cmd.Parameters.AddWithValue("@CustomerID", CustomerId);
                cmd.Parameters.AddWithValue("@Name", Name);
                cmd.Parameters.AddWithValue("@MobileNo", MobileNo);
                cmd.Parameters.AddWithValue("@PinCode", PinCode);
                cmd.Parameters.AddWithValue("@Locality", Locality);
                cmd.Parameters.AddWithValue("@Address", Address);
                cmd.Parameters.AddWithValue("@StateId", StateId);
                cmd.Parameters.AddWithValue("@CityId", CityId);
                cmd.Parameters.AddWithValue("@LanMark", LanMark);

                cmd.Parameters.AddWithValue("@MSG", "");
                cmd.Parameters["@MSG"].Direction = ParameterDirection.Output;
                cmd.Parameters["@MSG"].Size = 256;

                cmd.Transaction = trans;
                cmd.ExecuteNonQuery();
                str = cmd.Parameters["@MSG"].Value.ToString();

                trans.Commit();
                con.Close();
            }
            catch (Exception ex)
            {
                str = "False";
                trans.Rollback();
            }
            finally
            {
                con.Close();
            }
            return str;
        }


        public DataTable InsertDeliveryAddress(string CustomerId, string Action, string SSName, string ContactNo, string PinCode, string locality, string Address, string CityName, string StCode, string landmark, string altmobileno, string OfferType, string latitude, string longtitude, string address2, string ProcName)
        {
            DataTable dtt = new DataTable();
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@customercode",CustomerId),
                new SqlParameter("@Action",Action),
                new SqlParameter("@name",SSName),
                new SqlParameter("@mobileno",ContactNo),
                new SqlParameter("@pincode",PinCode),
                new SqlParameter("@locality",locality),
                new SqlParameter("@address",Address),
                new SqlParameter("@cityname",CityName),
                new SqlParameter("@stateid",StCode),
                new SqlParameter("@landmark",landmark),
                new SqlParameter("@altmobileno",altmobileno),
                new SqlParameter("@addresstype",OfferType),
                new SqlParameter("@latitude",latitude),
                new SqlParameter("@longitude",longtitude),
                new SqlParameter("@adress2",address2),
            };
            dtt = db.ExecProcDataTable(ProcName, param);
            return dtt;
        }



        public string GetOtp(string mobile, out string msg1)
        {
            string msg = "";
            Connection();
            con.Open();
            DataTable dt = new DataTable();
            try
            {
                string qry = "EXECUTE dbo.getOTP '" + mobile + "' ";
                SqlCommand com = new SqlCommand(qry, con);
                SqlDataAdapter sda = new SqlDataAdapter(com);
                sda.Fill(dt);
                if (dt != null && dt.Rows.Count > 0)
                {
                    if (dt.Rows[0]["Status"].ToString() == "True")
                    {
                        msg1 = dt.Rows[0]["otp"].ToString();
                        msg = "True";
                        string message = "Dear Customer,Your OTP is " + msg1.ToString().Trim() + ". Do not share This OTP any one and can be used only once.";
                        //sendsms(message, mobile);
                        SendSMS sms = new SendSMS();
                        //sm.sendSMS(mobile, message);
                    }
                    else
                    {
                        msg = "False";
                        msg1 = "";
                    }
                }
                else
                {
                    msg = "False";
                    msg1 = "";
                }
            }
            catch
            {
                msg = "False";
                msg1 = "";
            }
            return msg;
        }

        public string GetOtpLogin(string mobile, out string msg1)
        {
            string msg = "";
            Connection();
            con.Open();
            DataTable dt = new DataTable();
            try
            {
                string qry = "EXECUTE dbo.getOTPLogin '" + mobile + "' ";
                SqlCommand com = new SqlCommand(qry, con);
                SqlDataAdapter sda = new SqlDataAdapter(com);
                sda.Fill(dt);
                if (dt != null && dt.Rows.Count > 0)
                {
                    if (dt.Rows[0]["Status"].ToString() == "True")
                    {
                        msg1 = dt.Rows[0]["otp"].ToString();
                        msg = "True";
                        //string message = "Dear Customer,Your OTP is " + msg1.ToString().Trim() + ". Do not share This OTP any one and can be used only once.";
                        //sendsms(message, mobile);
                        SendSMS sms = new SendSMS();
                        //sm.sendSMS(mobile, message);
                    }
                    else
                    {
                        msg = "False";
                        msg1 = "";
                    }
                }
                else
                {
                    msg = "False";
                    msg1 = "";
                }
            }
            catch
            {
                msg = "False";
                msg1 = "";
            }
            return msg;
        }


        public DataTable InsertUpdatePincode(string PinCode, string ProcName)
        {
            DataTable dt = new DataTable();
            SqlParameter[] parm = new SqlParameter[] {
                    new SqlParameter("@Action","3"),
                    new SqlParameter("@PinCode",PinCode)

            };
            dt = db.ExecProcDataTable(ProcName, parm);
            return dt;
        }



        public string GetPincode(string zipcode, out string msg1)
        {
            string msg = "";
            Connection();
            con.Open();
            DataTable dt = new DataTable();
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "Proc_InsertUpdzipcode";
                cmd.Parameters.AddWithValue("@Action", "4");
                cmd.Parameters.AddWithValue("@code", zipcode);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                sda.Fill(dt);
                if (dt != null && dt.Rows.Count > 0)
                {
                    if (dt.Rows[0]["Id"].ToString() == "1")
                    {
                        msg1 = dt.Rows[0]["Id"].ToString();
                        msg = "True";

                    }
                    else
                    {
                        msg = "False";
                        msg1 = "";
                    }
                }
                else
                {
                    msg = "False";
                    msg1 = "";
                }
            }
            catch
            {
                msg = "False";
                msg1 = "";
            }
            return msg;
        }



        public DataTable Proc_OrderTracking(string OrderId)
        {
            DataTable dt = new DataTable();
            SqlParameter[] parm = new SqlParameter[] {
                    new SqlParameter("@Action","1"),
                    new SqlParameter("@OrderId",OrderId)

            };
            dt = db.ExecProcDataTable("Proc_OrderTracking", parm);
            return dt;
        }


        public DataTable GetDeliveryTimeSlot()
        {

            Connection();
            con.Open();
            DataTable dt = new DataTable();
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "Proc_getDeliveryType";
                cmd.Parameters.AddWithValue("@Action", "2");
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                sda.Fill(dt);
            }
            catch
            {

            }
            return dt;

        }


        public void sendsms(string Msg, string MobileNo)
        {
            string strAPI = "";
            strAPI = "http://mysmsshop.in/http-api.php?username=&password=Admin123$&senderid=&route=1&number=" + MobileNo + "&message=" + Msg + "";

            HttpWebRequest myReq = (HttpWebRequest)WebRequest.Create(strAPI);
            HttpWebResponse myResp = (HttpWebResponse)myReq.GetResponse();
            System.IO.StreamReader respStreamReader = new System.IO.StreamReader(myResp.GetResponseStream());
            string responseString = respStreamReader.ReadToEnd();
            respStreamReader.Close();
            myResp.Close();
        }

        public DataTable showAddress(string CustomerId)
        {
            Connection();
            con.Open();
            DataTable dt = new DataTable();

            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "proc_InsertDeliveryAddress";
                cmd.Parameters.AddWithValue("@Action", "2");
                cmd.Parameters.AddWithValue("@customercode", CustomerId);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                sda.Fill(dt);


            }
            catch (Exception ex)
            {
                dt = null;
            }
            finally
            {
                con.Close();
            }
            return dt;
        }



        public DataTable Membertype()
        {
            string msg = "";
            Connection();
            con.Open();
            DataTable dt = new DataTable();

            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "Proc_MemberShipMaster";
                cmd.Parameters.AddWithValue("@Action", "1");

                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                sda.Fill(dt);


            }
            catch (Exception ex)
            {
                dt = null;
            }
            finally
            {
                con.Close();
            }
            return dt;
        }

        public DataTable finalCheckOut(string customerid, string totalcharge, string type, string paymode, string OrderDate, string TimeSlot, string LocationCode)
        {
            Connection();
            con.Open();
            DataTable dt = new DataTable();
            DateTime? Orderdate = null;
            if (!string.IsNullOrEmpty(OrderDate))
            {
                Orderdate = Convert.ToDateTime(OrderDate);
            }

            try
            {



                SqlCommand cmd = new SqlCommand();
                cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "proc_CheckOutApi";
                cmd.Parameters.AddWithValue("@CustomerID", customerid);
                cmd.Parameters.AddWithValue("@totalDelCharge", totalcharge);
                cmd.Parameters.AddWithValue("@deltype", type);
                cmd.Parameters.AddWithValue("@paymode", paymode);
                cmd.Parameters.AddWithValue("@Orderdate", Orderdate);
                cmd.Parameters.AddWithValue("@TimeSlot", TimeSlot);
                cmd.Parameters.AddWithValue("@FranchiseCode", LocationCode);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                sda.Fill(dt);
                if (dt != null && dt.Rows.Count > 0 && dt.Rows[0]["Status"].ToString() != "False")
                {
                    string Mobile = "";
                    DateTime thisDay = Convert.ToDateTime(Orderdate);

                    string msg = "Your order " + dt.Rows[0]["OrderId"].ToString() + " is placed and Order amount= Rs." + dt.Rows[0]["TotalPrice"].ToString() + "  It will be delivered on " + thisDay.ToString("D") + " between " + TimeSlot.ToString() + " .\n Happy Shopping! \n Fsthome";
                    SendSMS sms = new SendSMS();
                    if (paymode != "CARD")
                    {

                        sm.sendSMS(dt.Rows[0]["Mobile"].ToString(), msg);
                        string Msg1 = "A Order has been Arrived .The Order Id is  " + dt.Rows[0]["OrderId"].ToString() + " \n Order Amount=" + dt.Rows[0]["TotalPrice"].ToString() + " \n paymode=" + paymode + ". It will be delivered on " + thisDay.ToString("D") + " between " + TimeSlot.ToString();
                        sm.sendSMS(ConfigurationManager.AppSettings["AdminMobileNo"].ToString(), Msg1);


                        //for (int i = 0; i < dt.Rows.Count; i++)
                        //{
                        //    string msg2 = "A Order has been Arrived .The Order Id is = " + dt.Rows[0]["OrderId"].ToString() + "";
                        //    sm.sendSMS(dt.Rows[0]["ContactNo"].ToString(), msg2);

                        //}
                    }

                }
            }
            catch (Exception ex)
            {
                dt = null;
            }
            finally
            {
                con.Close();
            }
            return dt;
        }

        public string UpdateAdd(string id, string customerId)
        {
            string i = "";
            Connection();
            con.Open();
            try
            {
                SqlCommand com = new SqlCommand("UPDATE tbl_DeliveryAddressDetails SET IsDefaultAccount =1 where CustomerCode='" + customerId + "' AND SrNo='" + id + "' ; UPDATE tbl_DeliveryAddressDetails SET IsDefaultAccount =0 where CustomerCode='" + customerId + "' AND SrNo!='" + id + "'", con);
                com.ExecuteNonQuery();
                i = "True";
            }
            catch (Exception ae)
            {
                i = "False";
            }
            finally
            {
                con.Close();
            }
            return i;
        }

        public DataTable getOrderlist(string customerId, string fromdate, string todate, string ProcName)
        {
            DataTable dt = new DataTable();
            SqlParameter[] parm = new SqlParameter[] {
                new SqlParameter("@Action","5"),
                new SqlParameter("@CustomerId",customerId),
                new SqlParameter("@fromdate",fromdate),
                new SqlParameter("@todate",todate)
            };
            dt = db.ExecProcDataTable(ProcName, parm);
            return dt;
        }


        public DataTable getorderitemdetail(string customerId, string OrderId, string ProcName)
        {
            DataTable dt = new DataTable();
            SqlParameter[] parm = new SqlParameter[] {
                new SqlParameter("@Action","4"),
                new SqlParameter("@CustomerId",customerId),
                 new SqlParameter("@OrderId",OrderId)
            };
            dt = db.ExecProcDataTable(ProcName, parm);
            return dt;
        }



        public DataTable getWishlist(string customerId)
        {
            DataTable dt = new DataTable();
            Connection();

            try
            {
                con.Open();
                string qry = "EXEC getWishList '" + customerId + "'";
                SqlCommand cmd = new SqlCommand(qry, con);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                sda.Fill(dt);
            }
            catch (Exception ae)
            {
                dt = null;
            }
            finally
            {
                con.Close();
            }
            return dt;
        }
        public string AddWishList(string CustomerID, string ProductID)
        {
            string msg = "";
            Connection();
            con.Open();
            SqlTransaction trans = con.BeginTransaction();
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "Proc_AddWishList";

                cmd.Parameters.AddWithValue("@CustomerID", CustomerID);
                cmd.Parameters.AddWithValue("@ProductID", ProductID);
                cmd.Parameters.AddWithValue("@MSG", "");
                cmd.Parameters["@MSG"].Direction = ParameterDirection.Output;
                cmd.Parameters["@MSG"].Size = 256;

                cmd.Transaction = trans;
                cmd.ExecuteNonQuery();
                str = cmd.Parameters["@MSG"].Value.ToString();

                trans.Commit();
                con.Close();
            }
            catch (Exception ex)
            {
                str = "Item not added to WishList";
                trans.Rollback();
            }
            finally
            {
                con.Close();
            }
            return str;
        }
        public string DeleteWishList(string CustomerID, string ProductID)
        {
            string msg = "";
            Connection();
            con.Open();
            SqlTransaction trans = con.BeginTransaction();
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "Proc_DeleteWishlist";

                cmd.Parameters.AddWithValue("@CustomerID", CustomerID);
                cmd.Parameters.AddWithValue("@ProductID", ProductID);
                cmd.Parameters.AddWithValue("@MSG", "");
                cmd.Parameters["@MSG"].Direction = ParameterDirection.Output;
                cmd.Parameters["@MSG"].Size = 256;

                cmd.Transaction = trans;
                cmd.ExecuteNonQuery();
                str = cmd.Parameters["@MSG"].Value.ToString();

                trans.Commit();
                con.Close();
            }
            catch (Exception ex)
            {
                str = "False";
                trans.Rollback();
            }
            finally
            {
                con.Close();
            }
            return str;
        }

        public DataTable GetSubCateGoryList()
        {
            Connection();
            DataTable dt = new DataTable();
            try
            {
                con.Open();
                string qry = "EXEC proc_GetSubCategory ";
                SqlCommand cmd = new SqlCommand(qry, con);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                sda.Fill(dt);
            }
            catch (Exception ex)
            {
                dt = null;
            }
            finally
            {
                con.Close();
            }
            return dt;
        }
        public string getPassword(string mobile, out string msg1)
        {
            string userid = "";
            string msg = "";
            Connection();
            con.Open();
            DataTable dt = new DataTable();
            try
            {
                string qry = "EXECUTE dbo.getPassword '" + mobile + "' ";
                SqlCommand com = new SqlCommand(qry, con);
                SqlDataAdapter sda = new SqlDataAdapter(com);
                sda.Fill(dt);
                if (dt != null && dt.Rows.Count > 0)
                {

                    userid = dt.Rows[0]["UserName"].ToString();
                    msg1 = dt.Rows[0]["Password"].ToString();
                    if (msg1 != "")
                    {
                        //string message2 = "Your Member ID / Username is {#\" + userid + \"#}  and password: { #\" + msg1 + \"#}  Thankyou,";
                        string message2 = "Your Member ID/Username is " + userid + " and password: " + msg1 + " Thankyou,Claco [MSUREW]";
                        //string message = "Your Password is " + msg1 + ". ";                      
                        SendSMS sms = new SendSMS();
                        sm.sendSMS(mobile, message2);
                        msg = "True";
                    }

                }
                else
                {
                    msg = "False";
                    msg1 = "";
                }
            }
            catch
            {
                msg = "False";
                msg1 = "";
            }
            return msg;
        }

        public string ChangePassword(string mobile, string oldPassword, string Password)
        {
            string msg = "";
            Connection();
            con.Open();
            DataTable dt = new DataTable();
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "dbo.ChangePassword";

                cmd.Parameters.AddWithValue("@UserId", mobile);
                cmd.Parameters.AddWithValue("@OldPassword", oldPassword);
                cmd.Parameters.AddWithValue("@NewPassword", Password);
                cmd.Parameters.AddWithValue("@msg", "");
                cmd.Parameters["@msg"].Direction = ParameterDirection.Output;
                cmd.Parameters["@msg"].Size = 256;
                cmd.ExecuteNonQuery();
                str = cmd.Parameters["@msg"].Value.ToString();
                if (str == "True")
                {
                    msg = "True";
                    string message = "Your New Password is " + Password + ". ";
                    SendSMS sms = new SendSMS();
                    sm.sendSMS(mobile, message);
                }
                else
                {
                    msg = "False";
                }

                con.Close();
            }
            catch
            {
                msg = "False";
            }
            return msg;
        }
        public DataTable updateProfile(string customerId, string Name, string Email, string PhoneNumber, string Gender)
        {
            Connection();
            DataTable dt = new DataTable();
            try
            {
                con.Open();
                string qry = "EXEC Proc_UpdateCustomerDetail '" + customerId + "','" + Name + "','" + Email + "','" + PhoneNumber + "','" + Gender + "' ";
                SqlCommand cmd = new SqlCommand(qry, con);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                sda.Fill(dt);
            }
            catch (Exception ex)
            {
                dt = null;
            }
            finally
            {
                con.Close();
            }
            return dt;
        }

        public DataTable getVendorDashboard()
        {
            Connection();
            DataTable dt = new DataTable();
            try
            {
                con.Open();
                string qry = "EXEC getVendorDashBoard ";
                SqlCommand cmd = new SqlCommand(qry, con);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                sda.Fill(dt);
            }
            catch (Exception ex)
            {
                dt = null;
            }
            finally
            {
                con.Close();
            }
            return dt;
        }

        public DataTable getVendorProductDashboard(string vendorID)
        {
            Connection();
            DataTable dt = new DataTable();
            try
            {
                con.Open();
                string qry = "EXEC getVendorWiseProduct '" + vendorID + "'";
                SqlCommand cmd = new SqlCommand(qry, con);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                sda.Fill(dt);
            }
            catch (Exception ex)
            {
                dt = null;
            }
            finally
            {
                con.Close();
            }
            return dt;
        }

        public DataTable GetDeals()
        {
            Connection();
            DataTable dt = new DataTable();
            try
            {
                con.Open();
                string qry = "EXEC DealOfTheDay";
                SqlCommand cmd = new SqlCommand(qry, con);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                sda.Fill(dt);
            }
            catch (Exception ex)
            {
                dt = null;
            }
            finally
            {
                con.Close();
            }
            return dt;
        }
        public string InsertRVAndCancel(string orderid, string productcode, string revstatus, string CustomerId, string customer, string description, string flag)
        {
            string s = "";
            Connection();
            con.Open();
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "Asp_ProcData";
                cmd.Parameters.AddWithValue("@OrderId", orderid);
                cmd.Parameters.AddWithValue("@ProductCode", productcode);
                cmd.Parameters.AddWithValue("@ReviewStatus", revstatus);
                cmd.Parameters.AddWithValue("@CustomerId", CustomerId);
                cmd.Parameters.AddWithValue("@CustomertName", customer);
                cmd.Parameters.AddWithValue("@ReviewDiscription", description);
                cmd.Parameters.AddWithValue("@flag", flag);
                cmd.Parameters.AddWithValue("@msg", "");
                cmd.Parameters["@msg"].Direction = ParameterDirection.Output;
                cmd.Parameters["@msg"].Size = 256;
                cmd.ExecuteNonQuery();
                s = cmd.Parameters["@msg"].Value.ToString();
                SendSMS sms = new SendSMS();
                string Msg1 = "A Order has been Canceled .The Order Id is = " + orderid + "";
                sm.sendSMS(ConfigurationManager.AppSettings["AdminMobileNo"].ToString(), Msg1);
            }
            catch (Exception ex)
            {
                throw ex;
                s = "False";
            }
            finally
            {
                con.Close();
            }
            return s;
        }



        public string ReturnOrder(string CustomerId, string OrderId, string OrderItemId, string Comment, string ReasonId)
        {
            string s = "";
            Connection();
            con.Open();
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "ReturnOrder";
                cmd.Parameters.AddWithValue("@OrderId", OrderId);

                cmd.Parameters.AddWithValue("@CancelRemark", Comment);
                cmd.Parameters.AddWithValue("@CustomerCode", CustomerId);
                cmd.Parameters.AddWithValue("@ProductId", OrderItemId);
                cmd.Parameters.AddWithValue("@OrderReturnResonId", ReasonId);
                cmd.Parameters.AddWithValue("@Action", "2");
                int i = cmd.ExecuteNonQuery();
                if (i > 0)
                {
                    s = "True";
                    SendSMS sms = new SendSMS();
                    string Msg1 = "A Return request has been arrived for this Order " + OrderId + "";
                    sm.sendSMS(ConfigurationManager.AppSettings["AdminMobileNo"].ToString(), Msg1);
                }
            }
            catch
            {
                s = "False";
            }
            finally
            {
                con.Close();
            }
            return s;
        }

        public DataTable GetReviewList(string productid)
        {
            Connection();
            DataTable dt = new DataTable();
            try
            {
                con.Open();
                string qry = "EXEC Asp_ProcData '','" + productid + "','','','','','RVlist',''";
                SqlCommand cmd = new SqlCommand(qry, con);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                sda.Fill(dt);
            }
            catch (Exception ex)
            {
                dt = null;
            }
            finally
            {
                con.Close();
            }
            return dt;
        }


        public DataTable GetWalletPoint(string CustomerId)
        {
            Connection();
            DataTable dt = new DataTable();
            try
            {
                con.Open();
                string qry = "EXEC GetWalletPoint '" + CustomerId + "'";
                SqlCommand cmd = new SqlCommand(qry, con);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                sda.Fill(dt);
            }
            catch (Exception ex)
            {
                dt = null;
            }
            finally
            {
                con.Close();
            }
            return dt;
        }


        public DataTable GetCoupanDetails(string CustomerCode)
        {
            Connection();
            DataTable dt = new DataTable();
            try
            {
                con.Open();
                string qry = "EXEC Asp_ApiCopan '" + CustomerCode + "','','CHeckValid'";
                SqlCommand cmd = new SqlCommand(qry, con);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                sda.Fill(dt);
            }
            catch (Exception ex)
            {
                dt = null;
            }
            finally
            {
                con.Close();
            }
            return dt;
        }

        public DataTable CoupanList(string CustomerCode)
        {
            Connection();
            DataTable dt = new DataTable();
            try
            {
                con.Open();
                string qry = "EXEC CoupanList '" + CustomerCode + "','','CHeckValid'";
                SqlCommand cmd = new SqlCommand(qry, con);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                sda.Fill(dt);
            }
            catch (Exception ex)
            {
                dt = null;
            }
            finally
            {
                con.Close();
            }
            return dt;
        }



        public DataTable GetVersion()
        {
            Connection();
            DataTable dt = new DataTable();
            try
            {
                con.Open();
                string qry = "EXEC VersionMaster 'Select'";
                SqlCommand cmd = new SqlCommand(qry, con);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                sda.Fill(dt);
            }
            catch (Exception ex)
            {
                dt = null;
            }
            finally
            {
                con.Close();
            }
            return dt;
        }

        public DataTable ApplyCupan(string CustomerCode, string CoupanCode, decimal OrderAmount)
        {
            Connection();
            DataTable dt = new DataTable();
            try
            {
                con.Open();
                string qry = "EXEC Asp_ApiCopan '" + CustomerCode + "','" + CoupanCode + "','CHeckAplly','" + OrderAmount + "'";
                SqlCommand cmd = new SqlCommand(qry, con);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                sda.Fill(dt);
            }
            catch (Exception ex)
            {
                dt = null;
            }
            finally
            {
                con.Close();
            }
            return dt;
        }
        public DataTable GetDeliveryType()
        {
            Connection();
            DataTable dt = new DataTable();
            try
            {
                con.Open();
                string qry = "EXEC Asp_DelevryType";
                SqlCommand cmd = new SqlCommand(qry, con);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                sda.Fill(dt);
            }
            catch (Exception ex)
            {
                dt = null;
            }
            finally
            {
                con.Close();
            }
            return dt;
        }

        public DataTable GetReturnReason()
        {
            Connection();
            DataTable dt = new DataTable();
            try
            {
                con.Open();
                string qry = "EXEC GetReturnReason '1'";
                SqlCommand cmd = new SqlCommand(qry, con);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                sda.Fill(dt);
            }
            catch (Exception ex)
            {
                dt = null;
            }
            finally
            {
                con.Close();
            }
            return dt;
        }


        public string payment(string OrderId, string TransactionId, string TransactionDate, string RecieptNo, string PaymentStatus, string TotalAmount, string PaidAmount, string DiscountAmount, string EntryDate, string DeliveryType, string CustomerCode, string PaymentDate, string Myhpayid, string CardNum, string CardType, string ProductCode, string contact, string paymentMode, string OrderDate, string TimeSlot)
        {
            string msg = "";
            Connection();
            con.Open();
            DataTable dt = new DataTable();

            DateTime? Orderdate = null;
            if (!string.IsNullOrEmpty(OrderDate))
            {
                Orderdate = Convert.ToDateTime(OrderDate);
            }

            try
            {

                decimal price = Convert.ToDecimal(TotalAmount) / 100;

                SqlCommand cmd = new SqlCommand();
                cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "dbo.Api_Payment";

                cmd.Parameters.AddWithValue("@OrderNo", OrderId);
                cmd.Parameters.AddWithValue("@PayStatus", PaymentStatus);
                cmd.Parameters.AddWithValue("@TransId", TransactionId);
                cmd.Parameters.AddWithValue("@TotalPrice", price);
                cmd.Parameters.AddWithValue("@PaidPrice", price.ToString() == "" ? null : price.ToString());
                cmd.Parameters.AddWithValue("@DiscountAmount", DiscountAmount == "" ? null : DiscountAmount);
                cmd.Parameters.AddWithValue("@PaymentMode", paymentMode);
                cmd.Parameters.AddWithValue("@DeliveryType", DeliveryType);
                cmd.Parameters.AddWithValue("@CustomerCode", CustomerCode);

                cmd.Parameters.AddWithValue("@Myhpayid", Myhpayid);
                cmd.Parameters.AddWithValue("@CardType", CardType);
                cmd.Parameters.AddWithValue("@CardNum", CardNum);
                cmd.Parameters.AddWithValue("@Orderdate", Orderdate);
                cmd.Parameters.AddWithValue("@TimeSlot", TimeSlot);


                //cmd.Parameters.AddWithValue("@Error_msg", "");
                //cmd.Parameters["@Error_msg"].Direction = ParameterDirection.Output;
                //cmd.Parameters["@Error_msg"].Size = 256;
                int i = cmd.ExecuteNonQuery();
                //str = cmd.Parameters["@Error_msg"].Value.ToString();
                if (i > 0)
                {
                    DateTime thisDay = Convert.ToDateTime(Orderdate);

                    msg = "True";

                    //decimal price = Convert.ToDecimal(PaidAmount);
                    string msg1 = "Your Payment is successfully done.|Your order Id " + OrderId + " amounting to Rs." + price.ToString() + ". It will be delivered on " + thisDay.ToString("D") + " between " + TimeSlot.ToString() + ".\nHappy Shopping!\nFsthome";

                    SendSMS sms = new SendSMS();
                    sm.sendSMS(contact, msg1);
                    sm.sendSMS(ConfigurationManager.AppSettings["AdminMobileNo"].ToString(), "One Order Id=" + OrderId + " amounting to Rs.=" + price.ToString() + " requested for shopping. payment mode=online" + ".\n It will be delivered on " + thisDay.ToString("D") + " between " + TimeSlot.ToString());
                }
                else
                {
                    msg = "False";
                }

                con.Close();
            }
            catch (Exception ex)
            {
                msg = "False";
            }
            return msg;
        }


        public DataTable LastNewitemlist()
        {

            Connection();
            DataTable dt = new DataTable();
            try
            {
                con.Open();
                string qry = "EXEC proc_GetDashBoardProducts '2',null";
                SqlCommand cmd = new SqlCommand(qry, con);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                sda.Fill(dt);
            }
            catch (Exception ex)
            {
                dt = null;
            }
            finally
            {
                con.Close();
            }
            return dt;
        }

        public DataTable GetSingleProductDetail(string SearchText, string ProcName)
        {
            DataTable dtt = new DataTable();
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@Action","6"),
                new SqlParameter("@SearchText",null),
                new SqlParameter("@CatId",SearchText),
            };
            dtt = db.ExecProcDataTable(ProcName, param);
            return dtt;
        }


        public DataTable GetProductSearch(string SearchText, string ProcName)
        {
            DataTable dtt = new DataTable();
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@Action","4"),
                new SqlParameter("@SearchText",SearchText),
            };
            dtt = db.ExecProcDataTable(ProcName, param);
            return dtt;
        }


        public DataTable LastWeekAddedProduct(string Days, string LocationCode)
        {
            Days = Days == "" ? "7" : Days;
            Connection();
            DataTable dt = new DataTable();
            try
            {
                con.Open();
                string qry = "EXEC GetProductDayWise '" + Days + "','" + LocationCode + "'";
                SqlCommand cmd = new SqlCommand(qry, con);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                sda.Fill(dt);
            }
            catch (Exception ex)
            {
                dt = null;
            }
            finally
            {
                con.Close();
            }
            return dt;
        }


        public DataTable GetProductNew(string Action, string subCateid, string AreaCode)
        {
            Connection();
            DataTable dt = new DataTable();
            try
            {
                con.Open();
                string qry = "EXEC [Proc_App_GetProductNew]  " + Action + ", '" + subCateid + "',NULL,'" + AreaCode + "'";
                SqlCommand cmd = new SqlCommand(qry, con);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                sda.Fill(dt);
            }
            catch (Exception ex)
            {
                dt = null;
            }
            finally
            {
                con.Close();
            }
            return dt;
        }
        public DataTable GetareawiseMultiRate(string Action, string Productid, string AreaCode)
        {
            Connection();
            DataTable dt = new DataTable();
            try
            {
                con.Open();
                string qry = "EXEC [Proc_App_GetProductNew]  " + Action + ",null," + Productid + ",'" + AreaCode + "'";
                SqlCommand cmd = new SqlCommand(qry, con);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                sda.Fill(dt);
            }
            catch (Exception ex)
            {
                dt = null;
            }
            finally
            {
                con.Close();
            }
            return dt;
        }

        public DataSet GetProductdetail(string Productid, string areacode)
        {
            Connection();
            DataSet ds = new DataSet();
            try
            {
                con.Open();
                string qry = "EXEC proc_getproductApi '" + Productid + "','" + areacode + "'";
                SqlCommand cmd = new SqlCommand(qry, con);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                sda.Fill(ds);
            }
            catch (Exception ex)
            {
                ds = null;
            }
            finally
            {
                con.Close();
            }
            return ds;
        }


        public DataTable InsertOnlineOrderDetails(PropertyClass Objp, string ProcName, DataTable dt, DataTable dtfreeitem)
        {
            DataTable dtt = new DataTable();
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@Action",Objp.Action),
                new SqlParameter("@company_code",Objp.CompanyCode),
                new SqlParameter("@customerid",Objp.CustomerId),
                new SqlParameter("@grossamount",Objp.GrossPayable),
                new SqlParameter("@deliverycharges",Objp.deliverycharges),
                new SqlParameter("@iscoupenapplied",Objp.iscoupenapplied),
                new SqlParameter("@coupenamount",Objp.CoupenAmount),
                new SqlParameter("@discountamount",Objp.DiscAmt),
                new SqlParameter("@deliveryaddressid",Objp.DeliveryTo),
                new SqlParameter("@paymentmode",Objp.PayMode),
                new SqlParameter("@paymentstatus",Objp.Status),
                new SqlParameter("@NetPayable",Objp.NetTotal),
                new SqlParameter("@StockiestId",Objp.StockistId),
                new SqlParameter("@type_OnlineOrderItem",dt),
                new SqlParameter("@type_OnlineOrderFreeItem",dtfreeitem),
            };
            dtt = db.ExecProcDataTable(ProcName, param);
            return dtt;
        }


        // InsertOnlineOrderDetailsReturn

        public DataTable InsertOnlineOrderDetailsReturn(PropertyClass Objp, string ProcName, DataTable dt)
        {
            DataTable dtt = new DataTable();
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@Action",Objp.Action),
                new SqlParameter("@customerid",Objp.CustomerId),
                new SqlParameter("@OrderId",Objp.OrderId),
                new SqlParameter("@Comment",Objp.Comment),
                new SqlParameter("@ReasonId",Objp.ReasonId),
                new SqlParameter("@type_OnlineOrderItem_Return",dt),
            };
            dtt = db.ExecProcDataTable(ProcName, param);
            return dtt;
        }


        //


        public DataTable InsertCancelRequest(PropertyClass Objp, string ProcName)
        {
            DateTime? delDate = null;
            if (!string.IsNullOrEmpty(Objp.mDate))
            {
                delDate = Convert.ToDateTime(Objp.mDate);
            }
            DataTable dt = new DataTable();
            SqlParameter[] parm = new SqlParameter[] {
                new SqlParameter("@Action",Objp.Action),
                new SqlParameter("@OrderId",Objp.OrderId),
                new SqlParameter("@CancelReason",Objp.Description),
                new SqlParameter("@updatedate",delDate),
                new SqlParameter("@status",Objp.Status),
                new SqlParameter("@deliveryby",Objp.DeliveryTo),
                new SqlParameter("@paymentCollectionBy",Objp.SupplierAccCode),
            };
            dt = db.ExecProcDataTable(ProcName, parm);
            return dt;
        }


        public DataTable InsertCustomerAccountNewMember(PropertyClass Objp, string ProcName)
        {
            DataTable dtt = new DataTable();
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@Action",Objp.Action),
                new SqlParameter("@name",Objp.SSName),
                new SqlParameter("@EmailId",Objp.EmailAddress),
                new SqlParameter("@Password",Objp.Password),
                new SqlParameter("@mobileno",Objp.ContactNo),
                new SqlParameter("@address",Objp.Address),
                new SqlParameter("@cityname",Objp.CityName),
                new SqlParameter("@stateid",Objp.StCode),
                new SqlParameter("@gstin",Objp.GstNo),
                new SqlParameter("@panno",Objp.PanNo),
                new SqlParameter("@CustId",Objp.UserName),
                new SqlParameter("@AccountType",Objp.AccountType),
                new SqlParameter("@memberShipId",Objp.memberShipId),
                new SqlParameter("@PaymentMode",Objp.PayMode),
                new SqlParameter("@EntryBy",Objp.CustomerId),
                new SqlParameter("@CardValidFrom",Objp.CardValidFrom),
                new SqlParameter("@CardValidTo",Objp.CardValidTo),
                new SqlParameter("@cardBarCode",Objp.CardBarCode),
                new SqlParameter("@MeMberPic",Objp.ProfilePicPath),
                new SqlParameter("@MemberShipType","MemberShip"),
                  new SqlParameter("@CardId",Objp.memberShipId)
            };
            dtt = db.ExecProcDataTable(ProcName, param);
            return dtt;
        }

        public DataTable GetDistrict()
        {
            DataTable dt = new DataTable();
            Connection();
            con.Open();
            SqlCommand cmd = new SqlCommand("EXEC GetDistrict", con);
            SqlDataAdapter adp = new SqlDataAdapter(cmd);
            adp.Fill(dt);
            con.Close();
            return dt;
        }


        //Get Item Code Data

        public DataTable GetItemCodeData(string itemcode)
        {
            DataTable dt = new DataTable();
            Connection();
            con.Open();
            //string qry = "EXEC proc_getproductApi '" + Productid + "','" + areacode + "'";
            SqlCommand cmd = new SqlCommand("EXEC GetItemCodeData '" + itemcode + "'", con);
            SqlDataAdapter adp = new SqlDataAdapter(cmd);
            adp.Fill(dt);
            con.Close();
            return dt;
        }




        public DataSet GetSingleProductDetailNew(string action, string Productid, string ProductCategory, string title, string CatId, string VarId, string ProcName)
        {
            DataSet dtt = new DataSet();
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@Action",action),
                new SqlParameter("@ProductCode",Productid),
                new SqlParameter("@CatId",CatId),
                new SqlParameter("@varId",VarId),
                new SqlParameter("@StockiestId",null)
            };
            dtt = db.ExecProcDataSet(ProcName, param);
            return dtt;
        }


        public DataTable getproductvariation(string ProductId, string ProcName)
        {
            DataTable dtt = new DataTable();
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@Action","1"),
                new SqlParameter("@ProductId",ProductId)
            };
            dtt = db.ExecProcDataTable(ProcName, param);
            return dtt;
        }

        public DataTable GetManufacturere()
        {
            Connection();
            DataTable dt = new DataTable();
            try
            {
                con.Open();
                string qry = "EXEC proc_GetManufacturer";
                SqlCommand cmd = new SqlCommand(qry, con);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                sda.Fill(dt);
            }
            catch (Exception ex)
            {
                dt = null;
            }
            finally
            {
                con.Close();
            }
            return dt;
        }
        public DataTable getOfferslist(string type, string ProcName)
        {
            DataTable dt = new DataTable();
            SqlParameter[] parm = new SqlParameter[] {
                new SqlParameter("@type",type)
            };
            dt = db.ExecProcDataTable(ProcName, parm);
            return dt;
        }
        public DataTable GetProductsdetails(string brand, string product, string FromRate, string ToRate, string Category, string ProcName)
        {
            DataTable dtt = new DataTable();
            SqlParameter[] param = new SqlParameter[]
            {

                new SqlParameter("@brand",brand),
                new SqlParameter("@product",product),
                new SqlParameter("@Category",Category),
                new SqlParameter("@FromRate ",FromRate ),
                new SqlParameter("@ToRate ",ToRate ),
               // new SqlParameter("@varId",Objp.VariationId),
              //  new SqlParameter("@StockiestId",Objp.StockistId)
            };
            dtt = db.ExecProcDataTable(ProcName, param);
            return dtt;
        }
        public DataTable getreferalllist(string CustomerId, string ProcName)
        {
            DataTable dt = new DataTable();
            SqlParameter[] parm = new SqlParameter[] {
                new SqlParameter("@Action","1"),
                new SqlParameter("@CustomerId",CustomerId)
            };
            dt = db.ExecProcDataTable(ProcName, parm);
            return dt;
        }
        public DataTable getPincodeBiseDeliveryCharges(int Action, string Pincode, string MRP, string ProcName)
        {
            DataTable dt = new DataTable();
            SqlParameter[] parm = new SqlParameter[] {
                new SqlParameter("@Action",Action),
                new SqlParameter("@pincode",Pincode),
                new SqlParameter("@Mrp",MRP)
            };
            dt = db.ExecProcDataTable(ProcName, parm);
            return dt;
        }
        public DataTable Proc_CancelItemOrder(string Action, string OrderId, string productCode, string ProcName)
        {
            DataTable dt = new DataTable();
            SqlParameter[] parm = new SqlParameter[] {
                new SqlParameter("@Action",Action),
                new SqlParameter("@orderId",OrderId),
                new SqlParameter("@productCode",productCode)
            };
            dt = db.ExecProcDataTable(ProcName, parm);
            return dt;
        }

        public DataTable Proc_ReturnItemOrder(string Action, string OrderId, string productCode, string Reason, string ProcName)
        {
            DataTable dt = new DataTable();
            SqlParameter[] parm = new SqlParameter[] {
                new SqlParameter("@Action",Action),
                new SqlParameter("@orderId",OrderId),
                new SqlParameter("@productCode",productCode),
                new SqlParameter("@Reason",Reason)
            };
            dt = db.ExecProcDataTable(ProcName, parm);
            return dt;
        }

        public DataTable Proc_Redeemgift(string CustomerId, string ProcName)
        {
            DataTable dt = new DataTable();
            SqlParameter[] parm = new SqlParameter[] {
                new SqlParameter("@CustomerId",CustomerId)
            };
            dt = db.ExecProcDataTable(ProcName, parm);
            return dt;
        }
        public DataTable usedpoint(PropertyClass Objp, string ProcName)
        {
            DataTable dtt = new DataTable();
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@Action",1),
                new SqlParameter("@CustomerId",Objp.CustomerId),
                new SqlParameter("@OrderId",Objp.OrderId),
                new SqlParameter("@UsedPoint",Objp.Usedpoint),
                new SqlParameter("@itemid",Objp.ItemID),
            };
            dtt = db.ExecProcDataTable(ProcName, param);
            return dtt;
        }

        public DataTable getBonus(string customerid)
        {
            SqlParameter[] p = new SqlParameter[]
            {
                new SqlParameter("@customerID",customerid)
            };
            return db.ExecProcDataTable("GetBonus", p);
        }



        // Recent Api For 

        public DataSet RecentApi(string procName)
        {
            DataSet dataSet = new DataSet();
            SqlParameter[] p = new SqlParameter[0];
            dataSet = db.ExecProcDataSet(procName, p);
            return dataSet;
        }

    }

    public class ShowMsg
    {
        public void Show(string message)
        {
            //string cleanMessage = message.Replace("'", "\'");
            //Page page = HttpContext.Current.CurrentHandler as Page;
            //string script = string.Format("alert('{0}');", cleanMessage);
            //ScriptManager.RegisterStartupScript(page, this.GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('" + message + "');</script>", false);
        }
    }





}