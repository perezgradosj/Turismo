$(document).ready(function (e) {

    //Boton Ingresar
    var formLogin = function (e) {
        e.preventDefault();
        var formulario = $(this).serialize();
        var menssageDanger = $("#alert-danger-login");
        $.ajax({
            type: "POST",
            url: "/Usuario/Autentication",
            data: formulario,
            dataType: "json",
            success: function (result) {
                if (result == false) {
                    menssageDanger.text("Usuario o clave incorrecta");
                    menssageDanger.css("display", "block");
                } else {
                    menssageDanger.css("display", "none");
                    location.reload(true);
                }               
            }
        });
    }
    $("#form-ingresar").submit(formLogin);
});