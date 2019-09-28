$(document).ready(function () {
    ConsultarMarineros();
});




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

                    $("#grid-selection").append("<tr><td>" + v.nombreMarinero + "</td><td>" + v.cedula + "</td><td>" + v.estadoCivil + "</td></tr>");


                });
                $("#grid-selection").append("</tbody>");
            }
            else {
                
                alertify.error("Error en la transacción. No fue posible consultar el listado de marineros.");
            }
        }
    });
}
