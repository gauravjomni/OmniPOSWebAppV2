function assignusrname(id1, id2, id3) 
{
    if (document.getElementById(id1).value.length > 0){
        document.getElementById(id2).value = document.getElementById(id1).value + "_" + document.getElementById(id3).value;}
    else {
        document.getElementById(id2).value = '';
    }
}

function setrestaurant() {
    document.forms[0].action='Home.aspx';
    document.forms[0].submit();
}

function openpage(pgname,cid,mid)
{
	document.getElementById(mid).className="nav-top-item current";
	document.getElementById(mid).style.display = '';
	document.getElementById(cid).className="current";
	window.location.href=pgname;
}

function removerec(id, pgname) {

if (confirm("Do you want to delete ?"))
    window.location.href=pgname+ "?id=" + id + "&mode=del";
}

function removeLocationAlert(id, pgname) {

    var r = confirm("Are you sure you want to delete this location? This operation cannot be undone!");
    if (r == true) {
        window.location.href = pgname + "?RID=" + id + "&mode=del";
    }
}

function showhide(id1,id2,id3,cp,lbcp,tcd,lbtcd,mode) {

    if (document.getElementById(id1).value == "D")
    {
        document.getElementById(id2).style.display='';
        //document.getElementById('Copies').style.display='';
        document.getElementById(cp).style.display='';
        document.getElementById(lbcp).style.display='';
        document.getElementById(id3).style.display='none';
        document.getElementById(tcd).style.display='none';
        document.getElementById(lbtcd).style.display='none';
        //document.getElementById('TCD').style.display='none';
    }
    else if (document.getElementById(id1).value == "P")
    {
        document.getElementById(id2).style.display='none';
        document.getElementById(cp).style.display='none';
        document.getElementById(lbcp).style.display='none';
        //document.getElementById('Copies').style.display='none';
        //document.getElementById('TCD').style.display='';
        document.getElementById(id3).style.display='';
        document.getElementById(tcd).style.display='';
        document.getElementById(lbtcd).style.display='';
    }
}

function hide(mode,id1,id2,printertype) {
    if (mode == "add") {
        document.getElementById(id1).style.display = 'none';
        document.getElementById(id2).style.display = 'none';
    }
    else 
    {
        if (document.getElementById(printertype).value == "D") {
            document.getElementById(id1).style.display = '';
            document.getElementById(id2).style.display = 'none';
        }
        else if (document.getElementById(printertype).value == "P") {

            document.getElementById(id1).style.display = 'none';
            document.getElementById(id2).style.display = '';
        }
    }
}

function copyval(source,target)
{
    document.getElementById(target).value = document.getElementById(source).value;
}

function colorChanged(sender) {
    sender.get_element().style.color = "#" + sender.get_selectedColor();
}

function disabletextbox(id) {
    document.getElementById(id).disabled = true;
}

function enabletextbox(id) {
    document.getElementById(id).disabled = false;
}

function PopupPicker(ctl,w,h)
{
  var PopupWindow=null;
  settings='width='+ w + ',height='+ h + ',location=no,directories=0,  menubar=0,toolbar=0,status=0,  scrollbars=0,resizable=0,  dependent=0';
  PopupWindow=window.open('DatePicker.aspx?Ctl=' +     ctl,'DatePicker',settings);
  PopupWindow.focus();
}

function SetDate(dateValue) {
    // retrieve from the querystring the value of the Ctl param,
    // that is the name of the input control on the parent form
    // that the user want to set with the clicked date
    ctl = window.location.search.substr(1).substring(4);
    alert(ctl)
    // set the value of that control with the passed date
    thisForm = window.opener.document.forms[0].elements[ctl].value = dateValue;
    // close this popup
    self.close();
}

function selectAll(invoker) {
    // Since ASP.NET checkboxes are really HTML input elements
    //  let's get all the inputs 
    var inputElements = document.getElementsByTagName('input');

    for (var i = 0; i < inputElements.length; i++) {
        var myElement = inputElements[i];

        // Filter through the input types looking for checkboxes
        if (myElement.type === "checkbox") {

            // Use the invoker (our calling element) as the reference 
            //  for our checkbox status
            myElement.checked = invoker.checked;
        }
    }
}

function deselectAll(invoker) {
alert('here')
    // Since ASP.NET checkboxes are really HTML input elements
    //  let's get all the inputs
    var flag = 0;
    var inputElements = document.getElementsByTagName('input');

    for (var i = 0; i < inputElements.length; i++) {
        var myElement = inputElements[i];

        // Filter through the input types looking for checkboxes
        if (myElement.type === "checkbox") {

            // Use the invoker (our calling element) as the reference 
            //  for our checkbox status
            invoker.checked = myElement.checked;
        }
    }
}

function PrintDoc() 
{
    window.print();
}


function getClientCurrentDateTime() {
    var curdate = new Date()
    var offset = curdate.getTimezoneOffset()

    //offset = ((offset<0? '+':'-')+  pad(parseInt(Math.abs(offset/60)), 2)+ pad(Math.abs(offset%60), 2))
    offset = ((offset < 0 ? 'p' : 'm') + pad(parseInt(Math.abs(offset / 60)), 2) + ':' + pad(Math.abs(offset % 60), 2))
    //alert(curdate)
    //alert(offset)
    //alert(timenow())
   //alert(timenow() + offset)
    return (timenow() + offset);
}

function pad(number, length){
    var str = "" + number
    while (str.length < length) {
        str = '0'+str
    }
    return str
}

function timenow() {
    var now = new Date(), ampm = 'am', h = now.getHours(), m = now.getMinutes(), s = now.getSeconds(), yr = now.getFullYear(), dy = now.getDate(), mth = now.getMonth() + 1;

    if (mth < 10)
        mth = '0' + mth;

    /*    if(h>= 12){
    if(h>12)h-= 12;
    ampm= 'pm';
    }
    */
    if (h < 10) h = '0' + h;
    if (m < 10) m = '0' + m;
    if (s < 10) s = '0' + s;
    //    return now.toLocaleDateString()+' '+h+':'+m+':'+s+' '+ampm;
    return mth + '/' + dy + '/' + yr + ' ' + h + ':' + m + ':' + s + ' ';
}

function calcreduceamt(id1, id2, id3, id4, symbol) {
    var reduceamt,finalamt;
    var orderedamt = parseFloat(document.getElementById(id1).value);
    var reducepercent = parseFloat(document.getElementById(id2).value);
    if (document.getElementById(id4).checked) {
        reduceamt = parseFloat((orderedamt * reducepercent) / 100);
        finalamt = parseFloat(orderedamt - reduceamt);
    }
    else
        finalamt = orderedamt;
         
    document.getElementById(id3).innerHTML = symbol + finalamt.toFixed(2);
}


function myFunc(evt) {
    var charCode = (evt.which) ? evt.which : event.keyCode;
    if (charCode != 46 && charCode > 31 && (charCode < 48 || charCode > 57))
        return false;

    return true;
}

function select_all() {
    $('input[class=case]:checkbox').each(function () {
        if ($('input[class=check_all]:checkbox:checked').length == 0) {
            $(this).prop("checked", false);
        } else {
            $(this).prop("checked", true);
        }
    });
}

function select_allPurchase() {
    $('input[class=case2]:checkbox').each(function () {
        if ($('input[class=check_all2]:checkbox:checked').length == 0) {
            $(this).prop("checked", false);
        } else {
            $(this).prop("checked", true);
        }
    });
}