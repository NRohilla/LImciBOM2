$(document).ready(function () {

    const $tableID = $('#table');
    const $BTN = $('#export-btn');
    const $EXPORT = $('#export');
    const FinalAgreed = document.getElementById('FinalAgreedPrice').value;

    $('#DepositPerc').change(function () {
        var dperc = document.getElementById('DepositPerc').value;
        var pdperc = document.getElementById('PreDeliveryPerc').value;

        var Depositamt = (FinalAgreed / 100) * dperc;
        var PreDeliveryamt = (FinalAgreed / 100) * pdperc;

       $('#Deposit').text(Depositamt).formatCurrency();
       $('#PreDelivery').text(PreDeliveryamt).formatCurrency();

        if ((Depositamt + PreDeliveryamt) < FinalAgreed) {

            var FinalAmt = FinalAgreed - (Depositamt + PreDeliveryamt);
            $('#Final').text(FinalAmt).formatCurrency();
            var FinalPerc = (FinalAmt / FinalAgreed) * 100;
            $('#FinalPerc').text(FinalPerc).formatCurrency();

        }
        else {
            $('#Final').text("0").formatCurrency();
            $('#FinalPerc').text("0").formatCurrency();
        }

        var Fianlamt = FinalAgreed - (Depositamt + PreDeliveryamt);
    
 
       
       
    })


    $('#PreDeliveryPerc').change(function () {
        var dperc = document.getElementById('DepositPerc').value;
        var pdperc = document.getElementById('PreDeliveryPerc').value;

        var Depositamt = (FinalAgreed / 100) * dperc;
        var PreDeliveryamt = (FinalAgreed / 100) * pdperc;

        $('#Deposit').text(Depositamt).formatCurrency();
        $('#PreDelivery').text(PreDeliveryamt).formatCurrency();

        if ((Depositamt + PreDeliveryamt) < FinalAgreed) {

            var FinalAmt = FinalAgreed - (Depositamt + PreDeliveryamt);
            $('#Final').text(FinalAmt).formatCurrency();
            var FinalPerc = (FinalAmt / FinalAgreed) * 100;
            $('#FinalPerc').text(FinalPerc).formatCurrency();

        }
        else {
            $('#Final').text("0").formatCurrency();
            $('#FinalPerc').text("0").formatCurrency();
        }

        var Fianlamt = FinalAgreed - (Depositamt + PreDeliveryamt);
               

    })


    $('#btnSave').on('click', function (e) {

        var List = new Array();
        var Item = {};
        Item.BOMID = document.getElementById('BOMID').value;
        Item.OpportunityID = document.getElementById('OpportunityID').value;
        Item.DepositPerc = document.getElementById('DepositPerc').value;
        Item.Deposit = document.getElementById('Deposit').innerHTML;
        Item.PreDeliveryPerc = document.getElementById('PreDeliveryPerc').value;
        Item.PreDelivery = document.getElementById('PreDelivery').innerHTML;
        Item.FinalPerc = document.getElementById('FinalPerc').innerHTML;
        Item.Final = document.getElementById('Final').innerHTML;


        List.push(Item);

        $.ajax({
            type: "POST",
            url: "/Quote/SaveProjectFinancials",
            dataType: "json",
            data: JSON.stringify(List, getCircularReplacer()),
            contentType: "application/json; charset=utf-8",

            success: function (r) {
                if (r === '') {
                    SucessAlert();
                }
                else {
                    $('#Error').text(r)
                    ErrorAlert();
                }


            }
        })
    })

    //------Show the Messages
    function SucessAlert() {
        $('#Sucessful').fadeIn(1000);
        setTimeout(function () {
            $('.alert').fadeOut(1000);
        }, 5000);
    }
    function ErrorAlert() {
        $('#Error').fadeIn(1000);
        setTimeout(function () {
            $('.alert').fadeOut(1000);
        }, 5000);
    }


})