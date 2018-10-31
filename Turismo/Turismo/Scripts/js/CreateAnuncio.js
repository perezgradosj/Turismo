$(document).ready(function (e) {

    //Input desde
    $("#input-desde").change(function () {
        var inputDesde = $("#input-desde");
        var inputHasta = $("#input-hasta");

        if (inputDesde.val() != "") {
            inputHasta.attr("disabled", false);

            inputHasta.attr("min", inputDesde.val());
            inputHasta.focus();
            inputHasta.val("");
        }
        else {
            inputHasta.attr("disabled", "disabled");
            inputHasta.val("");
        }

    });

    //Listado de destinos
    var lstDestino = function () {
        var selectDestino = $("#select-destino");
        $.ajax({
            type:"POST",
            url: "/Anuncio/LstDestinos",
            dataType: "json",
            success: function (result) {
                //console.log(result);
                $.each(result, function (key, rs) {
                    selectDestino.append(
                        "<option value='"+rs.destino+"'>"+rs.destino+"</option>"
                    );
                });
            }
        });
    }     
    lstDestino();

    //Listado de alojamientos
    var lstAlojamiento = function () {
        var selectAlojamiento = $(".select-alojamiento");
        $(".select-alojamiento").empty();
        $.ajax({
            type: "POST",
            url: "/Anuncio/LstAlojamientos",
            dataType: "json",
            success: function (result) {
                //console.log(result);
                $.each(result, function (key, rs) {
                    selectAlojamiento.append(
                        "<option value='" + rs.id + "'>" + rs.alojamiento + "</option>"
                    );
                });
            }
        });
    }
    lstAlojamiento();

    //Agregar casillas de itinerario
    var addItinerario = function (value_select) {
        var tbody_length = $("#table-body-itinerario")[0].children.length;

        if (value_select < tbody_length) {
            var trDelete = tbody_length - value_select;
            for (var i = tbody_length; i > 0; i--) {
                if (i + trDelete == tbody_length) {
                    break;
                }
                $("#tr" + i).remove();
            }
        } else {
            for (var i = tbody_length; i < value_select; i++) {
                $("#table-body-itinerario").append(
                    "<tr id='tr" + (i + 1) + "'>" +
                    "<td><input type='text' readonly class='form-control-plaintext' value='" + (i + 1) + "' name='itiDia' style='width: 20px'/></td>" +
                    "<td><input type = 'text' class = 'form-control form-control-sm' placeholder='Ingrese título para día " + (i + 1) + "' name='itiTitulo' required/></td>" +
                    "<td><textarea class='form-control form-control-sm' placeholder=' * Ejemplo: Actividad 1' name='itiDescripcion' required></textarea></td>" +
                    "<td><select class='form-control form-control-sm select-alojamiento' name='itiAlojamiento' required></select></td>" +
                    "<td>" +
                        "<select class='form-control form-control-sm select-alimentacion' name='itiAlimentacion' required>" +
                            "<option value='Incluir desayuno'>Incluir desayuno</option>" +
                            "<option value='Incluir desayuno, almuerzo y cena'>Incluir desayuno, almuerzo y cena</option>" +
                            "<option value='No incluir ninguno'>No incluir ninguno</option>" +              
                        "</select>"+
                    "</td>"+
                    "</tr>"
                );
            }
            lstAlojamiento();
        }
    }

    //Select dias
    var dinamicNigth = function () {
        $("#select-noches").empty();
        var value = $(this).val();

        addItinerario(value);
           
        $("#select-noches").append(
            "<option value='" + (value - 1) + "'>" + (value - 1) + "</option>",
            "<option value='" + (value) + "'>" + (value) + "</option>"
        );
    }
    $("#select-dias").change(dinamicNigth);

    //Boton terminar y guardar
    var form_anuncioCreate = function (e) {
        e.preventDefault();
        var alertSuccess = $("#alert-success");
        var btnTerminar = $("#btn-terminar");
        $.ajax({
            type: "POST",
            url: "/Anuncio/Create",
            data: new FormData(this),
            contentType: false,
            cache: false,
            processData: false,
            success: function (result) {
                console.log(result);
                btnTerminar.attr("disabled", "disabled");

                alertSuccess.css("display", "block");
                alertSuccess.html(result);
            }
        });
    }
    $("#form-anuncioCreate").submit(form_anuncioCreate);

    //Boton limpiar
    var resetForm = function () {
        $("#alert-success").css("display", "none");

        $("#btn-terminar").attr("disabled", false);

        $("#input-hasta").attr("disabled", "disabled");

        $("#select-noches").empty();
        $("#select-noches").append(
            "<option value='0'>0</option>",
            "<option value='1'>1</option>"
        );

        var tbody_length = $("#table-body-itinerario")[0].children.length;
        var value_select = 1;
        var trDelete = tbody_length - value_select;
        for (var i = tbody_length; i > 0; i--) {
            if (i + trDelete == tbody_length) {
                break;
            }
            $("#tr" + i).remove();
        }
    }
    $("#btn-limpiar").click(resetForm);
});