$(document).ready(function () {

});


function GuardarMarinero() {
    var DTMarinero = {};
    DTMarinero.NombreMarinero = $('#txtNombreMarinero').val();
    DTMarinero.Cedula = $('#txtCedula').val();
    DTMarinero.EstadoCivil = $('#cmbEstadoCivil').val();
    
    $.ajax({
        type: "POST",
        url: "/Marinero/GenerarArchivoMarinero",
        dataType: "json",
        data: JSON.stringify(DTMarinero),
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            if (data.Resultado) {
                alertify.success("Transacción Exitosa, El marinero fue guardado.");
            }
            else {
                alertify.error("Error en la transacción. No fue posible ingresar el Marinero. Verifique si ya está inscrito");
            }
        }
    });
}

function ConsultarMarineros() {

    var DTMarinero = {};

    $.ajax({
        type: "POST",
        url: "/Marinero/ConsultarMarineros",
        dataType: "json",
        data: JSON.stringify(DTMarinero),
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            if (data.Resultado) {
                
                $("#tbodyid").html('');
                $("#grid-selection").append("<tbody>");
                $.each(data.Datos, function (i, v) {

                    $("#grid-selection").append("<tr><td>" + v.nombreMarinero + "</td><td>" + v.cedula + "</td><td>" + v.estadoCivil + "</td><td>" + "<a class='btn btn-warning' placeholder='Editar' id='btn' href='javascript:EditarMarinero();><span class='k-icon next'></span>Edit</a></td></tr>");


                });
                $("#grid-selection").append("</tbody>");
            }
            else {
                
                alertify.error("Error en la transacción. No fue posible consultar el listado de marineros.");
            }
        }
    });
}
function EditarMarinero(cedula) {
    var DTMarinero = {};
    DTMarinero.cedula = cedula;

    $.ajax({
        type: "POST",
        url: "/Marinero/ConsultarMarineroPorCedula",
        dataType: "json",
        data: JSON.stringify(DTMarinero),
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            if (data.Resultado) {

                $.each(data.Datos, function (i, v) {


                    $("#grid-selection").append("<tr><td>" + v.nombreMarinero + "</td><td>" + v.cedula + "</td><td>" + v.estadoCivil + "</td><td>" +  "</td></tr>");
                })
                $("#grid-selection").append("</tbody>");
            }
            else {

                alertify.error("Error en la transacción. No fue posible consultar el listado de marineros.");
            }
        }
    });
}