$(document).ready(function () {
    LlenarBarcos();
    LlenarCapitanes();
    LlenarPuertos();
});

function GuardarViaje() {
    
    var DTViaje = {};
    DTViaje.idBarco = $('#cmbNombreBarco').val();
    DTViaje.idPuertoOrigen = $('#cmbPuertoOrigen').val();
    DTViaje.idPuertoDestino = $('#cmbPuertoDestino').val();
    DTViaje.cedulaCapitan = $('#cmbCapitan').val();

    $.ajax({
        type: "POST",
        url: "/Viaje/GenerarArchivoViaje",
        dataType: "json",
        data: JSON.stringify(DTViaje),
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            if (data.Resultado) {
                
                alertify.success("Transacción Exitosa, El viaje fue registrado.");

            }
            else {
                if (data.Mensaje.Texto === "") {
                    alertify.error("Error en la transacción. No fue posible registrar el viaje.");
                } else {
                    alertify.error(data.Mensaje.Texto);
                }
                
            }
        }
    });
}

function ConsultarViaje() {

    var DTViaje = {};

    $.ajax({
        type: "POST",
        url: "/Viaje/ConsultarViajes",
        dataType: "json",
        data: JSON.stringify(DTViaje),
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            if (data.Resultado) {
                $("#bodyId").empty();

                alertify.success("Transacción Exitosa.");
                
                $("#grid-selection").append("<tbody>");
                $.each(data.Datos, function (i, v) {
                    $("#grid-selection").append("<tr><td>" + v.idViaje + "</td><td>" + v.NombreBarco + "</td><td>" + v.PuertoOrigen + "</td><td>" + v.PuertoDestino + "</td><td>" + v.Capitan + "</td><td>" + '<a onclick="redireccionar(' + v.idViaje + ');" class="btn btn-info"><span class="glyphicon glyphicon-edit"></span></a>' +"</td></tr>");
                });
                $("#grid-selection").append("</tbody>");
            }
            else {
                $("#bodyId").empty();
                alertify.error("Error en la transacción. No fue posible consultar el listado de viajes.");
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
                    $("#cmbNombreBarco").append("<option value='" + v.idBarco + "'>" + v.nombreBarco + "</option>");
                });
                
            }
            else {
                
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
                    $("#cmbCapitan").append("<option value='" + v.cedulaCapitan + "'>" + v.nombreCapitan + "</option>");
                });
                

            }
            else {
                
                alertify.error("Error en la transacción. No fue posible consultar los capitanes.");
            }
        }
    });
}

function LlenarPuertos() {
    
    var DTPuerto = {};

    $.ajax({
        type: "POST",
        url: "/Puerto/ConsultarPuertos",
        dataType: "json",
        data: JSON.stringify(DTPuerto),
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            if (data.Resultado) {
                $.each(data.Datos, function (i, v) {
                    $("#cmbPuertoOrigen").append("<option value='" + v.idPuerto + "'>" + v.nombrePuerto + "</option>");
                    $("#cmbPuertoDestino").append("<option value='" + v.idPuerto + "'>" + v.nombrePuerto + "</option>");
                });
                

            }
            else {
                
                alertify.error("Error en la transacción. No fue posible consultar los puertos.");
            }
        }
    });
}

function redireccionar(idViaje) {
    
    var DTViaje = {};
    
    window.location.href = "/Viaje/DetalleViaje?idViaje=" + idViaje;

}