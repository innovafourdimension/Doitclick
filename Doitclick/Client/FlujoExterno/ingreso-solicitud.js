
window._servicios_detalle_cotizacion = [];

function restaCantidad(e) {
    let $that = $(this);
    let prnt = $that.closest('tr');
    let opts = JSON.parse($(prnt).data('opts'));
    if (opts.cantidad > 1) {
        opts.cantidad--;
        $.each(_servicios_detalle_cotizacion, function (i, e) {
            if (e.id == opts.id) {
                _servicios_detalle_cotizacion.splice(i, 1);
                _servicios_detalle_cotizacion.push(opts)
            }
        });
        $(prnt).data('opts', JSON.stringify(opts));
        $(prnt).find('.showcan').text(opts.cantidad)
        $(prnt).find('.showtot').text('$' + parseInt(opts.cantidad * opts.valor).toMoney())
        $(document).trigger('doitclick.events.onCambiaValor', { opts, type: 'resta' });
    }
}

function sumaCantidad(e) {
    let $that = $(this);
    let prnt = $that.closest('tr');
    let opts = JSON.parse($(prnt).data('opts'));
    opts.cantidad++;
    $.each(_servicios_detalle_cotizacion, function (i, e) {
        if (e.id == opts.id) {
            _servicios_detalle_cotizacion.splice(i, 1);
            _servicios_detalle_cotizacion.push(opts)
        }
    });
    $(prnt).data('opts', JSON.stringify(opts));
    $(prnt).find('.showcan').text(opts.cantidad)
    $(prnt).find('.showtot').text('$' + (opts.cantidad * opts.valor).toMoney())
    $(document).trigger('doitclick.events.onCambiaValor', { opts, type: 'suma' });
}

function eliminaServicio(e){
    let $that = $(this);
    let prnt = $that.closest('tr');
    let opts = JSON.parse($(prnt).data('opts'));
    let svcIndex = _servicios_detalle_cotizacion.findIndex(function(element){
        return element.id == opts.id;
    });
    _servicios_detalle_cotizacion.splice(svcIndex, 1);
    prnt.remove();
    $('.mensaje-otros').is(":visible") && $('.mensaje-otros').hide('slow')
    $(document).trigger('doitclick.events.onCambiaValor', { opts, type: 'borra' });

}

function previsualizarImagen() {
    var preview = document.querySelector('#img-previsualiza');
    var file = document.querySelector('input[type=file]').files[0];
    var reader = new FileReader();

    reader.onloadend = function () {
        preview.src = reader.result;
        $(preview).css("display", "block");
    }

    if (file) {
        reader.readAsDataURL(file);
    } else {
        preview.src = "";
    }
}





