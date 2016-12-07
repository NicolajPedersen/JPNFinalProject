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
$(':radio[name=parcel-pickup]').change(function () {
    console.log($(this).filter(':checked').val());
});

document.getElementById("delivery-submit").addEventListener("click", function () {
    document.getElementById("delivery-form").submit();
});

$(function () {
    $('body').on('change', ':radio[name=parcel-pickup]', function () {
        
        document.getElementById('parcel-pickup').value = $(this).filter(':checked').val();
    });
});