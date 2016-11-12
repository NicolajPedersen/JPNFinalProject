$(document).ready(function () {
    var indexInTable;
    $(document).on("click", "#putAside", function () {
        console.log($(this).closest("tr").find("#doneOrNot"));
        $(this).closest("tr").find("#doneOrNot").attr("checked", "checked");
        $(this).attr("disabled", "disabled");
        $(this).closest("tr").find("#outOfStock").attr("disabled", "disabled");
    });

    $(document).on("click", "#outOfStock", function () {
        console.log($(this).parent().parent().index());
        console.log($(this).closest("tr").find("#ordreNumber").html());
        indexInTable = $(this).parent().parent().index();
        var item = $(this).closest("tr").find("#item").html();
        $("#SendMail #item").html(item);
        var ordreNumber = $(this).closest("tr").find("#ordreNumber").html();
        $("#SendMail #ordreNumber").html(ordreNumber);
    });

    $(document).on("click", "#SendMail #send", function () {
        console.log($(".container").find("tbody").children());
        $(".container").find("tbody").children().eq(indexInTable).find("#putAside").attr("disabled", "disabled");
        $(".container").find("tbody").children().eq(indexInTable).find("#outOfStock").attr("disabled", "disabled");
    });
});