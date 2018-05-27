// Wrseite your JavaScript code.
var paymentForm = $('.payment-form');
var settlementForm = $('.SettlementForm');


$('.bill .btn-pay').on('click', function (evt) {
    evt.preventDefault();

    var billId = $(evt.currentTarget).parent().parent().find('.input-bill-id').val();

    paymentForm.find('.payment-bill-id').val(billId);

    //$.ajax({
    //    url: 'http://localhost:55869/Home/paybill',
    //    type: 'Post',
    //    data: paymentForm.serialize()
    //}).done(function (data) {
    //    $('.js-new-amount').html(data.newAmount);
    //}).fail(function () {
    //    alert('fail');
    //});
});


$('.payment-form .paymentbtn').on('click', function (evt) {
    evt.preventDefault();

    $.ajax({
        url: 'http://localhost:55869/Home/paybill',
        type: 'Post',
        data: {
            billId: $('#billId').val(),
            CreditcardNumber: $('#CreditcardNumber').val()
        }
    }).done(function (data) {
        $('.js-new-amount').html(data.billId),       
            window.location.reload();;
    }).fail(function () {
        alert('fail');
    });
});


$('.btn-cal').on('click', function (evt) {

    var elements = $('.billCheck:checked');
    var seletedBillIds = [];

    for (var i = 0; i < elements.length; i++) {
        seletedBillIds.push($(elements[i]).data('billid'));
        console.log(seletedBillIds);
    }

    $.ajax({
        url: 'http://localhost:55869/Home/CalculateSettlements',
        type: 'Post',
        data: {
            billId: seletedBillIds,
            SelectedInstallments: $('#Installments').val(),
            SelectedSettlementTypeId: $('#type').val()
        }

    }).done(function (data) {
        console.log(data);        
          
    }).fail(function () {
        window.location.reload();
    });
});










$('.btn-cal-setl').on('click', function (evt) {

    var elements = $('.billCheck:checked');
    var seletedBillIds = [];

    for (var i = 0; i < elements.length; i++) {
        seletedBillIds.push($(elements[i]).data('billid'));
        console.log(seletedBillIds);
    }

    $.ajax({
        url: 'http://localhost:55869/Home/AddSettlements',
        type: 'Post',
        data: {
            billId: seletedBillIds,
            SelectedInstallments: $('#Installments').val(),
            SelectedSettlementTypeId: $('#type').val()
        }

    }).done(function (data) {
        console.log(data),
            window.location.reload();
        }).fail(function () {
            window.location.reload();
    });
});





























//var elements = $('.billCheck:checked');
//var seletedBillIds = [];

//for (var i = 0; i < elements.length; i++) {
//        seletedBillIds.push($(elements[i]).data('billid'));
//        console.log(seletedBillIds, elements);
//} 

//data: {
//    billId: seletedbillIds

//}