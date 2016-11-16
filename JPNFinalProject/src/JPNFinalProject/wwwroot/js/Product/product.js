$(document).ready(function () {
    var productId; 
    $(document).on("click", ".basketbtn", function () {
        productId = this.id;
        addToBasket();
    });



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
                console.log("number " + result);
                $(".navbar-collapse").find(".badge").html(result);
            }
        });
    }
});