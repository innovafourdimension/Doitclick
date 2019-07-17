window._listado_materiales = [];
window._modelo_formulario = {
    comentarios: '',
    materiales: []
};

$('#agregar-material-mensual').on('click', function(){


    if ($('#listado-materiales-mensuales').val() === ''){
        return false;
    }

    const material_solicitado = {
        id: $('#listado-materiales-mensuales option:selected').val(),
        nombre: $('#listado-materiales-mensuales option:selected').text(),
        descripcion: $('#listado-materiales-mensuales option:selected').data("descripcion"),
        cantidad: 1
    }

    let indice_material_Solicitado = _listado_materiales.findIndex(function (element) {
        return material_solicitado.id == element.id;
    });

    if (indice_material_Solicitado === -1){
        _listado_materiales.push(material_solicitado);

        render_material_solicitado(_listado_materiales);
    }

    

    console.log({ _listado_materiales });
});

$('#bt_generar_solicitud').on('click', function(){
        
    _modelo_formulario.comentarios = $('#comentarios').val();
    _modelo_formulario.materiales = _listado_materiales;

    $.ajax({
        type: "POST",
        url: "/MantenedorMaterialMensual/GuardarGeneraSolicitud",
        data: JSON.stringify(_modelo_formulario),
        contentType: "application/json; charset=utf-8"
    }).done(function (data) {
        console.log({data})
        $.niftyNoty({
            type: "success",
            container: "floating",
            title: "Notificaciones Workflow",
            message: `Ok`,
            closeBtn: true,
            timer: 5000,
            onHidden: function () {
                location.href = '/MantenedorMaterialMensual/BandejaSolicitante'
            }
        });
    }).fail(function (errMsg) {
        console.log(errMsg);

    })

    return false;

});


function render_material_solicitado(listado = []){
    $("#detalles-solicitud").html("");
    listado.forEach(function(matm){
        var $tr_contenedor = $('<tr>').data('id', matm.id).data('opts', JSON.stringify(matm));
        var $td_descripcion = $('<td>')
                                .append($('<div>')
                                    .append($('<button>').prop({ type: 'button' }).addClass('btn btn-xs btn-danger').text('x').on("click", eliminaMaterialSolicitud))
                                    .append($('<strong>').css({ 'padding-left': '7px' }).text(matm.nombre))
                                ).append($('<small>').css({ 'padding-left': '25px' }).text(matm.descripcion))

        $tr_contenedor.append($td_descripcion);

        var $td_cantidad = $('<td>')
            .addClass('text-center')
            .append($('<button>').prop({ type: 'button' }).addClass('btn btn-xs btn-primary').text('<').on("click", restaCantidad))
            .append($('<span>').addClass("pad-hor showcan").text(matm.cantidad))
            .append($('<button>').prop({ type: 'button' }).addClass('btn btn-xs btn-primary').text('>').on("click", sumaCantidad))

        $tr_contenedor.append($td_cantidad);
        $("#detalles-solicitud").append($tr_contenedor);
    })
    
}

function restaCantidad(e) {
    let $that = $(this);
    let prnt = $that.closest('tr');
    let opts = JSON.parse($(prnt).data('opts'));
    if (opts.cantidad > 1) {
        opts.cantidad--;
        $.each(_listado_materiales, function (i, e) {
            if (e.id == opts.id) {
                _listado_materiales.splice(i, 1);
                _listado_materiales.push(opts)
            }
        });
        $(prnt).data('opts', JSON.stringify(opts));
        $(prnt).find('.showcan').text(opts.cantidad)
        //$(document).trigger('doitclick.events.onCambiaValor', { opts, type: 'resta' });
    }
}

function sumaCantidad(e) {
    let $that = $(this);
    let prnt = $that.closest('tr');
    let opts = JSON.parse($(prnt).data('opts'));
    opts.cantidad++;
    $.each(_listado_materiales, function (i, e) {
        if (e.id == opts.id) {
            _listado_materiales.splice(i, 1);
            _listado_materiales.push(opts)
        }
    });
    $(prnt).data('opts', JSON.stringify(opts));
    $(prnt).find('.showcan').text(opts.cantidad)
    //$(document).trigger('doitclick.events.onCambiaValor', { opts, type: 'suma' });
}

function eliminaMaterialSolicitud(e) {
    let $that = $(this);
    let prnt = $that.closest('tr');

    let opts = JSON.parse($(prnt).data('opts'));
    let svcIndex = _listado_materiales.findIndex(function (element) {
        return element.id == opts.id;
    });
    _listado_materiales.splice(svcIndex, 1);
    prnt.remove();
    $('.mensaje-otros').is(":visible") && $('.mensaje-otros').hide('slow')
    //$(document).trigger('doitclick.events.onCambiaValor', { opts, type: 'borra' });
    //console.log({ e, that: $that, prnt, opts, svcIndex });
}