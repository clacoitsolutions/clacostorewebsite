$(document).ready(function () {

    //$('#txtDOJ').datepicker({
    //    format: 'dd-MM-yyyy'
    //});
    //$('#txtDOB').datepicker({
    //    format: 'dd-MM-yyyy'
    //});
    $('select').select2();

    $("input[name$='rblPayType']").change(function () {
        ClearBankDetails();
        var val = $(this).val();
        if (val == "Bank Transfer") {
            $('#divBank').show();
        }
        else {
            $('#divBank').hide();
        }
    });   


    $('#btnSaveBasicDetails').click(function () {
        if ($('#txtEmployeeName').val() == "") {
            alert('Please Enter Employee Name!');
            $('#txtEmployeeName').focus();
            return;
        }
        if ($('#ddlgender').val() == "Select") {
            alert('Please Select gender!');
            $('#ddlgender').focus();
            return;
        }
        if ($('#txtDOJ').val() == "") {
            alert('Please Enter Date of Joining!');
            $('#txtDOJ').focus();
            return;
        }
        if ($('#ddlDesignation').val() == "0") {
            alert('Please Select Designation!');
            $('#ddlDesignation').focus();
            return;
        }
        var dataobject = {
            Action: "1",
            EmpCode: $("#hdEmpCode").val(),
            AccountName: $("#txtEmployeeName").val(),
            FatherName: $("#txtFatherName").val(),
            strgender: $("#ddlgender").val(),
            eDate: $("#txtDOJ").val(),
            designation: $("#ddlDesignation").val(),
            workemailid: $("#txtWorkEmailAddress").val(),
            EPFNo: $("#txtEPFNo").val(),
            ESINo: $("#txtESINumber").val()
        };
        $("#showSpinner").show();
        $.ajax({
            url: "/HRM/InserEmployeeDetails",
            type: "POST",
            contentType: false,
            processData: false,
            //data: dataobject,
            data: JSON.stringify(dataobject),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (r) {
                if (r.strId != '0') {
                    alert(r.msg);
                    $("#showSpinner").hide();
                    $('#hdEmpCode').val(r.AccCode);

                    $('#aboutme').removeClass('show active');
                    $('#timeline').addClass('show active');

                    $('#Basic').removeClass('active');
                    $('#SalDetials').addClass('active');
                }
                else {
                    alert('Server not Responding !!!');
                    $("#showSpinner").hide();
                }
            },
            error: function (XMLHttpRequest, textStatus, errorThrown) {
                alert('Please Check values Entered by you !!!');
                $("#showSpinner").hide();
            }
        });


    });

    $('#btnSaveSalaryDetails').click(function () {
        if ($("#hdEmpCode").val() == '') {
            alert('Please Fill Basic Details First !');
            return;
        }
        if ($('#txtAnnualCTC').val() == '' || $('#txtAnnualCTC').val() == '0') {
            alert('Please Enter Annaul CTC !');
            $('#txtAnnualCTC').focus();
            return;
        }
        var dataobject = {
            Action: "4",
            EmpCode: $("#hdEmpCode").val(),
            basicmonthly: $("#txtBasicMonthlyAmount").val() != '' ? parseFloat($("#txtBasicMonthlyAmount").val()) : 0,
            hramonthly: $("#txtHRAMonthlyAmount").val() != '' ? parseFloat($("#txtHRAMonthlyAmount").val()) : 0,
            conveyanceallowancemonthly: $("#txtCAMonthlyAmount").val() != '' ? parseFloat($("#txtCAMonthlyAmount").val()) : 0,
            fixedallowancemonthly: $("#txtFAMonthlyAmount").val() != '' ? parseFloat($("#txtFAMonthlyAmount").val()) : 0,
            monthlyctc: $("#lblTotalMonthly").text() != '' ? parseFloat($("#lblTotalMonthly").text()) : 0,
            annuallyctc: $("#lblTotalAnnualy").text() != '' ? parseFloat($("#lblTotalAnnualy").text()) : 0,
            BasicPer: $("#txtBasicPer").val() != '' ? parseFloat($("#txtBasicPer").val()) : 0,
            HRAPer: $("#txtHRAPer").val() != '' ? parseFloat($("#txtHRAPer").val()) : 0,
        };
        $("#showSpinner").show();
        $.ajax({
            url: "/HRM/InserEmployeeDetails",
            type: "POST",
            contentType: false,
            processData: false,
            //data: dataobject,
            data: JSON.stringify(dataobject),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (r) {
                if (r.strId != '0') {
                    alert(r.msg);
                    $("#showSpinner").hide();
                    $('#timeline').removeClass('show active');
                    $('#settings').addClass('show active');
                    $('#SalDetials').removeClass('active');
                    $('#PerDetails').addClass('active');
                }
                else {
                    alert('Server not Responding !!!');
                    $("#showSpinner").hide();
                }
            },
            error: function (XMLHttpRequest, textStatus, errorThrown) {
                alert('Please Check values Entered by you !!!');
                $("#showSpinner").hide();
            }
        });

    });
    $('#btnSavePersonalDetails').click(function () {
        if ($("#hdEmpCode").val() == '') {
            alert('Please Fill Basic Details First !');
            return;
        }
        if ($('#txtContactNo').val() == "") {
            alert('Please Enter Contact No.!');
            $('#txtContactNo').focus();
            return;
        }

        var dataobject = {
            Action: "2",
            EmpCode: $("#hdEmpCode").val(),
            EmailAddress: $("#txtPersonalEmailId").val(),
            ContactNo: $("#txtContactNo").val(),
            mDate: $("#txtDOB").val(),
            CityName: $("#txtCity").val(),
            StateName: $("#txtStateName").val(),
            Address: $("#txtAddress").val(),
            PinCode: $("#txtPinCode").val(),
            PanNo: $("#txtPAnNo").val(),
        };
        $("#showSpinner").show();
        $.ajax({
            url: "/HRM/InserEmployeeDetails",
            type: "POST",
            contentType: false,
            processData: false,
            //data: dataobject,
            data: JSON.stringify(dataobject),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (r) {
                if (r.strId != '0') {
                    alert(r.msg);
                    $("#showSpinner").hide();
                    $('#settings').removeClass('show active');
                    $('#PayInfo').addClass('show active');

                    $('#PerDetails').removeClass('active');
                    $('#PayDetails').addClass('active');
                }
                else {
                    alert('Server not Responding !!!');
                    $("#showSpinner").hide();
                }
            },
            error: function (XMLHttpRequest, textStatus, errorThrown) {
                alert('Please Check values Entered by you !!!');
                $("#showSpinner").hide();
            }
        });
    });
    $('#btnSavePaymentDetails').click(function () {
        if ($("#hdEmpCode").val() == '') {
            alert('Please Fill Basic Details First !');
            return;
        }
        var value = $("input:radio[name=rblPayType]:checked").val();
        if (value == "Bank Transfer") {
            if ($('#txtAccHolderName').val() == "") {
                alert('Please Enter Account Holder Name!');
                $('#txtAccHolderName').focus();
                return;
            }
            if ($('#txtBankName').val() == "") {
                alert('Please Enter Bank Name!');
                $('#txtBankName').focus();
                return;
            }
            if ($('#txtAccNumber').val() == "") {
                alert('Please Enter Account Number!');
                $('#txtAccNumber').focus();
                return;
            }
            if ($('#txtIFSCCode').val() == "") {
                alert('Please Enter IFSC Code!');
                $('#txtIFSCCode').focus();
                return;
            }
        }

        var dataobject = {
            Action: "3",
            EmpCode: $("#hdEmpCode").val(),
            PayMode: value,
            AccName: $("#txtAccHolderName").val(),
            Bankname: $("#txtBankName").val(),
            accountno: $("#txtAccNumber").val(),
            ifsccode: $("#txtIFSCCode").val(),
            AccountType: $("#ddlAccountType").val()
        };
        $("#showSpinner").show();
        $.ajax({
            url: "/HRM/InserEmployeeDetails",
            type: "POST",
            contentType: false,
            processData: false,
            //data: dataobject,
            data: JSON.stringify(dataobject),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (r) {
                if (r.strId != '0') {
                    alert(r.msg);
                    window.location.href = "../HRM/EmployeeList";
                    $("#showSpinner").hide();
                }
                else {
                    alert('Server not Responding !!!');
                    $("#showSpinner").hide();
                }
            },
            error: function (XMLHttpRequest, textStatus, errorThrown) {
                alert('Please Check values Entered by you !!!');
                $("#showSpinner").hide();
            }
        });


    });
});

