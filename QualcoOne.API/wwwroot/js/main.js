
// Hide Mobile menu
function mobileMenuHide() {
    var windowWidth = $(window).width();
    if (windowWidth < 1024) {
        $('.header').addClass('mobile-menu-hide');
    }
}
// /Hide Mobile menu


// Mobile menu
$('.menu-toggle').on("click", function () {
    $('.header').toggleClass('mobile-menu-hide');
});

// Mobile menu hide on main menu item click
$('.site-main-menu').on("click", "a", function (e) {
    mobileMenuHide();
});

//On Window load & Resize
$(window)
    .on('load', function () { //Load
        // Animation on Page Loading
        $(".preloader").fadeOut("slow");

        // initializing page transition.
        var ptPage = $('.subpages');

        UpdatePayment()

    })
    .on('resize', function () { //Resize
        mobileMenuHide();


    });



//payment method open-close
jQuery('input[type="radio"]').click(function (e) {
    jQuery('.collapse').collapse('hide');
});


//disable payment - one to one

function UpdatePayment() {
    var bills = $("input.billCheck:checked").length;
    if (bills <1) {
        $('button.settlement').attr('disabled', true);
        $('#error').show();
     //   $('button.settlement').prop('checked', false);
       // $('.SettlementForm').collapse('hide');
    }

    else {
        $('button.settlement').attr('disabled', false);
        $('#error').hide();
    }
}

function SetInstallments() {
    var type = $('#type').val();
    var Installments = 24;
    if (type == 3 || type==4) Installments = 36;
    else if (type == 5) Installments = 48;
    $('#Installments').empty();
    for (var i = 3; i <= Installments; i += 3) {
        $('#Installments').append('<option value="' + i + '">' + i + '</option>');
    }
}