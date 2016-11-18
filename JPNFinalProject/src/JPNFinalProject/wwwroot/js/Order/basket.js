$(document).ready(function () {
    (function () {
        setPrices();
    })();

    $(document).on("click", "#productInfo #remove", function () {
        var id = $(this).parent().parent().attr("id");
        $(this).parent().parent().remove();

        $.ajax({
            type: "POST",
            url: "/Product/RemoveAllFromBasket",
            data: JSON.stringify(id),
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            success: function (result) {
                $(".navbar-collapse").find(".badge").html(result);
            }
        });

        setPrices();
    });

    $(document).on("click", "#quantity-picker > .down", function () {
        var id = $(this).parent().parent().attr("id");
        var value = parseInt($(this).parent().find("input").val());
        if (value > 0) {
            var value = value - 1;
            $(this).parent().find("input").val(value);
            var productPrice = parseFloat($(this).parent().parent().children().find("#productPrice").html());
            var totalPrice = parseFloat($(this).parent().parent().children().find("#totalPrice").html());
            var price = totalPrice - productPrice;
            $(this).parent().parent().children().find("#totalPrice").html(price);

            setPrices();

            $.ajax({
                type: "POST",
                url: "/Product/RemoveFromBasket",
                data: JSON.stringify(id),
                dataType: "json",
                contentType: "application/json; charset=utf-8",
                success: function (result) {
                    $(".navbar-collapse").find(".badge").html(result);
                }
            });
        }
    });

    $(document).on("click", "#quantity-picker > .up", function () {
        var id = $(this).parent().parent().attr("id");
        var value = parseInt($(this).parent().find("input").val());
        var value = value + 1;
        $(this).parent().find("input").val(value);
        var productPrice = parseFloat($(this).parent().parent().children().find("#productPrice").html());
        var totalPrice = parseFloat($(this).parent().parent().children().find("#totalPrice").html());
        var price = totalPrice + productPrice;
        $(this).parent().parent().children().find("#totalPrice").html(price);

        setPrices();

        $.ajax({
            type: "POST",
            url: "/Product/AddToBasket",
            data: JSON.stringify(id),
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            success: function (result) {
                $(".navbar-collapse").find(".badge").html(result);
            }
        });
    });

    $(document).on("click", "#continueToDelivery", function () {
        $.ajax({
            type: "POST",
            url: "/Product/BasketCount",
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            success: function (result) {
                if (result !== 0) {
                    window.location = "Delivery";
                }
            }
        });
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