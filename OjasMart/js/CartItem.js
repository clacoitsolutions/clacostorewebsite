var cart = {
    'add': function (product_id, quantity) {
        addProductNotice('Product added to shopping Cart', '', '');
    }
}

//var wishlist = {
//    'add': function (product_id) {
//        addProductNotice('Product added to Wishlist', '<img src="image/demo/shop/product/e11.jpg" alt="">', '<h3>You must <a href="#">login</a>  to save <a href="#">Apple Cinema 30"</a> to your <a href="#">wish list</a>!</h3>', 'success');
//    }
//}
//var compare = {
//    'add': function (product_id) {
//        addProductNotice('Product added to compare', '<img src="image/demo/shop/product/e11.jpg" alt="">', '<h3>Success: You have added <a href="#">Apple Cinema 30"</a> to your <a href="#">product comparison</a>!</h3>', 'success');
//    }
//}

/* ---------------------------------------------------
    jGrowl – jQuery alerts and message box
-------------------------------------------------- */
function addProductNotice(title, thumb, text, type) {
    $.jGrowl.defaults.closer = false;
    //Stop jGrowl
    //$.jGrowl.defaults.sticky = true;
    var tpl = thumb + '<h3>' + text + '</h3>';
    $.jGrowl(tpl, {
        life: 4000,
        header: title,
        speed: 'slow',
        theme: type
    });
}

function AddToCartfun(pro) {
    debugger
    var html = '';
    // window.location = '/AddToCart/ShowCart' + pro + '';
    $.ajax({
        url: "/AddToCart/ShowCart",
        type: "POST",
        data: { ProductId: pro, IPAddress: $('#hiddenipadd').val() },
        datatype: "json",
        success: function (data) {
            html = data
            $('#cart').html($(html).find('#cart'));
            $('#divHeadCount').html($(html).find('#divHeadCount'));
            //var serial = 0;
            //var ProductName = $(html).find('.cart_product_name')
            //ProductName = ProductName[0].innerHTML;
            //$.each(list, function (i, datalist) {
            //    // serial++;
            //    ProductName = datalist[0].ProductTitle
            //});
            cart.add(pro);
            //$('.items_cart').html(serial);
        },
        error: function () {
            alert('Error!')
            //cart.add('69');
        }
    });
};

