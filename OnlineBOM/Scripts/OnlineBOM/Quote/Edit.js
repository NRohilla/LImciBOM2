

$(document).ready(function () {

    $('#btnSaveAssembly').on('click', function (e) {
        var List = new Array();
        var Item = {};
        Item.QuoteNo = document.getElementById('QuoteNo').value;
        Item.CompanyName = document.getElementById('CompanyName').value;
        Item.Representative = document.getElementById('Representative').value;
        Item.CustomerType = document.getElementById('CustomerType').value;
        Item.DeliveryDate = document.getElementById('DeliveryDate').value;
        Item.PONumber = document.getElementById('PONumber').value;
        Item.Authorisation = document.getElementById('Authorisation').value;
        Item.Campaign = document.getElementById('Campaign').value;
        Item.CampaignCode = document.getElementById('CampaignCode').value;
        Item.SalesPerson = document.getElementById('SalesPerson').value;
      
        List.push(Item);

     
                $.ajax({
                    type: "POST",
                    url: "/Quote/SaveAssembly",
                    dataType: "json",
                    data: JSON.stringify(List, getCircularReplacer()),
                    contentType: "application/json; charset=utf-8",

                    success: function (r) {
           
                        if (r == '') {
                            $('#SucessfulAss').text('Assembly Saved Sucessfully')
                            SucessAlert('#SucessfulAss');
                        }
                        else {
                            $('#ErrorAss').text(r)
                            ErrorAlert('#ErrorAss');
                        }
                    }
                })
     
    })



    $('#DeliveryDate').change(function () {
        var selDate = moment(document.getElementById("DeliveryDate").value).format("DD/MM/YYYY");
        var calcDate =document.getElementById("CalcDeliveryDate").value;

        if (selDate < calcDate)
        {
            var err = "Ideal Delivery date is less than the estimated delivery date " + calcDate;
            $('#ErrorAss').text(err);
            ErrorAlert('#ErrorAss');
        }

    });

    $('#btnSaveCustomer').on('click', function (e) {
        var CustomerList = new Array();
        var CustomerItem = {};
        CustomerItem.QuoteNo = document.getElementById('QuoteNo').value;
        CustomerItem.DispatchAddress = document.getElementById('DispatchAddress').value;
        CustomerItem.AccountContactName = document.getElementById('AccountContactName').value;
        CustomerItem.AccountContactTitle = document.getElementById('AccountContactTitle').value;
        CustomerItem.AccountContactPhoneNo = document.getElementById('AccountContactPhoneNo').value;
        CustomerItem.AccountContactEmail = document.getElementById('AccountContactEmail').value;
        CustomerItem.FinanceDeal = document.getElementById('FinanceDeal').value;
        CustomerItem.FinanceType = document.getElementById('FinanceType').value;
        CustomerItem.FinanceApproved = document.getElementById('FinanceApproved').value;
        CustomerItem.FinanceTotalAmount = document.getElementById('FinanceTotalAmount').value;
        CustomerItem.FinancePeriod = document.getElementById('FinancePeriod').value;
        CustomerList.push(CustomerItem);
        $.ajax({
            type: "POST",
            url: "/Quote/SaveCustomer",
            dataType: "json",
            data: JSON.stringify(CustomerList, getCircularReplacer()),
            contentType: "application/json; charset=utf-8",

            success: function (r) {
                if (r == '') {
                    $('#SucessfulCust').text('Customer Saved Sucessfully')
                    SucessAlert('#SucessfulCust');
                }
                else {
                    $('#ErrorCust').text(r)
                    ErrorAlert('#ErrorCust');
                }                
            }
        })

    })



    $('#btnSaveConsumable').on('click', function (e) {
        var List = new Array();
        var Item = {};
        Item.QuoteNo = document.getElementById('QuoteNo').value;
        Item.InkUsage = document.getElementById('InkUsage').value;
        Item.SolventUsage = document.getElementById('SolventUsage').value;
        Item.Comments = document.getElementById('Comments').value;

        List.push(Item);
        $.ajax({
            type: "POST",
            url: "/Quote/SaveConsumables",
            dataType: "json",
            data: JSON.stringify(List, getCircularReplacer()),
            contentType: "application/json; charset=utf-8",

            success: function (r) {
                if (r == '') {
                    $('#SucessfulConsu').text('Consumables Saved Sucessfully')
                    SucessAlert('#SucessfulConsu');
                }
                else {
                    $('#ErrorConsu').text(r)
                    ErrorAlert('#ErrorConsu');
                }
            }
        })

    })


    $('#btnSaveChopCommentse').on('click', function (e) {
        var CustomerList = new Array();
        var CustomerItem = {};
        CustomerItem.QuoteNo = document.getElementById('QuoteNo').value;
        CustomerItem.CHOPComments = document.getElementById('CHOPComments').value;
    
        CustomerList.push(CustomerItem);
        $.ajax({
            type: "POST",
            url: "/Quote/SaveCHOPComments",
            dataType: "json",
            data: JSON.stringify(CustomerList, getCircularReplacer()),
            contentType: "application/json; charset=utf-8",

            success: function (r) {
                if (r == '') {
                    $('#SucessfulCHOP').text('CHOP Comments Saved Sucessfully')
                    SucessAlert('#SucessfulCHOP');
                }
                else {
                    $('#ErrorCHOP').text(r)
                    ErrorAlert('#ErrorCHOP');
                }
            }
        })

    })

    $('#btnSavePMCommentse').on('click', function (e) {
        var CustomerList = new Array();
        var CustomerItem = {};
        CustomerItem.QuoteNo = document.getElementById('QuoteNo').value;
        CustomerItem.PMComments = document.getElementById('PMComments').value;

        CustomerList.push(CustomerItem);
        $.ajax({
            type: "POST",
            url: "/Quote/SavePMComments",
            dataType: "json",
            data: JSON.stringify(CustomerList, getCircularReplacer()),
            contentType: "application/json; charset=utf-8",

            success: function (r) {
                if (r == '') {
                    $('#SucessfulPM').text('PM Comments Saved Sucessfully')
                     SucessAlert('#SucessfulPM');
                }
                else {
                    $('#ErrorPM').text(r)
                     ErrorAlert('#ErrorPM');
                }
            }
        })

    })


    

    $('#btnSaveTerritorySplit').on('click', function (e) {
        var TerritoryList = new Array();
        var Territory = {};

        Territory.QuoteNo = document.getElementById('QuoteNo').value;
        Territory.Territory1ID = document.getElementById('Territory1ID').value;
        Territory.Territory1Split = document.getElementById('Territory1Split').value;
        Territory.Territory2ID = document.getElementById('Territory2ID').value;
        Territory.Territory2Split = document.getElementById('Territory2Split').value;

        var Split1 = parseFloat(document.getElementById("Territory1Split").value);
        var Split2 = parseFloat(document.getElementById("Territory2Split").value);
        var Total = Split1 + Split2;
        TerritoryList.push(Territory);

        if (Total <= 100) {
             $.ajax({
                        type: "POST",
                        url: "/Quote/SaveTerritorySplit",
                        dataType: "json",
                        data: JSON.stringify(TerritoryList, getCircularReplacer()),
                        contentType: "application/json; charset=utf-8",

                        success: function (r) {
                            if (r == '') {
                                $('#SucessfulTS').text('Territory Split Saved Sucessfully')
                                SucessAlert('#SucessfulTS');
                            }
                            else {
                                $('#ErrorTS').text(r)
                                ErrorAlert('#ErrorTS');
                            }
                        }
                    })


        }
        else {
            $('#ErrorTS').text("Split Total % is not valid,please correct.")
            ErrorAlert('#ErrorTS'); 
        }
       
    })


    $('#Territory1Split').change(function () {
        var Split1 = parseFloat(document.getElementById("Territory1Split").value);
        var Split2 = parseFloat(document.getElementById("Territory2Split").value);
        var Total = Split1 + Split2;

        if (Split1 <= 100) {
            if (Total > 100)
            {
                $('#ErrorTS').text("Split Total % is not valid,please correct.")
                ErrorAlert('#ErrorTS');  
            }
        }
        else {
            $('#ErrorTS').text(" Split1 % is not valid,please correct.")
            ErrorAlert('#ErrorTS');            
        }
            
    });


    $('#Territory2Split').change(function () {
        var Split1 = parseFloat(document.getElementById("Territory1Split").value);
        var Split2 = parseFloat(document.getElementById("Territory2Split").value);
        var Total = Split1 + Split2;

        if (Split2 <= 100) {
            if (Total > 100) {
                $('#ErrorTS').text("Split Total % is not valid,please correct.")
                ErrorAlert('#ErrorTS');
            }
        }
        else {
            $('#ErrorTS').text(" Split2 % is not valid,please correct.")
            ErrorAlert('#ErrorTS');
        }
    });
});

//Dont Delete this Block the tabs call back
function CallAjax()
{ }

//--------------------------------------
function ViewFinancialReview(OpportunityID, BOMID) {
    var url = "/Quote/FinancialReviewModal?OpportunityID=" + OpportunityID + "&BOMID=" + BOMID;
    $("#modelbody").load(url, function () {
        $("#modelFinancialReview").modal("show");
    })
}

//------Show the Messages
function SucessAlert(sdiv) {
    $(sdiv).fadeIn(1000);
    setTimeout(function () {
        $('.alert').fadeOut(1000);
    }, 5000);
}
function ErrorAlert(ediv) {
    $(ediv).fadeIn(1000);
    setTimeout(function () {
        $('.alert').fadeOut(1000);
    }, 5000);
}