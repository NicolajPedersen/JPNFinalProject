$(document).ready(function () {
    $(document).on("click", "#productInfo #remove", function () {
        var index = $(this).parent().parent().attr("data-number");
        console.log(index);
        $("#productInfo").children().eq(index).remove();
    });

    $(document).on("click", "#quantity-picker > .down", function () {
        var value = parseInt($(this).parent().find("input").val());
        if (value > 0) {
            var value = value - 1;
            console.log(value);
            $(this).parent().find("input").val(value);
        }
    });

    $(document).on("click", "#quantity-picker > .up", function () {
        var value = parseInt($(this).parent().find("input").val());
        var value = value + 1;
        console.log(value);
        $(this).parent().find("input").val(value);
    });
});