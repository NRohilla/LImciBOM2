$(function () {

    $("#Sucess").click(function () {

        toastr.success('sucess', 'sucess');
    });
});




$(document).ready(function () {

    var table = $('#example').DataTable();

    $('#example tbody').on('click', 'tr', function () {
        if ($(this).hasClass('selected')) {
            $(this).removeClass('selected');
        }
        else {

            table.$('tr.selected').removeClass('selected');
            $(this).addClass('selected');
            var rowData = table.row(this).data();
            //var UnitId = rowData.item[0];
            //alert(rowData[0]);
        }
    });

    $('#button').click(function () {
        table.row('.selected').remove().draw(false);
    });
});