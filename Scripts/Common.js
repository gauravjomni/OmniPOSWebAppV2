(function ($) {
    $(document).ready(function () {
        var _href = location.href.toLowerCase();
        var adminSearchPage = ("/admin/search.aspx");

        if (adminSearchPage == _href.substring(0, adminSearchPage.length)) {
            $('body').css('font-size', '0.5875em');
        }

//        $('.pdf').mouseover(function () {
//            $('.li_cont').removeClass('disbl-cls').addClass('enbl-cls');
//        });

//        $('.pdf').mouseout(function () {
//            $('.li_cont').removeClass('enbl-cls').addClass('disbl-cls');
//        });
    });

    $(document).ajaxStart(function () {
        $('#backgroundFade').show();
        $('#lightBox2').show();
    }).ajaxStop(function () {
        $('#backgroundFade').hide();
        $('#lightBox2').hide();
    });

    // Esc key action
    $(document).keyup(function (e) {
        if (e.keyCode == 27) { // Esc
            $('#backgroundFade').hide();
            $('#lightBox').hide();
        }
    });

})(jQuery);
var holdJsonData = null;
function exportTo(to) {
    var dt = new Date();
    var ticks = dt.getTime();
    $('body').prepend("<form method='post' action='" + appPath + "/report/Download.aspx?prm=" + ticks + "&exp=" + to + "' style='display:none' id='formExport'><input type='hidden' name='hdnFrom' id='hdnFrom' value='" + holdJsonData + "'/></form>");
    $('#formExport').submit().remove();
}

function setChart(type) {
    var dt = new Date();
    var ticks = dt.getTime();

    if (type == 'bar') {
        $('#imgChart').attr('src', '../image/chart/1/BarChart.png?d=' + ticks);
    }
    else {
        $('#imgChart').attr('src', '../image/chart/1/PieChart.png?d=' + ticks);
    }
}

function searchCurrentSale() {
    var emsg = '';

    if (($.trim($('#txtFromDate').val()) != '') || ($.trim($('#txtToDate').val()) != '')) {
        if (($.trim($('#txtFromDate').val()) == '')) {
            emsg += (emsg == '' ? '' : '<br/>') + "Please select [From Date]";
        }
        else if (!isDate($('#txtFromDate').val())) {
            emsg += (emsg == '' ? '' : '<br/>') + 'Invalid [From Date]';
        }

        if ($.trim($('#txtToDate').val()) == '') {
            emsg += (emsg == '' ? '' : '<br/>') + "Please select [To Date]";
        }
        else if (!isDate($('#txtToDate').val())) {
            emsg += (emsg == '' ? '' : '<br/>') + 'Invalid [To Date]';
        }

        if (!isgreater_daterange($.trim($('#txtFromDate').val()), $.trim($('#txtToDate').val())) && ($.trim($('#txtToDate').val()) != '')) {
            emsg += (emsg == '' ? '' : '<br/>') + '[From Date] date should be greater than or equal to [To Date] date';
        }
    }

    if (emsg != '') {
        alert(emsg);
        return;
    }

    var dt = new Date();
    var ticks = dt.getTime();

    var get = '&fromDt=' + $('#txtFromDate').val() + '&toDt=' + $('#txtToDate').val();

    $("div.dvResults").compassDatagrid({
        sort: true,
        ajax: true,
        striping: true,
        selectable: true,
        images: 'image',
        url: appPath + '/CallBackRequest/CallBackRequest.aspx?prm=' + ticks + get,
        perPage: "10"
    });
}

function getCurrentSale() {
    $.getScript('../Scripts/Report.js', function () {
        // script is now loaded and executed.
        var dt = new Date();
        var ticks = dt.getTime();

        var get = '&fromDt=' + $('#txtFromDate').val() + '&toDt=' + $('#txtToDate').val();

        $("div.dvResults").compassDatagrid({
            sort: true,
            ajax: true,
            striping: true,
            selectable: true,
            images: 'image',
            url: appPath + '/CallBackRequest/CallBackRequest.aspx?prm=' + ticks + get,
            perPage: "10"
        });
    });
}

