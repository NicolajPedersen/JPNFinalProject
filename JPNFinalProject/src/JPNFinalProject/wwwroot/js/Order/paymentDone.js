
$(document).ready(function () {
    console.log($("#productInfo > .row"));
    $("#productInfo > .row").each(function () {
        var productPrice = $(this).find("#productPrice").html();
        var quantity = $(this).find("#amount").html();
        var price = productPrice * quantity;
        $(this).find("#totalPrice").html(price);
    });

    //let order = $.connection.order;
    //let hub = $.connection.hub;
    //console.log(order);
    //console.log(hub);

    //order.client.sayHello = function (msg) {
    //    let p = $("<p/>").text(msg);
    //    $('.demo').append(p);
    //};

    //hub.start().done(function () {
    //    order.server.hello()

    //});
});