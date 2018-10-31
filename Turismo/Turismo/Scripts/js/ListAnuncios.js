$(document).ready(function (e) {

    //Listado de anuncios
    var lstAnuncios = function () {
        var table = $("#table-body-misAnuncios");
        $.ajax({
            type: "POST",
            url: "/Anuncio/LstAnuncios",
            dataType: "json",
            success: function (result) {
                //console.log(result);
                $.each(result, function (key, rs) {
                    table.append(
                        "<tr>" +
                        "<td>" + (key+1) + "</td>" +
                        "<td>" + rs.titulo + "</td>" +
                        "<td>" + rs.destino + "</td>" +
                        "<td>" + rs.fecha_public_desde + "</td>" +
                        "<td>" + rs.fecha_public_hasta + "</td>" +
                        "<td>" + (rs.idestado == 1 ? "Activo" : "Inactivo") + "</td>" +
                        "<td><img src='../Img/" + rs.foto1Name + "' class='rounded mx-auto d-block' style='width:60px;height:60px;' /> </td>" +
                        "</tr>"
                    );
                });
            }
        });
    }
    lstAnuncios();
});