function onPostBack() {
    var y = generateRandomSequence();
    var hdnGuid = document.getElementById("hdnGuid");
    hdnGuid.value = y;
}


function generateRandomSequence() {
    var g = "";
    for (var i = 0; i < 32; i++)
        g += Math.floor(Math.random() * 0xF).toString(0xF)
    return g;
}



function loadVerifyRequest() {
    $.getScript(imageURL + 'Scripts/VerifyRequest.js', function () {
        // script is now loaded and executed.
        var dt = new Date();
        var ticks = dt.getTime();

        $("div.dvResults").compassDatagrid({
            sort: true,
            ajax: true,
            striping: true,
            selectable: true,
            images: 'Theme/web/images',
            url: '/CallBackRequest/CallBackRequest.aspx?prm=' + ticks,
            perPage: "10"
        });
    });
}

function ShipmentSearchValidation() {
    $('div.error').html('');
    // validate search form on keyup and submit
    var emsg = '';

    if (
    ($.trim($('#txtFirstName').val()) == '') &&
    ($.trim($('#txtLastName').val()) == '') &&
    ($.trim($('#txtAddress1').val()) == '') &&
    ($.trim($('#txtAddress2').val()) == '') &&
    ($.trim($('#txtCity').val()) == '') &&
    ($.trim($('#cntnBodyPlaceHolder_ddlState').val()) == '') &&
    ($.trim($('#txtZipCode').val()) == '') &&
    ($.trim($('#txtEmail').val()) == '') &&
    ($.trim($('#txtPhone').val()) == '') &&
    ($.trim($('#cntnBodyPlaceHolder_ddlPhysicianType').val()) == '') &&
    ($.trim($('#txtFromDate').val()) == '') &&
    ($.trim($('#txtToDate').val()) == '')
    ) {
        emsg = "Please select at least one searching criteria";
        $('#txtME').focus();
    }

    if (($.trim($('#txtFromDate').val()) != '') || ($.trim($('#txtToDate').val()) != '')) {
        var flagDT = 1;
        if (($.trim($('#txtFromDate').val()) == '')) {
            emsg += (emsg == '' ? '' : '<br/>') + "Please select [Order Start Date]";
            flagDT = 0;
        }
        if ($.trim($('#txtToDate').val()) == '') {
            emsg += (emsg == '' ? '' : '<br/>') + "Please select [Order End Date]";
            flagDT = 0;
        }

        if (flagDT == 1) {
            if (!isDate($('#txtFromDate').val())) {
                emsg += (emsg == '' ? '' : '<br/>') + 'Invalid [Order Start Date]';
                flagDT = 0;
            }
            if (!isDate($('#txtToDate').val())) {
                emsg += (emsg == '' ? '' : '<br/>') + 'Invalid [Order End Date]';
                flagDT = 0;
            }

            if (flagDT == 1 && !isgreater_daterange($.trim($('#txtFromDate').val()), $.trim($('#txtToDate').val()))) {
                emsg += (emsg == '' ? '' : '<br/>') + '[Order Start Date] date should be greater than or equal to [Order End Date] date';
            }
        }
    }

    if (emsg != '') {
        $('div.error').html(emsg);
        return false;
    } else {
        // jQuery
        $.getScript(imageURL + 'Scripts/Shipment.js', function () {
            // script is now loaded and executed.
            var dt = new Date();
            var ticks = dt.getTime();

            $("div.dvResults").compassDatagrid({
                sort: true,
                ajax: true,
                striping: true,
                selectable: true,
                images: imageURL + 'Theme/web/images',
                url: appPath + '/CallBackRequest/CallBackRequest.aspx?prm=' + ticks,
                perPage: "10"
            });
        });
        return true;
    }
}

function loadOrderHistory() {
    $.getScript(imageURL + 'Scripts/OrderHistory.js', function () {
        // script is now loaded and executed.
        var dt = new Date();
        var ticks = dt.getTime();

        $("div.dvResults").compassDatagrid({
            sort: true,
            ajax: true,
            striping: true,
            selectable: true,
            images: imageURL + 'Theme/web/images',
            url: appPath + '/CallBackRequest/CallBackRequest.aspx?prm=' + ticks,
            perPage: "10"
        });
    });
}

