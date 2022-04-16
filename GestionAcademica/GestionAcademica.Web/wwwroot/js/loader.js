window.addEventListener("load", function () {
    $(function () {
        $('.loader').addClass('hidden');
    });
});
$(document).ajaxStart(function () {
    $('.loader').removeClass('hidden');
}).ajaxStop(function () {
    $('.loader').addClass('hidden');
});