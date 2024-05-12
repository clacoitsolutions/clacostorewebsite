
$(document).ready(function () {
    $('select').select2();
    //$('#txtmfgDate').datepicker({
    //    format: 'dd-MM-yyyy'
    //});
    //$('#txtExpiryDate').datepicker({
    //    format: 'dd-MM-yyyy'
    //});
    $("#tblItemHead").DataTable();
    var result = [];
    var iterations = eval($('#itemlist').val());
    //var result = '';
    //for (var i = 0; i < iterations.length; i++) {
    //    //result = result + '<option value="' + iterations[i].ItemCode + '">' + iterations[i].ItemName + '</option>';
    //    result.push(iterations[i].ItemCode + ':' + minute)
    //}

    //for (var i = 0; i < iterations.length; i++) {
    //    var SrNo = iterations[i].SrNo;
    //    var ItemName = iterations[i].ItemName;
    //    //result.push(SrNo);
    //    //result.push(':');
    //    result.push(SrNo + '' + ':' + '' + ItemName);
    //}

    //$('#txtdate').datepicker({
    //    format: 'dd-MM-yyyy'
    //});


});


function calcRate() {
    var PurchRate = $("#txtPurchaseRate").val() != "" ? parseFloat($("#txtPurchaseRate").val()) : 0;
    var MRP = $("#txtMRP").val() != "" ? parseFloat($("#txtMRP").val()) : 0;
    var IGST = $("#txtIGSt").val() != "" ? parseFloat($("#txtIGSt").val()) : 0;

    var cgst = IGST / 2;
    var sgst = IGST / 2;
    var Rate = (MRP / ((100 + IGST) / 100));
    $("#txtcgst").val(cgst.toFixed(2));
    $("#txtsgst").val(sgst.toFixed(2));
    $("#txtRate").val(Rate.toFixed(2));

}