function loadOrderSample() {
    $.getScript(imageURL + 'Scripts/OrderSample.js', function () {
        // script is now loaded and executed.
        var dt = new Date();
        var ticks = dt.getTime();

        $("div.dvResults").compassDatagrid({
            sort: true,
            ajax: true,
            striping: true,
            selectable: true,
            images: imageURL + 'Theme/web/images',
            url: appPath + '/CallBackRequest/CallBackRequest.aspx?prm=' + ticks,
            perPage: "10"
        });
    });
}

//Common Functions
function clkBtn(e, buttonid) {
    var bt = document.getElementById(buttonid);
    if (typeof bt == 'object') {
        if (navigator.appName.indexOf("Netscape") > (-1)) {
            if (e.keyCode == 13) {
                bt.click();
                return false;
            }
        }
        if (navigator.appName.indexOf("Microsoft Internet Explorer") > (-1)) {
            if (event.keyCode == 13) {
                bt.click();
                return false;
            }
        }
    }
}

function ValidateEmail(EmailId) {
    var mailformat = /^\w+([\.-]?\w+)*@\w+([\.-]?\w+)*(\.\w{2,3})+$/;
    if (mailformat.test(EmailId)) {
        // document.forms.emailform();
        return true;
    }
    else {
        //alert("You have entered an invalid email address!");
        //emsg += (emsg == '' ? '' : '<br/>') + "Please provide valid email id";
        // document.forms.emailform();
        return false;
    }
}

function isDate(txtDate) {
    var currVal = txtDate;
    if (currVal == '')
        return false;

    var rxDatePattern = /^(\d{1,2})(\/|-)(\d{1,2})(\/|-)(\d{4})$/; //Declare Regex
    var dtArray = currVal.match(rxDatePattern); // is format OK?

    if (dtArray == null)
        return false;

    //Checks for mm/dd/yyyy format.
    dtMonth = dtArray[1];
    dtDay = dtArray[3];
    dtYear = dtArray[5];

    if (dtMonth < 1 || dtMonth > 12)
        return false;
    else if (dtDay < 1 || dtDay > 31)
        return false;
    else if ((dtMonth == 4 || dtMonth == 6 || dtMonth == 9 || dtMonth == 11) && dtDay == 31)
        return false;
    else if (dtMonth == 2) {
        var isleap = (dtYear % 4 == 0 && (dtYear % 100 != 0 || dtYear % 400 == 0));
        if (dtDay > 29 || (dtDay == 29 && !isleap))
            return false;
    }

    return true;
}

function isgreater_daterange(date1, date2) {
    var chkdate1 = new Date(date1.substr(6, 4), date1.substr(0, 2), date1.substr(3, 2));
    var chkdate2 = new Date(date2.substr(6, 4), date2.substr(0, 2), date2.substr(3, 2));
    if (chkdate1.getTime() <= chkdate2.getTime()) {
        return true;
    }
    else
        return false;
}

function downloadData() {
    var err = '';
    $('#divErrormsg').html(err);

    if ($.trim($('#txtFromDate').val()) == '') {
        err = '<span class="red">' + $('#txtFromDate').attr('title') + '</span>';
    } else if (!isDate($('#txtFromDate').val())) {
        err += '<span class="red">Invalid [From] date</span>';
    }

    if ($.trim($('#txtToDate').val()) == '') {
        err += (err == '' ? '' : '<br/>') + '<span class="red">' + $('#txtToDate').attr('title') + '</span>';
    } else if (!isDate($('#txtToDate').val())) {
        err += (err == '' ? '' : '<br/>') + '<span class="red">Invalid [To] date</span>';
    }

    if (err != '') {
        $('#divErrormsg').html(err);
        return false;
    } else {
        if (!isgreater_daterange($.trim($('#txtFromDate').val()), $.trim($('#txtToDate').val()))) {
            $('#divErrormsg').html('<span class="red">[From] date should be greater than or equal to [To] date</span>');
            return false;
        }
    }

    var dt = new Date();
    var ticks = dt.getTime();

    $('body').prepend("<form method='post' action='" + appPath + '/Admin/Download.aspx?prm=' + ticks + "' style='display:none' id='DailyDataExtraction'><input type='hidden' name='hdnFrom' id='hdnFrom' value='" + $('#txtFromDate').val() + "'/><input type='hidden' name='hdnTo' id='hdnTo' value='" + $('#txtToDate').val() + "' ></form>");
    $('#DailyDataExtraction').submit().remove();
    return false;
}

