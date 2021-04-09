$(document).ready(function () {

    const $tableID = $('#table');
    const $BTN = $('#export-btn');
    const $EXPORT = $('#export');

    //--numeric entry only in the qty field
    $(".allownumeric").on("keypress keyup blur", function (event) {
        const $row = $(this).parents('tr')
        var IsDecimal = parseInt($row.find("TD").eq(18).html());

        $(this).val($(this).val().replace(/[^\d]+/, ""));
        if (IsDecimal == 1) {
            if ((event.which < 45 || event.which > 57)) {
                event.preventDefault();
            }
        }
        else {
            if ((event.which < 47 || event.which > 57)) {
                event.preventDefault();
            }
        }
    });

    //--Calculate the price when changing the Qty
    $('body').on('focus', '[contenteditable]', function () { })
        .on('blur keyup paste input', '[contenteditable]', function () {
            const $this = $(this);

            if ($this.data('before') !== $this.html()) {

                // Set to Max Qty if exceeding the Max Qty
                const $rowin = $(this).parents('tr');
                var maxQty = parseFloat($rowin.children("td:eq(14)").html());
                var qty = parseFloat($rowin.children("td:eq(7)").html());
                if (maxQty != 0 && qty > maxQty) {
                    var txt = 'Exceeding the defined maximum qty of' + $rowin.children("td:eq(14)").html();
                    $('#Error').text(txt);
                    ErrorAlert();
                    $rowin.children("td:eq(7)").html(maxQty)
                }

                //----------------------------------------------------------------------------
                //Add Child Rows o the parent
                //var strBOMItemID = parseFloat($rowin.children("td:eq(3)").html());
                //var BOMID = parseFloat($rowin.children("td:eq(2)").html());
                //var intOpportunityID = parseFloat($rowin.children("td:eq(0)").html());
                //var intState = parseFloat($rowin.children("td:eq(16)").html());
                //var newRow = $("<tr>");

                //$.ajax({
                //    type: "POST",
                //    url: "/Quote/GetChildBOMList",
                //    dataType: "json",
                //    data: JSON.stringify({ 'OpportunityID': intOpportunityID, 'BOMItemID': strBOMItemID, 'BOMID': BOMID, 'State': intState }, getCircularReplacer()),
                //    contentType: "application/json; charset=utf-8",

                //    success: function (r) {


                //        if (r === '') {
                //            alert(r.$rows(0).BOMID);
                //        }
                //        else {
                //            var trHTML = '';
                //            var i;
                //            $.each(r, function (i, item) {
                //                trHTML += '<tr><td>' + item.Code + '</td><td>' + item.Description + '</td></tr>';
                //            });


                //            for (var i = 0; i < r.length; i++) {
                //                if (r[i].OpportunityID != '0') { 
                //                        var cols = "";

                //                        cols += '<td class="pt-3-half" contenteditable="false" > ' + r[i].OpportunityID +' </td>';
                //                        cols += '<td class="pt-3-half" contenteditable="false" > ' + r[i].OpportunityBOMListID +'</td>';
                //                        cols += '<td class="pt-3-half" contenteditable="false" > ' + r[i].BOMID +'</td>';
                //                        cols += '<td class="pt-3-half" contenteditable="false" > ' + r[i].BOMItemID +'</td>';
                //                        cols += '<td class="pt-3-half" contenteditable="false" > ' + r[i].Category +'</td>';
                //                        cols += '<td class="pt-3-half" contenteditable="false"> ' + r[i].MatthewsCode +'</td>';
                //                        cols += '<td class="pt-3-half" contenteditable="false"> ' + r[i].Description + '</td>';
                //                        var isQtyFixed = r[i].IsQtyFixed;
                //                        if (isQtyFixed == false) {
                //                            cols += '<td class="allownumeric" contenteditable="true" align="center"> ' + r[i].Qty + '</td>';
                //                        }
                //                        else {
                //                            cols += '<td class="allownumeric" contenteditable="false" align="center"> ' + r[i].Qty + '</td>';
                //                        }

                //                        cols += '<td class="pt-3-half" contenteditable="false" align="right"> ' + r[i].ItemPrice +'</td>';
                //                        cols += '<td class="pt-3-half" contenteditable="false" > ' + r[i].Price +'</td>';
                //                        cols += '<td class="pt-3-half" contenteditable="false" align="right">  ' + r[i].Price + '</td>';
                //                        var IsDiscountApply = r[i].IsDiscountApply;
                //                        if (IsDiscountApply == true) {
                //                            cols += '<td class="pt-3-half" contenteditable="false" > <input checked="checked" class="check-box" disabled="disabled" type="checkbox" /></td>';
                //                        }
                //                        else {
                //                            cols += '<td class="pt-3-half" contenteditable="false" > <input class="check-box" disabled="disabled" type="checkbox" /></td>';
                //                        }
                //                        cols += '<td class="pt-3-half" contenteditable="false" > ' + r[i].Discount +'</td>';
                //                        cols += '<td class="pt-3-half" contenteditable="false" > ' + r[i].AfterDiscount +'</td>';
                //                        cols += '<td class="pt-3-half" contenteditable="false" > ' + r[i].MaximumQty +'</td>';
                //                        cols += '<td class="pt-3-half" contenteditable="false" align="center" s> ' + r[i].Stock +'</td>';
                //                        cols += '<td class="pt-3-half" contenteditable="false" > ' + r[i].State +'</td>';
                //                        cols += '<td class="pt-3-half" contenteditable="false" > ' + r[i].IsInTotal +'</td>';
                //                        cols += '<td class="pt-3-half" contenteditable="false" > ' + r[i].IsDecimalAllowed +'</td>';


                //                        newRow.append(cols);
                //                }

                //            }
                //        }


                //    }
                //})

                //$(this).closest("tr").after(newRow);

                //---------------------------------------------------------------

                //Recalculate the Totals
                $(this).parents('tr').find('input:checkbox').each(function () {
                    const $row = $(this).parents('tr')
                    var Discount = document.getElementById('Discount').value;
                    var RowUnitPrice = $row.children("td:eq(8)").html();
                    var RowQty = $row.children("td:eq(7)").html();
                    var price = RowUnitPrice * RowQty;
                    var IsInTotal = parseInt($row.find("TD").eq(17).html());
                    if (IsInTotal == 1) {
                        if (this.checked) {

                            var afterDiscount = (RowUnitPrice - ((RowUnitPrice / 100) * Discount)) * RowQty;
                            $row.children("td:eq(13)").html(afterDiscount);
                            $row.children("td:eq(9)").html(price);
                            $row.children("td:eq(10)").html(price).formatCurrency();
                        }
                        else {
                            $row.children("td:eq(9)").html(price);
                            $row.children("td:eq(10)").html(price).formatCurrency();
                            $row.children("td:eq(13)").html(price);
                        }
                    }
                    else {
                        $row.children("td:eq(9)").html(0);
                        $row.children("td:eq(10)").html(0).formatCurrency();
                        $row.children("td:eq(13)").html(0);
                    }

                })

                $this.trigger('change');
            }
        });


    //--------------------------------------------
    //

    $('tbody').on('change', 'td', function () {
        var Total = 0;
        var DTotal = 0;
        $('.body tr').each(function () {
            var row = $(this);
            var IsInTotal = parseInt(row.find("TD").eq(17).html());
            if (IsInTotal == 1) {
                var val = parseFloat(row.find("TD").eq(13).html());
                var valD = parseFloat(row.find("TD").eq(9).html());
                Total += val;
                DTotal += valD;
            }
        });

        $('#GrandTotalAfterDiscount').text(Total).formatCurrency();
        $('#GrandTotal').text(DTotal).formatCurrency();
    });


    //Recalculate the Discount
    $('#Discount').change(function () {

        $('.body tr').find('input:checkbox').each(function () {

            if (this.checked) {
                const $row = $(this).parents('tr')
                var IsInTotal = parseInt($row.find("TD").eq(17).html());
                if (!isNaN(IsInTotal) && IsInTotal.length != 0) {
                    //Calculate the  only if IsInTotal Flag is on
                    if (IsInTotal == 1) {
                        var discount = document.getElementById('Discount').value;
                        var qty = $row.find("TD").eq(7).html();
                        var unitPrice = $row.find("TD").eq(8).html();
                        var afterDiscount = (unitPrice - ((unitPrice / 100) * discount)) * qty;
                        $row.children("td:eq(13)").html(afterDiscount);
                    }
                    else {
                        $row.children("td:eq(13)").html(0);
                    }
                }
                else {
                    $row.children("td:eq(13)").html(0);
                }
            }

        })

        var grandTotal = 0;
        var DgrandTotal = 0;
        $('.body tr').each(function () {
            var $row = $(this);
            var val = $row.find("TD").eq(9).html();
            var valD = $row.find("TD").eq(13).html();
            var IsInTotal = parseInt($row.find("TD").eq(17).html());

            //Add to the totals only if IsInTotal Flag is on
            if (IsInTotal == 1) {
                if (!isNaN(val) && val.length != 0) {
                    grandTotal += parseFloat(val);
                }

                if (!isNaN(valD) && valD.length != 0) {
                    DgrandTotal += parseFloat(valD);
                }
            }

        })

        $('#GrandTotal').text(grandTotal).formatCurrency();
        $('#GrandTotalAfterDiscount').text(DgrandTotal).formatCurrency();
    });




    //---Save the BOM List 
    $('#btnSave').on('click', function (e) {
        var BOMList = new Array();
        debugger;
        var GetVersionNum = location.href.substring(location.href.toLocaleLowerCase().indexOf("version"));
        GetVersionNum = GetVersionNum.split('=')
        if (GetVersionNum != null)
            GetVersionNum = GetVersionNum[1];

        var GetOppID = location.href.substring(location.href.toLocaleLowerCase().indexOf("opportunityid")).split('&');
        if (GetOppID.length > 0) {
            GetOppID = GetOppID[0].split('=');
            if (GetOppID.length > 0) {
                GetOppID = GetOppID[1];
            }
        }

        var GetBOMID = location.href.substring(location.href.toLocaleLowerCase().indexOf("bomid")).split('&');
        if (GetBOMID.length > 0) {
            GetBOMID = GetBOMID[0].split('=');
            if (GetBOMID.length > 0) {
                GetBOMID = GetBOMID[1];
            }
        }


        $('.body tr').each(function () {
            var row = $(this);
            var BOMItem = {};
           
            if (row.find("TD").eq(0).html().indexOf("<div") < 0 && row.find("TD").eq(4).html().trim().toLocaleLowerCase() == "Consumables".toLocaleLowerCase()) {
 debugger;
                var BOMItem = {};
                BOMItem.BOMID = GetBOMID;//1;
                BOMItem.BOMItemID = row.find("TD").eq(3).html();//1;
                BOMItem.ItemPrice = row.find("TD").eq(8).html();//2.2;
                BOMItem.Price = row.find("TD").eq(9).html();// 2.2;
                BOMItem.Qty = row.find("TD").eq(7).html();//2.2;
                BOMItem.OpportunityID = GetOppID;//61
                BOMItem.OpportunityBOMListID = row.find("TD").eq(1).html();//1;
                if (BOMItem.BOMItemID == '' && row.find("TD").eq(5).html() !== '') {
                    BOMItem.CustomCode = row.find("TD").eq(5).html();
                    BOMItem.CustomDescription = row.find("TD").eq(6).html();
                }
                BOMItem.Discount = document.getElementById('Discount').value;//2.2;
                BOMItem.FinalAgreedPrice = document.getElementById('FinalAgreedPrice').value;// 2.2;
                BOMItem.IsDiscountApply = row.find('input[type="checkbox"]').is(':checked');//false;
                BOMItem.AfterDiscount = row.find("TD").eq(13).html();//2.2;
                BOMItem.MaximumQty = row.find("TD").eq(14).html();//2.2;
                BOMItem.State = row.find("TD").eq(16).html();// 1;
                BOMItem.IsInTotal = row.find("TD").eq(17).html();//1;
                BOMItem.IsDecimalAllowed = row.find("TD").eq(18).html();//1;

                if (document.getElementById('InkUsage') != undefined)
                    BOMItem.InkUsage = document.getElementById('InkUsage').value;

                BOMList.push(BOMItem);
            }
        });
       


        //$.ajax({
        //    type: 'post',
        //    dataType: 'json',
        //    url: '/quote/createbom',
        //    data: {
        //        "BOMList": BOMList,
        //        "VersionNum": GetVersionNum
        //    },
        //    success: function (r) {
        //        if (r === '') {
        //            SucessAlert();
        //        }
        //        else {
        //            $('#error').text(r)
        //            ErrorAlert();
        //        }


        //    }
        //});
    });


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


    $(".add-row").click(function () {
        const newCustomePartTr = ` 
                        <tr class="hide">
                          <td class="pt-3-half" contenteditable="false" ></td>
                          <td class="pt-3-half" contenteditable="false" ></td>
                          <td class="pt-3-half" contenteditable="false" ></td>
                          <td class="pt-3-half" contenteditable="false" ></td>
                          <td class="pt-3-half" contenteditable="false" ></td>
                          <td class="pt-3-half" contenteditable="true" ></td>
                          <td class="pt-3-half" contenteditable="true"></td>
                          <td class="pt-3-half" contenteditable="true">0</td>
                          <td class="pt-3-half" contenteditable="true">0</td>
                          <td class="pt-3-half" contenteditable="true">0</td>
                          <td class="pt-3-half" contenteditable="true">0</td>
                     
                         </tr> `;
        $("#tblCustomParts tbody").append(newCustomePartTr);
    });




    const newTr = `
                        <tr class="hide">
                          <td class="pt-3-half" contenteditable="true">Example</td>
                          <td class="pt-3-half" contenteditable="true">Example</td>
                          <td class="pt-3-half" contenteditable="true">Example</td>
                          <td class="pt-3-half" contenteditable="true">Example</td>
                          <td class="pt-3-half" contenteditable="true">Example</td>
                          <td class="pt-3-half">
                            <span class="table-up"><a href="#!" class="indigo-text"><i class="fas fa-long-arrow-alt-up" aria-hidden="true"></i></a></span>
                            <span class="table-down"><a href="#!" class="indigo-text"><i class="fas fa-long-arrow-alt-down" aria-hidden="true"></i></a></span>
                          </td>
                          <td>
                            <span class="table-remove"><button type="button" class="btn btn-danger btn-rounded btn-sm my-0 waves-effect waves-light">Remove</button></span>
                          </td>
                        </tr>`;







    $('.table-add').on('click', 'i', () => {

        const $clone = $tableID.find('tbody tr').last().clone(true).removeClass('hide table-line');

        if ($tableID.find('tbody tr').length === 0) {

            $('tbody').append(newTr);
        }

        $tableID.find('table').append($clone);
    });

    $tableID.on('click', '.table-remove', function () {

        $(this).parents('tr').detach();
    });

    $tableID.on('click', '.table-up', function () {

        const $row = $(this).parents('tr');

        if ($row.index() === 0) {
            return;
        }

        $row.prev().before($row.get(0));
    });

    $tableID.on('click', '.table-down', function () {

        const $row = $(this).parents('tr');
        $row.next().after($row.get(0));
    });

    // A few jQuery helpers for exporting only
    jQuery.fn.pop = [].pop;
    jQuery.fn.shift = [].shift;

    $BTN.on('click', () => {

        const $rows = $tableID.find('tr:not(:hidden)');
        const headers = [];
        const data = [];

        // Get the headers (add special header logic here)
        $($rows.shift()).find('th:not(:empty)').each(function () {

            headers.push($(this).text().toLowerCase());
        });

        // Turn all existing rows into a loopable array
        $rows.each(function () {
            const $td = $(this).find('td');
            const h = {};

            // Use the headers from earlier to name our hash keys
            headers.forEach((header, i) => {

                h[header] = $td.eq(i).text();
            });

            data.push(h);
        });

        // Output the result
        $EXPORT.text(JSON.stringify(data));
    });



});
