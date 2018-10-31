$(document).ready(function (e) {


    //Form Viajero
    var FormViajero = function (e) {
        e.preventDefault();

        var clave = $("#clave-viajero");
        var confirmaClave = $("#confirma-clave-viajero");
        var mensajeDanger = $("#alert-danger-viajero");  
        var mensajeSuccess = $("#alert-success-viajero");

        if (clave.val() != confirmaClave.val()) {
            mensajeDanger.text("Las claves no coinciden");
            mensajeSuccess.css("display", "none");
            mensajeDanger.css("display", "block");
        }
        else {
            var formulario = $(this).serialize();            
            $.ajax({
                type: "POST",
                url: "/Usuario/Create",
                data: formulario,
                dataType: "json",
                success: function (result) {
                    if (result == true) {
                        mensajeDanger.text("El correo ingresado ya ha sido registrado anteriormente");
                        mensajeDanger.css("display", "block");
                        mensajeSuccess.css("display", "none");
                    }
                    else
                    {
                        mensajeSuccess.text(result);
                        mensajeDanger.css("display", "none");
                        mensajeSuccess.css("display", "block");
                    }                    
                }
            });
        }        
    }
    $("#form-viajero").submit(FormViajero);

    //Form Tour - Agencia de viaje
    var FormTourAgencia = function (e) {
        e.preventDefault();

        var clave = $("#clave-agencia");
        var confirmaClave = $("#confirma-clave-agencia");
        var mensajeDanger = $("#alert-danger-agencia");
        var mensajeSuccess = $("#alert-success-agencia");

        if (clave.val() != confirmaClave.val()) {
            mensajeDanger.text("Las claves no coinciden");
            mensajeSuccess.css("display", "none");
            mensajeDanger.css("display", "block");
        }
        else {
            $.ajax({
                type: "POST",
                url: "/Usuario/Create",
                data: new FormData(this),
                contentType: false,
                cache: false,
                processData: false,
                dataType: "json",
                success: function (result) {
                    if (result == true) {
                        mensajeDanger.text("El correo ingresado ya ha sido registrado anteriormente");
                        mensajeDanger.css("display", "block");
                        mensajeSuccess.css("display", "none");
                    }
                    else {
                        mensajeSuccess.text(result);
                        mensajeDanger.css("display", "none");
                        mensajeSuccess.css("display", "block");
                    }
                }
            });
        }              
    }    
    $("#form-agencia").submit(FormTourAgencia);

    //Botón cerrar viajero
    var btn_cerrar_viajero = function () {
        var modal_registro = $("#modal-registro");        
        modal_registro.modal("hide");

        var mensajeDanger = $("#alert-danger-viajero");
        mensajeDanger.css("display", "none");

        var mensajeSuccess = $("#alert-success-viajero");
        mensajeSuccess.css("display", "none");
    }
    $("#btn-cerrar-viajero").click(btn_cerrar_viajero);

    //Botón cerrar agencia
    var btn_cerrar_agencia = function () {
        var modal_registro = $("#modal-registro");
        modal_registro.modal("hide");

        var mensajeDanger = $("#alert-danger-agencia");
        mensajeDanger.css("display", "none");

        var mensajeSuccess = $("#alert-success-agencia");
        mensajeSuccess.css("display", "none");
    }
    $("#btn-cerrar-agencia").click(btn_cerrar_agencia);
});