function InsertItemHead() {
    debugger;
    var itmtype = null;
    debugger
    var type = $("#AccountType").val();
    if ($("#chkcommon").is(":checked")) {
        itmtype = "Common Good";
    }
    else if (type == "Inventory") {
        itmtype = "Finished Good";
    }
    else {
        itmtype = "Raw Item";
    }
    if ($("#ddlItemGroup").val() == "0") {
        alert("Select Item Group !!!");
        $("#ddlItemGroup").focus();
        return;
    }
    if ($("#txtProductName").val() == "") {
        alert("Enter Product Name !!!");
        $("#txtProductName").focus();
        return;
    }
    //if ($("#txtHsnCode").val() == "") {
    //    alert("Enter HSN Code !!!");
    //    $("#txtHsnCode").focus();
    //    return;
    //}
    if ($("#AccountType").val() != 'Bundle') {
        if ($("#txtIGSt").val() == "") {
            alert("Enter IGST % !!!");
            $("#txtIGSt").focus();
            return;
        }
    }
    if ($("#ddlUOMLoose").val() == "" || $("#ddlUOMLoose").val() == "0") {
        alert("Please Select UOM !!!");
        $("#ddlUOMLoose").focus();
        return;
    }
    //if ($("#AccountType").val() == 'Inventory') {
    //    if ($("#txtInitialQty").val() == "") {
    //        alert("Enter Initial quantity on hand!!!");
    //        $("#txtInitialQty").focus();
    //        return;
    //    }
    //    if ($("#txtdate").val() == "") {
    //        alert("Enter As of date!!!");
    //        $("#txtdate").focus();
    //        return;
    //    }
    //}
    //if ($("#AccountType").val() == 'Service') {
    //    if ($("#txtServicetype").val() == "") {
    //        alert("Enter Service type!!!");
    //        $("#txtServicetype").focus();
    //        return;
    //    }
    //}
    var ItemCodenew = '';
    var arr = [];
    $("#inlineeditable tbody tr").each(function () {
        var row = $(this);
        var ItemCode = row.find('select').eq(0).val();
        var Qty = row.find('input').eq(1).val();
        itemNew = {};
        itemNew["ItemCode"] = ItemCode;
        itemNew["InitialQty"] = Qty;
        arr.push(itemNew);
        if (ItemCode == '0') {

            ItemCodenew = 'select';
        }
        if (Qty == '0') {
            ItemCodenew = 'Qty';
        }
    });
    if ($("#AccountType").val() == 'Bundle') {
        if (ItemCodenew == 'select') {
            alert('Please Choose Products/services');
            return false
        }
        if (ItemCodenew == 'Qty') {
            alert('Please Choose Qty');
            return false
        }
    }

    var pur_taxIncExcl = $("input:radio[name=radioInline0]:checked").val();
    var Sale_taxIncExcl = $("input:radio[name=radioInline1]:checked").val();
    var dataobject = {
        GroupCode: $("#ddlItemGroup").val(),
        ItemName: $("#txtProductName").val(),
        BatchNo: $("#txtBatchNo").val(),
        HSNCode: $("#txtHsnCode").val(),
        MRP: $("#txtMRP").val(),
        GSTPer: $("#txtIGSt").val(),
        CGSTPer: $("#txtcgst").val(),
        SGSTPer: $("#txtsgst").val(),
        mDate: $("#txtmfgDate").val() != "" ? $("#txtmfgDate").val() : "01/01/1900",
        eDate: $("#txtExpiryDate").val() != "" ? $("#txtExpiryDate").val() : "01/01/1900",
        Action: "1",
        ItemBarCode: $("#txtBarCode").val(),
        PurchaseRate_Bulk: $("#txtPurchaseRateBulk").val() != "" ? parseFloat($("#txtPurchaseRateBulk").val()) : 0,
        PurchaseRate_Loose: $("#txtPurchaseRateLoose").val() != "" ? parseFloat($("#txtPurchaseRateLoose").val()) : 0,
        SaleRate_Bulk: $("#txtSaleRateBulk").val() != "" ? parseFloat($("#txtSaleRateBulk").val()) : 0,
        SaleRate_Loose: $("#txtSaleRateLoose").val() != "" ? parseFloat($("#txtSaleRateLoose").val()) : 0,
        StorePrice: $("#txtStorePrice").val() != "" ? parseFloat($("#txtStorePrice").val()) : 0,
        OnlinePrice: $("#txtOnlinePrice").val() != "" ? parseFloat($("#txtOnlinePrice").val()) : 0,
        BulkUOM: $("#ddlUOMBulk").val(),
        LooseUOM: $("#ddlUOMLoose").val(),
        BulkUOMQty: $("#txtBulkUnitQty").val() != "" ? parseFloat($("#txtBulkUnitQty").val()) : 0,
        Purchase_taxIncludeExclude: pur_taxIncExcl,
        Sale_taxIncludeExclude: Sale_taxIncExcl,
        InitialQty: $("#txtInitialQty").val() == "" ? 0: $("#txtInitialQty").val(),
        LowStock: $("#txtLowStock").val() == "" ? 0 : $("#txtLowStock").val(),
        EntryDate: $("#txtdate").val() != "" ? $("#txtdate").val() : "01/01/1900",
        AccountType: itmtype,
        Servicetype: $("#txtServicetype").val(),
        BundleList: arr,
    };
    $("#showSpinner").show();
    $.ajax({
        url: "/Master/InsertItemHead",
        type: "POST",
        contentType: false,
        processData: false,
        //data: dataobject,
        data: JSON.stringify(dataobject),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (r) {
            if (r != '0') {
                alert(r);
                location.reload();
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
}

function UpdateItemHead() {
    debugger;
    if ($("#hdItemCode").val() == "") {
        alert("Product Not Found !!!");
        return;
    }
    if ($("#ddlItemGroup").val() == "0") {
        alert("Select Item Group !!!");
        $("#ddlItemGroup").focus();
        return;
    }
    if ($("#txtProductName").val() == "") {
        alert("Enter Product Name !!!");
        $("#txtProductName").focus();
        return;
    }
    if ($("#txtHsnCode").val() == "") {
        alert("Enter HSN Code !!!");
        $("#txtHsnCode").focus();
        return;
    }
    if ($("#AccountType").val() != 'Bundle') {
        if ($("#txtIGSt").val() == "") {
            alert("Enter IGST % !!!");
            $("#txtIGSt").focus();
            return;
        }
    }
    if ($("#AccountType").val() == 'Inventory') {
        if ($("#txtInitialQty").val() == "") {
            alert("Enter Initial quantity on hand!!!");
            $("#txtInitialQty").focus();
            return;
        }
        if ($("#txtdate").val() == "") {
            alert("Enter As of date!!!");
            $("#txtdate").focus();
            return;
        }
    }
 
    //if ($("#AccountType").val() == 'Service') {
    //    if ($("#txtServicetype").val() == "") {
    //        alert("Enter Service type!!!");
    //        $("#txtServicetype").focus();
    //        return;
    //    }
    //}
    var pur_taxIncExcl = $("input:radio[name=radioInline0]:checked").val();
    var Sale_taxIncExcl = $("input:radio[name=radioInline1]:checked").val();
    var ItemCodenew = '';
    var arr = [];
    $("#inlineeditable tbody tr").each(function () {
        var row = $(this);
        var ItemCode = row.find('select').eq(0).val();
        var Qty = row.find('input').eq(1).val();
        itemNew = {};
        itemNew["ItemCode"] = ItemCode;
        itemNew["InitialQty"] = Qty;
        arr.push(itemNew);
        if (ItemCode == '0') {

            ItemCodenew = 'select';
        }
        if (Qty == '0') {
            ItemCodenew = 'Qty';
        }
    });
    if ($("#AccountType").val() == 'Bundle') {
        if (ItemCodenew == 'select') {
            alert('Please Choose Products/services');
            return false
        }
        if (ItemCodenew == 'Qty') {
            alert('Please Choose Qty');
            return false
        }
    }
    var itmtype = null;
    debugger
    var type = $("#AccountType").val();
    if ($("#chkcommon").is(":checked")) {
        itmtype = "Common Good";
    }
    else if (type == "Inventory") {
        itmtype = "Finished Good";
    }
    else {
        itmtype = "Raw Item";
    }
    var dataobject = {
        ItemCode: $("#hdItemCode").val(),
        GroupCode: $("#ddlItemGroup").val(),
        ItemName: $("#txtProductName").val(),
        BatchNo: $("#txtBatchNo").val(),
        HSNCode: $("#txtHsnCode").val(),
        MRP: $("#txtMRP").val(),
        GSTPer: $("#txtIGSt").val(),
        CGSTPer: $("#txtcgst").val(),
        SGSTPer: $("#txtsgst").val(),
        mDate: $("#txtmfgDate").val() != "" ? $("#txtmfgDate").val() : "01/01/1900",
        eDate: $("#txtExpiryDate").val() != "" ? $("#txtExpiryDate").val() : "01/01/1900",
        Action: "2",
        ItemBarCode: $("#txtBarCode").val(),
        PurchaseRate_Bulk: $("#txtPurchaseRateBulk").val() != "" ? parseFloat($("#txtPurchaseRateBulk").val()) : 0,
        PurchaseRate_Loose: $("#txtPurchaseRateLoose").val() != "" ? parseFloat($("#txtPurchaseRateLoose").val()) : 0,
        SaleRate_Bulk: $("#txtSaleRateBulk").val() != "" ? parseFloat($("#txtSaleRateBulk").val()) : 0,
        SaleRate_Loose: $("#txtSaleRateLoose").val() != "" ? parseFloat($("#txtSaleRateLoose").val()) : 0,
        StorePrice: $("#txtStorePrice").val() != "" ? parseFloat($("#txtStorePrice").val()) : 0,
        OnlinePrice: $("#txtOnlinePrice").val() != "" ? parseFloat($("#txtOnlinePrice").val()) : 0,
        BulkUOM: $("#ddlUOMBulk").val(),
        LooseUOM: $("#ddlUOMLoose").val(),
        BulkUOMQty: $("#txtBulkUnitQty").val() != "" ? parseFloat($("#txtBulkUnitQty").val()) : 0,
        Purchase_taxIncludeExclude: pur_taxIncExcl,
        Sale_taxIncludeExclude: Sale_taxIncExcl,
        InitialQty: $("#txtInitialQty").val(),
        LowStock: $("#txtLowStock").val(),
        EntryDate: $("#txtdate").val(),
        AccountType: itmtype,
        Servicetype: $("#txtServicetype").val(),
        BundleList: arr,
    };
    $("#showSpinner").show();
    $.ajax({
        url: "/Master/InsertItemHead",
        type: "POST",
        contentType: false,
        processData: false,
        //data: dataobject,
        data: JSON.stringify(dataobject),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (r) {
            if (r != '0') {
                alert(r);
                location.reload();
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
}

function GetDetailsForEdit(ItemCode) {
    var dataobject = {
        ItemCode1: ItemCode,
        AccountType: $('#AccountType').val(),
    };
    $.ajax({
        type: "POST",
        url: "/Master/GetItemHeadDetailsForEdit",
        data: dataobject,
        dataType: "json",
        success: function (r) {
            if (r != null) {
                $("#itemlist").val(r.ActiveStatus);
                $('#AccountType').val(r.AccountType);
                $("#txtProductName").val(r.ItemName);
                $("#hdItemCode").val(r.ItemCode);
                $("#txtBatchNo").val(r.BatchNo);
                $("#txtHsnCode").val(r.HSNCode);
                $('#txtMRP').val(r.MRP);
                $('#txtIGSt').val(r.GSTPer);
                $('#txtcgst').val(r.CGSTPer);
                $('#txtsgst').val(r.SGSTPer);
                $('#txtmfgDate').val(r.mDate);
                $('#txtExpiryDate').val(r.eDate);
                $('#ddlItemGroup').val(r.GroupCode).trigger('change');
                $('#txtBarCode').val(r.ItemBarCode);
                $('#txtPurchaseRateBulk').val(r.PurchaseRate_Bulk);
                $('#txtPurchaseRateLoose').val(r.PurchaseRate_Loose);
                $('#txtSaleRateBulk').val(r.SaleRate_Bulk);
                $('#txtSaleRateLoose').val(r.SaleRate_Loose);
                $('#txtStorePrice').val(r.StorePrice);
                $('#txtOnlinePrice').val(r.OnlinePrice);
                $('#ddlUOMBulk').val(r.BulkUOM).trigger('change');
                $('#ddlUOMLoose').val(r.LooseUOM).trigger('change');
                $('#txtBulkUnitQty').val(r.BulkUOMQty);
                $("#txtInitialQty").val(r.InitialStockQty);
                $("#txtLowStock").val(r.LowStockAlert);
                $("#txtdate").val(r.EntryDate);
                if (r.Purchase_taxIncludeExclude == "Include") {
                    $("#inlineRadio1").attr("checked", "checked");
                }
                else {
                    $("#inlineRadio2").attr("checked", "checked");
                }
                if (r.Sale_taxIncludeExclude == "Include") {
                    $("#inlineRadio3").attr("checked", "checked");
                }
                else {
                    $("#inlineRadio4").attr("checked", "checked");
                }
                //$("input:radio[name=radioInline0]").val(r.Purchase_taxIncludeExclude);
                //$("input:radio[name=radioInline1]").val(r.Sale_taxIncludeExclude);

                $("#txtProductName").focus();
                $("#btnSave").hide();
                $("#btnUpdate").show();
                $("#btnCancel").show();
                var html = $("#inlineeditable tbody").find('tr').eq(0).find('td').eq(1).html()
                $('#inlineeditable tbody').empty();
                var iterations = eval($('#itemlist').val());
                var result = '';
                for (var i = 0; i < iterations.length; i++) {
                    var roi = $("#inlineeditable tbody").find('tr').length + 1;
                    //result = result + '<option value="' + iterations[i].ItemCode + '">' + iterations[i].ItemName + '</option>';
                    $('#inlineeditable tbody').append('<tr>' +
                                '<td>' + roi + '</td>' +
                                '<td>' + html + '</td>' +
                                '<td>1</td>' +
                                '<td><a href="#" class="btn" onclick="removeBuddlerow(this)"><i class="mdi mdi-delete" style="color:red"></i></a></td>' +
                           ' </tr>');
                    var indexji = $('#inlineeditable tbody').find('tr').length - 1
                    $('#inlineeditable tbody').find('tr').eq(indexji).find('select').val(iterations[i].ItemCode).trigger('change');
                    $("#inlineeditable").Tabledit({
                        inputClass: "form-control form-control-sm",
                        editButton: !1,
                        deleteButton: !1,
                        columns: {
                            identifier: [0, "id"],
                            //editable: [[1, "col1", '{"1": "Red", "2": "Green", "3": "Blue"}'],
                            //    [2, "col2"]]
                            editable: [
                             [2, "col2"]]
                        }
                    })
                }
            }
            else {
                alert("Item Details Not Found !!!");
            }
        }
    });
}
function CancelUpdate() {
    location.reload();
}
function DeleteItemHeadDetails(ItemCode) {
    var x = confirm("Are you sure you want to delete?");
    if (x) {
        var dataobject = {
            ItemCode1: ItemCode
        };
        $.ajax({
            type: "POST",
            url: "/Master/DeleteItemHeadDetails",
            data: dataobject,
            dataType: "json",
            success: function (r) {
                if (r.strId == "1") {
                    alert(r.msg);
                    location.reload();
                }
                else {
                    alert(r.msg);
                }
            }
        });
    }
    else {
        return false;
    }
}

function ChangeStatus(pCode) {
    var mm = confirm('are you sure you want to change the status?');
    if (mm) {
        var data = new FormData;
        data.append("Action", '4');
        data.append("ItemCode", pCode);
        $.ajax({
            url: "../Ecommarce/DeleteProductDetails",
            type: "POST",
            contentType: false,
            processData: false,
            data: data,
            success: function (r) {
                if (r.strId == "1") {
                    alert("Status changed successfully!!");
                    location.reload();
                }
                else {
                    alert(r.msg);
                    $("#showSpinner").hide();
                }
            }
        });
    }
}

function choosingmethod(data) {
    debugger;

    if (data == 'Back') {
        $('#chooseingDiv').show();
        $('.mainDiv').hide();
    }
    else {
        $('#chooseingDiv').hide();
        $('.mainDiv').show();
        $('#AccountType').val(data);
        $('.clsbundle').hide();
        $('.Servicetype').hide();
        $('.nonInventory').hide();
        if (data == 'Inventory') {
            $('.nonInventory').show();
        }
            //else if (data == 'Service') {
            //    $('.Servicetype').show();
            //}

        else if (data == 'Bundle') {
            $('.clsbundle').show();
            $('.clsnonbundle').hide();

            $("#inlineeditable").Tabledit({
                inputClass: "form-control form-control-sm",
                editButton: !1,
                deleteButton: !1,
                columns: {
                    identifier: [0, "id"],
                    //editable: [[1, "col1", '{"1": "Red", "2": "Green", "3": "Blue"}'],
                    //    [2, "col2"]]
                    editable: [
                     [2, "col2"]]
                }
            })
        }
        else {
            $('.clsnonbundle').show();
            $('.nonInventory').hide();
        }
        var inType = null;
        if (data == "Inventory") {
            inType = "Finished Good";
        }
        else {
            inType = "Raw Item";
        }
        $.ajax({
            url: "/Master/CreateItemHead",
            type: "POST",
            data: { 'AccountType': inType },
            datatype: "json",
            success: function (data1) {
                var htmlNew = data1;
                $('#divmainheadtbl').html($(htmlNew).find('#divmainheadtbl2'));
                $("#tblItemHead").DataTable().destroy();
                $("#tblItemHead").DataTable();
            }
        });
    }

}

function allowonlyDegitandDot() {
    if (event.which < 46 || event.which >= 58 || event.which == 47) {
        event.preventDefault();
    }

    if (event.which == 46 && $(this).val().indexOf('.') != -1) {
        this.value = '';
    }
}
var rowno = 1;
function addBuddlerow() {

    rowno = $("#inlineeditable tbody").find('tr').length + 1;
    $('#inlineeditable').append('<tr>' +
                                '<td>' + rowno + '</td>' +
                                '<td>' + $("#inlineeditable tbody").find('tr').eq(0).find('td').eq(1).html() + '</td>' +
                                '<td>1</td>' +
                                '<td><a href="#" class="btn" onclick="removeBuddlerow(this)"><i class="mdi mdi-delete" style="color:red"></i></a></td>' +
                           ' </tr>');


    $("#inlineeditable").Tabledit({
        inputClass: "form-control form-control-sm",
        editButton: !1,
        deleteButton: !1,
        columns: {
            identifier: [0, "id"],
            //editable: [[1, "col1", '{"1": "Red", "2": "Green", "3": "Blue"}'],
            //    [2, "col2"]]
            editable: [
             [2, "col2"]]
        }
    })
}

//function bindQuantity(data) {
//    var row = $(data);
//}

function removeBuddlerow(data) {
    var row = $(data).closest("TR");
    var name = $("TD", row).eq(0).text();
    if (confirm("Do you want to delete")) {
        //Get the reference of the Table.
        var table = $("#inlineeditable")[0];
        //Delete the Table row using it's Index.
        table.deleteRow(row[0].rowIndex);

    }
}