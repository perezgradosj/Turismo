$(document).ready(function (e) {

    //Listado de anuncios
    var listado = function (key, rs) {
        var divPaquetes = $("#div-paquetes");
        divPaquetes.append(
            "<div class='col-md-4' style='border-style: solid;padding: 10px;border-color: #eaedef;border-width: 1px;background: #eaedef47;'>" +
                "<img src='/Img/" + rs.foto1Name + "' class='img-thumbnail rounded mx-auto d-block'  style='width:323px;height:221px;' />" +
                "<div class='row'>" +
                    "<div class='col-md-12' align='center'>" +
                        "<label>" + rs.titulo + "</label>" +
                    "</div>" +
                    "<div class='col-md-12' align='center'>" +
                        "<label>Precio: S/. " + rs.precioHabDobleTripe + " por persona</label>" +
                    "</div>" +
                    "<div class='col-md-12' align='center'>" +
                        "<label>Duración: " + rs.dias + " días y " + rs.noches + " noches</label>" +
                    "</div>" +
                    "<div class='col-md-12' align='center'>" +
                        "<button class='btn btn-outline-info btn-block btnDetallePaquete' value='" + rs.id + "' data-toggle='modal' data-target='#DetallePaqueteModal' >Ver detalle >></button>"+
                    "</div>"+
                "</div>" +
            "</div>"
        );
    };
    var lstPaquetes = function () {        
        $.ajax({
            type: "POST",
            url: "/Anuncio/LstAnuncios",
            dataType: "json",
            success: function (result) {
                //console.log(result);
                $.each(result, listado);
            }
        });
    }
    lstPaquetes();

    var lstDestino = function () {
        var selectDestino = $("#select-región");
        $.ajax({
            type: "POST",
            url: "/Anuncio/LstDestinos",
            dataType: "json",
            success: function (result) {
                //console.log(result);
                selectDestino.append(
                    "<option value='todos'>Listar todos</option>"
                );
                $.each(result, function (key, rs) {
                    selectDestino.append(
                        "<option value='" + rs.destino + "'>" + rs.destino + "</option>"
                    );
                });
            }
        });
    }
    lstDestino();

    var filtrarPaquetesByRegion = function () {
        var region = $("#select-región").val();
        $("#div-paquetes").empty();
        $.ajax({
            type: "POST",
            url: "/Paquete/LstByRegion",
            data: { region: region },
            dataType: "json",
            success: function (result) {
                console.log(result);
                $.each(result, listado);
            }
        });
    };
    $("#select-región").change(filtrarPaquetesByRegion);

    var DetallePaquete = function () {
        var idpaquete = $(this).val();
        $("#iditinerarios tbody").empty();
        $("#idalimentacion tbody").empty();
        $.ajax({
            type: "POST",
            url: "/Paquete/FindById",
            data: { idpaquete: idpaquete },
            dataType: "json",
            success: function (result) {
                console.log(result);
                console.log(result.itinerarios);
                var itinerarios = result.itinerarios;
                $("#idfoto1").attr("src", "/Img/" + result.foto1Name);
                $("#idfoto2").attr("src", "/Img/" + result.foto2Name);
                $("#idfoto3").attr("src", "/Img/" + result.foto3Name);
                $("#idfoto4").attr("src", "/Img/" + result.foto4Name);
                $("#idfoto5").attr("src", "/Img/" + result.foto5Name);
                $("#idprecio").text(result.precioHabDobleTripe);
                $("#idtitulo").text(result.titulo);
                $("#idregion").text(result.destino);
                $("#idduracion").text(result.dias + " dias y " + result.noches + " noches");
                $("#idpasajeros").text(result.minPasajeros);
                $("#idanticipado").text(result.horasAnticipado + " horas");

                $.each(itinerarios, function (key, rs)  {
                    $("#iditinerarios tbody").append(
                        "<tr>" +
                        "<td>" + rs.dia + "</td>" +
                        "<td>" + rs.descripcion + "</td>" +
                        "</tr>"
                    );
                });

                $.each(itinerarios, function (key, rs) {
                    $("#idalimentacion tbody").append(
                        "<tr>" +
                        "<td>" + rs.dia + "</td>" +
                        "<td>" + rs.alojamiento + "</td>" +
                        "<td>" + rs.alimentacion + "</td>" +
                        "</tr>"
                    );
                });
            }
        });
    };
    $("#div-paquetes").on("click", ".btnDetallePaquete", DetallePaquete);
});