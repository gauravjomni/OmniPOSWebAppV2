/*
 * SimpleModal Basic Modal Dialog
 * http://simplemodal.com
 *
 * Copyright (c) 2013 Eric Martin - http://ericmmartin.com
 *
 * Licensed under the MIT license:
 *   http://www.opensource.org/licenses/mit-license.php
 */

jQuery(function ($) {
    // Load dialog on page load
    //$('#basic-modal-content').modal();

    // Load dialog on click
    $('#feedback .basic').click(function (e) {
        $('#basic-modal-content').modal();

        return false;
    });

    $('#basic-modal-content .modalclose').click(function (e) {
        var cnt = 0;
        $('#basic-modal-content').find(':checked').each(function () {
            var name = $(this).attr('name');
            MyLoad('purc_name' + cnt, 'purc_id' + cnt, 'prod_type' + cnt, 'purc_unitcost' + cnt, 'purc_unit' + cnt, 'purc_qty' + cnt);
            alert(name);
        });
        $.modal.close();
    });

});


jQuery(function ($){
});