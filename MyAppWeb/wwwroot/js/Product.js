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
                data: "category.id",
                render: function (data) {
                    return `
                    <a href="/Admin/Product/CreateUpdate?id=${data}"><i class="bi bi-pencil-square"></i></a>
                    <a href="/Admin/Product/CreateUpdate?id=${data}"><i class="bi bi-trash3"></i></a>
                    `
                }
            },
        ]

    });

});

