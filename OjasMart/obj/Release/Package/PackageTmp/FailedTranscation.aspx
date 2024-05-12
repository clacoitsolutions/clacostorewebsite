<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FailedTranscation.aspx.cs" Inherits="OjasMart.FailedTranscation" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <title>Payment Failed</title>
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <meta content="A fully featured admin theme which can be used to build CRM, CMS, etc." name="description" />
    <meta content="Coderthemes" name="author" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <!-- App favicon -->
    <link rel="shortcut icon" href="../img/logo11.ico" />

    <!-- App css -->
    <link href="../assets/css/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <link href="../assets/css/icons.min.css" rel="stylesheet" type="text/css" />
    <link href="../assets/css/app.min.css" rel="stylesheet" type="text/css" />
</head>
<body class="authentication-bg authentication-bg-pattern">
    <form id="form1" runat="server">
        <div class="account-pages mt-5 mb-5">
            <div class="container">
                <div class="row justify-content-center">
                    <div class="col-md-8 col-lg-6 col-xl-5">
                        <div class="card bg-pattern">
                            <div class="card-body p-4">
                                <div class="text-center w-75 m-auto">
                                    <a href="#">
                                        <span>
                                            <img src="../img/logo1.png" alt="" style="height: 40px" /></span>
                                    </a>
                                </div>
                                <hr />
                                <div class="form-row">
                                    <div class="form-group col-md-6"><b>Txn Id:</b>  <asp:Label ID="lblTxnId" runat="server"></asp:Label></div>
                                    <div class="form-group col-md-6" style="text-align:right"><b>Amount:</b> &#8377; <asp:Label ID="lblAmount" runat="server"></asp:Label> </div>
                                </div>
                                <div class="mt-3 text-center">
                                    <img src="../img/Failed.png" alt="" style="height: 54px" />
                                    <h3>Failed !</h3>
                                    <p class="text-muted font-14 mt-2">
                                        Sorry! Your Transaction is Failed.
                                    </p>

                                    <a href="../Home/Dashboard" class="btn btn-block btn-pink waves-effect waves-light mt-3">Back to Home</a>
                                </div>

                            </div>
                            <!-- end card-body -->
                        </div>
                        <!-- end card -->

                    </div>
                    <!-- end col -->
                </div>
                <!-- end row -->
            </div>
            <!-- end container -->
        </div>
        <!-- end page -->


        <footer class="footer footer-alt">
            2019 - 2020 &copy; Ozas Mart Developed by <a href="#">Claco Online Services Pvt. Ltd.</a>
        </footer>

        <!-- Vendor js -->
        <script src="../assets/js/vendor.min.js"></script>

        <!-- App js -->
        <script src="../assets/js/app.min.js"></script>
    </form>
</body>
</html>
