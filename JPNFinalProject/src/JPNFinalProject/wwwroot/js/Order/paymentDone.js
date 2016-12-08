$(function () {
    window.hu = function () {
        var hh = $.connection.hub;
        return hh;
    }
});

$(document).ready(function () {
    console.log(window.orderId);
    console.log(window.businessId);

    //console.log($("#productInfo > .row"));
    $("#productInfo > .row").each(function () {
        var productPrice = $(this).find("#productPrice").html();
        var quantity = $(this).find("#amount").html();
        var price = productPrice * quantity;
        $(this).find("#totalPrice").html(price);
    });

    var test = $.connection.test;
    var hub = $.connection.hub;

    $.connection.hub.start();

    $.connection.hub.start().done(function () {
        test.server.signalRConnectionId("User", 0);
    });

    var id;

    test.client.getConnectionId = function (msg) {
        id = msg;
        console.log(id);

        $.connection.hub.start().done(function () {
            test.server.addObject(id, { User: "User", BusinessId: window.businessId, OrderId: window.orderId });
        });
    };    

    test.client.getMessage = function (msg) {
        var message = msg;
        console.log("Message: " + message);
        $("#msg").text(message);
    }
});

//<input id="message" type="text" value="" />

//<select id="dropdown"></select>

//<button id="h" type="button" class="btn btn-sm btn-default">Send Message</button>

//@section scripts {
//    <script src="~/js/Product/product.js"></script>
//    <script src="~/lib/jquery/dist/jquery.js"></script>
//    <script src="~/js/SignalR/jquery.signalR-2.2.0.js"></script>
//    <script src="/signalr/hubs"></script>

//    <script>
//        let test = $.connection.test;
//        let hub = $.connection.hub;

//        hub.start().done(function () {
//            test.server.signalRConnectionId("Admin", 1);
//});

//        test.client.getConnectionId = function (msg) {
//            id = msg;
//            console.log("My id: " + id);
//};

//        hub.start();

//        test.client.getAll = function (msg) {
//            $("#dropdown").find("option").remove();
//            $("#dropdown").append("<option>Select something</option>");
//            let p = msg;
//            console.log(p);
//            $.each(p, function () {
//                console.log("Client id: " + this.Key);
//                $("#dropdown").append('<option value=' + this.Key + '>' + this.Key + '</option>');
//});
//};

//        hub.start();

//        var clientId;

//        var message;

//        $("#dropdown").change(function () {
//            clientId = $("#dropdown option:selected").html();
//            console.log(clientId);
//            message = $("#message").val();
//            console.log(message);
//});

//        $("#h").click(function () {
//            test.server.sendMessage(clientId, message);
//});

//    </script>
//}