function downloadAllData() {
    $('#divErrormsg').html('');
    var dt = new Date();
    var ticks = dt.getTime();

    $('body').prepend("<form method='post' action='" + appPath + '/Admin/Download.aspx?prm=' + ticks + "' style='display:none' id='DailyDataExtraction'><input type='hidden' name='hdnFrom' id='hdnFrom' value='*'/><input type='hidden' name='hdnTo' id='hdnTo' value='*' ></form>");
    $('#DailyDataExtraction').submit().remove();
    return false;
}

function downloadOptInData() {
    var err = '';
    $('#divErrormsg').html(err);

    if ($.trim($('#txtFromDate').val()) == '') {
        err = '<span class="red">' + $('#txtFromDate').attr('title') + '</span>';
    } else if (!isDate($('#txtFromDate').val())) {
        err += '<span class="red">Invalid [From] date</span>';
    }

    if ($.trim($('#txtToDate').val()) == '') {
        err += (err == '' ? '' : '<br/>') + '<span class="red">' + $('#txtToDate').attr('title') + '</span>';
    } else if (!isDate($('#txtToDate').val())) {
        err += (err == '' ? '' : '<br/>') + '<span class="red">Invalid [To] date</span>';
    }

    if (err != '') {
        $('#divErrormsg').html(err);
        return false;
    } else {
        if (!isgreater_daterange($.trim($('#txtFromDate').val()), $.trim($('#txtToDate').val()))) {
            $('#divErrormsg').html('<span class="red">[From] date should be greater than or equal to [To] date</span>');
            return false;
        }
    }

    var dt = new Date();
    var ticks = dt.getTime();

    $('body').prepend("<form method='post' action='" + appPath + '/Admin/DownloadOptIns.aspx?prm=' + ticks + "' style='display:none' id='DailyDataExtraction'><input type='hidden' name='hdnFrom' id='hdnFrom' value='" + $('#txtFromDate').val() + "'/><input type='hidden' name='hdnTo' id='hdnTo' value='" + $('#txtToDate').val() + "' ></form>");
    $('#DailyDataExtraction').submit().remove();
    return false;
}

function downloadAllOptInData() {
    $('#divErrormsg').html('');
    var dt = new Date();
    var ticks = dt.getTime();

    $('body').prepend("<form method='post' action='" + appPath + '/Admin/DownloadOptIns.aspx?prm=' + ticks + "' style='display:none' id='DailyDataExtraction'><input type='hidden' name='hdnFrom' id='hdnFrom' value='*'/><input type='hidden' name='hdnTo' id='hdnTo' value='*' ></form>");
    $('#DailyDataExtraction').submit().remove();
    return false;
}

function AddDatePicker(cntrolID) {
    $(cntrolID).datepicker({
        changeMonth: true,
        changeYear: true,
        showOn: "both",
        buttonImage: '../image/calender.jpg',
        buttonImageOnly: true,
        numberOfMonths: 1,
        maxDate: "+0M +0D"
    });
}

function DisableAllNumeric(event, id) {

    var controlId = id.id;
    var key = (event.charCode) ? event.charCode : ((event.which) ? event.which : event.keyCode);

    if ((key == 37)    // Arrow key
        || (key == 38) // Arrow key
        || (key == 39) // Arrow key
        || (key == 40) // Arrow key
        || (key == 8) // Backspace
        || (key == 35) // End
        || (key == 36) // Home
         || (key == 46) // Delete
         || (key == 9) //Tab
    ) {
        return true;
    }
    var controlKeys = "49,50,51,52,53,54,55,56,57";

    var index = controlKeys.indexOf(key);
    // alert(key);
    if (index != -1) {
        return false;
    }
    else if (key == 8) {
        var oldLength = document.getElementById(controlId).value.length;
        var newlength = (document.getElementById(controlId).value.length - 1);

        document.getElementById(controlId).value = document.getElementById(controlId).value.substring(0, newlength);

        return true;
    }
    else {
        return true;
    }
    //var chCode = ('charCode' in event) ? event.charCode : event.keyCode;
    //alert("The Unicode character code is: " + key);
}

