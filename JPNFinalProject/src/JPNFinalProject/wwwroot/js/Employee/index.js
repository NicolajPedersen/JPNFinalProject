﻿$(document).ready(function () {

    let test = $.connection.test;
    let hub = $.connection.hub;

    hub.start().done(function () {
        test.server.signalRConnectionId("Admin", 1);
    });

    test.client.getConnectionId = function (msg) {
        id = msg;
        console.log("My id: " + id);
    };

    //hub.start();

    var orderIds = new Array();
    test.client.getAll = function (msg) {
        let p = msg;
        console.log(p);
        $.each(p, function () {
            console.log("Client id: " + this.Key);
            console.log("OrderId: " + this.Value.OrderId);
            orderIds.push(this.Value.OrderId);
        });

        getProducts();
    };

    //var orderIds = [1023, 1024];

    //console.log(orderIds);

    function getProducts () {
        $.ajax({
            type: "POST",
            url: "/Order/GetPersonByOrderId",
            data: JSON.stringify(orderIds),
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            success: function (data) {
                $.each(data, function () {
                    $(".container tbody").append(
                    "<tr>" + 
                        "<td id='customerName'>" + this.person.name + "</td>" +
                        "<td id='customerMail'>" + this.person.email + "</td>" +
                        "<td id='ordreNumber'>" + this.id + "</td>" +
                        "<td> <button id='viewProducts' type='button' class='btn btn-sm'>Vis Produkter</button> </td>" +
                    "</tr>"
                    );
                });

                //console.log($(".container tbody").find("tr"));
                //console.log(data);
            }
        });
    };


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


        console.log($("#productContainer").find("tbody").children().eq(indexInTable).find("#productId").html());
        console.log($("#productContainer").find("tbody").children().eq(indexInTable).find("#ordreNumber").html());

        product.push($("#productContainer").find("tbody").children().eq(indexInTable).find("#productId").html());
        product.push($("#productContainer").find("tbody").children().eq(indexInTable).find("#ordreNumber").html());

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

    $(document).on("click", "#viewProducts", function () {
        var indexInTable = $(this).parent().parent().index();

        var orderId = $(".container").find("tbody").children().eq(indexInTable).find("#ordreNumber").html();
        //console.log(orderId);
        $.ajax({
            type: "POST",
            url: '/Employee/ProductPartial',
            data: JSON.stringify(orderId),
            dataType: "html",
            contentType: "application/json; charset=utf-8",
            success: function (data) {
                //console.log('Success!', data);
                $('#productContainer').html(data);

                $("#productModal").modal('show');
            },
            error: function (e) {
                console.log('Error!', e);
            }
        });


    })

    $(document).on("click", "#modalClose", function () {
        $("#productModal").hide();

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
        $("#productContainer").find("tbody").children().eq(indexInTable).find("#putAside").attr("disabled", "disabled");
        $("#productContainer").find("tbody").children().eq(indexInTable).find("#outOfStock").attr("disabled", "disabled");

        var product = new Array();
        //var product = { Id: $(".container").find("tbody").children().eq(indexInTable).find("#productId").html(), OrderId: $(".container").find("tbody").children().eq(indexInTable).find("#ordreNumber").html()};

        product.push($("#productContainer").find("tbody").children().eq(indexInTable).find("#productId").html());
        product.push($("#productContainer").find("tbody").children().eq(indexInTable).find("#ordreNumber").html());

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