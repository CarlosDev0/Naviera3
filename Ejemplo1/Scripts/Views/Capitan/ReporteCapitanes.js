$(document).ready(function () {
    ConsultarCapitanes(); 
   
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
                //RTVSSSp_ConsultarCiudadesXDepto();

            }
            else {
                //objMensajes.OcultarMensaje();
                alertify.error("Error en la transacción. No fue posible registrar el capitán. Verifique si ya existe en el sistema.");
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
                });
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
