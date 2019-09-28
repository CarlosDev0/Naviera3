var ObjHome = new Home;

$(document).ready(function () {
    var URLactual = window.location.href;
    let params = new URL(URLactual).searchParams;
    ObjHome.DTViaje.idViaje = parseInt((params.get('idViaje')));
    llenvarViaje(ObjHome.DTViaje.idViaje);
    cargarMarineros();
});

function llenvarViaje(idViaje) {
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
                    if (v.idViaje == idViaje) {
                        $("#txtNombreBarco").html(v.NombreBarco);
                        $("#txtPuertoOrigen").html(v.PuertoOrigen);
                        $("#txtPuertoDestino").html(v.PuertoDestino);
                        $("#txtCapitan").html(v.Capitan);

                        //$("#grid-selection").append("<tr><td>" + v.idViaje + "</td><td>" + v.NombreBarco + "</td><td>" + v.PuertoOrigen + "</td><td>" + v.PuertoDestino + "</td><td>" + v.Capitan + "<td>" + "<input id='btnDetalleViaje' type='button' onclick='javascript:detalleViaje(" + v.idViaje + ")' value='Detalle' class='btn btn - success' />" + "</td></tr>");
                    }
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

function ConsultarMarinerosViaje() {
    DTDlleViaje = {
        idViaje: ObjHome.DTViaje.idViaje
    };

    $.ajax({
        type: "POST",
        url: "/Viaje/ConsultarMarinerosViaje",
        dataType: "json",
        data: JSON.stringify(DTDlleViaje),
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            if (data.Resultado) {
                $("#bodyId").empty();

                alertify.success("Transacción Exitosa.");

                $("#grid-selection").append("<tbody>");
                $.each(data.Datos, function (i, v) {
                    $("#grid-selection").append("<tr><td>" + v.idViaje + "</td><td>" + v.cedulaMarinero + "</td><td>" + v.nombreMarinero +  "</td></tr>");


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

function Home() {
    this.DTViaje = {
        idViaje: null,
        ceduladMarinero:null
    };

}
function cargarMarineros() {
    var DTMarinero = {};

    $.ajax({
        type: "POST",
        url: "/Marinero/ConsultarMarineros",
        dataType: "json",
        data: JSON.stringify(DTMarinero),
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            if (data.Resultado) {
                $("#cmbMarineros").append("<option>Seleccione un marinero</option>");
                $.each(data.Datos, function (i, v) {
                    $("#cmbMarineros").append('<option id=' + v.cedula + '>' + v.nombreMarinero + '</option>');

                });
            }
            else {
                alertify.error("Error en la transacción. No fue posible consultar el listado de marineros.");
            }
        }
    });
    
}

function showOptions(s) {
    ObjHome.DTViaje.ceduladMarinero=s[s.selectedIndex].id;
}

function AdicionarMarinero() {
    if (ObjHome.DTViaje.ceduladMarinero !== null) {
        var DTDlleViaje = {
            idViaje: ObjHome.DTViaje.idViaje,
            cedulaMarinero: ObjHome.DTViaje.ceduladMarinero
        };

    var cedulaMarinero = $("#cmbMarineros").val();


        $.ajax({
            type: "POST",
            url: "/Viaje/AdicionarMarinero",
            dataType: "json",
            data: JSON.stringify(DTDlleViaje),
            contentType: "application/json; charset=utf-8",
            success: function (data) {
                if (data.Resultado) {
                    $("#bodyId").empty();

                    alertify.success("Transacción Exitosa.");

                    $("#grid-selection").append("<tbody>");
                    $.each(data.Datos, function (i, v) {
                        if (v.idViaje == DTDlleViaje.idViaje) {
                            //$("#txtNombreBarco").html(v.NombreBarco);
                            //$("#txtPuertoOrigen").html(v.PuertoOrigen);
                            //$("#txtPuertoDestino").html(v.PuertoDestino);
                            //$("#txtCapitan").html(v.Capitan);

                            $("#grid-selection").append("<tr><td>" + v.idViaje + "</td><td>" + v.cedulaMarinero + "</td><td>" + v.nombreMarinero + "</td></td></tr>");
                        }
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

}