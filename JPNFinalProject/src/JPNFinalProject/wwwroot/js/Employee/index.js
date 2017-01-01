$(document).ready(function () {

    var test = $.connection.test;
    var hub = $.connection.hub;

    hub.start();

    var id;

    test.client.getConnectionId = function (msg) {
        id = msg;
        //console.log("My id: " + id);
        test.server.addAdmin(id, "Admin", 1);
    };

    var orderIds;
    var orders = new Array();
    test.client.getAll = function (msg) {
        orderIds = new Array();
        orders = msg;
        $.each(orders, function () {
            //console.log("Client id: " + this.Key);
            //console.log("OrderId: " + this.Value.OrderId);
            orderIds.push(this.Value.OrderId);
        });

        getProducts();
    };

    function getProducts () {
        $(".container tbody:first").empty();
        $.ajax({
            type: "POST",
            url: "/Order/GetPersonByOrderId",
            data: JSON.stringify(orderIds),
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            success: function (data) {
                $.each(data, function (i) {
                    $.each(orders, function (j) {
                        if (orders[j].Value.OrderId == data[i].id) {
                            $(".container tbody:first").append(
                                "<tr>" +
                                    "<td id='customerName'>" + data[i].person.name + "</td>" +
                                    "<td id='customerMail'>" + data[i].person.email + "</td>" +
                                    "<td id='ordreNumber'>" + data[i].id + "</td>" +
                                    "<td><button id='viewProducts' data-connectionId=" + orders[j].Key + " type='button' class='btn btn-sm'>Vis Produkter</button><div id='indicator'></div></td>" +
                                    "<td id='ordreStatus'></td>" +
                                "</tr>"
                            );

                            if (orders[j].Value.IsConnected == true) {
                                $(".container tbody:first").find('[data-connectionId=' + orders[j].Key + ']').parent().find("#indicator").css("background", "green");
                            }
                            else if (orders[j].Value.IsConnected == false) {
                                $(".container tbody:first").find('[data-connectionId=' + orders[j].Key + ']').parent().find("#indicator").css("background", "red");
                            }

                            if (orders[j].Value.OrderConfirmed == true) {
                                $(".container tbody:first").find('[data-connectionId=' + orders[j].Key + ']').parent().parent().find("#ordreStatus").append('<span class="glyphicon glyphicon-ok" aria-hidden="true"></span>');
                            }
                            else if (orders[j].Value.OrderConfirmed == false) {
                                $(".container tbody:first").find('[data-connectionId=' + orders[j].Key + ']').parent().parent().find("#ordreStatus").append('<span class="glyphicon glyphicon-remove" aria-hidden="true"></span>');
                            }
                        };
                    });
                });
            }
        });
    };

    var indexInTable;
    var isChecked = false;
    var customerInfo;

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
            contentType: "application/json; charset=utf-8"
        });

        //var connectionId;

        //var rows = $(".container tbody:first").find("tr").find("#ordreNumber");
        //$.each(rows, function () {
        //    if ($(this).html() === product[1]) {
        //        connectionId = $(this).parent().find("#viewProducts").attr("data-connectionId");
        //    };
        //});

        //console.log(connectionId);

        //var msg = "Produkt: " + product[0] + " i Order: " + product[1] + " er tilsidesat."

        //test.server.sendMessage(connectionId, msg);

        var item = $(this).closest("tr").find("#item").html();

        console.log(item);

        var indexInTable = $(this).parent().parent().index();
        console.log(indexInTable);
        customerInfo[indexInTable] = { ProductName: item, ProductId: product[0], PutASide: true };

        console.log(customerInfo);

        checkArray($("#productContainer").find("#sendMessage"));
    });

    $(document).on("click", "#outOfStock", function () {
        //console.log($(this).parent().parent().index());
        //console.log($(this).closest("tr").find("#ordreNumber").html());
        indexInTable = $(this).parent().parent().index();
        var item = $(this).closest("tr").find("#item").html();
        $("#SendMail #item").html(item);
        var ordreNumber = $(this).closest("tr").find("#ordreNumber").html();
        $("#SendMail #ordreNumber").html(ordreNumber);

        var orderId = $(this).parent().find("#ordreNumber").html();
        var productId = $(this).parent().find("#productId").html();
        //console.log(orderId);
        //var connectionId;
        
        //var rows = $(".container tbody:first").find("tr").find("#ordreNumber");
        //$.each(rows, function () {
        //    if ($(this).html() === orderId) {
        //        connectionId = $(this).parent().find("#viewProducts").attr("data-connectionId");
        //    };
        //});
        //console.log(connectionId);

        //var msg = "Dette produkt er ikke på lager";

        //test.server.sendMessage2(connectionId, productId, msg);

        console.log(item);

        var indexInTable = $(this).parent().parent().index();
        //customerInfo[indexInTable] = { ProductName: item, ProductId: productId, PutASide: false, AdminConnectionId: id };
        customerInfo[indexInTable] = { ProductName: item, ProductId: productId, PutASide: false };

        console.log(customerInfo);

        checkArray($("#productContainer").find("#sendMessage"));
    });

    $(document).on("click", "#viewProducts", function () {
        customerInfo = new Array();

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
                customerInfo.length = $(data).find("tbody tr").length;
                //console.log(customerInfo.length);
                console.log($("#productContainer").find("#sendMessage"));
                checkArray($("#productContainer").find("#sendMessage"));
            },
            error: function (e) {
                console.log('Error!', e);
            }
        });
    });

    $(document).on("click", "#modalClose", function () {
        $("#productModal").hide();
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
            contentType: "application/json; charset=utf-8"
        });
    });

    $(document).on("click", "#sendMessage", function () {
        var connectionId;
        var ordreId = $(this).parent().parent().parent().find("#ordreNumber").html();
        console.log(ordreId);
        var rows = $(".container tbody:first").find("tr").find("#ordreNumber");
        $.each(rows, function () {
            if ($(this).html() === ordreId) {
                connectionId = $(this).parent().find("#viewProducts").attr("data-connectionId");
            };
        });

        console.log(connectionId);

        test.server.sendMessage3(connectionId, customerInfo);

        $("#productModal").hide();
    });

    function checkArray(button) {
        var allAreSat;
        for (var i = 0; i < customerInfo.length; i++) {
            if (customerInfo[i] == null) {
                allAreSat = false;
            }
            else {
                allAreSat = true;
            }
        };
        console.log(allAreSat);
        console.log(button);
        if (allAreSat == false) {
            $(button).prop("disabled", true);
        }
        else {
            $(button).prop("disabled", false);
        }
    };
});