function ValidateText(event, id) {

    var status = false;
    var CheckNumeric = DisableAllNumeric(event, id);

    var CheckSpecialChar = DisableAllSpecialchar(event, id);

    if (CheckNumeric == true && CheckSpecialChar == true) {
        status = true;
    }

    return status;
}

function DisableAllSpecialchar(event, id) {

    var controlId = id.id;
    var key = (event.charCode) ? event.charCode : ((event.which) ? event.which : event.keyCode);
    // alert(key);
    if ((key == 37)    // Arrow key
        || (key == 38) // Arrow key
    // || (key == 39) // Arrow key
        || (key == 40) // Arrow key
        || (key == 8) // Backspace
        || (key == 35) // End
        || (key == 36) // Home
         || (key == 46) // Delete
         || (key == 9) //Tab
    ) {
        return true;
    }

    var controlKeys = "33,64,35,36,94,38,38,42,37,40,41,95,43,91,123,93,125,92,124,58,59,39,34,44,60,62,46,63,47,45,61,96,39,46,126";
    var index = controlKeys.indexOf(key);

    if (index != -1) {
        return false;
    }
    else {
        return true;
    }
    //var chCode = ('charCode' in event) ? event.charCode : event.keyCode;
    //alert("The Unicode character code is: " + key);
}

function DisableSpecialchar(event, id) {
    var controlId = id.id;

    var key = (event.charCode) ? event.charCode : ((event.which) ? event.which : event.keyCode);

    if ((key == 37)    // Arrow key
        || (key == 38) // Arrow key
        || (key == 39) // Arrow key
        || (key == 40) // Arrow key
        || (key == 8) // Backspace
        || (key == 35) // End
        || (key == 36) // Home
        || (key == 46) // Delete
        || (key == 9) //Tab
    ) {
        return true;
    }
    var controlKeys = "33, 37, 94, 38, 42, 40, 41,58,34,44,32, 60, 62, 43, 61, 45, 47, 63, 123, 125, 91, 93, 92, 126,46,39,59,124,95,96";

    var index = controlKeys.indexOf(key);
    //alert(key);
    if (index != -1) {
        return false;
    }
    else {
        return true;
    }
    //var chCode = ('charCode' in event) ? event.charCode : event.keyCode;
    //alert("The Unicode character code is: " + key);
}


