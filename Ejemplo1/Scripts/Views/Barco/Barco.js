$(document).ready(function () {

});


function GuardarBarco() {
    var Nombre = $('#txtNombreBarco').val();
    var DTBarco = {};
    DTBarco.NombreBarco = $('#txtNombreBarco').val();
    DTBarco.TipoBarco = $('#Tipo').val();
    DTBarco.AñoConstruccion = $('#Año').val();
    DTBarco.CapacidadMaxima = $('#Capacidad').val();
    DTBarco.RegistroMercantil = $("#registroMercantil").val();

    $.ajax({
        type: "POST",
        url: "/Barco/GenerarArchivoBarco",
        dataType: "json",
        data: JSON.stringify(DTBarco),
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            if (data.Resultado) {
                
                
                alertify.success("Transacción Exitosa, El registro fue ingresado.");
                

            }
            else {
                
                alertify.error("Error. Ya existe un Barco con ese Registro Mercantil.");
            }
        }
    });
}

function ConsultarBarcos() {
    
    var DTBarco = {};

    $.ajax({
        type: "POST",
        url: "/Barco/ConsultarBarcos",
        dataType: "json",
        data: JSON.stringify(DTBarco),
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            if (data.Resultado) {
                $("#bodyId").empty();
                alertify.success("Transacción Exitosa.");
                //RTVSSSp_ConsultarCiudadesXDepto();
                $("#grid-selection").append("<tbody>");
                $.each(data.Datos, function (i, v) {
                    $("#grid-selection").append("<tr><td>" + v.idBarco + "</td><td>" + v.nombreBarco + "</td><td>" + v.tipoBarco + "</td><td>" + v.añoConstruccion + "</td><td>" + v.capacidadMaxima + "</td><td>" + v.registroMercantil + "</td></tr>");
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