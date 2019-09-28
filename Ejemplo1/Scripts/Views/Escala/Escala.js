$(document).ready(function () {
    LlenarViajes();
    LlenarPuertos();
});

function GuardarEscala() {

    var DTEscala = {};
    DTEscala.idViaje = $('#cmbViaje').val();
    DTEscala.idPuerto = $('#cmbPuerto').val();
    DTEscala.fechaEscala = $('#txtFecha').val();
    DTEscala.numeroDias = $('#txtDias').val();

    $.ajax({
        type: "POST",
        url: "/Escala/generarArchivoEscala",
        dataType: "json",
        data: JSON.stringify(DTEscala),
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            if (data.Resultado) {

                alertify.success("Transacción Exitosa, La escala fue registrada.");

            }
            else {

                alertify.error("Error en la transacción. No fue posible registrar la escala.");
            }
        }
    });
}

function ConsultarEscalas() {

    var DTViaje = {};

    $.ajax({
        type: "POST",
        url: "/Escala/consultarEscalas",
        dataType: "json",
        data: JSON.stringify(DTViaje),
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            if (data.Resultado) {
                $("#bodyId").empty();

                alertify.success("Transacción Exitosa.");

                $("#grid-selection").append("<tbody>");
                $.each(data.Datos, function (i, v) {
                    $("#grid-selection").append("<tr><td>" + v.idEscala + "</td><td>" + v.viaje + "</td><td>" + v.nombrePuerto + "</td><td>" + v.fechaEscala + "</td><td>" + v.numeroDias + "</td></tr>");
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

function LlenarViajes() {
    var DTBarco = {};

    $.ajax({
        type: "POST",
        url: "/Viaje/ConsultarViajes",
        dataType: "json",
        data: JSON.stringify(DTBarco),
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            if (data.Resultado) {
                $.each(data.Datos, function (i, v) {
                    $("#cmbViaje").append("<option value='" + v.idViaje + "'>" + v.idViaje + "." + v.PuertoOrigen + "-" + v.PuertoDestino + "</option>");
                });

            }
            else {

                //alertify.error("Error en la transacción. No fue posible consultar el listado de barcos.");
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
                    $("#cmbPuerto").append("<option value='" + v.idPuerto + "'>" + v.nombrePuerto + "</option>");
                    
                });


            }
            else {

                alertify.error("Error en la transacción. No fue posible consultar los puertos.");
            }
        }
    });
}
