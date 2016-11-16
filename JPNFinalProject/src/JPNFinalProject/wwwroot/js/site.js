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
});