function EditPhysicianProfile(sender, args) {

    
    //var cntnBodyPlaceHolder_txtDOB = document.getElementById('cntnBodyPlaceHolder_txtDOB');
    var cntnBodyPlaceHolder_chkSubscription = document.getElementById('cntnBodyPlaceHolder_ChkEnewlatter');
    var flag = '0';
    var emsg = '';

    if ($.trim($('#cntnBodyPlaceHolder_ddlPhysicianType').val()) == '0') {
        emsg += $('#cntnBodyPlaceHolder_ddlPhysicianType').attr('title');
        $('#cntnBodyPlaceHolder_ddlPhysicianType').focus();
    }

    if ($.trim($('#cntnBodyPlaceHolder_ddlState').val()) == '0') {
        if (emsg == '') $('#cntnBodyPlaceHolder_ddlState').focus();
        emsg += (emsg == '' ? '' : '<br/>') + $('#cntnBodyPlaceHolder_ddlState').attr('title');
    }

    if ($.trim($('#cntnBodyPlaceHolder_txtFirstName').val()) == '') {
        if (emsg == '') $('#cntnBodyPlaceHolder_txtFirstName').focus();
        emsg += (emsg == '' ? '' : '<br/>') + $('#cntnBodyPlaceHolder_txtFirstName').attr('title');
    }



    if ($.trim($('#cntnBodyPlaceHolder_txtLastName').val()) == '') {
        if (emsg == '') $('#cntnBodyPlaceHolder_txtLastName').focus();
        emsg += (emsg == '' ? '' : '<br/>') + $('#cntnBodyPlaceHolder_txtLastName').attr('title');
    }

   

    if ($.trim($('#cntnBodyPlaceHolder_txtEmail').val()) == '') {
        if (emsg == '') $('#cntnBodyPlaceHolder_txtEmail').focus();
        emsg += (emsg == '' ? '' : '<br/>') + $('#cntnBodyPlaceHolder_txtEmail').attr('title');
    }
    else {
        if (!ValidateEmail($('#cntnBodyPlaceHolder_txtEmail').val())) {
            emsg += (emsg == '' ? '' : '<br/>') + "Please provide valid email id";
        }
    }

//    if (($.trim($('#cntnBodyPlaceHolder_txtPwd').val()) == '')) {
//        if (emsg == '') {
//            $('#cntnBodyPlaceHolder_txtPwd').focus();
//        }
//        emsg += (emsg == '' ? '' : '<br/>') + $('#cntnBodyPlaceHolder_txtPwd').attr('title');
//    }
//    else {
//        if (!ValidatePassword($.trim($('#cntnBodyPlaceHolder_txtPwd').val()))) {
//            emsg += (emsg == '' ? '' : '<br/>') + errMsg;
//        }
//    }

    if ($.trim($('#cntnBodyPlaceHolder_ddlActive').val()) == '0') {
        if (emsg == '') $('#cntnBodyPlaceHolder_ddlActive').focus();
        emsg += (emsg == '' ? '' : '<br/>') + $('#cntnBodyPlaceHolder_ddlActive').attr('title');
    }

    

    args.IsValid = (emsg == '');

   
    if (emsg != '') {
        $('#cntnBodyPlaceHolder_divErrormsg').html('<span class="red">Following field(s) has error(s):<br/>' + emsg + '</span>');
    }

    return args.IsValid;
}

var errMsg = '';
function ValidatePassword(inputText) {

    var isvalid = true;
    var exp
    if (inputText.length < 8) {
        isvalid = false;
        errMsg = "Password length should be 8 character minimum";
    }
    else if (!/[0-9]/.test(inputText)) {
        isvalid = false;
        errMsg = "Password must contain at least one number";
    }
    else if (!/[a-zA-Z]/.test(inputText)) {
        isvalid = false;
        errMsg = "Password must contain at least one letter ";

    }
    else {
        isvalid = true;
    }
    return isvalid;
}


function DisableSpecialChar_numeric(event, id) {
    var status = false;
    var CheckChar = DisableAllchar(event, id);
    var CheckSpecialChar = DisableAllSpecialchar(event, id);
    if (CheckChar == true && CheckSpecialChar == true) {
        status = true;
    }

    return status;
}

function DisableAllchar(event, id) {

    var controlId = id.id;
    var key = (event.charCode) ? event.charCode : ((event.which) ? event.which : event.keyCode);

    if ((key == 37)    // Arrow key
        || (key == 38) // Arrow key
        || (key == 39) // Arrow key
        || (key == 40) // Arrow key
        || (key == 8) // Backspace
        || (key == 35) // End
        || (key == 36) // Home
         || (key == 46) // Delete
         || (key == 9) //Tab
    ) {
        return true;
    }
    var controlKeys = "97,98,99,100,101,102,103,104,105,106,107,108,109,110,111,112,113,114,115,116,117,118,119,120,121,122,65,66,67,68,69,70,71,72,73,74,75,76,77,78,79,80,81,82,83,84,85,86,87,88,89,90";
    var index = controlKeys.indexOf(key);

    if (index != -1) {
        return false;
    }
    else {
        return true;
    }
    //var chCode = ('charCode' in event) ? event.charCode : event.keyCode;
    //alert("The Unicode character code is: " + key);
}


