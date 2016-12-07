$(document).ready(function () {
    var indexInTable;
    var isChecked = false;
    $(document).on("click", "#putAside", function () {
        //console.log($(this).closest("tr").find("#doneOrNot"));

        console.log($(this).closest("tr").find("#stockAmount"));
        if ($(this).closest("tr").find("#stockAmount").html() <= 10 && !isChecked)
        {
            confirm("Databasen er muligvis ikke opdateret endnu, tjek om der er flere af varen: " + $(this).closest("tr").find("#item").html());
            isChecked = true;
        }
        else
        {
            $(this).closest("tr").find("#doneOrNot").attr("checked", "checked");
            $(this).attr("disabled", "disabled");
            $(this).closest("tr").find("#outOfStock").attr("disabled", "disabled");
        }

        indexInTable = $(this).parent().parent().index();

        var product = new Array();

        console.log("Pro = " + $(".container").find("tbody").children().eq(indexInTable).find("#productId").html());
        console.log("Orde = " + $(".container").find("tbody").children().eq(indexInTable).find("#ordreNumber").html());

        product.push($(".container").find("tbody").children().eq(indexInTable).find("#productId").html());
        product.push($(".container").find("tbody").children().eq(indexInTable).find("#ordreNumber").html());

        console.log("ProductID = " + product[0]);

        console.log("OrderID = " + product[1]);

        $.ajax({
            type: "POST",
            url: '/Employee/PutAside',
            data: JSON.stringify(product),
            dataType: "json",
            traditional: true,
            contentType: "application/json; charset=utf-8",
        });

    });

    $(document).on("click", "#outOfStock", function () {
        //console.log($(this).parent().parent().index());
        //console.log($(this).closest("tr").find("#ordreNumber").html());
        indexInTable = $(this).parent().parent().index();
        var item = $(this).closest("tr").find("#item").html();
        $("#SendMail #item").html(item);
        var ordreNumber = $(this).closest("tr").find("#ordreNumber").html();
        $("#SendMail #ordreNumber").html(ordreNumber);
    });

    $(document).on("click", "#SendMail #send", function () {
        //console.log($(".container").find("tbody").children());
        $(".container").find("tbody").children().eq(indexInTable).find("#putAside").attr("disabled", "disabled");
        $(".container").find("tbody").children().eq(indexInTable).find("#outOfStock").attr("disabled", "disabled");

        var product = new Array();
        //var product = { Id: $(".container").find("tbody").children().eq(indexInTable).find("#productId").html(), OrderId: $(".container").find("tbody").children().eq(indexInTable).find("#ordreNumber").html()};

        product.push($(".container").find("tbody").children().eq(indexInTable).find("#productId").html());
        product.push($(".container").find("tbody").children().eq(indexInTable).find("#ordreNumber").html());

        //console.log("ProductID = " + product.ProductId);

        //console.log("OrderID = " + product.OrderId);

        $.ajax({
            type: "POST",
            url: '/Employee/NotInStock',
            data: JSON.stringify(product),
            dataType: "json",
            traditional: true,
            contentType: "application/json; charset=utf-8",
        });

    });


});