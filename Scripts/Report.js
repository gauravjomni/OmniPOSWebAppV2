/*************************************************************************
File Name: OrderSample.js

Description: This library is displaying the search result

Browsers Support - Firefox 15, Safari 5, Chrome 22.0, IE 7, 8, 9 (Win XP), IE 8, 9 (Winows 7)

Revision History
Date		Name
12/19/2013	Arun Kumar
*************************************************************************/

(function ($) {
    $.fn.compassDatagrid = function (options) {
        var opts = $.extend({}, {
            hover: true,
            hoverClass: 'hover',
            selectable: true,
            selectableClass: 'selected',
            checkboxToggle: true,
            sort: true,
            ajax: true,
            striping: true,
            oddClass: 'odd',
            evenClass: 'even',
            resizable: false,
            resizeOpacity: 0.65,
            resizer: '#ctResizer',
            toggle: true,
            toggleHolder: 'toggleHolder',
            pager: true,
            pagerBefore: true,
            pagerAfter: true,
            images: 'image',
            url: '',
            perPage: 15,
            perPageOptions: ["5", "10", "15", "20", "25", "30", "40", "50", "100", "500", "1000", "10000"],
            paramsStart: "?",
            paramsSeparator: "&",
            paramsJoin: "="
        }, options);

        /*==================================================================================================================================*/
        $.fn.IsSearchHavingData = true;
        $.fn.CartQty = 0;
        $.fn.itemOldQty = [];

        $.fn.phyName = '';
        $.fn.emailAdd = '';

        return this.each(function () {
            var $this = $(this);
            $this.find('#tblData').addClass('gridVerifyRequest');
            $this.find('#addPhysician').addClass('gridVerifyRequest');
            $.fn.CartQty = 0;

            $().ajaxStart(function () {
            });
            $().ajaxStop(function () {
            });
            if (opts.ajax) {
                $this.ctLoadResult(opts);
            }

            //Condition and initiallising for paging
            $('#searchDiv').livequery(function () {
                $('#searchBox').unbind('click').bind('click', function () {
                    $(this).closest('#searchDiv').find('#searchContainer').toggle('slow');
                });

                /*$('input[type="checkbox"]').unbind('click').bind('click', function () {
                });

                $('#div-formbtns a').unbind('click').bind('click', function () {
                });*/
            });
        });
    };

    //===================================================================================================================================
    // Hide the IDs column
    //===================================================================================================================================
    $.fn.ctIdTable = function () {
        if ($.fn.IsSearchHavingData) {
            var $this = $(this);
            $this.find('tr').each(function (i) {
                $(this).find('th').each(function (z) {
                    $(this).addClass('col-' + z);
                });
                $(this).find('td').each(function (z) {
                    $(this).addClass('col-' + z);
                });
            });
        }
    };

    //===================================================================================================================================
    // Apply the CSS on header
    //===================================================================================================================================
    $.fn.ctHover = function (opts) {
        if ($.fn.IsSearchHavingData) {
            $this = $(this);
            $this.find('thead th').hover(function () {
                if ($(this).attr('class') != "") {
                    $(this).addClass(opts.hoverClass);
                }
            }, function () {
                $(this).removeClass(opts.hoverClass);
            });
        }
    };

    $.fn.ctLoadResult = function (opts) {
        var table = '';
        var $this = $(this);

        var curPageNumber = 0;
        var get = '';

        $.fn.countTR = 1;

        //Send request to web server for data
        $.post(opts.url + get, function (data) {
            if (data == '') {
                alert('Request is not authorized');
                window.location.href = appPath + "/default.aspx";
                return;
            }

            holdJsonData = data;

            data = JSON.parse(data);

            //If there is no data found
            if (data == null) {
                $.fn.IsSearchHavingData = false;
                $this.find('.errorMsg').html("<span class'red'>There is some problem to display the cart information, Please contact a customer service representative at (800) 358-8707.</span>");
                return;
            }
            else {
                if (data.session != null && data.session == "1") {
                    alert("Session Out");
                    window.location.href = appPath + "/default.aspx";
                    return;
                }
                if (data.error != null) {
                    alert(data.error);
                    return;
                }
                table = ''; // '<table class="Data rounded" width="85%">';
                table += '<thead><tr><th>Description</th><th>Amount (in $)</th></tr></thead>';
                table += '<tbody>';

                //                if (data.Products == null) {
                //                    table += '<tr><td colspan="2"><span class="red">Sorry, there is no sale data available</span></td></tr>';
                //                } else {
                var dt = new Date();
                var ticks = dt.getTime();
                table += '<tr><td>Cash</td><td>$' + data.CashSale + '</td></tr>';
                table += '<tr><td>Card</td><td>$' + data.CardSale + '</td></tr>';
                table += '<tr><td>Voucher</td><td>$' + data.VoucherSale + '</td></tr>';
                table += '<tr><td>Total Discount</td><td>$' + data.Discount + '</td></tr>';
                table += '<tr><td>Total Surcharge</td><td>$' + data.SurCharge + '</td></tr>';
                table += '<tr><td>FLOAT(+)</td><td>$' + data.TotalFloatAmt + '</td></tr>';
                table += '<tr><td>REFUND(-)</td><td>$' + data.TotalRefundAmount + '</td></tr>';
                table += '<tr><td>PAYOUT(-)</td><td>$' + data.TotalPayoutAmount + '</td></tr>';
                table += '<tr><td>TIPS(+)</td><td>$' + data.TipAmount + '</td></tr>';
                table += '<tr><td>GST Included (10 %)</td><td>$' + data.TaxAmount + '</td></tr>';
                table += '<tr><td>No Sale Count</td><td>$' + data.NoSaleCount + '</td></tr>';
                table += '<tr><td>TOTAL Net Sale</td><td><strong>$' + data.TotalNetAmount + '</strong></td></tr>';
                table += '<tr><td>TOTAL Gross Sale</td><td><strong>$' + data.TotalGrossAmount + '</strong></td></tr>';
                //}

                if (data.Drawers) {
                    $.each(data.Drawers, function (i, row) {
                        table += '<tr style="color:#B7400E;"><td>Total in DRAWER (' + row.PrinterName + ')</td><td>$' + (row.CashSale - (row.PayOut + row.PayRefund)) + '</td></tr>';
                    });
                }
                table += '</tbody>';

                $this.find('.tblData').html(table);
                if ($.trim($('#txtToDate').val()) != '') {
                    $('#sec_header').html('Current Sales Report - Date Selected From : ' + data.TransactDate + ', to : ' + $.trim($('#txtToDate').val()))
                }
                else {
                    $('#sec_header').html('Current Sales Report - Date Selected From : ' + data.TransactDate + ' till now');
                }
                //$this.find('.tblData').css({ 'width': '40%' });
            }
        });
    };
})(jQuery);
