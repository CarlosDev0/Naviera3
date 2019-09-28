$(document).ready(function () {
    $("#grid-basic").bootgrid();

    g = $("#grid").val();
    grid = $("#grid").grid({
        dataKey: "ID",
        uiLibrary: "bootstrap",
        columns: [
            { field: "ID", width: 50, sortable: true },
            { field: "Name", sortable: true },
            { field: "PlaceOfBirth", title: "Place Of Birth", sortable: true },
            { field: "DateOfBirth", title: "Date Of Birth", sortable: true },
            { title: "", field: "Edit", width: 34, type: "icon", icon: "glyphicon-pencil", tooltip: "Edit", events: { "click": Edit } },
            { title: "", field: "Delete", width: 34, type: "icon", icon: "glyphicon-remove", tooltip: "Delete", events: { "click": Remove } }
        ],
        pager: { enable: true, limit: 5, sizes: [2, 5, 10, 20] }
    });
    $("#btnAddPlayer").on("click", Add);
    $("#btnSave").on("click", Save);
    $("#btnSearch").on("click", Search);
});


function GuardarCapitan() {
    var Nombre = $('#txtNombreCapitan').val();
    var DTCapitan = {};
    DTCapitan.nombreCapitan = $('#txtNombreCapitan').val();
    DTCapitan.cedulaCapitan = $('#txtCedulaCapitan').val();
 

    $.ajax({
        type: "POST",
        url: "/Capitan/GenerarArchivoCapitan",
        dataType: "json",
        data: JSON.stringify(DTCapitan),
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            if (data.Resultado) {
                //console.log(data);
                //objMensajes.OcultarMensaje();
                alertify.success("Transacción Exitosa, El capitán fue registrado.");
                
                
                $.each(data.Datos, function (i, v) {
                    alertify.success(v.nombreCapitan);
                });
            }
            else {
                //objMensajes.OcultarMensaje();
                alertify.error("La cédula Ingresada ya existe en el sistema. No se registró la información.");
            }
        }
    });
}

function ConsultarCapitanes() {

    var DTCapitan = {};

    $.ajax({
        type: "POST",
        url: "/Capitan/ConsultarCapitanes",
        dataType: "json",
        data: JSON.stringify(DTCapitan),
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            if (data.Resultado) {
                $("#tbodyid").html('');
                
                
                $("#grid-selection").append("<tbody>");
                $.each(data.Datos, function (i, v) {
                    $("#grid-selection").append("<tr><td>" + v.nombreCapitan + "</td><td>" + v.cedulaCapitan + "</td></tr>");
                })
                $("#grid-selection").append("</tbody>");
            }
            else {
                //objMensajes.OcultarMensaje();
                alertify.error("Error en la transacción. No fue posible consultar el listado de barcos.");
            }
        }
    });
}

function LlenarBarcos() {
    var DTBarco = {};

    $.ajax({
        type: "POST",
        url: "/Barco/ConsultarBarcos",
        dataType: "json",
        data: JSON.stringify(DTBarco),
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            if (data.Resultado) {
                $.each(data.Datos, function (i, v) {
                    $("#CmbNombreBarco").append("<option value='" + v.NombreBarco + "'>" + v.NombreBarco + "</option>");
                });
                //alertify.success("Transacción Exitosa.");
                //RTVSSSp_ConsultarCiudadesXDepto();

            }
            else {
                //objMensajes.OcultarMensaje();
                alertify.error("Error en la transacción. No fue posible consultar el listado de barcos.");
            }
        }
    });
}

function LlenarCapitanes() {
    var DTCapitan = {};

    $.ajax({
        type: "POST",
        url: "/Capitan/ConsultarCapitanes",
        dataType: "json",
        data: JSON.stringify(DTCapitan),
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            if (data.Resultado) {
                $.each(data.Datos, function (i, v) {
                    $("#CmbCapitan").append("<option value='" + v.NombreCapitan + "'>" + v.NombreCapitan + "</option>");
                });
                //alertify.success("Transacción Exitosa.");
                //RTVSSSp_ConsultarCiudadesXDepto();

            }
            else {
                //objMensajes.OcultarMensaje();
                alertify.error("Error en la transacción. No fue posible consultar el capitanes.");
            }
        }
    });
}