function Add_UpdateSubmition(sender, args) {
    // debugger;
    //    var cntnBodyPlaceHolder_txtAddress1 = document.getElementById('cntnBodyPlaceHolder_txtAddress1');
    //    var cntnBodyPlaceHolder_txtAddress2 = document.getElementById('cntnBodyPlaceHolder_txtAddress2');
    //    var cntnBodyPlaceHolder_txtCity = document.getElementById('cntnBodyPlaceHolder_txtCity');
    //    var cntnBodyPlaceHolder_ddlState = document.getElementById('cntnBodyPlaceHolder_ddlState');
    //    var cntnBodyPlaceHolder_txtZipcode = document.getElementById('cntnBodyPlaceHolder_txtZipcode');
    //    var cntnBodyPlaceHolder_txtPhone = document.getElementById('cntnBodyPlaceHolder_txtPhone');

    var flag = '0';
    var emsg = '';

    if ($.trim($('#cntnBodyPlaceHolder_txtAddress1').val()) == '') {

        emsg = $('#cntnBodyPlaceHolder_txtAddress1').attr('title');
        $('#cntnBodyPlaceHolder_txtAddress1').focus();
    }

    if ($.trim($('#cntnBodyPlaceHolder_txtCity').val()) == '') {
        if (emsg == '') $('#cntnBodyPlaceHolder_txtCity').focus();
        emsg += (emsg == '' ? '' : '<br/>') + $('#cntnBodyPlaceHolder_txtCity').attr('title');
    }

    if ($.trim($('#cntnBodyPlaceHolder_ddlState').val()) == '0') {
        if (emsg == '') $('#cntnBodyPlaceHolder_ddlState').focus();
        emsg += (emsg == '' ? '' : '<br/>') + $('#cntnBodyPlaceHolder_ddlState').attr('title');
    }





    if ($.trim($('#cntnBodyPlaceHolder_txtZipcode').val()) == '') {
        if (emsg == '') $('#cntnBodyPlaceHolder_txtZipcode').focus();
        emsg += (emsg == '' ? '' : '<br/>') + $('#cntnBodyPlaceHolder_txtZipcode').attr('title');
    }
    else {
        // debugger;
        if ($.trim($('#cntnBodyPlaceHolder_txtZipcode').val()).length < 5) {
            emsg += (emsg == '' ? '' : '<br/>') + "Please provide valid zip code length";
        }
        if (!CheckNumericVal($('#cntnBodyPlaceHolder_txtZipcode').val())) {
            emsg += (emsg == '' ? '' : '<br/>') + "Please provide zip code in numeric value";
        }
    }

//    if ($.trim($('#cntnBodyPlaceHolder_txtPhone').val()) == '') {
//        if (emsg == '') $('#cntnBodyPlaceHolder_txtPhone').focus();
//        emsg += (emsg == '' ? '' : '<br/>') + $('#cntnBodyPlaceHolder_txtPhone').attr('title');
//    }
//    else {

//        if (!CheckNumericVal($('#cntnBodyPlaceHolder_txtPhone').val())) {
//            emsg += (emsg == '' ? '' : '<br/>') + "Please provide valid numeric phone number";
//        }
    //    }

    if ($.trim($('#cntnBodyPlaceHolder_txtPhone').val()) != '') {
        if (!CheckNumericVal($('#cntnBodyPlaceHolder_txtPhone').val())) {
            emsg += (emsg == '' ? '' : '<br/>') + "Please provide valid numeric phone number";
        }
    }

    args.IsValid = (emsg == '');

    //    if (emsg != '') {
    //        if (sender.innerText) {
    //            sender.innerText = 'Following field(s) has error(s):' + emsg;
    //        } else {
    //            //sender.textContent = emsg;
    //            sender.innerHTML = 'Following field(s) has error(s):<br/>' + emsg;
    //        }
    //    }
    if (emsg != '') {
        $('#cntnBodyPlaceHolder_divErrormsg').html('<span class="red">Following field(s) has error(s):<br/>' + emsg + '</span>');
    }
    return args.IsValid;
}

function CheckNumericVal(inputText) {

    var numFormat = /^\d+$/;
    if (numFormat.test(inputText)) {
        return true;
    }
    else {
        return false;
    }
}