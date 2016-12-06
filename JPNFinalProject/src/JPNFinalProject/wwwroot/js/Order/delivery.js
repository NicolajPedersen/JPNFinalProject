$("#search-parcel").click(function () {
    var term = $("#search-zip-input").val();
    $.ajax({
        type: "Post",
        url: "/Order/BusinessByPostalcode",
        dataType: 'html',
        contentType: 'application/json; charset=utf-8',
        data: JSON.stringify(term),
        success: function (data) {
            console.log('Success!', data);
            $('#container-sresults').html(data);
        },
        error: function (e) {
            console.log('Error!', e);
        }
    })
})
$("input[type=radio][name=parcel-pickup]").change(function () {
    $("#parcel-pickup").val(parcelPickup());
})
  function parcelPickup () {
    var parcelPickups = document.getElementsByName('parcel-pickup');
    for (var i = 0; i < parcelPickups.length; i++) {
        if (parcelPickups[i].checked) {
            return parcelPickups[i].value;
        }
  }
  };
