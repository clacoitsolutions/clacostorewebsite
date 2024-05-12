using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;

namespace OjasMart.Models
{
    public class SendSMS
    {

        public void sendsms1(string MobileNo, string Message)
        {
            try
            {
                string URL = "";
                //string URL = "http://mysmsshop.in/http-api.php?username=123&password=Admin123$&senderid=&route=1&number=";
                string result = apicall(URL + "&number=" + MobileNo + "&message=" + Message);
            }
            catch
            {

            }
        }


        public string sendSMS(string mobile, string sms)
        {
            //string user = "", password = "", senderid = "", route = "";

            //user = ConfigurationManager.AppSettings["user"].ToString();
            //password = ConfigurationManager.AppSettings["password"].ToString();
            //senderid = ConfigurationManager.AppSettings["senderid"].ToString();
            //route = ConfigurationManager.AppSettings["route"].ToString();

            try
            {
                string url = "http://mysmsshop.in/http-api.php?username=prad11004&password=Saniya8957.&senderid=MYWISY&route=1&number=" + mobile + "&message=" + sms + "";

                HttpWebRequest httpreq = (HttpWebRequest)WebRequest.Create(url);

                HttpWebResponse httpres = (HttpWebResponse)httpreq.GetResponse();

                StreamReader sr = new StreamReader(httpres.GetResponseStream());

                string results = sr.ReadToEnd();

                sr.Close();
                return "1";
            }
            catch (Exception ex)
            {
                return "0";

            }
        }

        public string sendSMS3(string mobile, string sms)
        {
            //string user = "", password = "", senderid = "", route = "";

            //user = ConfigurationManager.AppSettings["user"].ToString();
            //password = ConfigurationManager.AppSettings["password"].ToString();
            //senderid = ConfigurationManager.AppSettings["senderid"].ToString();
            //route = ConfigurationManager.AppSettings["route"].ToString();

            try
            {
                string url = "http://mysmsshop.in/http-api.php?username=prad11004&password=Saniya8957.&senderid=HOTZLA&route=1&number=" + mobile + "&message=" + sms + "";

                HttpWebRequest httpreq = (HttpWebRequest)WebRequest.Create(url);

                HttpWebResponse httpres = (HttpWebResponse)httpreq.GetResponse();

                StreamReader sr = new StreamReader(httpres.GetResponseStream());

                string results = sr.ReadToEnd();

                sr.Close();
                return "1";
            }
            catch (Exception ex)
            {
                return "0";

            }
        }

        public string sendSMS1(string mobile, string sms)
        {
            try
            {
                string strAPI = "";
                strAPI = "http://mysmsshop.in/http-api.php?username=prad11004&password=Saniya8957.&senderid=AIDBOK&route=1&number=" + mobile + "&message=" + sms + "";

                HttpWebRequest httpreq = (HttpWebRequest)WebRequest.Create(strAPI);

                HttpWebResponse httpres = (HttpWebResponse)httpreq.GetResponse();

                StreamReader sr = new StreamReader(httpres.GetResponseStream());

                string results = sr.ReadToEnd();

                sr.Close();
                return "1";
            }
            catch (Exception ex)
            {
                return "0";

            }
        }


        public string apicall(string url)
        {
            HttpWebRequest httpreq = (HttpWebRequest)WebRequest.Create(url);
            try
            {
                HttpWebResponse httpres = (HttpWebResponse)httpreq.GetResponse();
                StreamReader sr = new StreamReader(httpres.GetResponseStream());
                string results = sr.ReadToEnd();
                sr.Close();
                return results;
            }
            catch
            {
                return "0";
            }
        }

        public int GenerateOTP()
        {
            int _min = 1000;
            int _max = 9999;
            Random _rdm = new Random();
            return _rdm.Next(_min, _max);
        }

        //public static void sendSMS(string Mob, string Msg)
        //{

        //    string URL = "http://88.99.240.160/http-api.php?username=cabio&password=123456&senderid=CABIOO&route=1&number=" + Mob + "&message=" + Msg + "";
        //    // string URL = "http://priority.callcum.in/app/smsapi/index.php?key=559E48F50D66A8&routeid=371&type=text&contacts=" + Mob + "&senderid=CABIOO&msg=" + Msg + "";    
        //    HttpWebRequest myReq = (HttpWebRequest)WebRequest.Create(URL);
        //    HttpWebResponse myResp = (HttpWebResponse)myReq.GetResponse();
        //    System.IO.StreamReader respStreamReader = new System.IO.StreamReader(myResp.GetResponseStream());
        //    string responseString = respStreamReader.ReadToEnd();
        //    respStreamReader.Close();
        //    myResp.Close();
        //}

    }
}