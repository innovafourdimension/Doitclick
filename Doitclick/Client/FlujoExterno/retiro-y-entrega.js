$(function(){

    $('#frm-generico').on('submit', function(){
        event.preventDefault();
        let $form = $(this); 
        let model = $form.serializeFormJSON();
        const initialLabelText = $("#btn-confirmar").text();
        $("#btn-confirmar").prop("enabled", false).text("...Cargando");
        
        $.ajax({
            type: "POST",
            url: "/procesos/ext/retiro-y-entrega",
            data: JSON.stringify(model),
            contentType: "application/json; charset=utf-8"
        }).done(function (data) {
            $.niftyNoty({
                type: "success",
                container: "floating",
                title: "Notificaciones Workflow",
                message: "Datos Guardados, Nro Seguimiento: " + data.numeroTicket + ".<small>Puedes revisar tus seguimientos en tu pagina principal</small>",
                closeBtn: false,
                timer: 5000,
                onHidden: function () {
                    location.href = '/mi-gestion'
                    //window.open('/FlujoInterno/VerCotizacion?ticket=' + data.numeroTicket)
                }
            });
        }).fail(function (errMsg) {
            console.log(errMsg);

        }).always(function () {
            $("#btn-confirmar").prop("enabled", true).text(initialLabelText);
        });        

        return false;
    });

});