function ClearBankDetails() {
    $('#txtAccHolderName').val('');
    $('#txtBankName').val('');
    $('#txtAccNumber').val('');
    $('#txtIFSCCode').val('');   
}

function Salary() {
    var AnnualCTC = $('#txtAnnualCTC').val() != "" ? parseFloat($('#txtAnnualCTC').val()) : 0;
    var BasicPer = $('#txtBasicPer').val() != "" ? parseFloat($('#txtBasicPer').val()) : 0;
    var Basic_MonthlyAmount = $('#txtBasicMonthlyAmount').val() != "" ? parseFloat($('#txtBasicMonthlyAmount').val()) : 0;
    var BasicAnnualAmount = $('#lblAnnualAmount').text() != "" ? parseFloat($('#lblAnnualAmount').text()) : 0;
    var HRAPer = $('#txtHRAPer').val() != "" ? parseFloat($('#txtHRAPer').val()) : 0;
    var HRA_MonthlyAmount = $('#txtHRAMonthlyAmount').val() != "" ? parseFloat($('#txtHRAMonthlyAmount').val()) : 0;
    var HRAAnnaulAmount = $('#lblHRAAnnualAmount').text() != "" ? parseFloat($('#lblHRAAnnualAmount').text()) : 0;
    var CA_MonthlyAmount = $('#txtCAMonthlyAmount').val() != "" ? parseFloat($('#txtCAMonthlyAmount').val()) : 0;
    var CA_AnnualAmount = $('#lblCAAnnualAmount').text() != "" ? parseFloat($('#lblCAAnnualAmount').text()) : 0;

    var FA_MonthlyAmount = $('#txtFAMonthlyAmount').val() != "" ? parseFloat($('#txtFAMonthlyAmount').val()) : 0;
    var FA_AnnualAmount = $('#lblFAAnnualAmount').text() != "" ? parseFloat($('#lblFAAnnualAmount').text()) : 0;

    var BasicAnnualAmt = (AnnualCTC * BasicPer) / 100;
    var BasicMonthlyAmt = BasicAnnualAmt / 12;

    var HRAAnnualAmt = (BasicAnnualAmt * HRAPer) / 100;
    var HRAMonthlyAmt = HRAAnnualAmt / 12;

    $('#lblCAAnnualAmount').text((CA_MonthlyAmount * 12).toFixed(0));

    $('#txtHRAMonthlyAmount').val(HRAMonthlyAmt.toFixed(0));
    $('#lblHRAAnnualAmount').text(HRAAnnualAmt.toFixed(0));

    $('#txtBasicMonthlyAmount').val(BasicMonthlyAmt.toFixed(0));
    $('#lblAnnualAmount').text(BasicAnnualAmt.toFixed(0));

    var fAmt = AnnualCTC / 12;

    var totFixedAmtMonthly = fAmt - (BasicMonthlyAmt + HRAMonthlyAmt + CA_MonthlyAmount);

    var totFixedAmtAnnual = totFixedAmtMonthly * 12;
    //var totFixedAmtMonthly = totFixedAmtAnnual / 12;

    $('#txtFAMonthlyAmount').val(totFixedAmtMonthly.toFixed(0));
    $('#lblFAAnnualAmount').text(totFixedAmtAnnual.toFixed(0));

    var TotCTCAnnualy = (BasicAnnualAmt + HRAAnnualAmt + (CA_MonthlyAmount * 12) + totFixedAmtAnnual);
    var TotCTCMonthly = TotCTCAnnualy / 12;

    $('#lblTotalMonthly').text(TotCTCMonthly.toFixed(0));
    $('#lblTotalAnnualy').text(TotCTCAnnualy.toFixed(0));

}