$(document).ready(function () {
    (function () {
        setSubtotalPrice();
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
            setSubtotalPrice();
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
        setSubtotalPrice();
    });
});

function setSubtotalPrice() {
    console.log($("#productInfo > .row"))
    var totalPrice = 0.0;
    $("#productInfo > .row").each(function () {
        //$(this).each(function() {console.log($(this).html())});
        console.log($(this).find("#totalPrice").html());
        var price = parseFloat($(this).find("#totalPrice").html());
        totalPrice = totalPrice + price;
    });
    console.log(parseFloat(totalPrice));
    $("#details #subtotalPrice").html(totalPrice);
}