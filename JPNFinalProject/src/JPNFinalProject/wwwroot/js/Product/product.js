$(document).ready(function () {
    var productId; 
    $(document).on("click", ".basketbtn", function () {
        productId = this.id;
        addToBasket();
    });

    $("#barbering").click(function () {
        console.log($(this).closest("li").find("#subCategory").css("display"));
        if ($("#subCategory").css("display") == "none")
        {
            console.log("test1");
        }
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
                $(".navbar-collapse").find(".badge").html(result);
            }
        });
    }
});