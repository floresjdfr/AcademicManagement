
$(document).ajaxStart(function () {
    $('.loader').removeClass('hidden');
}).ajaxStop(function () {
    $('.loader').addClass('hidden');
});