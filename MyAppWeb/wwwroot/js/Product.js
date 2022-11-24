var dtable;
$(document).ready(function () {
    dtable = $('#myTable').DataTable({

        ajax: {
            url: "/Admin/Product/AllProducts"
        },

        columns: [
            { data: "name" },//Key value pair name= column name return through json
            { data: "description" },
            { data: "price" },
            { data: "category.name" },
            {
                data: "id",
                render: function (data) {
                    return `

                    <a href="/Admin/Product/CreateUpdate?id=${data}"><i class="bi bi-pencil-square"></i></a>
                    <a onCLick=RemoveProduct("/Admin/Product/Delete/${data}")><i class="bi bi-trash3"></i></a>
                   
                    `
                }
            },
        ]

    });

});

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
