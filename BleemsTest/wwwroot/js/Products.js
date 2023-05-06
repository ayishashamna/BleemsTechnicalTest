$(document).ready(function () {
    var dataTable = $('#productsTable').DataTable({
        "autoWidth": "false",
        "width":"100%",
        "processing": true,
        "lengthMenu": [[10, 20, 50, -1], [10, 20, 50, "All"]],
        "ajax": {
            "url": "/Product/GetProducts",
            "type": "GET",
            "datatype": "json",
            "dataSrc": ""
        },
        "columns": [
            {
                "data": "photoUrl",
                "render": function (data, type, row, meta) {
                    return '<img src="' + data + '" height="50">';
                }
            },
            { "data": "productName" },
            {
                "data": "productPrice",
                "render": function (data, type, row, meta) {
                    return data + ' ' + row.currency;
                }
            },
            {
                "data": "date",
                "render": function (data, type, row, meta) {
                    return moment(data).format('DD-MM-YYYY');
                }
            },
            { "data": "categoryName" },
            {
                "data": "productId",
                "render": function (data) {
                    return '<a class="btn btn-primary btn-sm" href="/Product/Edit/' + data + '">Edit</a>';
                }
            },
            {
                "data": "productId",
                "render": function (data) {
                    return '<form method="post" action="/Product/Delete/' + data + '" onsubmit="return confirm(\'Are you sure you want to delete this product?\')"><input type="submit" value="Delete" class="btn btn-danger btn-sm" /></form>';
                  }
            }
        ],
        "language": {
            "emptyTable": "No products available."
        }

    });
    $('#applyFilters').click(function () {
        var year = $('#yearFilter').val();
        var month = $('#monthFilter').val();
        var day = $('#dayFilter').val();
        var table = $('#productsTable').DataTable();        
        var dt = (day + '' + month + '' + year);
        if (year != '' && month != '' && day != '') {
            dt = (day + '-' + month + '-' + year);
        }
        else if (month == '' && day != '' && year != '') {
            alert("Please select month");
        }
        else if (day == '' && month != '' && year != '') {
            dt = (day + '-' + month + '-' + year);
        }
        else if (year == '' && month != '' && day != '') {
            dt = (day + '-' + month + '-' + year);
        }
        console.log(dt);
        table.column(3).search(dt).draw();

    });

});
