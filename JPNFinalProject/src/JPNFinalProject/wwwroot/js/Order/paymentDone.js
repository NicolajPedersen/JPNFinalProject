$(document).ready(function () {
    console.log($("#productInfo > .row"));
    $("#productInfo > .row").each(function () {
        var productPrice = $(this).find("#productPrice").html();
        var quantity = $(this).find("#amount").html();
        var price = productPrice * quantity;
        $(this).find("#totalPrice").html(price);
    });
});