$(function () {

    $('#ver-orden').on("click", function(){
        $('#previsualizacion').toggleHide();
    });

    $('#demo-cs-multiselect').chosen({width:'95%'});
    
    $(document).on('doitclick.events.onCambiaValor', function (event, extra) {
        var totalGeneral = 0;
        $.each(_servicios_detalle_cotizacion, function (i, e) {
            totalGeneral += (e.cantidad * e.valor)

        });
        $('.showapagar').text('$' + totalGeneral.toMoney());

    });

    $('.agrgar-trabajo').on('click', function () {


        if ($('#demo-cs-multiselect option:selected').val() == '') {

            return;
        }


        if ($('#demo-cs-multiselect option:selected').val() == 'Otro') {

            const opts = {
                id: 'Otro',
                texto: "Evaluación de Otras Prestaciones",
                descripcion: $('#DescripcionEvaluacionOP').val(),
                valor: 0,
                cantidad: 1
            }

            var _tr = $('<tr>').data("opts", JSON.stringify(opts));
            var _td_descripcion = $('<td>');
            _td_descripcion
                .append($('<div>')
                            .append($('<button>').prop({ type: 'button' }).addClass('btn btn-xs btn-danger').text('x').on("click", eliminaServicio))
                            .append($('<strong>').css({'padding-left':'7px'}).text(opts.texto))
                ).append($('<small>').css({'padding-left':'25px'}).text(opts.descripcion));    
            
            //.append($('<strong>').text(opts.texto)).append($('<small>').text(opts.descripcion))
            _tr.append(_td_descripcion);

            var _td_valor = $('<td>').addClass('text-center').text('Por Evaluar');
            _tr.append(_td_valor);

            var _td_cantidad = $('<td>').addClass('text-center').text(1);
            _tr.append(_td_cantidad);

            var _td_total = $('<td>').addClass('text-right').text('Por Evaluar');
            _tr.append(_td_total);

            _servicios_detalle_cotizacion.push(opts);
            $("#detalles-cotizacion").append(_tr);

            $('.mensaje-otros').show('slow')

            return;
        }

        const opts = {
            id: $('#demo-cs-multiselect option:selected').val(),
            texto: $('#demo-cs-multiselect option:selected').text(),
            descripcion: $('#demo-cs-multiselect option:selected').data("descripcion"),
            valor: $('#demo-cs-multiselect option:selected').data("valor"),
            cantidad: 1
        }
        let idxSvc = _servicios_detalle_cotizacion.findIndex(function(element){
            return opts.id == element.id;
        });
        if(idxSvc == -1)
        {
            _servicios_detalle_cotizacion.push(opts);
            var _tr = $('<tr>').data("opts", JSON.stringify(opts));
            var _td_descripcion = $('<td>');
            _td_descripcion
                .append($('<div>')
                            .append($('<button>').prop({ type: 'button' }).addClass('btn btn-xs btn-danger').text('x').on("click", eliminaServicio))
                            .append($('<strong>').css({'padding-left':'7px'}).text(opts.texto))
                ).append($('<small>').css({'padding-left':'25px'}).text(opts.descripcion))
            _tr.append(_td_descripcion);

            var _td_valor = $('<td>').addClass('text-center').text('$' + opts.valor.toMoney());
            _tr.append(_td_valor);

            var _td_cantidad = $('<td>').addClass('text-center');
            _td_cantidad
                .append($('<button>').prop({ type: 'button' }).addClass('btn btn-xs btn-primary').text('<').on("click", restaCantidad))
                .append($('<span>').addClass("pad-hor showcan").text(opts.cantidad))
                .append($('<button>').prop({ type: 'button' }).addClass('btn btn-xs btn-primary').text('>').on("click", sumaCantidad))
                
            _tr.append(_td_cantidad);

            var _td_total = $('<td>').addClass('text-right showtot').text('$' + opts.valor.toMoney());
            _tr.append(_td_total);

            $("#detalles-cotizacion").append(_tr);
            $(document).trigger('doitclick.events.onCambiaValor', { opts, type: 'nuevo' });
        }
    });


    $("#demo-cs-multiselect").on("change", function () {
        if ($(this).val() == 'Otro') {
            $('.extension-otros').show('slow');
        } else {
            $('.extension-otros').hide('slow');
        }
    });

    $('#frm-generico').on("submit", function (event) {
        event.preventDefault();
        let $form = $(this); 
        const initialLabelText = $("#btn-confirmar").text();
        $("#btn-confirmar").prop("enabled", false).text("...Cargando");
        let model = $form.serializeFormJSON();
        model.SrcImagen = $("#img-previsualiza").prop("src");
        model.Servicios = _servicios_detalle_cotizacion;
        model.RequiereDelivery = $('#requiere-delivery').is(':checked');
        
        $.ajax({
            type: "POST",
            url: "/procesos/ext/ingreso-solicitud",
            data: JSON.stringify(model),
            contentType: "application/json; charset=utf-8"
        }).done(function (data) {
            $.niftyNoty({
                type: "success",
                container : "floating",
                title : "Notificaciones Workflow",
                message : "Datos Guardados, Nro Seguimiento: " + data.numeroTicket + ".<small>Puedes revisar tus seguimientos en tu pagina principal</small>",
                closeBtn : false,
                timer : 5000,
                onHidden: function(){
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


    $("#requiere-delivery").on("click", function(){
        $("#retiro-detail").toggleHide();
    })

});