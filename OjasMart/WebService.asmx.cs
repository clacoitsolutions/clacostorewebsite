using Newtonsoft.Json;
using OjasMart.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Script.Services;
using System.Web.Services;
using static System.Net.WebRequestMethods;

namespace OjasMart
{

    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    [ScriptService]
    public class WebService : System.Web.Services.WebService
    {

        SendSMS sm = new SendSMS();


        public WebService()
        {

        }

        #region Registration
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json,UseHttpGet =false)]
        public void Registration(string Mobile, string Email, string Name, string Password, string RefferCode)
        {

            CustomerRegistration objService = new CustomerRegistration();
            CustomerRegistrationResponse objError = new CustomerRegistrationResponse();
            CustomerRegistration obj = new CustomerRegistration();
            DataTable dt = new DataTable();
            try
            {

                if (Mobile == null || Mobile == "")
                {
                    obj.Status = false;
                    obj.status1 = "Please enter mobile!!";
                }
                //else if (Mobile.Length != 10)
                //{
                //    obj.Status = false;
                //    obj.status1 = "Please enter valid mobile no!!";
                //}

                else
                {
                    dt = new ConnectionClass().InsertCustomer(Mobile, Email, Name, Password, RefferCode, 1);
                    if (dt.Rows.Count > 0)
                    {

                        obj.Id = dt.Rows[0]["Id"].ToString();
                        if (obj.Id == "1")
                        {
                            obj.Status = true;
                            obj.CustomerId = dt.Rows[0]["customerid"].ToString();
                            obj.Name = dt.Rows[0]["UserId"].ToString();

                            obj.status1 = dt.Rows[0]["msg"].ToString();

                            SendSMS sms = new SendSMS();
                            // sm.sendSMS(obj.Mobile, "Thank you for Registration with Kalash.");
                            //  sm.sendSMS(ConfigurationManager.AppSettings["AdminMobileNo"].ToString(), "A new Customer having email =" + Email + " and mobile =" + Mobile + " has been registered on Kalash.");
                        }
                        else
                        {
                            obj.Status = false;
                            obj.status1 = dt.Rows[0]["msg"].ToString();
                        }
                    }
                    else
                    {
                        obj.Status = false;
                        obj.status1 = "Number Already EXISTS..!!";
                    }
                }

                objError.Response = obj;
            }
            catch (Exception ex)
            {
                obj.Status = false;
                obj.status1 = "Something went wrong!!";
            }
          HttpContext.Current.Response.Write(new JavaScriptSerializer().Serialize(objError));
        }
        #endregion



        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string Registration1(string Mobile, string RefferCode)
        {

            CustomerRegistration objService = new CustomerRegistration();
            CustomerRegistrationResponse objError = new CustomerRegistrationResponse();
            CustomerRegistration obj = new CustomerRegistration();
            DataTable dt = new DataTable();
            try
            {

                if (Mobile == null || Mobile == "")
                {
                    obj.Status = false;
                    obj.status1 = "Please enter mobile!!";
                }
                else if (Mobile.Length != 10)
                {
                    obj.Status = false;
                    obj.status1 = "Please enter valid mobile no!!";
                }

                else
                {
                    dt = new ConnectionClass().InsertCustomer(null, null, Mobile, null, RefferCode, 4);
                    if (dt.Rows.Count > 0)
                    {
                        //obj.Status = dt.Rows[0]["msg"].ToString();
                        obj.Id = dt.Rows[0]["Id"].ToString();

                        if (Mobile == "7233099069")
                        {
                            obj.otp = 7233;
                            obj.Status = true;
                            obj.Password = "7233";
                        }
                        else if (obj.Id != "")
                        {

                            int _min = 1000;
                            int _max = 9999;
                            Random _rdm = new Random();
                            obj.otp = _rdm.Next(_min, _max);
                            SendSMS sms = new SendSMS();
                            string sms1 = "Dear Customer, Your OTP is " + obj.otp + " Do not share This OTP any One and can be used only once. [SSDPL]";
                            sm.sendSMS(Mobile, sms1);
                            obj.Status = true;
                        }
                        else
                        {
                            obj.Status = false;
                            obj.status1 = dt.Rows[0]["msg"].ToString();
                        }
                    }
                    else
                    {
                        obj.Status = false;
                        obj.status1 = "Server not responding";
                    }
                }

                objError.Response = obj;
            }
            catch (Exception ex)
            {
                obj.Status = false;
                obj.status1 = "Something went wrong!!";
            }
            return new JavaScriptSerializer().Serialize(objError);
        }

        #region Login
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json,UseHttpGet =false)]
        public void Login(string Userid, string password)
        {
            Login objl = new Login();
            LoginResponse objError = new LoginResponse();
            List<Login> obj = new List<Login>();
            try
            {
                if (Userid == null || Userid == "")
                {
                    obj.Add(new Login { CustomerID = "Please enter user id", firstName = "", Email = "", Phone1 = "", Response = false, Role = "" });
                    objl.Status = false;
                }

                else
                {
                    DataTable dt = new ConnectionClass().GetCustomer(Userid, password);
                    if (dt.Rows.Count > 0)
                    {
                        foreach (DataRow row in dt.Rows)
                        {
                            obj.Add(new Login { CustomerID = row["CustomerId"].ToString(), firstName = row["Name"].ToString(), Email = row["EmailAddress"].ToString(), Phone1 = row["MobileNo"].ToString(), Response = true, Role = row["Role"].ToString(), ReferCode = row["ReferCode"].ToString() });
                        }

                    }
                    else
                    {
                        obj.Add(new Login { CustomerID = "invalid user id and password!!", firstName = "", Email = "", Phone1 = "", Response = false, Role = "" });

                    }
                }

                objError.Response = obj;
            }
            catch (Exception ex)
            {
                obj.Add(new Login { CustomerID = "Something went wrong!!", firstName = "", Email = "", Phone1 = "", Response = false, Role = "" });

            }

            HttpContext.Current.Response.Write(new JavaScriptSerializer().Serialize(objError));
            
        }
        #endregion

        #region State
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string fetchState()
        {
            FetchState objl = new FetchState();
            Stateresponse objError = new Stateresponse();
            List<FetchState> obj = new List<FetchState>();
            try
            {
                DataTable dt = new ConnectionClass().GetState();

                foreach (DataRow row in dt.Rows)
                {
                    obj.Add(new FetchState { StateId = Convert.ToInt32(row["State_id"].ToString()), StateName = row["State_name"].ToString() });
                }
            }
            catch (Exception ex)
            { }
            objError.response = obj;
            return new JavaScriptSerializer().Serialize(objError);
        }
        #endregion

        #region City
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string getCity(int stateid)
        {

            FetchCity objl = new FetchCity();
            Cityresponse objError = new Cityresponse();
            List<FetchCity> obj = new List<FetchCity>();
            try
            {
                if (stateid != 0)
                {
                    DataTable dt = new ConnectionClass().getCity(stateid);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        foreach (DataRow row in dt.Rows)
                        {
                            obj.Add(new FetchCity { ID = Convert.ToInt32(row["ID"].ToString()), CityName = row["CityName"].ToString() });
                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }
            objError.response = obj;
            return new JavaScriptSerializer().Serialize(objError);
        }
        #endregion

        #region Main Category
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void GetMainCategory()
        {
            FetchCity objl = new FetchCity();
            GetMainCategoryResponse objError = new GetMainCategoryResponse();
            List<GetMainCategory> obj = new List<GetMainCategory>();
            try
            {
                DataTable dt = new ConnectionClass().GetMainCateGory();
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        obj.Add(new GetMainCategory { MainCategoryId = Convert.ToInt32(row["SrNo"].ToString()), MainCategoryName = row["ProductCategory"].ToString(), mainCategoryImage = row["CatImage"].ToString() });
                    }
                }
            }
            catch (Exception ex)
            { }

            objError.Response = obj;
            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.ContentType = "application/json; charset=utf-8";
            HttpContext.Current.Response.Write(new JavaScriptSerializer().Serialize(objError));
        }
        #endregion

        #region Category
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string getCategory()
        {
            getCategory objl = new getCategory();
            CategoryResponse objError = new CategoryResponse();
            List<getCategory> obj = new List<getCategory>();
            try
            {
                DataTable dt = new ConnectionClass().GetCateGory();
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        string imgurl = Convert.ToString(row["CatImage"]);//.Replace(".", "");
                        if (imgurl != null && imgurl.Length > 2)
                        {
                            if (imgurl.Substring(0, 2).IndexOf(".") >= 0)
                            {
                                imgurl = imgurl.Substring(2, imgurl.Length - 2);
                            }
                        }
                        obj.Add(new getCategory { CategoryId = Convert.ToInt32(row["SrNo"].ToString()), CategoryName = row["ProductCategory"].ToString(), CategoryImage = imgurl });
                    }
                }
            }
            catch (Exception ex)
            { }
            objError.CatResponse = obj;
            return new JavaScriptSerializer().Serialize(objError);
        }
        #endregion

        #region Sub Category
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string getsubCategory(int Category)
        {
            getSubCategory objl = new getSubCategory();
            subCategoryresponse objError = new subCategoryresponse();
            List<getSubCategory> obj = new List<getSubCategory>();
            try
            {
                if (Category != 0)
                {
                    DataTable dt = new ConnectionClass().GetSubCateGory(Category);
                    if (dt.Rows.Count > 0)
                    {
                        foreach (DataRow row in dt.Rows)
                        {
                            obj.Add(new getSubCategory { SubCategoryId = Convert.ToInt32(row["SubCategoryId"].ToString()), SubCategoryName = row["SubCategoryName"].ToString(), SubImage = row["SubImage"].ToString(), Description = row["Description"].ToString() });
                        }
                    }
                }

            }
            catch (Exception ex)
            { }
            objError.subCategoryResponse = obj;
            return new JavaScriptSerializer().Serialize(objError);
        }
        #endregion

        #region Banner
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string getBanner(string LocationCode)
        {
            getBanner objl = new getBanner();
            BannerResponse objError = new BannerResponse();
            List<getBanner> obj = new List<getBanner>();
            try
            {
                DataTable dt = new ConnectionClass().getBanner(LocationCode);
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        obj.Add(new getBanner { BannerId = Convert.ToInt32(row["SrNo"].ToString()), BannerImage = row["BannerImages"].ToString(), RedirectUrl = row["RedirectUrl"].ToString(), BannerDirection = "Up" });
                    }
                }
            }
            catch (Exception ex)
            { }
            objError.getBannerResponse = obj;
            return new JavaScriptSerializer().Serialize(objError);
        }
        #endregion

        #region Banner Bottom
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string getBannerbootom()
        {
            getBannerbot objl = new getBannerbot();
            BannerResponsebot objErrorbot = new BannerResponsebot();
            List<getBannerbot> obj = new List<getBannerbot>();
            try
            {
                DataTable dt = new ConnectionClass().getBannerbottome();
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        obj.Add(new getBannerbot { BannerImages = row["imageFile"].ToString(), Bannertitle = row["OfferTitle"].ToString(), DiscountType = row["DiscountType"].ToString(), DiscountValue = row["DiscountValue"].ToString(), ProductCategory = row["ProductCategory"].ToString() });
                    }
                }
            }
            catch (Exception ex)
            {

            }
            objErrorbot.getBannerResponsebot = obj;
            return new JavaScriptSerializer().Serialize(objErrorbot);
        }
        #endregion

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string GetProductNew(string Action, string subCategory, string AreaCode)
        {
            getProduct objl = new getProduct();
            productResponse objError = new productResponse();

            List<getProduct> obj = new List<getProduct>();
            List<AreaWiseRate> obj3 = new List<AreaWiseRate>();
            DataTable dt = new ConnectionClass().GetProductNew(Action, subCategory, AreaCode);
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    getProduct objparent = new getProduct();
                    objparent.ProductId = row["ProductId"].ToString();
                    objparent.TitleTag = row["TitleTag"].ToString();
                    objparent.ProductTitle = row["ProductTitle"].ToString();
                    objparent.MRP = Convert.ToDecimal(row["MRP"]);
                    objparent.SellingPrice = Convert.ToDecimal(row["SellingPrice"]);
                    objparent.MainPicture = row["MainPicture"].ToString();
                    objparent.ProductDescription = row["ProductDescription"].ToString();
                    objparent.Discount = row["Discount"].ToString();
                    objparent.MainCategoryName = row["MainCategoryName"].ToString();
                    objparent.Status = row["Status"].ToString();
                    objparent.RateId = row["RateId"].ToString();

                    DataTable dt1 = new ConnectionClass().GetareawiseMultiRate("2", row["ProductId"].ToString(), AreaCode);
                    if (dt1.Rows.Count > 0)
                    {

                        foreach (DataRow row1 in dt1.Rows)
                        {
                            objparent.WeekendAreaWiseRate.Add(new AreaWiseRate { Id = Convert.ToInt32(row1["Id"].ToString()), Particular = row1["Particular"].ToString(), Regularprice = row1["regularPrice"].ToString(), Sellingprice = row1["salePrice"].ToString() });
                        }

                    }
                    obj.Add(objparent);
                }

            }
            objError.getProductResponse = obj;

            return new JavaScriptSerializer().Serialize(objError);
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string getProductdetail(string Productid, string ProductCategory, string title, string CatId, string VarId)
        {
            int countcheck = 0;

            MainProductList objmplist = new MainProductList();
            objmplist.VariationId = VarId;
            List<ProductItem> objpitem = new List<ProductItem>();
            List<CrousalImage> objcrimage = new List<CrousalImage>();
            List<ProductMoreLikethis> objpmorelink = new List<ProductMoreLikethis>();
            List<clsVariation> objVar = new List<clsVariation>();
            List<clsVariationcolor> objcolor = new List<clsVariationcolor>();
            List<clsVariationsize> objsize = new List<clsVariationsize>();

            DataSet dt = new ConnectionClass().GetSingleProductDetailNew("1", Productid, ProductCategory, title, CatId, VarId, "proc_GetSingleProductView");
            if (dt.Tables[0].Rows.Count > 0)
            {
                countcheck++;
                DataRow row = dt.Tables[0].Rows[0];

                objpitem.Add(new ProductItem { VarId = row["VarId"].ToString(), ProductCode = row["ProductCode"].ToString(), ProductName = row["ProductName"].ToString(), RegularPrice = row["RegularPrice"].ToString(), SalePrice = row["salevalue"].ToString(), ProductMainImageUrl = row["ProductMainImageUrl"].ToString(), Discper = row["Discper"].ToString(), ProductDescription = row["ProductDescription"].ToString(), ProductSpecification = row["ProductSpecification"].ToString(), ProductType = row["ProductType"].ToString() });
            }
            if (dt.Tables[1].Rows.Count > 0)
            {
                foreach (DataRow rowdt2 in dt.Tables[1].Rows)
                {
                    countcheck++;
                    objcrimage.Add(new CrousalImage { ImageUrl = rowdt2["ImageUrl"].ToString() });
                }
            }
            if (dt.Tables[2].Rows.Count > 0)
            {
                foreach (DataRow rowdt1 in dt.Tables[2].Rows)
                {
                    countcheck++;
                    objpmorelink.Add(new ProductMoreLikethis { ProductMainImageUrl = rowdt1["ProductMainImageUrl"].ToString(), Discper = rowdt1["Discper"].ToString(), ProductCategory = rowdt1["ProductCategory"].ToString(), ProductCode = rowdt1["ProductCode"].ToString(), ProductName = rowdt1["ProductName"].ToString(), MainCategoryCode = rowdt1["MainCategoryCode"].ToString(), pName = rowdt1["pName"].ToString(), SalePrice = rowdt1["salevalue"].ToString(), RegularPrice = rowdt1["RegularPrice"].ToString(), SaleValue = rowdt1["SaleValue"].ToString() });
                }
            }


            if (dt.Tables[3].Rows.Count > 0)
            {
                foreach (DataRow rowdt1Var in dt.Tables[3].Rows)
                {
                    countcheck++;
                    objVar.Add(new clsVariation { ProductId = rowdt1Var["ProductId"].ToString(), SalePrice = rowdt1Var["saleValue"].ToString(), VarriationName = rowdt1Var["VarriationName"].ToString(), VariationId = rowdt1Var["VariationId"].ToString() });
                }
            }


            DataSet dtr = new ConnectionClass().GetSingleProductDetailNew("61", Productid, ProductCategory, title, CatId, VarId, "proc_GetSingleProductView");

            if (dtr.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow rowdt1Var9 in dtr.Tables[0].Rows)
                {
                    countcheck++;
                    objsize.Add(new clsVariationsize
                    {
                        ProductCode = rowdt1Var9["ProductCode"].ToString(),
                        sizename = rowdt1Var9["size_name"].ToString(),
                        ActiveStatus = rowdt1Var9["ActiveStatus"].ToString(),
                    });
                }
            }
            DataSet dtrt = new ConnectionClass().GetSingleProductDetailNew("71", Productid, ProductCategory, title, CatId, VarId, "proc_GetSingleProductView");

            if (dtrt.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow rowdt1Var8 in dtrt.Tables[0].Rows)
                {
                    countcheck++;
                    objcolor.Add(new clsVariationcolor
                    {
                        ColorName = rowdt1Var8["ColorName"].ToString(),
                        ProductCode = rowdt1Var8["ProductCode"].ToString()
                        ,
                        //VariationId = rowdt1Var8["VariationId"].ToString()
                    });
                }
            }




            objmplist.plistitem = objpitem;
            objmplist.crimage = objcrimage;
            objmplist.prductmore = objpmorelink;
            objmplist.prVarr = objVar;
            objmplist.varcolor = objcolor;
            objmplist.varsize = objsize;
            if (countcheck > 0)
            {
                objmplist.Status = true;
                objmplist.Msg = "Success";
            }
            else
            {
                objmplist.Status = false;
                objmplist.Msg = "Failed";
            }


            return new JavaScriptSerializer().Serialize(objmplist);
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string addToCart(string CustomerID, string ProductID, string Quantity, string AttrId, string VarId, string colorname, string sizename)
        {
            AttrId = !string.IsNullOrEmpty(AttrId) ? AttrId : null;
            VarId = !string.IsNullOrEmpty(VarId) ? VarId : null;
            AddCart objService = new AddCart();
            AddCartResponse objError = new AddCartResponse();
            AddCart obj = new AddCart();
            DataTable dt = new ConnectionClass().AddToCart(CustomerID, ProductID, Quantity, colorname, sizename, AttrId, VarId);

            if (dt.Rows.Count > 0)
            {
                if (dt.Rows[0]["Id"].ToString() == "1")
                {

                    obj.Status = true;
                    obj.Msg = "Insert";
                }
                else if (dt.Rows[0]["Id"].ToString() == "2")
                {
                    obj.Status = true;
                    obj.Msg = "Update";
                }
                else
                {
                    obj.Status = false;
                    obj.Msg = "Not Added";
                }
            }
            else
            {
                obj.Status = false;
                obj.Msg = "Not Added";

            }



            objError.Response = obj;
            return new JavaScriptSerializer().Serialize(objError);
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string getCartList(string CustomerId)
        {
            getCartList objl = new getCartList();
            cartListResponse objError = new cartListResponse();
            decimal totalamt = 0, regAmt = 0, totSaving = 0; decimal totalgst = 0;
            List<getCartList> obj = new List<getCartList>();
            List<GetComboOffer> objoffer = new List<GetComboOffer>();
            DataTable dt = new ConnectionClass().getCartlist(CustomerId);
            DataTable dtComboOffer = new ConnectionClass().GetComboOffer(CustomerId);
            if (dt.Rows.Count > 0)
            {
                decimal Saleprice = 0;
                decimal Quantity = 0;
                foreach (DataRow row in dt.Rows)
                {
                    obj.Add(new getCartList { CartListID = row["CartListID"].ToString(), ProductCode = row["ProductCode"].ToString(), ProductName = row["ProductName"].ToString(), ProductCategory = row["ProductCategory"].ToString(), Quantity = row["Quantity"].ToString(), pName = row["pName"].ToString(), MainPicture = row["MainPicture"].ToString(), ProductType = row["ProductType"].ToString(), VarriationName = row["VarriationName"].ToString(), varId = row["varId"].ToString(), RegularPrice = row["SalePrice"].ToString(), SalePrice = row["saleValue"].ToString(), DiscPer = row["DiscPer"].ToString(), Totalprice = row["Totalprice"].ToString(), PayableAmt = row["PayableAmt"].ToString(), salevalue = row["salevalue"].ToString(), sizename = row["SizeName"].ToString(), colorname = row["ColorName"].ToString() });
                    decimal.TryParse(Convert.ToString(row["salevalue"].ToString()), out Saleprice);
                    decimal.TryParse(Convert.ToString(row["Quantity"].ToString()), out Quantity);
                    var price = Saleprice * Quantity;
                    totalamt += price.ToString() != "" ? price : 0;
                    regAmt += row["Totalprice"].ToString().Trim() != "" ? Convert.ToDecimal(row["Totalprice"].ToString().Trim()) : 0;
                    totalgst += row["TotalGst"].ToString() != "" ? Convert.ToDecimal(row["TotalGst"].ToString()) : 0;
                }

                //if (dtComboOffer.Rows.Count > 0)
                //{
                //    foreach (DataRow drcombo in dtComboOffer.Rows)
                //    {
                //        objoffer.Add(new GetComboOffer { ProductId = drcombo["ProductCode"].ToString(), varId = drcombo["VariationCode"].ToString(), qty = drcombo["Quantity"].ToString(), ProductName = drcombo["ProductName"].ToString(), ProductImage = drcombo["PorductImage"].ToString() });
                //    }
                //}


                regAmt = 0;
                totSaving = totalamt;
                totSaving = totSaving + totalgst;
                objError.todayPoAmt = totSaving.ToString("0.00");
                objError.gstamount = totalgst;
                objError.WalletBal = regAmt.ToString("0.00");
                objError.TotalCountCart = Convert.ToString(dt.Rows.Count);
            }
            objError.getCartResponse = obj;
            objError.GetComboOfferresponse = objoffer;
            return new JavaScriptSerializer().Serialize(objError);
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string UpdateCartNew(string CustomerID, string ProductID, string Quantity, string VarId)
        {
            UpdateCart objService = new UpdateCart();
            UpdateCartResponse objError = new UpdateCartResponse();

            UpdateCart obj = new UpdateCart();

            DataTable dt = new ConnectionClass().UpdateCartUser(CustomerID, ProductID, Quantity, VarId);
            if (dt.Rows.Count > 0)
            {
                if (dt.Rows[0]["Id"].ToString() == "1")
                {
                    obj.Status = true;
                }
                else
                {
                    obj.Status = false;
                }
            }
            objError.Response = obj;
            return new JavaScriptSerializer().Serialize(objError);
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string getSimilarProduct(string subCategory, string ProductID)
        {
            getProduct objl = new getProduct();
            productResponse objError = new productResponse();

            List<getProduct> obj = new List<getProduct>();
            DataTable dt = new ConnectionClass().GetSimilarProduct(subCategory, ProductID);
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    obj.Add(new getProduct { VendorId = row["VendorId"].ToString(), ProductId = row["ProductId"].ToString(), ProductTitle = row["ProductTitle"].ToString(), SellingPrice = Convert.ToDecimal(row["SellingPrice"]), Quantity = Convert.ToInt32(row["Quantity"]), ProductDescription = row["ProductDescription"].ToString(), GTIN = row["GTIN"].ToString(), HSNCode = row["HSNCode"].ToString(), MerchantRef = row["MerchantRef"].ToString(), Status = row["Status"].ToString(), ApproveStatus = row["ApproveStatus"].ToString(), IsPrePacked = Convert.ToBoolean(row["IsPrePacked"]), MainPicture = row["MainPicture"].ToString(), TitleTag = row["TitleTag"].ToString(), Productfeature = row["Productfeature"].ToString(), IsTrending = Convert.ToBoolean(row["IsTrending"]), IsAvailableforSale = Convert.ToBoolean(row["IsAvailableforSale"]), size = row["size"].ToString(), weight = row["weight"].ToString(), material = row["material"].ToString(), SkuCode = row["SkuCode"].ToString(), brand = row["brand"].ToString(), OcassionType = row["OcassionType"].ToString(), DisplayType = row["DisplayType"].ToString(), ProcessorType = row["ProcessorType"].ToString(), OS = row["OS"].ToString(), Ram = row["Ram"].ToString(), ImageURL = row["ImageURL"].ToString(), minQuantity = Convert.ToInt32(row["minQuantity"].ToString()) });
                }
            }
            objError.getProductResponse = obj;
            return new JavaScriptSerializer().Serialize(objError);
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string addAddress(string CustomerId, string Name, string MobileNo, string PinCode, string Locality, string Address, string StateId, string CityId, string LandMark, string Alternatemobileno, string Offertype, string latitude, string longtitude, string address2)
        {
            MsgAddress msgadd = new MsgAddress();
            DataTable dt = new ConnectionClass().InsertDeliveryAddress(CustomerId, "1", Name, MobileNo, PinCode, Locality, Address, CityId, StateId, LandMark, Alternatemobileno, Offertype, latitude, longtitude, address2, "proc_InsertDeliveryAddress");
            if (dt.Rows.Count > 0)
            {
                if (dt.Rows[0]["id"].ToString() == "1")
                {
                    msgadd.Status = true;
                }
                else
                {
                    msgadd.Status = false;
                }
            }
            else
            {
                msgadd.Status = false;
            }


            return new JavaScriptSerializer().Serialize(msgadd);
        }

        #region OTP
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string getOTP(string Mobile)
        {

            sendOtp objService = new sendOtp();
            OtpReponse objError = new OtpReponse();
            sendOtp obj = new sendOtp();
            string mes = "";
            try
            {

                if (Mobile == null || Mobile == "")
                {
                    obj.Status = "Please enter mobile no";
                    obj.otpR = mes;
                    objError.Response = obj;
                }
                else if (Mobile.Length != 10)
                {
                    obj.Status = "Invalid mobile no!! Plz enter valid mobile no";
                    obj.otpR = mes;
                    objError.Response = obj;
                }
                else if (Mobile == "7233099069")
                {
                    obj.otpR = "7233";
                    objError.Response = obj;
                }

                else
                {
                    string msg = new ConnectionClass().GetOtp(Mobile, out mes);
                    obj.Status = msg;
                    obj.otpR = mes;
                    objError.Response = obj;
                }

            }
            catch (Exception ex)
            {
                obj.Status = "Something went wrong!!";
                obj.otpR = mes;
                objError.Response = obj;
            }
            return new JavaScriptSerializer().Serialize(objError);

        }


        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string getOTPLogin(string Mobile)
        {

            sendOtp objService = new sendOtp();
            OtpReponse objError = new OtpReponse();
            sendOtp obj = new sendOtp();
            string mes = "";
            try
            {

                if (Mobile == null || Mobile == "")
                {
                    obj.Status = "Please enter mobile no";
                    obj.otpR = mes;
                    objError.Response = obj;
                }
                else if (Mobile.Length != 10)
                {
                    obj.Status = "Invalid mobile no!! Plz enter valid mobile no";
                    obj.otpR = mes;
                    objError.Response = obj;
                }

                else if (Mobile == "7233099069")
                {
                    obj.otpR = "7233";
                    objError.Response = obj;
                }

                else
                {
                    string msg = new ConnectionClass().GetOtpLogin(Mobile, out mes);
                    obj.Status = msg;
                    obj.otpR = mes;
                    objError.Response = obj;
                }

            }
            catch (Exception ex)
            {
                obj.Status = "Something went wrong!!";
                obj.otpR = mes;
                objError.Response = obj;
            }
            return new JavaScriptSerializer().Serialize(objError);

        }



        #endregion OTP


        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string checkPinCode(string PinCode)
        {

            PincodeReponse objError = new PincodeReponse();

            sendPincode obj = new sendPincode();

            DataTable dt = new ConnectionClass().InsertUpdatePincode(PinCode, "proc_InsertUpdatePinCode");
            if (dt.Rows.Count > 0)
            {
                obj.Status = true;
                obj.Res = "Success";
            }
            else
            {
                obj.Status = false;
                obj.Res = "Failed";
            }


            objError.Response = obj;
            return new JavaScriptSerializer().Serialize(objError);

        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string TrackingOrder(string OrderId)
        {

            OrderTrackingReponse objError = new OrderTrackingReponse();

            sendOrderTracking obj = new sendOrderTracking();
            string mes = "";
            DataTable dt = new ConnectionClass().Proc_OrderTracking(OrderId);
            if (dt != null && dt.Rows.Count > 0)
            {
                obj.OrderStatus = Convert.ToString(dt.Rows[0]["OrderStatus"]);
                obj.Status = "True";
                obj.Res = mes;
                objError.Response = obj;
            }
            else
            {
                obj.Status = "False";
                obj.Res = mes;
                objError.Response = obj;
            }


            return new JavaScriptSerializer().Serialize(objError);

        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string AddMembershiptype(string CustomerName, string MobileNo, string EmailAddress, string Password, string MemberShip, string PaymentMode, string MemberBarCode)
        {
            StatusMemberAdd obj = new StatusMemberAdd();
            PropertyClass p = new PropertyClass();

            p.SSName = CustomerName;
            p.ContactNo = MobileNo;
            p.EmailAddress = EmailAddress;
            p.Action = "1";
            p.Password = Password;
            p.memberShipId = MemberShip;
            p.PayMode = PaymentMode;
            p.CardBarCode = MemberBarCode;
            DataTable dt = new ConnectionClass().InsertCustomerAccountNewMember(p, "Proc_InserCustomerAccountWeb");
            if (dt != null && dt.Rows.Count > 0)
            {
                if (dt.Rows[0]["Id"].ToString() == "1")
                {
                    obj.Status = true;
                    obj.Msg = dt.Rows[0]["msg"].ToString();
                }
                else
                {
                    obj.Status = false;
                    obj.Msg = dt.Rows[0]["msg"].ToString();
                }
            }
            else
            {
                obj.Status = false;
                obj.Msg = "Failed";
            }

            return new JavaScriptSerializer().Serialize(obj);
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string GetMembershiptype()
        {

            MainMembertype objError = new MainMembertype();

            List<Memebertypecls> obj = new List<Memebertypecls>();
            DataTable dt = new ConnectionClass().Membertype();
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    obj.Add(new Memebertypecls { pk_MemberShipId = row["pk_MemberShipId"].ToString(), MemberShipTitle = row["MemberShipTitle"].ToString() });
                }
            }
            objError.membetype = obj;
            return new JavaScriptSerializer().Serialize(objError);
        }

        #region Get Address
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string GetAddress(string customerId)
        {
            addAddress objl = new addAddress();
            AddressListResponse objError = new AddressListResponse();
            List<addAddress> obj = new List<addAddress>();
            try
            {
                DataTable dt = new ConnectionClass().showAddress(customerId);
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        obj.Add(new addAddress { SrNo = row["SrNo"].ToString(), Name = row["Name"].ToString(), Address = row["Address"].ToString(), AddressType = row["AddressType"].ToString(), MobileNo = row["MobileNo"].ToString(), PinCode = row["PinCode"].ToString(), Locality = row["Locality"].ToString(), CityName = row["CityName"].ToString(), State_name = row["State_name"].ToString(), compAdd = row["compAdd"].ToString(), IsDefaultAccount = row["IsDefaultAccount"].ToString() });
                    }
                }
            }
            catch (Exception ex)
            { }
            objError.getAddressResponse = obj;
            return new JavaScriptSerializer().Serialize(objError);
        }

        #endregion

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string demojson(string tes)
        {

            List<ItemListProduct> ItemList = new List<ItemListProduct>();
            for (int i = 0; i < 3; i++)
            {
                ItemListProduct n = new ItemListProduct();
                n.ItemCode = "TIKB0000756";
                n.Quantity = "5";
                n.Rate = "4";
                n.Reason = "s";
                ItemList.Add(n);
            }
            string jsoni = JsonConvert.SerializeObject(ItemList);

            ItemList = JsonConvert.DeserializeObject<List<ItemListProduct>>(jsoni);
            return new JavaScriptSerializer().Serialize(ItemList);
        }

        #region Checkout
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string CheckOut(string CustomerId, string GrossPayable, string deliverycharges, string iscoupenapplied, string CoupenAmount, string DiscAmt, string DeliveryTo, string PayMode, string Status, string NetTotal, string StockistId, string UsedPoint, string ItemId, string jsonData, string CombojsonData)
        {
            PropertyClass objp = new PropertyClass();
            checkoutrespons rescheckount = new checkoutrespons();
            List<ItemListProduct> ItemList = new List<ItemListProduct>();
            List<FreeItemList> FreeItemList = new List<FreeItemList>();

            ItemList = JsonConvert.DeserializeObject<List<ItemListProduct>>(jsonData);
            FreeItemList = JsonConvert.DeserializeObject<List<FreeItemList>>(CombojsonData);
            try
            {

                objp.Action = "1";
                objp.CustomerId = CustomerId;
                objp.CompanyCode = "MRHUB00001";

                objp.GrossPayable = Convert.ToDecimal(GrossPayable);
                objp.deliverycharges = Convert.ToDecimal(deliverycharges);
                objp.iscoupenapplied = iscoupenapplied;
                objp.CoupenAmount = Convert.ToDecimal(CoupenAmount);
                objp.DiscAmt = Convert.ToDecimal(DiscAmt);
                objp.DeliveryTo = DeliveryTo;
                objp.PayMode = PayMode;
                objp.Status = Status;
                objp.NetTotal = Convert.ToDecimal(NetTotal);
                if (StockistId != null)
                {
                    objp.StockistId = StockistId.ToString();
                }
                else
                {
                    objp.StockistId = null;
                }

                DataTable dt = new DataTable();
                dt.Columns.Add("ItemCode");
                dt.Columns.Add("Quantity");
                dt.Columns.Add("UnitRate");
                dt.Columns.Add("TotalAmount");
                dt.Columns.Add("VariationId");
                dt.Columns.Add("Aff_CustomerId");
                dt.Columns.Add("GstAmount");
                dt.Columns.Add("GSTRate");
                dt.Columns.Add("TotGstAmount");
                dt.Columns.Add("regrate");
                dt.Columns.Add("discount_amt");
                dt.Columns.Add("color");
                dt.Columns.Add("Size");
                if (ItemList != null)
                {
                    foreach (var item in ItemList)
                    {
                        dt.Rows.Add(item.ItemCode, item.Quantity, item.Rate, item.TotalAmount, item.Reason, item.Aff_CustomerId
                       , item.GstAmount, item.GSTRate, item.TotGstAmount, item.regrate, item.discount_amt, item.color, item.Size
                            );
                    }
                }


                DataTable dtfreeitem = new DataTable();
                dtfreeitem.Columns.Add("ItemCode");
                dtfreeitem.Columns.Add("qty");
                dtfreeitem.Columns.Add("varIdL");
                if (FreeItemList != null)
                {
                    foreach (var freeitem in FreeItemList)
                    {
                        dtfreeitem.Rows.Add(freeitem.ItemCode, freeitem.qty, freeitem.varIdL);
                    }
                }





                DataTable dt1 = new ConnectionClass().InsertOnlineOrderDetails(objp, "Proc_InsertOnlineOrder", dt, dtfreeitem);

                if (ItemId != "" && ItemId != null)
                {
                    objp.Usedpoint = Convert.ToDecimal(UsedPoint);
                    objp.ItemID = ItemId;
                    objp.OrderId = dt1.Rows[0]["Orderid"].ToString();
                    DataTable dt2 = new ConnectionClass().usedpoint(objp, "proc_UsedPointInOrder");
                }

                if (dt1 != null && dt1.Rows.Count > 0)
                {
                    objp.strId = dt1.Rows[0]["id"].ToString();
                    objp.OrderId = dt1.Rows[0]["OrderId"].ToString();
                    rescheckount.Status = true;
                    rescheckount.OrderId = dt1.Rows[0]["OrderId"].ToString();
                    rescheckount.msg = "Success";
                }
                else
                {
                    objp.strId = "0";
                    rescheckount.Status = false;
                    rescheckount.OrderId = "";
                    rescheckount.msg = "Failed";
                }

            }
            catch (Exception ex)
            {
                objp.strId = "0";
                rescheckount.Status = false;
                rescheckount.OrderId = "";
                rescheckount.msg = "Failed";
            }
            return new JavaScriptSerializer().Serialize(rescheckount);
        }
        #endregion

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string CancelOrder(string OrderId, string Reason)
        {
            CancelStatus obj = new CancelStatus();
            PropertyClass objp = new PropertyClass();
            try
            {
                objp.OrderId = OrderId.Trim();
                objp.Description = Reason.Trim();
                objp.Action = "1";
                DataTable dt = new ConnectionClass().InsertCancelRequest(objp, "proc_CancelOrder");
                if (dt != null && dt.Rows.Count > 0)
                {
                    objp.strId = dt.Rows[0]["id"].ToString();
                    objp.msg = dt.Rows[0]["msg"].ToString();
                    if (objp.strId == "1")
                    {
                        obj.PayMode = dt.Rows[0]["payMode"].ToString();
                        obj.Status = true;
                        obj.Msg = dt.Rows[0]["msg"].ToString();
                    }
                    else
                    {
                        obj.Status = false;
                        obj.Msg = dt.Rows[0]["msg"].ToString();
                    }
                }
                else
                {
                    obj.Status = false;
                    obj.Msg = "Failed";
                }
            }
            catch (Exception ex)
            {
                obj.Status = false;
                obj.Msg = "Failed";
            }


            return new JavaScriptSerializer().Serialize(obj);
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string UpdateAddress(string Id, string customerid)
        {
            sendOtp objService = new sendOtp();
            OtpReponse objError = new OtpReponse();
            string Error = "";
            string Verify = "";
            sendOtp obj = new sendOtp();
            string mes = "";
            string msg = new ConnectionClass().UpdateAdd(Id, customerid);
            obj.Status = msg;
            objError.Response = obj;
            return new JavaScriptSerializer().Serialize(objError);
        }

        #region Orderlist
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string orderList(string customerId, string FromDate, string ToDate)
        {
            int countcheck = 0;
            mainorder objl = new mainorder();
            List<orderListResponse> objError = new List<orderListResponse>();
            try
            {
                DataTable dt = new ConnectionClass().getOrderlist(customerId, FromDate, ToDate, "proc_BindCustomerDashBoardApp");
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow rowitem in dt.Rows)
                    {
                        countcheck++;
                        List<orderList> obj = new List<orderList>();
                        DataTable dtorderitem = new ConnectionClass().getorderitemdetail(customerId, rowitem["OrderId"].ToString(), "proc_BindCustomerDashBoardApp");
                        foreach (DataRow row in dtorderitem.Rows)
                        {
                            obj.Add(new orderList { OrderId = row["OrderId"].ToString(), MainImage = row["MainImage"].ToString(), ProductName = row["ProductName"].ToString(), VarriationName = row["VarriationName"].ToString(), Quantity = row["Quantity"].ToString(), ItemCode = row["ItemCode"].ToString(), IsCancel = row["IsCancel"].ToString() });
                        }

                        objError.Add(new orderListResponse { OrderId = rowitem["OrderId"].ToString(), OrderDate = rowitem["OrdDate"].ToString(), GrossAmount = rowitem["GrossAmount"].ToString(), DeliveryCharges = rowitem["DeliveryCharges"].ToString(), PaymentMode = rowitem["PaymentMode"].ToString(), NetPayable = rowitem["NetPayable"].ToString(), DeliveryStatus = rowitem["DeliveryStatus"].ToString(), getOrderList = obj });
                    }
                }

                if (countcheck > 0)
                {
                    objl.Status = true;
                    objl.Msg = "Success";
                    objl.morder = objError;
                }
                else
                {
                    objl.Status = false;
                    objl.Msg = "Failed";
                    objl.morder = objError;
                }
            }
            catch (Exception ex)
            {
                objl.Status = false;
                objl.Msg = "Failed";
                objl.morder = objError;
            }

            return new JavaScriptSerializer().Serialize(objl);
        }
        #endregion

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string getsubCategoryList()
        {
            getSubCategory objl = new getSubCategory();
            subCategoryresponse objError = new subCategoryresponse();

            List<getSubCategory> obj = new List<getSubCategory>();
            DataTable dt = new ConnectionClass().GetSubCateGoryList();
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    obj.Add(new getSubCategory { SubCategoryId = Convert.ToInt32(row["SubCategoryId"].ToString()), SubCategoryName = row["SubCategoryName"].ToString(), SubImage = row["SubImage"].ToString(), Description = row["Description"].ToString() });
                }
            }
            objError.subCategoryResponse = obj;
            return new JavaScriptSerializer().Serialize(objError);
        }


        #region Forget Password
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string ForgetPassword(string Mobile)
        {
            sendPassword objService = new sendPassword();
            PasswordReponse objError = new PasswordReponse();
            sendPassword obj = new sendPassword();
            string mes = "";
            try
            {
                if (Mobile == null || Mobile == "")
                {
                    obj.Response = false;
                    obj.otpR = mes;
                }
                else if (Mobile.Length != 10)
                {
                    obj.Response = false;
                    obj.otpR = mes;
                }
                else
                {
                    string msg = new ConnectionClass().getPassword(Mobile, out mes);
                    obj.Response = Convert.ToBoolean(msg);
                    obj.otpR = mes;
                }

            }
            catch (Exception ex)
            {
                obj.Response = false;
                obj.otpR = mes;
            }
            objError.Response = obj;
            return new JavaScriptSerializer().Serialize(objError);
        }
        #endregion

        #region Change Password
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string ChangePassword(string Mobile, string oldPassword, string Password)
        {
            sendPassword objService = new sendPassword();
            PasswordReponse objError = new PasswordReponse();
            sendPassword obj = new sendPassword();
            string mes = "";
            try
            {
                if (Mobile == null || Mobile == "")
                {
                    obj.Status = "Please enter mobile no";
                    obj.otpR = mes;
                }
                else if (oldPassword == null || oldPassword == "")
                {
                    obj.Status = "Please enter old password";
                    obj.otpR = mes;
                }
                else if (Password == null || Password == "")
                {
                    obj.Status = "Please enter password";
                    obj.otpR = mes;
                }
                else if (ValidatePassword(Password) == false)
                {
                    obj.Status = "Password must be 8 characters with at least 1 Upper Case, 1 lower case, and 1 numeric character!!";
                    obj.otpR = mes;
                }
                else
                {
                    string msg = new ConnectionClass().ChangePassword(Mobile, oldPassword, Password);
                    obj.Status = msg;
                    obj.otpR = mes;
                }
            }
            catch (Exception ex)
            {
                obj.Status = "Something went wrong!!";
                obj.otpR = mes;
            }
            objError.Response = obj;
            return new JavaScriptSerializer().Serialize(objError);
        }
        #endregion

        #region Update Profile
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string UpdateProfile(string customerId, string Name, string Email, string PhoneNumber, string Gender)
        {
            UpdateProfileResponse objError = new UpdateProfileResponse();
            List<UpdateProfile> obj = new List<UpdateProfile>();
            try
            {
                if (customerId == null || customerId == "")
                {
                    obj.Add(new UpdateProfile
                    {
                        Status = "Please enter customer id!!",
                    });
                }
                else if (Name == null || Name == "")
                {
                    obj.Add(new UpdateProfile
                    {
                        Status = "Please enter name!!",
                    });
                }
                else if (Email == null || Email == "")
                {
                    obj.Add(new UpdateProfile
                    {
                        Status = "Please enter email!!",
                    });
                }
                else if (IsValidEmail(Email) == false)
                {
                    obj.Add(new UpdateProfile
                    {
                        Status = "Please enter valid email!!",
                    });
                }
                else if (PhoneNumber == null || PhoneNumber == "")
                {
                    obj.Add(new UpdateProfile
                    {
                        Status = "Please enter mobile no!!",
                    });
                }
                else if (PhoneNumber.Length != 10)
                {
                    obj.Add(new UpdateProfile
                    {
                        Status = "Please enter valid mobile no!!",
                    });
                }
                else if (Gender == null || Gender == "")
                {
                    obj.Add(new UpdateProfile
                    {
                        Status = "Please enter gender!!",
                    });
                }
                else
                {
                    DataTable dt = new ConnectionClass().updateProfile(customerId, Name, Email, PhoneNumber, Gender);
                    if (dt.Rows.Count > 0)
                    {
                        foreach (DataRow row in dt.Rows)
                        {
                            obj.Add(new UpdateProfile
                            {
                                FirstName = row["Name"].ToString(),
                                EmailId = row["EmailAddress"].ToString(),
                                MobNo = row["MobileNo"].ToString(),
                                Gender = row["Gender"].ToString(),
                                Status = row["msg"].ToString(),
                            });
                        }
                    }
                    else
                    {
                        obj.Add(new UpdateProfile
                        {
                            Status = "Record not found!!",
                        });
                    }
                }


            }
            catch (Exception ex)
            {
                obj.Add(new UpdateProfile
                {
                    Status = "Something went wrong!!",
                });
            }

            objError.Response = obj;
            return new JavaScriptSerializer().Serialize(objError);
        }
        #endregion

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string GetVendorDashboard()
        {
            //VendorDashboard objl = new VendorDashboard();
            VendorDashboardResponse objError = new VendorDashboardResponse();
            List<VendorDashboard> obj = new List<VendorDashboard>();
            List<getproductvendor> obj1 = new List<getproductvendor>();
            DataTable dt = new ConnectionClass().getVendorDashboard();

            if (dt.Rows.Count > 0)
            {

                foreach (DataRow row in dt.Rows)
                {
                    VendorDashboard objparent = new VendorDashboard();
                    objparent.VendorCode = row["VendorCode"].ToString();
                    objparent.vendorName = row["vendorName"].ToString();
                    objparent.shopName = row["shopName"].ToString();
                    objparent.StateName = row["StateName"].ToString();
                    objparent.CityName = row["CityName"].ToString();
                    objparent.StoreImage = row["StoreImage"].ToString();

                    DataTable dt1 = new ConnectionClass().getVendorProductDashboard(row["VendorCode"].ToString());
                    if (dt1.Rows.Count > 0)
                    {
                        foreach (DataRow row1 in dt1.Rows)
                        {
                            objparent.ProductDetails.Add(new getproductvendor { ProductId = row1["ProductId"].ToString(), ProductTitle = row1["ProductTitle"].ToString(), MinimumQuantity = Convert.ToInt32(row1["MinimumQuantity"].ToString()), MainPicture = row1["MainPicture"].ToString(), MRP = Convert.ToDecimal(row1["MRP"].ToString()), RetailPrice = Convert.ToDecimal(row1["RetailPrice"].ToString()), SellingPrice = Convert.ToDecimal(row1["SellingPrice"].ToString()) });
                        }
                    }


                    obj.Add(objparent);
                }

            }

            objError.getVendorDashboard = obj;

            return new JavaScriptSerializer().Serialize(objError);
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string DealsofTheDay()
        {
            getSubCategory objl = new getSubCategory();
            subCategoryresponse objError = new subCategoryresponse();

            List<getSubCategory> obj = new List<getSubCategory>();
            DataTable dt = new ConnectionClass().GetSubCateGoryList();
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    obj.Add(new getSubCategory { SubCategoryId = Convert.ToInt32(row["SubCategoryId"].ToString()), SubCategoryName = row["SubCategoryName"].ToString() });
                }
            }
            objError.subCategoryResponse = obj;
            return new JavaScriptSerializer().Serialize(objError);
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string getDealsofTheDay()
        {
            dealsOfTheDay objl = new dealsOfTheDay();
            dealsOfTheDayResponse objError = new dealsOfTheDayResponse();

            List<dealsOfTheDay> obj = new List<dealsOfTheDay>();
            DataTable dt = new ConnectionClass().GetDeals();
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    obj.Add(new dealsOfTheDay
                    {
                        ProductId = row["ProductId"].ToString(),
                        ProductTitle = row["ProductTitle"].ToString(),
                        MainPicture = row["MainPicture"].ToString(),
                        MRP = Convert.ToDecimal(row["MRP"].ToString()),
                        SellingPrice = Convert.ToDecimal(row["SellingPrice"].ToString()),
                        DealerPrice = Convert.ToDecimal(row["DealerPrice"].ToString())
                    });
                }
            }
            objError.getDealsOfTheDayResponse = obj;
            return new JavaScriptSerializer().Serialize(objError);
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string CancelProduct(string OrderId, string ProductCode)
        {
            CancelResponce objError = new CancelResponce();
            Cancel obj = new Cancel();
            DataTable dt = new ConnectionClass().Proc_CancelItemOrder("1", OrderId, ProductCode, "Proc_CancelItemOrder");
            if (dt != null && dt.Rows.Count > 0)
            {
                obj.Msg = dt.Rows[0]["OrderStatus"].ToString();
                obj.Status = true;
                objError.Response = obj;
            }
            else
            {
                obj.Msg = "Something Wrong";
                obj.Status = false;
                objError.Response = obj;
            }
            return new JavaScriptSerializer().Serialize(objError);
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string getDeliveryTimeSlot()
        {

            TimeSlotResponse objError = new TimeSlotResponse();

            List<TimeSlotItem> obj = new List<TimeSlotItem>();
            DataTable dt = new ConnectionClass().GetDeliveryTimeSlot();
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    obj.Add(new TimeSlotItem { DeliveryType = row["DeliveryType"].ToString(), Name = row["Name"].ToString() });
                }
            }
            objError.getTimeSlotResponse = obj;
            return new JavaScriptSerializer().Serialize(objError);
        }



        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string ReturnProduct(string OrderId, string ProductCode, string returnreason)
        {
            ReturnResponce objError = new ReturnResponce();
            Return obj = new Return();
            DataTable dt = new ConnectionClass().Proc_ReturnItemOrder("1", OrderId, ProductCode, returnreason, "Proc_ReturnItemOrder");
            if (dt != null && dt.Rows.Count > 0)
            {
                obj.msg = dt.Rows[0]["OrderStatus"].ToString();
                obj.Status = true;
                objError.Response = obj;
            }
            else
            {
                obj.msg = "Something Wrong";
                obj.Status = false;
                objError.Response = obj;
            }

            return new JavaScriptSerializer().Serialize(objError);
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string AddReview(string ReviewStatus, string UserId, string NameOrEmail, string Description, string ProductCode)
        {
            ReviewResponce objError = new ReviewResponce();
            Review obj = new Review();
            string msg = new ConnectionClass().InsertRVAndCancel("", ProductCode, ReviewStatus, UserId, NameOrEmail, Description, "RvIntr");
            obj.Status = msg;
            objError.Response = obj;
            return new JavaScriptSerializer().Serialize(objError);
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string ReviewList(string ProductCode)
        {
            ReviewListResponce objError = new ReviewListResponce();
            List<ReviewList> obj = new List<ReviewList>();
            DataTable dt = new ConnectionClass().GetReviewList(ProductCode);
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    obj.Add(new ReviewList
                    {
                        ReviewStatus = row["ReviewStatus"].ToString(),
                        NameOrEmail = row["CustomertName"].ToString(),
                        ReviewDiscription = row["ReviewDiscription"].ToString(),
                    });
                }
            }
            objError.Response = obj;
            return new JavaScriptSerializer().Serialize(objError);
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string CoupanList(string UserId)
        {
            CoupanResponce objError = new CoupanResponce();
            List<Coupan> obj = new List<Coupan>();

            try
            {

                DataTable dt = new ConnectionClass().GetCoupanDetails(UserId);
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        obj.Add(new Coupan
                        {
                            CouponId = row["SrNo"].ToString(),
                            CoupanNo = row["OfferCode"].ToString(),
                            CoupanAmount = row["DiscountValue"].ToString(),
                            ExDate = row["ValidEndDate"].ToString(),
                            Status = row["Status"].ToString(),

                            OfferTitle = row["OfferTitle"].ToString(),
                            DiscountType = row["DiscountType"].ToString(),
                            PromoCode = row["PromoCode"].ToString(),
                            ApplyMRPFrom = row["ApplyMRPFrom"].ToString(),
                            ApplyMRPTo = row["ApplyMRPTo"].ToString(),

                        });
                    }
                }

            }
            catch (Exception ex)
            {

            }


            objError.Response = obj;
            return new JavaScriptSerializer().Serialize(objError);
        }







        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string OfferList(string UserId)
        {
            CoupanResponce objError = new CoupanResponce();
            List<Coupan> obj = new List<Coupan>();

            try
            {

                DataTable dt = new ConnectionClass().CoupanList(UserId);
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        obj.Add(new Coupan
                        {
                            CouponId = row["SrNo"].ToString(),
                            CoupanNo = row["OfferCode"].ToString(),
                            CoupanAmount = row["DiscountValue"].ToString(),
                            ExDate = row["ValidEndDate"].ToString(),
                            Status = row["Status"].ToString(),

                            OfferTitle = row["OfferTitle"].ToString(),
                            DiscountType = row["DiscountType"].ToString(),
                            PromoCode = row["PromoCode"].ToString(),
                            ApplyMRPFrom = row["ApplyMRPFrom"].ToString(),
                            ApplyMRPTo = row["ApplyMRPTo"].ToString(),

                        });
                    }
                }

            }
            catch (Exception ex)
            {

            }


            objError.Response = obj;
            return new JavaScriptSerializer().Serialize(objError);
        }












        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string ApplyCoupan(string UserId, string CoupanCode, decimal OrderAmount)
        {
            ApplyResponce objError = new ApplyResponce();
            List<Apply> obj = new List<Apply>();
            DataTable dt = new ConnectionClass().ApplyCupan(UserId, CoupanCode, OrderAmount);
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    obj.Add(new Apply
                    {
                        CoupanAmount = row["CoupanAmount"].ToString(),
                        Status = row["MSG"].ToString(),
                    });
                }
            }
            objError.Response = obj;
            return new JavaScriptSerializer().Serialize(objError);
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string GetDeliveryType()
        {
            DeliveryResponse objError = new DeliveryResponse();
            List<Delivery> obj = new List<Delivery>();
            DataTable dt = new ConnectionClass().GetDeliveryType();
            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    obj.Add(new Delivery
                    {
                        DeliveryId = row["deleveryTypeId"].ToString(),
                        DeliveryType = row["deleveryTypeName"].ToString(),
                        DeliveryFixCharge = row["FixDeleryCharge"].ToString(),
                        DeliveryPerKgCharge = row["PerKgDeleveryCharge"].ToString(),
                        Status = "True",
                    });
                }
            }
            objError.Response = obj;
            return new JavaScriptSerializer().Serialize(objError);
        }



        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string Payment(string OrderId, string TransactionId, string TransactionDate, string RecieptNo, string PaymentStatus, string TotalAmount, string PaidAmount, string DiscountAmount, string EntryDate, string DeliveryType, string CustomerCode, string PaymentDate, string Myhpayid, string CardNum, string CardType, string ProductCode, string contact, string paymentMode, string OrderDate, string TimeSlot)
        {
            PaymentReponse objError = new PaymentReponse();
            PaymentPassword obj = new PaymentPassword();
            string mes = "";
            string msg = new ConnectionClass().payment(OrderId, TransactionId, TransactionDate, RecieptNo, PaymentStatus, TotalAmount, PaidAmount, DiscountAmount, EntryDate, DeliveryType, CustomerCode, PaymentDate, Myhpayid, CardNum, CardType, ProductCode, contact, paymentMode, OrderDate, TimeSlot);
            obj.Status = msg;
            objError.Response = obj;
            return new JavaScriptSerializer().Serialize(objError);
        }


        #region Add product new
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void LastWeekAddedProductNew()
        {
            int countcheck = 0;
            ProductNew obj = new ProductNew();
            List<ProductItemNew> objitemnew = new List<ProductItemNew>();
            try
            {
                DataTable dt = new ConnectionClass().LastNewitemlist();
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        countcheck++;
                        ProductItemNew objparent = new ProductItemNew();
                        objparent.ProductCategory = row["ProductCategory"].ToString();
                        objparent.ProductCode = row["ProductCode"].ToString();
                        objparent.ProductMainImageUrl = row["ProductMainImageUrl"].ToString();
                        objparent.ProductName = row["ProductName"].ToString();
                        objparent.RegularPrice = row["RegularPrice"].ToString();
                        objparent.SalePrice = row["saleValue"].ToString();
                        objparent.SaveAmount = row["SaveAmount"].ToString();
                        objparent.UnitName = row["UnitName"].ToString();
                        objparent.VarId = row["VarId"].ToString();
                        objparent.AttrId = row["AttrId"].ToString();
                        objparent.pName = row["pName"].ToString();
                        objparent.Discper = row["Discper"].ToString();
                        objparent.MainCategoryCode = row["MainCategoryCode"].ToString();
                        objparent.saleValue = row["saleValue"].ToString();
                        objitemnew.Add(objparent);
                    }

                    if (countcheck > 0)
                    {
                        obj.proitemnew = objitemnew;
                        obj.Status = true;
                    }
                    else
                    {
                        obj.proitemnew = objitemnew;
                        obj.Status = false;
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions if necessary
            }

            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.ContentType = "application/json; charset=utf-8";
            HttpContext.Current.Response.Write(new JavaScriptSerializer().Serialize(obj));
        }
        #endregion


        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string GetProductbyCategoryId(string CategoryId)
        {
            int countcheck = 0;
            ProductNew obj = new ProductNew();
            List<ProductItemNew> objitemnew = new List<ProductItemNew>();
            DataTable dt = new ConnectionClass().GetSingleProductDetail(CategoryId, "proc_GetSingleProductView");
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    List<clsVariation> objR = new List<clsVariation>();

                    countcheck++;
                    ProductItemNew objparent = new ProductItemNew();
                    objparent.ProductCategory = row["ProductCategory"].ToString();
                    objparent.ProductCode = row["ProductCode"].ToString();
                    objparent.ProductMainImageUrl = row["ProductMainImageUrl"].ToString();
                    objparent.ProductName = row["ProductName"].ToString();
                    objparent.RegularPrice = row["RegularPrice"].ToString();
                    objparent.SalePrice = row["saleValue"].ToString();
                    objparent.VarId = row["VarId"].ToString();
                    objparent.AttrId = row["AttrId"].ToString();
                    objparent.pName = row["pName"].ToString();
                    objparent.Discper = row["Discper"].ToString();
                    objparent.MainCategoryCode = row["MainCategoryCode"].ToString();
                    objparent.saleValue = row["saleValue"].ToString();

                    DataTable dt1 = new ConnectionClass().getproductvariation(row["ProductCode"].ToString(), "getproductvariation");
                    if (dt1.Rows.Count > 0)
                    {
                        //  objR.Clear();
                        foreach (DataRow dr in dt1.Rows)
                        {

                            clsVariation objP = new clsVariation();
                            objP.ProductId = dr["ProductId"].ToString();
                            objP.SalePrice = dr["SalePrice"].ToString();
                            objP.VarriationName = dr["VarriationName"].ToString();
                            objP.VariationId = dr["VariationId"].ToString();
                            objR.Add(objP);


                        }
                    }


                    objparent.proitemnewvar = objR;
                    objitemnew.Add(objparent);
                }

                if (countcheck > 0)
                {
                    obj.proitemnew = objitemnew;
                    obj.Status = true;
                }
                else
                {
                    obj.proitemnew = objitemnew;
                    obj.Status = false;
                }


            }


            return new JavaScriptSerializer().Serialize(obj);
        }


        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string GetProductSearch(string SearchText)
        {


            int countcheck = 0;
            ProductNew obj = new ProductNew();
            List<ProductItemNew> objitemnew = new List<ProductItemNew>();
            DataTable dt = new ConnectionClass().GetProductSearch(SearchText, "proc_GetSingleProductView");
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    List<clsVariation> objR = new List<clsVariation>();

                    countcheck++;
                    ProductItemNew objparent = new ProductItemNew();
                    objparent.ProductCategory = row["ProductCategory"].ToString();
                    objparent.ProductCode = row["ProductCode"].ToString();
                    objparent.ProductMainImageUrl = row["ProductMainImageUrl"].ToString();
                    objparent.ProductName = row["ProductName"].ToString();
                    objparent.RegularPrice = row["RegularPrice"].ToString();
                    objparent.SalePrice = row["saleValue"].ToString();
                    objparent.VarId = row["VarId"].ToString();
                    objparent.AttrId = row["AttrId"].ToString();
                    objparent.pName = row["pName"].ToString();
                    objparent.Discper = row["Discper"].ToString();
                    objparent.MainCategoryCode = row["MainCategoryCode"].ToString();
                    objparent.saleValue = row["salevalue"].ToString();

                    DataTable dt1 = new ConnectionClass().getproductvariation(row["ProductCode"].ToString(), "getproductvariation");
                    if (dt1.Rows.Count > 0)
                    {
                        //  objR.Clear();
                        foreach (DataRow dr in dt1.Rows)
                        {

                            clsVariation objP = new clsVariation();
                            objP.ProductId = dr["ProductId"].ToString();
                            objP.SalePrice = dr["SalePrice"].ToString();
                            objP.VarriationName = dr["VarriationName"].ToString();
                            objP.VariationId = dr["VariationId"].ToString();
                            objR.Add(objP);


                        }
                    }


                    objparent.proitemnewvar = objR;
                    objitemnew.Add(objparent);
                }

                if (countcheck > 0)
                {
                    obj.proitemnew = objitemnew;
                    obj.Status = true;
                }
                else
                {
                    obj.proitemnew = objitemnew;
                    obj.Status = false;
                }


            }


            return new JavaScriptSerializer().Serialize(obj);
        }





        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string LastWeekAddedProduct(string Days, string areacode)
        {
            getProduct objl = new getProduct();
            productResponse objError = new productResponse();

            List<getProduct> obj = new List<getProduct>();
            List<AreaWiseRate> obj3 = new List<AreaWiseRate>();
            DataTable dt = new ConnectionClass().LastWeekAddedProduct(Days, areacode);
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    getProduct objparent = new getProduct();
                    objparent.VendorId = row["VendorId"].ToString();
                    objparent.VendorName = row["VendorName"].ToString();
                    objparent.vendorEmail = row["vendorEmail"].ToString();
                    objparent.ProductId = row["ProductId"].ToString();
                    objparent.ProductTitle = row["ProductTitle"].ToString();
                    objparent.SellingPrice = Convert.ToDecimal(row["SellingPrice"]);
                    objparent.ProductDescription = row["ProductDescription"].ToString();
                    objparent.GTIN = row["GTIN"].ToString();
                    objparent.HSNCode = row["HSNCode"].ToString();
                    objparent.MerchantRef = row["MerchantRef"].ToString();
                    objparent.Status = row["Status"].ToString();
                    objparent.ApproveStatus = row["ApproveStatus"].ToString();
                    objparent.MainPicture = row["MainPicture"].ToString();
                    objparent.TitleTag = row["TitleTag"].ToString();
                    objparent.Productfeature = row["Productfeature"].ToString();
                    objparent.IsAvailableforSale = Convert.ToBoolean(row["IsAvailableforSale"] == DBNull.Value ? 0 : row["IsAvailableforSale"]);
                    objparent.size = row["size"].ToString();
                    objparent.weight = row["weight"].ToString();
                    objparent.material = row["material"].ToString();
                    objparent.SkuCode = row["SkuCode"].ToString();
                    objparent.brand = row["brand"].ToString();
                    objparent.OcassionType = row["OcassionType"].ToString();
                    objparent.DisplayType = row["DisplayType"].ToString();
                    objparent.ProcessorType = row["ProcessorType"].ToString();
                    objparent.OS = row["OS"].ToString();
                    objparent.Ram = row["Ram"].ToString();
                    objparent.SubCategoryId = Convert.ToInt32(row["SubCategoryId"] == DBNull.Value ? "0" : row["SubCategoryId"]);
                    objparent.RetailPrice = Convert.ToDecimal(row["RetailPrice"] == DBNull.Value ? "0" : row["RetailPrice"]);
                    objparent.MRP = Convert.ToDecimal(row["MRP"]);
                    objparent.minQuantity = Convert.ToInt32(row["minQuantity"].ToString());

                    objparent.RateId = row["RateId"].ToString();

                    DataTable dt1 = new ConnectionClass().GetareawiseMultiRate("2", row["ProductId"].ToString(), areacode);
                    if (dt1.Rows != null)
                    {
                        foreach (DataRow row1 in dt1.Rows)
                        {
                            objparent.WeekendAreaWiseRate.Add(new AreaWiseRate { Id = Convert.ToInt32(row1["Id"].ToString()), Particular = row1["Particular"].ToString(), Regularprice = row1["regularPrice"].ToString(), Sellingprice = row1["salePrice"].ToString() });
                        }
                        obj.Add(objparent);
                    }


                }

            }
            objError.getProductResponse = obj;
            return new JavaScriptSerializer().Serialize(objError);
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string GetColorMaster()
        {
            Respoce<colormaster> Res = new Respoce<colormaster>();
            List<colormaster> objl = new List<colormaster>();

            try
            {

                DataTable dt = null; // new ExecuteProc().Proc_Color("3", null);
                if (dt != null && dt.Rows.Count > 0)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        objl.Add(new colormaster { ColorId = Convert.ToString(row["id"]), ColorName = Convert.ToString(row["Color"]) });
                    }
                    Res.Responce = objl;
                    Res.Status = true;
                    Res.Message = "success";

                }
                else
                {
                    Res.Responce = null;
                    Res.Status = false;
                    Res.Message = "No Record Found";
                }

            }
            catch (Exception e)
            {
                Res.Responce = null;
                Res.Status = false;
                Res.Message = e.Message;
            }


            return new JavaScriptSerializer().Serialize(Res);
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string GetSizeMaster()
        {
            Respoce<SizeMaster> Res = new Respoce<SizeMaster>();
            List<SizeMaster> objl = new List<SizeMaster>();

            try
            {

                DataTable dt = null;//new ExecuteProc().Proc_Size("3", null);
                if (dt != null && dt.Rows.Count > 0)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        objl.Add(new SizeMaster { SizeId = Convert.ToString(row["id"]), Size = Convert.ToString(row["Size"]) });
                    }
                    Res.Responce = objl;
                    Res.Status = true;
                    Res.Message = "success";

                }
                else
                {
                    Res.Responce = null;
                    Res.Status = false;
                    Res.Message = "No Record Found";
                }

            }
            catch (Exception e)
            {
                Res.Responce = null;
                Res.Status = false;
                Res.Message = e.Message;
            }


            return new JavaScriptSerializer().Serialize(Res);
        }

        //[WebMethod]
        //[ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        //public string ReturnCustomerOrder(string CustomerId, string OrderId, string OrderItemId, string Comment, string ReasonId)
        //{
        //    ReviewResponce objError = new ReviewResponce();
        //    Review obj = new Review();
        //    string msg = new ConnectionClass().ReturnOrder(CustomerId, OrderId, OrderItemId, Comment, ReasonId);
        //    obj.Status = msg;
        //    objError.Response = obj;
        //    return new JavaScriptSerializer().Serialize(objError);

        //}

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string GetReturnReason()
        {
            Respoce<ReturnReason> Res = new Respoce<ReturnReason>();
            List<ReturnReason> objl = new List<ReturnReason>();

            try
            {

                DataTable dt = new ConnectionClass().GetReturnReason();
                if (dt != null && dt.Rows.Count > 0)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        objl.Add(new ReturnReason { ReasonId = Convert.ToString(row["ReasonId"]), ReasonName = Convert.ToString(row["ReasonName"]) });
                    }
                    Res.Responce = objl;
                    Res.Status = true;
                    Res.Message = "success";

                }
                else
                {
                    Res.Responce = null;
                    Res.Status = false;
                    Res.Message = "No Record Found";
                }

            }
            catch (Exception e)
            {
                Res.Responce = null;
                Res.Status = false;
                Res.Message = e.Message;
            }


            return new JavaScriptSerializer().Serialize(Res);
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string GetAccessToken()
        {
            Respoce<ReturnReason> Res = new Respoce<ReturnReason>();
            List<ReturnReason> objl = new List<ReturnReason>();
            try
            {
                //Instamojo objClass = InstamojoImplementation.getApi("Pc1E2KSx3GIpmp68GT2tG9H3mwmGWBfXT6Cms8Yz", "1Tfe1CUiYmFK8ANJn36osPYARgQUitR9UkCKj0RYf4SNQwYVSkuntmUoArfDhqtDQK85dQa0B80JSr5EBxXi6tiRJqfeq3l8L1E5J18DNU8Yocbhjdpq2gkZSRiBKWsQ", "https://api.instamojo.com/v2/", "https://www.instamojo.com/oauth2/token/");
                //PaymentOrder objPaymentRequest = new PaymentOrder();
                //string randomName = Path.GetRandomFileName();
                //randomName = randomName.Replace(".", string.Empty);
                //objPaymentRequest.transaction_id = randomName;
                //Res.AccessToken = InstamojoConstants.AccessToken;
                //Res.TxnID = objPaymentRequest.transaction_id;
                Res.Status = true;
                Res.Message = "success";

            }
            catch
            {
                Res.Responce = null;
                Res.Status = false;
                Res.Message = "No Record Found";
            }

            return new JavaScriptSerializer().Serialize(Res);

        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string GetAppVersion()
        {
            VersionResponse rsp = new VersionResponse();
            //List<Plantype> obj = new List<Plantype>();

            DataTable dt = new ConnectionClass().GetVersion();
            if (dt != null && dt.Rows.Count > 0)
            {

                rsp.Version = dt.Rows[0]["Version"].ToString();
                rsp.Count = dt.Rows[0]["Count"].ToString();
                rsp.lastModifiedDate = dt.Rows[0]["entryDate"].ToString();
                rsp.status = true;
                rsp.Message = "Success";
            }
            else
            {
                rsp.Version = null;
                rsp.Count = null;
                rsp.lastModifiedDate = null;
                rsp.status = false;
                rsp.Message = "Failed";
            }

            return new JavaScriptSerializer().Serialize(rsp);
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string GetDistrictList()
        {
            ConnectionClass con = new ConnectionClass();
            Respoce<clsDistrict> res = new Respoce<clsDistrict>();
            List<clsDistrict> Dis = new List<clsDistrict>();
            try
            {
                DataTable dt = new DataTable();
                dt = con.GetDistrict();
                if (dt != null && dt.Rows.Count > 0)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        Dis.Add(new clsDistrict { DistrictCode = row["VolCode"].ToString(), DistrictName = row["Name"].ToString() });
                    }
                    res.Responce = Dis;
                    res.Status = true;
                    res.Message = "success";
                }
                else
                {
                    res.Responce = null;
                    res.Status = false;
                    res.Message = "Record not exist.";
                }
            }
            catch (Exception e)
            {
                res.Responce = null;
                res.Status = false;
                res.Message = e.Message;
            }
            return new JavaScriptSerializer().Serialize(res);
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string DeleteCart(string CustomerID, string cartlistid)
        {
            UpdateCart objService = new UpdateCart();
            UpdateCartDeleteResponse objError = new UpdateCartDeleteResponse();

            UpdateCartDelete obj = new UpdateCartDelete();

            DataTable dt = new ConnectionClass().DeleteCart(CustomerID, cartlistid);
            if (dt.Rows.Count > 0)
            {
                if (dt.Rows[0]["id"].ToString() == "1")
                {
                    obj.Status = true;
                    obj.Msg = "Success";
                }
                else
                {
                    obj.Status = false;
                    obj.Msg = "Failed";
                }
            }
            else
            {
                obj.Status = false;
                obj.Msg = "Failed";
            }



            objError.Response = obj;
            return new JavaScriptSerializer().Serialize(objError);
        }

        #region Other function

        bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }


        static bool ValidatePassword(string password)
        {
            const int MIN_LENGTH = 8;
            const int MAX_LENGTH = 15;

            if (password == null) throw new ArgumentNullException();

            bool meetsLengthRequirements = password.Length >= MIN_LENGTH && password.Length <= MAX_LENGTH;
            bool hasUpperCaseLetter = false;
            bool hasLowerCaseLetter = false;
            bool hasDecimalDigit = false;

            if (meetsLengthRequirements)
            {
                foreach (char c in password)
                {
                    if (char.IsUpper(c)) hasUpperCaseLetter = true;
                    else if (char.IsLower(c)) hasLowerCaseLetter = true;
                    else if (char.IsDigit(c)) hasDecimalDigit = true;
                }
            }

            bool isValid = meetsLengthRequirements
                        && hasUpperCaseLetter
                        && hasLowerCaseLetter
                        && hasDecimalDigit
                        ;
            return isValid;

        }


        #endregion

        #region Class

        public class item
        {
            public string images { get; set; }
            public string color { get; set; }
            public string reason { get; set; }
            public string descr { get; set; }
            public string pcat { get; set; }

        }
        public class customerdata
        {
            public string name { get; set; }
            public string phone { get; set; }
            public string pincode { get; set; }
            public string Address { get; set; }
            public string State { get; set; }
            public string City { get; set; }
            public string Price { get; set; }
            public string Orderdate { get; set; }
            public string OrderId { get; set; }
            public string PaymentMode { get; set; }

        }
        public class ResponceFromApi
        {
            public string upload_wbn { get; set; }

            public List<packages> packages { get; set; }
        }
        public class packages
        {
            public string status { get; set; }
            public string client { get; set; }
            public string waybill { get; set; }
            public string refnum { get; set; }
        }

        public class ParmCancel
        {
            public string waybill { get; set; }
            public string cancellation { get; set; }
        }


        public class Mainclass
        {
            public List<ShipmentData> ShipmentData { get; set; }
        }
        public class ShipmentData
        {
            public Shipment Shipment { get; set; }

        }
        public class Shipment
        {
            public string Origin { get; set; }
            public string PickUpDate { get; set; }
            public string ChargedWeight { get; set; }
            public string OrderType { get; set; }
            public string Destination { get; set; }
            public string ReferenceNo { get; set; }
            public string ReturnedDate { get; set; }

            public string DestRecieveDate { get; set; }

            public string OriginRecieveDate { get; set; }
            public string OutDestinationDate { get; set; }

            public string CODAmount { get; set; }
            public string FirstAttemptDate { get; set; }
            public bool ReverseInTransit { get; set; }

            public string SenderName { get; set; }
            public string AWB { get; set; }
            public string DispatchCount { get; set; }

            public string InvoiceAmount { get; set; }

            public Status1 Status { get; set; }

            public Consignee Consignee { get; set; }

            public List<ScanDetail> Scan { get; set; }

        }



        public class Consignee
        {
            public string City { get; set; }
            public string Name { get; set; }
            public string Country { get; set; }
            public string[] Address2 { get; set; }
            public string Address3 { get; set; }
            public string PinCode { get; set; }
            public string State { get; set; }
            public string Telephone2 { get; set; }
            public string Telephone1 { get; set; }
            public string[] Address { get; set; }

        }
        public class ScanDetails
        {
            public ScanDetail ScanDetail { get; set; }
        }
        public class ScanDetail
        {
            public string ScanDateTime { get; set; }
            public string ScanType { get; set; }

            public string Scan { get; set; }
            public string StatusDateTime { get; set; }
            public string ScannedLocation { get; set; }

            public string Instructions { get; set; }
            public string StatusCode { get; set; }
        }
        public class Status1
        {
            public string Status { get; set; }
            public string StatusLocation { get; set; }
            public string StatusDateTime { get; set; }
            public string RecievedBy { get; set; }
            public string Instructions { get; set; }

            public string StatusType { get; set; }
            public string StatusCode { get; set; }

        }


        #endregion



        //
        #region ReturnCustomerOrder
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string ReturnCustomerOrder(string OrderId, string Reason)
        {
            ReturnResponce objError = new ReturnResponce();
            Return obj = new Return();
            DataTable dt = new ConnectionClass().Proc_ReturnItemOrder("2", OrderId, "", Reason, "Proc_ReturnItemOrder");
            if (dt != null && dt.Rows.Count > 0)
            {
                obj.Status = true;
                obj.msg = dt.Rows[0]["OrderStatus"].ToString();
                objError.Response = obj;
            }
            else
            {
                obj.Status = false;
                obj.msg = "Something wrong";
                objError.Response = obj;
            }

            return new JavaScriptSerializer().Serialize(objError);
        }
        #endregion



        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]






        public string WalletPoint(string CustomerId)
        {
            WalletPoints obj = new WalletPoints();
            try
            {
                DataTable dt = new ConnectionClass().GetWalletPoint(CustomerId);
                if (dt != null && dt.Rows.Count > 0)
                {
                    obj.WalletPoint = Convert.ToDecimal(dt.Rows[0]["WalletAmount"]);
                    obj.Status = true;
                    obj.Msg = "Success";
                }
                else
                {
                    obj.Status = false;
                    obj.Msg = "Failed";
                }
            }
            catch (Exception ex)
            {
                obj.Status = false;
                obj.Msg = "Failed";
            }
            return new JavaScriptSerializer().Serialize(obj);



            //if (dt != null && dt.Rows.Count > 0)
            //{
            //    objp.strId = dt.Rows[0]["id"].ToString();
            //    objp.msg = dt.Rows[0]["msg"].ToString();
            //    if (objp.strId == "1")
            //    {
            //        obj.PayMode = dt.Rows[0]["payMode"].ToString();
            //        obj.Status = true;
            //        obj.Msg = dt.Rows[0]["msg"].ToString();
            //    }
            //    else
            //    {
            //        obj.Status = false;
            //        obj.Msg = dt.Rows[0]["msg"].ToString();
            //    }
            //}

            //objError.Response = obj;
            //return new JavaScriptSerializer().Serialize(objError);
        }


        // My Wallet 

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]

        public string MyWallet(string CustomerID)
        {
            List<MyWallet> obj = new List<MyWallet>();
            MyWalletResponse objResponse = new MyWalletResponse();

            try
            {
                DataTable dt = new ConnectionClass().getBonus(CustomerID);
                if (dt.Rows[0]["ReferAmt"] != null)
                {
                    obj.Add(new Models.MyWallet
                    {
                        CustomerID = CustomerID,
                        ReferalAMT = dt.Rows[0]["ReferAmt"].ToString()

                    });
                }
            }
            catch (Exception)
            {

                throw;
            }
            return new JavaScriptSerializer().Serialize(obj);
        }



        /// <summary>
        /// This Api is used for Recent Product Some Item Show only
        /// </summary>
        /// <returns></returns>

        #region
        [WebMethod]
        public void Recent()
        {
            int countcheck = 0;
            ProductNew obj = new ProductNew();
            List<ProductItemNew> kidsList = new List<ProductItemNew>();
            List<ProductItemNew> MensList = new List<ProductItemNew>();
            List<ProductItemNew> WomenList = new List<ProductItemNew>();
            List<ProductItemNew> UniformList = new List<ProductItemNew>();
            List<ProductItemNew> BeutiProductList = new List<ProductItemNew>();
            List<ProductItemNew> JewelleryList = new List<ProductItemNew>();
            List<ProductItemNew> HomeDecorationList = new List<ProductItemNew>();
            List<ProductItemNew> GroceryList = new List<ProductItemNew>();

            DataSet dt = new ConnectionClass().RecentApi("ProcGetRecentApi");
            if (dt.Tables.Count > 0)
            {
                for (int i = 0; i < dt.Tables.Count; i++)
                {
                    foreach (DataRow row in dt.Tables[i].Rows)
                    {
                        List<clsVariation> objR = new List<clsVariation>();
                        countcheck++;
                        ProductItemNew objparent = new ProductItemNew();
                        objparent.ProductCategory = row["ProductCategory"].ToString();
                        objparent.ProductCode = row["ProductCode"].ToString();
                        objparent.ProductMainImageUrl = row["ProductMainImageUrl"].ToString();
                        objparent.ProductName = row["ProductName"].ToString();
                        objparent.RegularPrice = row["RegularPrice"].ToString();
                        objparent.SalePrice = row["saleValue"].ToString();
                        objparent.VarId = row["VarId"].ToString();
                        objparent.AttrId = row["AttrId"].ToString();
                        objparent.pName = row["pName"].ToString();
                        objparent.Discper = row["Discper"].ToString();
                        objparent.MainCategoryCode = row["MainCategoryCode"].ToString();
                        objparent.saleValue = row["saleValue"].ToString();

                        DataTable dt1 = new ConnectionClass().getproductvariation(row["ProductCode"].ToString(), "getproductvariation");
                        if (dt1.Rows.Count > 0)
                        {
                            foreach (DataRow dr in dt1.Rows)
                            {
                                clsVariation objP = new clsVariation();
                                objP.ProductId = dr["ProductId"].ToString();
                                objP.SalePrice = dr["SalePrice"].ToString();
                                objP.VarriationName = dr["VarriationName"].ToString();
                                objP.VariationId = dr["VariationId"].ToString();
                                objR.Add(objP);
                            }
                        }

                        objparent.proitemnewvar = objR;

                        // Check which list to add the item to
                        if (i == 0) // First table, add to kidsList
                        {
                            kidsList.Add(objparent);
                        }
                        else if (i == 1) // Second table, add to womenList
                        {
                            MensList.Add(objparent);
                        }
                        else if (i==2)
                        {
                            WomenList.Add(objparent);
                        }
                        else if (i == 3)
                        {
                            UniformList.Add(objparent);
                        }
                        else if(i == 4)
                        {
                            BeutiProductList.Add(objparent);
                        }
                        else if (i==5)
                        {
                            JewelleryList.Add(objparent);
                        }
                        else if (i == 6)
                        {
                            HomeDecorationList.Add(objparent);
                        }
                        else if (i==7)
                        {
                            GroceryList.Add(objparent);
                        }
                      
                    }
                }

                if (countcheck > 0)
                {
                    obj.Kids = kidsList;
                    obj.Mens = MensList;
                    obj.Women= WomenList;
                    obj.Uniform = UniformList;
                    obj.BeutiProduct=BeutiProductList;
                    obj.Jewellery = JewelleryList;
                    obj.HomeDecoration = HomeDecorationList;
                    obj.Grocery = GroceryList;
                    obj.Status = true;
                }
                else
                {
                    obj.proitemnew = kidsList; // Assuming if there's no data, proitemnew gets kidsList data
                    obj.Status = false;
                }
            }

            HttpContext.Current.Response.Write(new JavaScriptSerializer().Serialize(obj));
        }
        #endregion







        #region Manufacturerlist
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string getManufacturerlist()
        {
            getmanufacture objl = new getmanufacture();
            ManufacturerResponse objError = new ManufacturerResponse();
            List<getmanufacture> obj = new List<getmanufacture>();
            try
            {
                DataTable dt = new ConnectionClass().GetManufacturere();
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        string imgurl = Convert.ToString(row["Image"]);//.Replace(".", "");
                        if (imgurl != null && imgurl.Length > 2)
                        {
                            if (imgurl.Substring(0, 2).IndexOf(".") >= 0)
                            {
                                imgurl = imgurl.Substring(2, imgurl.Length - 2);
                            }
                        }
                        obj.Add(new getmanufacture { ManufacturerId = Convert.ToInt32(row["SrNo"].ToString()), ManufacturerName = row["ManufacturerName"].ToString(), ManufacturerImage = imgurl });
                    }
                }
            }
            catch (Exception ex)
            { }
            objError.ManufacturesResponse = obj;
            return new JavaScriptSerializer().Serialize(objError);
        }
        #endregion


        #region OfferList
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string getofferlist(string type)
        {
            getoffer objl = new getoffer();
            offerresponse objError = new offerresponse();
            List<getoffer> obj = new List<getoffer>();
            try
            {
                DataTable dt = new ConnectionClass().getOfferslist(type, "getOffers");
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        string imgurl = Convert.ToString(row["imageFile"]);//.Replace(".", "");
                        if (imgurl != null && imgurl.Length > 2)
                        {
                            if (imgurl.Substring(0, 2).IndexOf(".") >= 0)
                            {
                                imgurl = imgurl.Substring(2, imgurl.Length - 2);
                            }
                        }
                        obj.Add(new getoffer
                        {
                            OfferId = Convert.ToInt32(row["SrNo"].ToString()),
                            OfferCode = row["OfferCode"].ToString(),
                            Image = imgurl,
                            ItemCode = row["ItemCode"].ToString(),
                            PurchaseAmount = row["PurchaseAmount"].ToString(),
                            CashBackAmount = row["CashBackAmount"].ToString(),
                            ValidStartDate = row["ValidStartDate"].ToString(),
                            ValidEndDate = row["ValidEndDate"].ToString(),
                            points = row["points"].ToString(),
                            AmountPerPoint = row["AmountPerPoint"].ToString(),
                            CompanyCode = row["CompanyCode"].ToString(),
                            CenterCode = row["CenterCode"].ToString(),
                            IsFirstPurchase = row["IsFirstPurchase"].ToString(),
                            IsfreeItem = row["IsfreeItem"].ToString(),
                            FreeItemCode = row["FreeItemCode"].ToString(),
                            FreeQuantity = row["FreeQuantity"].ToString(),
                            OfferFor = row["OfferFor"].ToString(),
                            DiscountType = row["DiscountType"].ToString(),
                            DiscountValue = row["DiscountValue"].ToString(),
                            PromoCode = row["PromoCode"].ToString(),
                            ApplyMRPFrom = row["ApplyMRPFrom"].ToString(),
                            ApplyMRPTo = row["ApplyMRPTo"].ToString(),
                        });
                    }
                }
            }
            catch (Exception ex)
            { }
            objError.OfferRes = obj;
            return new JavaScriptSerializer().Serialize(objError);
        }



        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string GetProductlistByfilter(string brand, string product, string FromRate, string ToRate, string Category)
        {
            int countcheck = 0;
            ProductNew obj = new ProductNew();
            List<ProductItemNew> objitemnew = new List<ProductItemNew>();
            DataTable dt = new ConnectionClass().GetProductsdetails(brand, product, FromRate, ToRate, Category, "Proc_OfferProductList");
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    List<clsVariation> objR = new List<clsVariation>();

                    countcheck++;
                    ProductItemNew objparent = new ProductItemNew();
                    objparent.ProductCategory = row["ProductCategory"].ToString();
                    objparent.ProductCode = row["ProductCode"].ToString();
                    objparent.ProductMainImageUrl = row["ProductMainImageUrl"].ToString();
                    objparent.ProductName = row["ProductName"].ToString();
                    objparent.RegularPrice = row["RegularPrice"].ToString();
                    objparent.SalePrice = row["SalePrice"].ToString();

                    objparent.VarId = row["VarId"].ToString();
                    objparent.AttrId = row["AttrId"].ToString();
                    objparent.pName = row["pName"].ToString();
                    objparent.Discper = row["Discper"].ToString();
                    objparent.MainCategoryCode = row["MainCategoryCode"].ToString();

                    DataTable dt1 = new ConnectionClass().getproductvariation(row["ProductCode"].ToString(), "getproductvariation");
                    if (dt1.Rows.Count > 0)
                    {
                        //  objR.Clear();
                        foreach (DataRow dr in dt1.Rows)
                        {

                            clsVariation objP = new clsVariation();
                            objP.ProductId = dr["ProductId"].ToString();
                            objP.SalePrice = dr["SalePrice"].ToString();
                            objP.VarriationName = dr["VarriationName"].ToString();
                            objP.VariationId = dr["VariationId"].ToString();
                            objR.Add(objP);


                        }
                    }


                    objparent.proitemnewvar = objR;
                    objitemnew.Add(objparent);
                }

                if (countcheck > 0)
                {
                    obj.proitemnew = objitemnew;
                    obj.Status = true;
                }
                else
                {
                    obj.proitemnew = objitemnew;
                    obj.Status = false;
                }


            }


            return new JavaScriptSerializer().Serialize(obj);
        }
        #endregion



        #region Refferal
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string getRefferalCustomerWise(string CustomerID)
        {
            getreffereal objl = new getreffereal();
            referalresponse objError = new referalresponse();
            List<getreffereal> obj = new List<getreffereal>();
            try
            {
                DataTable dt = new ConnectionClass().getreferalllist(CustomerID, "Proc_Refferal");
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        obj.Add(new getreffereal
                        {
                            OrderId = row["OrderId"].ToString(),
                            OrderDate = row["OrderDate"].ToString(),
                            Name = row["Name"].ToString(),
                            MobileNo = row["MobileNo"].ToString(),
                            ItemCode = row["ItemCode"].ToString(),
                            Quantity = row["Quantity"].ToString(),
                            UnitRate = row["UnitRate"].ToString(),
                            TotalAmount = row["TotalAmount"].ToString(),
                            ref_Amount = row["ref_Amount"].ToString(),
                        });
                    }
                }
            }
            catch (Exception ex)
            { }
            objError.refresposne = obj;
            return new JavaScriptSerializer().Serialize(objError);
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string Redeemgift(string CustomerId)
        {
            redeemresponse obj = new redeemresponse();
            try
            {
                DataTable dt = new ConnectionClass().Proc_Redeemgift(CustomerId, "Redeemgift");
                if (dt != null && dt.Rows.Count > 0)
                {
                    obj.CustomerId = Convert.ToString(dt.Rows[0]["CustomerId"]);
                    obj.Status = true;
                    obj.Msg = Convert.ToString(dt.Rows[0]["Msg"]);
                }
                else
                {
                    obj.Status = false;
                    obj.Msg = "Failed";
                }
            }
            catch (Exception ex)
            {
                obj.Status = false;
                obj.Msg = "Failed";
            }
            return new JavaScriptSerializer().Serialize(obj);
        }


        #endregion

        #region Prahlad singh---Search DeliverCharges
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string getPincodeWiseDeliveryCharges(string Pinocode)
        {

            Delivecgargesresponse objError = new Delivecgargesresponse();
            List<getDelivecgarges> obj = new List<getDelivecgarges>();
            try
            {
                DataTable dt = new ConnectionClass().getPincodeBiseDeliveryCharges(1, Pinocode, null, "Proc_getPincodeBiseDeliveryCharges");
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        obj.Add(new getDelivecgarges
                        {
                            Pincode = Pinocode,
                            Area = row["Area"].ToString(),
                            Delivery_days = row["Delivery_days"].ToString(),
                            DeliveryCharges = row["DeliveryCharge"].ToString(),
                            MRPFrom = row["MRPFrom"].ToString(),
                            MRPTo = row["MRPTo"].ToString(),

                        });
                    }
                    objError.Delivecgarge = obj;
                    objError.Status = true;
                    objError.Msg = "successfull";
                }
                else
                {
                    objError.Status = true;
                    objError.Msg = "No Record Found";
                }
            }
            catch (Exception ex)
            {
                objError.Status = false;
                objError.Msg = "something went wrong";
            }

            return new JavaScriptSerializer().Serialize(objError);
        }
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string getMRPWiseDeliveryCharges(string MRP)
        {
            Delivecgargesresponse objError = new Delivecgargesresponse();
            List<getDelivecgarges> obj = new List<getDelivecgarges>();
            try
            {
                DataTable dt = new ConnectionClass().getPincodeBiseDeliveryCharges(3, null, MRP, "Proc_getPincodeBiseDeliveryCharges");
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        obj.Add(new getDelivecgarges
                        {
                            //Pincode = row["Pincode"].ToString(),
                            //Area = row["Area"].ToString(),
                            //Delivery_days = row["Delivery_days"].ToString(),
                            DeliveryCharges = row["Deliverycharge"].ToString(),
                            //MinDelAmount = row["MinDelAmount"].ToString(),
                        });
                    }
                    objError.Delivecgarge = obj;
                    objError.Status = true;
                    objError.Msg = "successfull";
                }
                else
                {
                    objError.Status = true;
                    objError.Msg = "No Record Found";
                }
            }
            catch (Exception ex)
            {
                objError.Status = false;
                objError.Msg = "something went wrong";
            }

            return new JavaScriptSerializer().Serialize(objError);
        }
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string getPointOfferList()
        {
            PropertyClass objp = new PropertyClass();
            LogicClass objl = new LogicClass();
            clspointOfeer objError = new clspointOfeer();
            List<poinlist> obj = new List<poinlist>();
            try
            {
                objp.Action = "2";
                DataTable dt = objl.InsertUpdatePointsOffer(objp);
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        obj.Add(new poinlist
                        {
                            ItemName = row["ProductName"].ToString(),
                            ItemCode = row["ItemCode"].ToString(),
                            pointId = row["Id"].ToString(),
                            Points = row["Points"].ToString(),
                            FromDate = row["FromDate"].ToString(),
                            ToDate = row["ToDate"].ToString(),

                        });
                    }
                    objError.pointOfferlist = obj;
                    objError.status = "true";
                    objError.msg = "successfull";
                }
                else
                {
                    objError.status = "true";
                    objError.msg = "No Record Found";
                }
            }
            catch (Exception ex)
            {
                objError.status = "false";
                objError.msg = "something went wrong";
            }

            return new JavaScriptSerializer().Serialize(objError);
        }



        #endregion



    }
    public class clspointOfeer
    {
        public string msg { get; set; }
        public string status { get; set; }

        public List<poinlist> pointOfferlist { get; set; }

    }
    public class poinlist
    {
        public string ItemCode { get; set; }
        public string ItemName { get; set; }
        public string pointId { get; set; }
        public string Points { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
    }
}
