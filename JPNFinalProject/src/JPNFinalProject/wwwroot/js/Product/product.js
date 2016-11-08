$(document).ready(function () {
    $('#adjust').click(function () {
        if ($('.extrasearch').css('display') == 'none')
        {
            $('.extrasearch').css('display', 'unset');
        }
        if ($('.extrasearch').css('display') == 'unset')
        {
            $('.extrasearch').css('display', 'none');
        }
    });
});