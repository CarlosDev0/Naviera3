var ObjHome = new Home;

$(document).ready(function () {
   
 
});



function GuardarPuerto() {
    
    var DTPuerto = {};
    DTPuerto.NombrePuerto = $('#txtNombrePuerto').val();
    DTPuerto.Pais = $('#txtPais').val();
    DTPuerto.RegistroMercantil = $('#txtRegistroMercantil').val();
    DTPuerto.EstadoRegistroMercantil = $('#cmbEstadoRM').val();

    $.ajax({
        type: "POST",
        url: "/Puerto/GenerarArchivoPuerto",
        dataType: "json",
        data: JSON.stringify(DTPuerto),
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            if (data.Resultado) {
                
                alertify.success("Transacción Exitosa, El puerto fue Registrado.");

            }
            else {
                
                alertify.error("Error en la transacción. Ya existe otro puerto con el mismo registro mercantil.");
            }
        }
    });
}

function ConsultarPuertos() {

    var DTPuerto = {};
    var Estado = "";

    $.ajax({
        type: "POST",
        url: "/Puerto/ConsultarPuertos",
        dataType: "json",
        data: JSON.stringify(DTPuerto),
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            if (data.Resultado) {

                $("#tbodyId").empty();
                
                $("#grid-selection").append("<tbody>");
                $.each(data.Datos, function (i, v) {
                    if (v.estadoRegistroMercantil) {
                        Estado = "Activo";
                    } else {
                        Estado = "Inactivo";
                    }
                    $("#grid-selection").append("<tr><td>" + v.idPuerto + "</td><td>" + v.nombrePuerto + "</td><td>" + v.pais + "</td><td>" + v.registroMercantil + "</td><td>" + Estado + "</td><td>" + '<a onclick="editarPuerto(' + v.idPuerto + ');" class="btn btn-info"><span class="glyphicon glyphicon-edit"></span></a>' + "</td></tr>");
                });
                $("#grid-selection").append("</tbody>");
            }
            else {
                
                alertify.error("Error en la transacción. Aún no existen Puertos en el Sistema.");
            }
        }
    });
}

function editarPuerto(idPuerto) {
    ObjHome.idPuerto = idPuerto;
    var DTPuerto = {
        idPuerto: idPuerto
    };

    var Estado = "";

    $.ajax({
        type: "POST",
        url: "/Puerto/ConsultarUnPuerto",
        dataType: "json",
        data: JSON.stringify(DTPuerto),
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            if (data.Resultado) {

                
                $.each(data.Datos, function (i, v) {
                    //$("#nombrePuertoM").html(v.nombrePuerto);
                    $("#txtPaisM").val(v.pais);
                    $("#nombrePuertoM").val(v.nombrePuerto);
                    $("#registroM").val(v.registroMercantil);
                    $("#Estado").val(v.estadoRegistroMercantil);
                });
                $("#playerModal").modal("show");
            }
            
        }
    });

}




function GuardarPuertoEditado() {

    var DTPuerto = {};
    DTPuerto.idPuerto = ObjHome.idPuerto;
    DTPuerto.nombrePuerto = $('#nombrePuertoM').val();
    DTPuerto.pais = $('#txtPaisM').val();
    DTPuerto.registroMercantil = $('#registroM').val();
    DTPuerto.estadoRegistroMercantil = $('#Estado').val();

    $.ajax({
        type: "POST",
        url: "/Puerto/GuardarPuertoEditado",
        dataType: "json",
        data: JSON.stringify(DTPuerto),
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            if (data.Resultado) {
                alertify.success("Transacción Exitosa, El puerto fue Actualizado.");

            }
            else {
                alertify.error("Error en la transacción. No fue posible actualizar el nombre de la ciudad.");
            }
        }
    });
}


function Home() {
    this.puerto = {
        idPuerto: null
    };
    
}