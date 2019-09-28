$(document).ready(function () {

    var home = {

    };

});

function download(filename, text) {
    var element = document.createElement('a');
    element.setAttribute('href', 'data:text/plain;charset=utf-8,' + encodeURIComponent(text));
    element.setAttribute('download', filename);

    element.style.display = 'none';
    document.body.appendChild(element);

    element.click();

    document.body.removeChild(element);
}


function GenerarArchivo() {
    $.ajax({
        type: "POST",
        url: "/Barco/GenerarArchivoBarco",
        dataType: "json",
        data: JSON.stringify(),
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            if (data.Resultado) {
                //console.log(data);
                //objMensajes.OcultarMensaje();
                alertify.success("Transacción Exitosa, El nombre fue actualizado");
                //RTVSSSp_ConsultarCiudadesXDepto();

            }
            else {
                //objMensajes.OcultarMensaje();
                alertify.error("Error en la transacción. No fue posible actualizar el nombre de la ciudad.");
            }
        }
    });
}