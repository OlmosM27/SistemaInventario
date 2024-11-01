﻿let datatable;

$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    datatable = $('#dtBodegas').DataTable({
        "ajax": {
            "url": "/Admin/Bodega/GetAll"
        },
        "columns": [
            {"data": "name", "width": "20%"},
            {"data": "description", "width": "40%"},
            {
                "data": "status", "width": "20%",
                "render": function (data) {
                    if (data==true) {
                        return "Activo"
                    }
                    else {
                        return "Inactivo"
                    }
                }
            },
            {
                "data": "id",
                "render": function (data) {
                    return `
                        <div class="text-center">
                            <a href="/Admin/Bodega/Upsert/${data}" class="btn btn-primary text-white" style="cursor:pointer">
                                <i class="bi bi-pencil-square"></i>
                            </a>
                            <a onclick=Delete("/Admin/Bodega/Delete/${data}") class="btn btn-danger text-white" style="cursor:pointer">
                                <i class="bi bi-trash-fill"></i>
                            </a>
                        </div>
                    `;
                }, "width": "20%"
            }

        ]
    })
}

function Delete(url) {
    swal({
        title: "¿Está seguro de eliminar esta Bodega?",
        text: "El registro no podrá ser recuperado",
        icon: "warning",
        buttons: true,
        dangerMode: true
    }).then((borrar) => {
        if (borrar) {
            $.ajax({
                type: "POST",
                url: url,
                success: function (data) {
                    if (data.success) {
                        toastr.success(data.message);
                        datatable.ajax.reload();
                    }
                    else {
                        toastr.error(data.message);
                    }
                }
            })
        }
    })
}