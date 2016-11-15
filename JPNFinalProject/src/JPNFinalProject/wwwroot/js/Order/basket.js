$(document).ready(function () {
    (function () {
        setPrices();
    })();

    $(document).on("click", "#productInfo #remove", function () {
        $(this).parent().parent().remove();
    });

    $(document).on("click", "#quantity-picker > .down", function () {
        var value = parseInt($(this).parent().find("input").val());
        if (value > 0) {
            var value = value - 1;
            $(this).parent().find("input").val(value);
            var productPrice = parseFloat($(this).parent().parent().children().find("#productPrice").html());
            var totalPrice = parseFloat($(this).parent().parent().children().find("#totalPrice").html());
            var price = totalPrice - productPrice;
            $(this).parent().parent().children().find("#totalPrice").html(price);
            setPrices();
        }
    });

    $(document).on("click", "#quantity-picker > .up", function () {
        var value = parseInt($(this).parent().find("input").val());
        var value = value + 1;
        $(this).parent().find("input").val(value);
        var productPrice = parseFloat($(this).parent().parent().children().find("#productPrice").html());
        var totalPrice = parseFloat($(this).parent().parent().children().find("#totalPrice").html());
        var price = totalPrice + productPrice;
        $(this).parent().parent().children().find("#totalPrice").html(price);
        setPrices();
    });
});

function setPrices() {
    var subtotalPrice = 0.00;
    $("#productInfo > .row").each(function () {
        var productPrice = $(this).find("#productPrice").html();
        var quantity = $(this).find(".quantity").val();
        var price = productPrice * quantity;
        $(this).find("#totalPrice").html(price);
        subtotalPrice = subtotalPrice + price;
    });
    var VATFromPrice = (subtotalPrice / 100) * 25;
    var priceWithVAT = subtotalPrice + VATFromPrice;
    $("#details #subtotalPrice").html(subtotalPrice.toFixed(2));
    $("#details #totalPrice").html(priceWithVAT.toFixed(2));
    $("#details #VATFromPrice").html(VATFromPrice.toFixed(2));
    $("#totals #totalPrice").html(priceWithVAT.toFixed(2));
}