$(document).ready(function () {
    $('#adjust').click(function () {
        if ($('.extrasearch').css('display') == 'none')
        {
            $('.extrasearch').css('display', 'unset');
        }
        else
        {
            $('.extrasearch').css('display', 'none');
        }
    });
});