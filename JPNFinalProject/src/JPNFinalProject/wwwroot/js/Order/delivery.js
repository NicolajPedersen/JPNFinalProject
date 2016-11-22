$("#search-parcel").click(function () {
    var term = $("#search-zip-input").val();

    $.ajax({
        type: "POST",
        url: "/Order/SearchParcelPickups",
        data: JSON.stringify(term),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {

        },
    })
})
$("input[type=radio][name=parcel]").change(function () {
    $("#parcel-pickup").val(parcelPickup());
})
  function parcelPickup () {
    var parcelPickups = document.getElementsByName('parcel');
    for (var i = 0; i < parcelPickups.length; i++) {
        if (parcelPickups[i].checked) {
            return parcelPickups[i].value;
        }
    }
};
