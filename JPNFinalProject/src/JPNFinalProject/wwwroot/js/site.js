$(document).ready(function () {
    $.ajax({
        type: "POST",
        url: '/Product/BasketCount',
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function (result) {
            $(".navbar-collapse").find(".badge").html(result);
        }
    });

    $("#addRandom").click(function () {
        $.ajax({
            type: "POST",
            url: '/Home/AddProduct',
            dataType: "json",
            contentType: "application/json; charset=utf-8",
        });
    });
    $("#addRandomCategory").click(function () {
        $.ajax({
            type: "POST",
            url: '/Home/AddRandomCategory',
            dataType: "json",
            contentType: "application/json; charset=utf-8",
        });
    });
});
