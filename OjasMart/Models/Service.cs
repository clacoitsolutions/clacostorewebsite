using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OjasMart.Models
{
    public class Service
    {

    }

    public class CustomerRegistrationResponse
    {
        public CustomerRegistration Response { get; set; }


    }
    public class CustomerRegistration
    {
        public bool Status { get; set; }
        public string status1 { get; set; }
        public string CustomerId { get; set; }
        public int otp { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public string Password { get; set; }
        public string Id { get; set; }
    }

    public class Login
    {
        public string CustomerID { get; set; }
        public string firstName { get; set; }
        public int age { get; set; }
        public string Gender { get; set; }
        public string DateofBirth { get; set; }
        public string Country { get; set; }
        public string State { get; set; }
        public string city { get; set; }
        public string PostalCode { get; set; }
        public string Email { get; set; }
        public string Phone1 { get; set; }
        public string Address1 { get; set; }
        public string Picture { get; set; }
        public bool Response { get; set; }
        public string Role { get; set; }
        public bool Status { get; set; }
        public string ReferCode { get; set; }
        
    }
    public class LoginResponse
    {
        public List<Login> Response { get; set; }
    }
    public class FetchState
    {
        public int StateId { get; set; }
        public string StateName { get; set; }
    }
    public class Stateresponse
    {
        public List<FetchState> response { get; set; }
    }

    public class FetchCity
    {
        public int ID { get; set; }
        public string CityName { get; set; }
    }
    public class Cityresponse
    {
        public List<FetchCity> response { get; set; }
    }

    public class GetMainCategoryResponse
    {
        public List<GetMainCategory> Response { get; set; }
    }
    public class GetMainCategory
    {
        public int MainCategoryId { get; set; }
        public string MainCategoryName { get; set; }
        public string mainCategoryImage { get; set; }
    }

    public class CategoryResponse
    {
        public List<getCategory> CatResponse { get; set; }
    }
    public class getCategory
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string CategoryImage { get; set; }
        public string Description { get; set; }
    }

    public class subCategoryresponse
    {
        public List<getSubCategory> subCategoryResponse { get; set; }
    }
    public class getSubCategory
    {
        public int SubCategoryId { get; set; }
        public string SubCategoryName { get; set; }
        public string SubImage { get; set; }
        public string Description { get; set; }
        //public string ProductTitle { get; set; }
        //public string SellingPrice { get; set; }
        //public string ProductId { get; set; }
        //public string MainPicture { get; set; }
        //public string TitleTag { get; set; } 

    }

    public class BannerResponse
    {
        public List<getBanner> getBannerResponse { get; set; }
    }

    public class BannerResponsebot
    {
        public List<getBannerbot> getBannerResponsebot { get; set; }
    }


    public class getBanner
    {
        public int BannerId { get; set; }
        public string RedirectUrl { get; set; }
        public string BannerImage { get; set; }

        public string BannerDirection { get; set; }
    }


    public class getBannerbot
    {
        public string BannerImages { get; set; }
        public string Bannertitle { get; set; }
        public string DiscountType { get; set; }

        public string DiscountValue { get; set; }
        public string ProductCategory { get; set; }
      
    }


    public class TimeSlotResponse
    {
        public List<TimeSlotItem> getTimeSlotResponse { get; set; }

    }
    public class TimeSlotItem
    {
        public string DeliveryType { get; set; }
        public string Name { get; set; }
    }

    public class UpdateCartDeleteResponse
    {
        public UpdateCartDelete Response { get; set; }
    }
    public class UpdateCartDelete
    {
        public bool Status { get; set; }
        public string Msg { get; set; }

    }

    public class AreaWiseRate
    {
        public int Id { get; set; }
        public string Particular { get; set; }
        public string Regularprice { get; set; }
        public string Sellingprice { get; set; }
    }

    public class MainProductList
    {
        public List<ProductItem> plistitem { get; set; }
        public List<CrousalImage> crimage { get; set; }
        public List<ProductMoreLikethis> prductmore { get; set; }
        public List<clsVariation> prVarr { get; set; }
        public List<clsVariationcolor> varcolor { get; set; }
        public List<clsVariationsize> varsize { get; set; }
        public bool Status { get; set; }
        public string Msg { get; set; }
        public string VariationId { get; set; }
        
    }

    public class CrousalImage
    {
        public string ImageUrl { get; set; }
    }



    public class clsVariation
    {
        public string ProductId { get; set; }
        public string SalePrice { get; set; }
        public string VarriationName { get; set; }
        public string VariationId { get; set; }
    }

    public class clsVariationcolor
    {
        public string ProductCode { get; set; }
        public string ColorName { get; set; }
        public string VariationId { get; set; }   
        public string id { get; set; }
       
    }
    public class clsVariationsize
    {
        public string ProductCode { get; set; }
        public string VarriationName { get; set; }

        public string ActiveStatus { get; set; }
        public string sizename { get; set; }
    }

    public class ProductMoreLikethis
    {
        public string ProductMainImageUrl { get; set; }
        public string Discper { get; set; }
        public string ProductCategory { get; set; }
        public string ProductCode { get; set; }
        public string ProductName { get; set; }
        public string MainCategoryCode { get; set; }
        public string pName { get; set; }
        public string SalePrice { get; set; }
        public string RegularPrice { get; set; }
        public string SaleValue { get; set; }

    }

    public class ProductItem
    {
        public string ProductCode { get; set; }
        public string ProductName { get; set; }
        public string RegularPrice { get; set; }
        public string SalePrice { get; set; }
        public string ProductMainImageUrl { get; set; }
        public string Discper { get; set; }
        public string ProductDescription { get; set; }
        public string ProductSpecification { get; set; }
        public string ProductType { get; set; }

        public string VarId { get; set; }

    }


    public class productResponse
    {
        public List<getProduct> getProductResponse { get; set; }
        public List<getmultiimage> Multipleimageresponse { get; set; }
        public List<AreaWiseRate> AreaWiseRate { get; set; }
    }
    public class getProduct
    {
        public string VendorId { get; set; }
        public string VendorName { get; set; }
        public string vendorEmail { get; set; }
        public string ProductId { get; set; }
        public string ProductTitle { get; set; }
        public string ProductCategory { get; set; }
        public decimal SellingPrice { get; set; }
        public int Quantity { get; set; }
        public string ProductDescription { get; set; }
        public string GTIN { get; set; }
        public string HSNCode { get; set; }
        public string MerchantRef { get; set; }
        public string ManufacturerRef { get; set; }
        public string Status { get; set; }
        public string ApproveStatus { get; set; }
        public bool IsPrePacked { get; set; }
        public string MainPicture { get; set; }
        public string TitleTag { get; set; }
        public string Productfeature { get; set; }
        public bool IsTrending { get; set; }
        public bool IsAvailableforSale { get; set; }
        public string size { get; set; }
        public string weight { get; set; }
        public string material { get; set; }
        public string SkuCode { get; set; }
        public string brand { get; set; }
        public string OcassionType { get; set; }
        public string DisplayType { get; set; }
        public string ProcessorType { get; set; }
        public string OS { get; set; }
        public string Ram { get; set; }
        public string ImageURL { get; set; }
        public decimal RetailPrice { get; set; }
        public decimal MRP { get; set; }
        public int SubCategoryId { get; set; }
        public int minQuantity { get; set; }
        public string storeLogo { get; set; }
        public string bussnessStartDate { get; set; }
        public string bussnessType { get; set; }
        public string StateName { get; set; }
        public string CityName { get; set; }
        public string vendorAddress { get; set; }
        public string StoreName { get; set; }
        public string BusinessDesc { get; set; }
        public string GST { get; set; }
        public string Color { get; set; }
        public string stock { get; set; }

        public string Discount { get; set; }
        public string MainCategoryName { get; set; }
        public string RateId { get; set; }

        public List<AreaWiseRate> WeekendAreaWiseRate = new List<AreaWiseRate>();
    }


    public class Productsignle
    {
        public string ProductName { get; set; }
        public string ItemCode { get; set; }
        public string ProductType { get; set; }
        public string RegularPrice { get; set; }
        public string SalePrice { get; set; }
        public string ProductMainImageUrl { get; set; }
        public string Discper { get; set; }
        public string ProductDescription { get; set; }
        public string ProductSpecification { get; set; }
      
        public string ImageURL { get; set; }
      
    }

    public class getmultiimage
    {
        public string ImageURL { get; set; }
    }

    public class ItemListProduct
    {
        public string ItemCode { get; set; }
        public string Quantity { get; set; }
        public string Rate { get; set; }
        public string TotalAmount { get; set; }
        public string Reason { get; set; }
        public string Aff_CustomerId { get; set; }

        public string GstAmount { get; set; }
        public string GSTRate { get; set; }
        public string TotGstAmount { get; set; }
        public string regrate { get; set; }
        public string discount_amt { get; set; }
        public string color { get; set; }
        public string Size { get; set; }
    }



    public class FreeItemList
    {
        public string ItemCode { get; set; }
        public string qty { get; set; }
        public string varIdL { get; set; }



    }





    public class ProductColors
    {
        public string ColorCode { get; set; }
        public string ColorImage { get; set; }
    }
    public class ProductSize
    {
        public string Size { get; set; }
        public string SizeChart { get; set; }
    }

    public class cartListResponse
    {
        public List<getCartList> getCartResponse { get; set; }
        public List<GetComboOffer> GetComboOfferresponse { get; set; }
        public string todayPoAmt { get; set; }
        public string WalletBal { get; set; }
        public string TotalCountCart { get; set; }
        public Decimal gstamount { get; set; }
        
    }

    public class GetComboOffer {
        public string ProductId { get; set; }
        public string varId { get; set; }
        public string qty { get; set; }
        public string ProductName { get; set; }
        public string ProductImage { get; set; }
    }
    public class getCartList
    {
      
        public string CartListID        { get; set; }
        public string ProductCode        { get; set; }
        public string ProductName        { get; set; }
        public string ProductCategory        { get; set; }
        public string Quantity        { get; set; }
        public string pName        { get; set; }
        public string MainPicture        { get; set; }
        public string colorname { get; set; }
        public string sizename { get; set; }
        public string ProductType        { get; set; }

        public string VarriationName        { get; set; }
        public string varId        { get; set; }

        public string RegularPrice        { get; set; }
        public string SalePrice        { get; set; }
        public string DiscPer        { get; set; }
        public string Totalprice        { get; set; }
        public string PayableAmt        { get; set; }
        public string salevalue        { get; set; }


    }

    public class AddCartResponse
    {
        public AddCart Response { get; set; }
    }
    public class AddCart
    {
        public bool Status { get; set; }
        public string Msg { get; set; }
    }

    public class UpdateCartResponse
    {
        public UpdateCart Response { get; set; }
    }
    public class UpdateCart
    {
        public bool Status { get; set; }
    }

    public class UpdateaAdResponse
    {
        public addAddress Response { get; set; }
    }

    public class AddressListResponse
    {
        public List<addAddress> getAddressResponse { get; set; }
    }

    public class MsgAddress
    {
        public bool Status { get; set; }
    }

    public class CancelStatus
    {
        public bool Status { get; set; }
        public string Msg { get; set; }
        public string PayMode { get; set; }
    }

    public class MainMembertype
    {
        public List<Memebertypecls> membetype { get; set; }
        public bool Status { get; set; }
        public string Msg { get; set; }
    }

    public class StatusMemberAdd
    {
        public bool Status { get; set; }
        public string Msg { get; set; }
    }
    public class Memebertypecls
    {
        public string pk_MemberShipId { get; set; }
        public string MemberShipTitle { get; set; }
    }
    public class checkoutrespons
    {
        public bool Status { get; set; }
        public string msg { get; set; }
        public string OrderId { get; set; }
    }
    public class addAddress
    {
        public string SrNo { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string AddressType { get; set; }
        public string MobileNo { get; set; }
        public string PinCode { get; set; }
        public string Locality { get; set; }
        public string CityName { get; set; }
        public string State_name { get; set; }
        public string compAdd { get; set; }
        public string IsDefaultAccount { get; set; }
      

    }

    public class OtpReponse
    {
        public sendOtp Response { get; set; }

    }
    public class sendOtp
    {
        public string Status { get; set; }
        public string otpR { get; set; }
    }


    public class PincodeReponse
    {
        public sendPincode Response { get; set; }

    }
    public class sendPincode
    {
        public bool Status { get; set; }
        public string Res { get; set; }
    }


    public class OrderTrackingReponse
    {
        public sendOrderTracking Response { get; set; }

    }
    public class sendOrderTracking
    {
        public string OrderStatus { get; set; }
        public string Status { get; set; }
        public string Res { get; set; }
    }



    public class checkOutResponse
    {
        public List<CheckOut> getCheckoutResponse { get; set; }
    }
    public class CheckOut
    {
        public string OrderId { get; set; }
        public string Email { get; set; }
        public string paymode { get; set; }
        public string Status { get; set; }
    }
    public class UpdateAddressResponse
    {
        public UpdateAddress Response { get; set; }
    }
    public class UpdateAddress
    {
        public string Status { get; set; }
    }

    public class mainorder
    {
        public List<orderListResponse> morder { get; set; }
        public string Msg { get; set; }
        public bool Status { get; set; }
    }


    public class orderListResponse
    {
        public List<orderList> getOrderList { get; set; }
        public string OrderId { get; set; }
        public string OrderDate { get; set; }
        public string GrossAmount { get; set; }
        public string DeliveryCharges { get; set; }
        public string PaymentMode { get; set; }
        public string NetPayable { get; set; }
        public string DeliveryStatus { get; set; }
       
    }
    public class orderList
    {
        public string OrderId { get; set; }
      
        public string CustomerId { get; set; }
        public string MainImage { get; set; }
        public string ProductName { get; set; }
        public string VarriationName { get; set; }
        public string Quantity { get; set; }
        public string ItemCode { get; set; } //
        public string IsCancel { get; set; } //

    }
    public class wishListResponse
    {
        public List<wishList> getwishList { get; set; }
    }
    public class wishList
    {
        public string CustomerID { get; set; }
        public string ProductID { get; set; }
        public int WishlistID { get; set; }
        public string ProductTitle { get; set; }
        public decimal SellingPrice { get; set; }
        public decimal RetailerPrice { get; set; }
        public string ImageURL { get; set; }
    }

    public class AddWishListResponse
    {
        public AddWishList Response { get; set; }
    }
    public class AddWishList
    {
        public string Status { get; set; }
    }

    public class PasswordReponse
    {
        public sendPassword Response { get; set; }

    }
    public class sendPassword
    {
        public string Status { get; set; }
        public bool Response { get; set; }
        public string otpR { get; set; }
    }

    public class UpdateProfileResponse
    {
        public List<UpdateProfile> Response { get; set; }
    }
    public class UpdateProfile
    {
        public string FirstName { get; set; }
        public string EmailId { get; set; }
        public string MobNo { get; set; }
        public string Gender { get; set; }
        public string Status { get; set; }
    }
    public class VendorDashboardResponse
    {
        public List<VendorDashboard> getVendorDashboard { get; set; }
    }
    public class VendorDashboard
    {
        public string VendorCode { get; set; }
        public string shopName { get; set; }
        public string vendorName { get; set; }
        public string StateName { get; set; }
        public string CityName { get; set; }
        public string StoreImage { get; set; }
        public List<getproductvendor> ProductDetails = new List<getproductvendor>();
    }
    public class getproductvendor
    {
        public int MinimumQuantity { get; set; }
        public string ProductId { get; set; }
        public string ProductTitle { get; set; }
        public decimal MRP { get; set; }
        public decimal RetailPrice { get; set; }
        public decimal SellingPrice { get; set; }
        public string MainPicture { get; set; }
    }
    public class VedorProductResponse
    {
        public List<VedorProduct> getVedorProduct { get; set; }
    }
    public class VedorProduct
    {
        public int MinimumQuantity { get; set; }
        public string ProductId { get; set; }
        public string ProductTitle { get; set; }
        public decimal MRP { get; set; }
        public decimal RetailPrice { get; set; }
        public decimal SellingPrice { get; set; }
        public string MainPicture { get; set; }
    }

    public class dealsOfTheDayResponse
    {
        public List<dealsOfTheDay> getDealsOfTheDayResponse { get; set; }
    }
    public class dealsOfTheDay
    {
        public string ProductId { get; set; }
        public string ProductTitle { get; set; }
        public decimal MRP { get; set; }
        public decimal SellingPrice { get; set; }
        public decimal DealerPrice { get; set; }
        public string MainPicture { get; set; }
    }
    public class CancelResponce
    {
        public Cancel Response { get; set; }
    }
    public class Cancel
    {
        public bool Status { get; set; }
        public string Msg { get; set; }
    }
    public class ReturnResponce
    {
        public Return Response { get; set; }
    }
    public class Return
    {
        public bool Status { get; set; }
        public string msg { get; set; }
    }
    public class ReviewResponce
    {
        public Review Response { get; set; }
    }
    public class Review
    {
        public string Status { get; set; }
    }
    public class ReviewListResponce
    {
        public List<ReviewList> Response { get; set; }
    }
    public class ReviewList
    {
        public string ReviewStatus { get; set; }
        public string NameOrEmail { get; set; }
        public string ReviewDiscription { get; set; }
    }

    //public class WalletPointResponce
    //{
    //    public List<WalletPoints> Response { get; set; }
    //}
    public class WalletPoints 
    {
        public decimal WalletPoint { get; set; }
        public bool  Status { get; set; }
        public string Msg { get; set; }
    }

    public class ApplyResponce
    {
        public List<Apply> Response { get; set; }
    }
    public class Apply
    {
        public string CoupanAmount { get; set; }
        public string Status { get; set; }
    }
    public class CoupanResponce
    {
        public List<Coupan> Response { get; set; }
    }
    public class Coupan
    {
        public string CouponId { get; set; }
        public string CoupanNo { get; set; }
        public string CoupanAmount { get; set; }
        public string ExDate { get; set; }
        public string Status { get; set; }


        public string OfferTitle { get; set; }
        public string DiscountType { get; set; }
        public string PromoCode { get; set; }
        public string ApplyMRPFrom { get; set; }
        public string ApplyMRPTo { get; set; }
       


    }

    public class DeliveryResponse
    {
        public List<Delivery> Response { get; set; }
    }
    public class Delivery
    {
        public string DeliveryId { get; set; }
        public string DeliveryType { get; set; }
        public string DeliveryFixCharge { get; set; }
        public string DeliveryPerKgCharge { get; set; }
        public string Status { get; set; }
    }
    public class PaymentReponse
    {
        public PaymentPassword Response { get; set; }

    }
    public class PaymentPassword
    {
        public string Status { get; set; }
    }


    public class colormaster
    {
        public string ColorId { get; set; }
        public string ColorName { get; set; }
    }
    public class SizeMaster
    {
        public string SizeId { get; set; }
        public string Size { get; set; }
    }
    public class ReturnReason
    {
        public string ReasonId { get; set; }
        public string ReasonName { get; set; }
    }

    public class clsDistrict
    {
        public string DistrictCode { get; set; }
        public string DistrictName { get; set; }
    }

    public class VersionResponse
    {
        public string Message { get; set; }
        public bool status { get; set; }
        public string Version { get; set; }
        public string Count { get; set; }
        public string lastModifiedDate { get; set; }
        //public List<Response> Response { get; set; }
    }
    public class TrackorderResponse
    {
        public string AwbNo { get; set; }
        public string Origin { get; set; }
        public string Status1 { get; set; }
        public string StatusDatetime { get; set; }
        public string PickupDate { get; set; }
        public string Destination { get; set; }
        public string OrderId { get; set; }
        public string CODAmount { get; set; }
        public string TrackUrl { get; set; }
        public string Message { get; set; }
        public bool status { get; set; }
    }

    public class Respoce<T>
    {
        public bool Status { get; set; }
        public string Message { get; set; }

        public List<T> Responce { get; set; }
        public string AccessToken { get; set; }
        public string TxnID { get; set; }
    }


    public class ProductNew
    {
        public List<ProductItemNew> Kids;

        public List<ProductItemNew> proitemnew { get; set; }
        public bool Status { get; set; }
        public string Msg { get; set; }
        public List<ProductItemNew> Mens { get; internal set; }
        public List<ProductItemNew> Women { get; internal set; }
        public List<ProductItemNew> Uniform { get; internal set; }
        public List<ProductItemNew> BeutiProduct { get; internal set; }
        public List<ProductItemNew> Jewellery { get; internal set; }
        public List<ProductItemNew> HomeDecoration { get; internal set; }
        public List<ProductItemNew> Grocery { get; internal set; }
    }


    public class ProductItemNew
    {
        public string ProductCategory { get; set; }
        public string ProductCode { get; set; }
        public string ProductName { get; set; }
        public string MainCategoryCode { get; set; }
        public string ProductMainImageUrl { get; set; }
        public string Discper { get; set; }
        public string pName { get; set; }
        public string SalePrice { get; set; }
        public string UnitName { get; set; }
        public string RegularPrice { get; set; }
        public string SaveAmount { get; set; }
        public string AttrId { get; set; }
        public string VarId { get; set; }
        public string saleValue { get; set; }

        public List<clsVariation> proitemnewvar { get; set; }


    }
    public class getmanufacture
    {
        public int ManufacturerId { get; set; }
        public string ManufacturerName { get; set; }
        public string ManufacturerImage { get; set; }
    }
    public class ManufacturerResponse
    {
        public List<getmanufacture> ManufacturesResponse { get; set; }
    }
   
    public class getoffer
    {
        public int OfferId { get; set; }
        public string OfferCode { get; set; }
        public string ItemCode { get; set; }
        public string PurchaseAmount { get; set; }
        public string CashBackAmount { get; set; }
        public string ValidStartDate { get; set; }
        public string ValidEndDate { get; set; }
        public string points { get; set; }
        public string AmountPerPoint { get; set; }
        public string CompanyCode { get; set; }
        public string CenterCode { get; set; }
        public string IsFirstPurchase { get; set; }
        public string IsfreeItem { get; set; }
        public string FreeItemCode { get; set; }
        public string FreeQuantity { get; set; }
        public string OfferFor { get; set; }
        public string DiscountType { get; set; }
        public string DiscountValue { get; set; }
        public string Image { get; set; }
        public string PromoCode { get; set; }
        public string ApplyMRPFrom { get; set; }
        public string ApplyMRPTo { get; set; }
    }
    public class offerresponse
    {
        public List<getoffer> OfferRes { get; set; }
    }

    public class getreffereal
    {
        public string OrderId { get; set; }
        public string OrderDate { get; set; }
        public string Name { get; set; }
        public string MobileNo { get; set; }
        public string ItemCode { get; set; }
        public string Quantity { get; set; }
        public string UnitRate { get; set; }
        public string TotalAmount { get; set; }
        public string ref_Amount { get; set; }

    }

     

    public class redeemresponse
    {
        public string CustomerId { get; set; }
        public string Msg { get; set; }
        public bool Status { get; set; }
    }

    public class referalresponse
    {
      
        public List<getreffereal> refresposne { get; set; }
    }
    #region prahlad singh
    public class getDelivecgarges
    {
        public string Pincode { get; set; }
        public string Area { get; set; }
        public string Delivery_days { get; set; }
        public string DeliveryCharges { get; set; }
        public string MinDelAmount { get; set; }
        public string MRPFrom { get; set; }
        public string MRPTo { get; set; }


    }
    public class Delivecgargesresponse
    {
        public List<getDelivecgarges> Delivecgarge { get; set; }
        public string Msg { get; set; }
        public bool Status { get; set; }
    }
    #endregion


    public class MyWallet
    {
        public string CustomerID { get; set; }
        public string ReferalAMT { get; set; }
    }

    public class MyWalletResponse
    {
        public List<MyWallet> MyWallets { get; set; }
        public Boolean Status { get; set; }
    }
}