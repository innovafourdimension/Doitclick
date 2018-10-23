$(function () {

    $.ajax({
        type: 'GET',
        url: '/api/Auth/listar-comisionistas'
    }).done(function(data){
        $.each(data, function(i,e){
            $('#selResultadoAsignacion').append(`<option value="${e.identificador}">${e.nombres}</option>`)
        });
    });

    $('#frm-generico').on('submit', function (e) {
        e.preventDefault();
        var model = $(this).serializeFormJSON();
        console.log(model)

        $.ajax({
            type: "POST",
            url: "/api/flujo-interno/asignar-cotizacion",
            data: JSON.stringify(model),
            contentType: "application/json; charset=utf-8"
        }).done(function (data) {
            console.log(data);
            $.niftyNoty({
                type: "success",
                container : "floating",
                title : "Notificaciones Workflow",
                message : "Trabajo Asignado Existosamente. Estamos cerrando esta tarea!<br/><small>Este mensaje se autocierra en 5 segundos y te redirige a tu gestión</small>",
                closeBtn : false,
                timer : 5000,
                onHidden: function(){
                    location.href = '/mi-gestion'
                }
            });
            //const user = "Chachacharles";
            //const message = new Date().toString() + " Charly";
            //connection.invoke("SendMessage", user, message).catch(err => console.error(err.toString()));

        }).fail(function (errMsg) {
            console.log(errMsg);
            $.niftyNoty({
                type: "warning",
                container : "floating",
                title : "Notificaciones Workflow",
                message : ":( Problemas al asignar trabajo contacta a soporte",
                closeBtn : false,
                timer : 5000
            });
        }).always(function () {
            $("#btn-confirmar").prop("enabled", true).text(initialLabelText);
            //const user = "Chachacharles";
            //const message = "Siempre paso por aqui";
            //connection.invoke("SendMessage", user, message).catch(err => console.error(err.toString()));
        });
    });
});