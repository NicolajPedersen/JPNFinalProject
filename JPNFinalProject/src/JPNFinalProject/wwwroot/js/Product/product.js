$(document).ready(function () {
    var productId; 
    $(document).on("click", ".basketbtn", function () {
        productId = this.id;
        addToBasket();
    });

    //$("#barbering").click(function () {
    //    console.log(window.location.href);
    //    if (window.location.href != "http://localhost:5000/Product/mainCategory/barbering")
    //    {
    //        console.log(window.location.href);
    //        window.location = window.location.href + "/mainCategory/barbering";
    //    }
    //});

    function addToBasket() {
        console.log(productId);
        $.ajax({
            type: "POST",
            url: '/Product/AddToBasket',
            data: JSON.stringify(productId),
            dataType: "json",
            traditional: true,
            contentType: "application/json; charset=utf-8",
            success: function (result) {
                $(".navbar-collapse").find(".badge").html(result);
            }
        });
    }
});