$(document).ready(function () {
    console.log($("#productInfo > .row"));
    $("#productInfo > .row").each(function () {
        var productPrice = $(this).find("#productPrice").html();
        console.log(productPrice);
        var quantity = $(this).find("#amount").html();
        console.log(quantity);
        var price = productPrice * quantity;
        console.log(price);
        $(this).find("#totalPrice").html(price);
    });
});