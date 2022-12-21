var dtable;
$(document).ready(function () {
    var url = window.location.search;
    if (url.includes("pending")) {
        orderTable("pending")
    }
    else {
        if (url.includes("approved")) {
            orderTable("approved")
        }
    }
   

});

function orderTable(status) {
    dtable = $('#myTable').DataTable({

        ajax: {
            url: "/Admin/Order/AllOrders?status=" + status
        },

        columns: [
            { data: "name" },//Key value pair name= column name return through json
            { data: "phone" },
            { data: "orderStatus" },
            { data: "orderTotal" }, //Use camel Notation
            {
                data: "id",
                render: function (data) {
                    return `

                    <a href="/Admin/Order/OrderDetails?id=${data}"><i class="bi bi-pencil-square"></i></a>
                   
                    `
                }
            },
        ]

    });
}

function RemoveProduct(url)
{
    const swalWithBootstrapButtons = Swal.mixin({
        customClass: {
            confirmButton: 'btn btn-success',
            cancelButton: 'btn btn-danger'
        },
        buttonsStyling: false
    })

    swalWithBootstrapButtons.fire({
        title: 'Are you sure?',
        text: "You won't be able to revert this!",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonText: 'Yes, delete it!',
        cancelButtonText: 'No, cancel!',
        reverseButtons: true
    }).then((result) => {

        if (result.isConfirmed) {
            $.ajax({
                url: url,
                type: 'Delete',
                success: function (data) {
                    if (data.success) {
                        toastr.success(data.message);
                        dtable.ajax.reload();
                    }
                    else {
                        toastr.error(data.message);
                    }
                }

            })
        }

    })

}
