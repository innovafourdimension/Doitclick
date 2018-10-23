window._destinos = [];

window.render = function(){
    $('#tabla-destinos').html(``);
    $.each(_destinos, function(i,e){
        $('#tabla-destinos').append(
            $('<tr>').append(
                $('<td>').text(e.etapaDestinoNombre)
            ).append(
                $('<td>').text(e.namespaceValidacion)
            ).append(
                $('<td>').text(e.claseValidacion)
            ).append(
                $('<td>').text(e.metodoValidacion)
            ).append(
                $('<td>').append(`<a href="#" data-target="#modal-destinos" data-toggle="modal" data-id="${e.id}" class="transito btn btn-primary btn-sm">Editar</a>`)
            )
        )
    })
}


$(function () {

    let elId = $('#Id').val();            
    $.ajax({
            type: "GET",
            url: `/WorkFlowAdmin/DestinosEtapa?etapaId=${elId}` 
        }).done(function (data) {
            _destinos = data.map(function(item){
                return {
                    id: item.id,
                    etapaDestino: item.etapaDestino.id,
                    etapaDestinoNombre: item.etapaDestino.nombre,
                    namespaceValidacion: item.namespaceValidacion,
                    claseValidacion: item.claseValidacion,
                    metodoValidacion: item.metodoValidacion
                }
            });
            render();
        })

    if($('#tabla-destinos > tr').length > 0)
    {
        $('#tabla-destinos > tr').each(function(i, e){
            console.log(e)
        });
    }

    $('#agregar-destino').on('click', function(){
        let idTransito = parseInt($('#IdTr').val());
        if(idTransito == 0){
            _destinos.push({
                id: idTransito,
                etapaDestino: parseInt($('#etapaDestino').val()),
                etapaDestinoNombre: $('#etapaDestino option:selected').text(),
                namespaceValidacion:$('#namespaceValidacion').val(),
                claseValidacion:$('#claseValidacion').val(),
                metodoValidacion:$('#metodoValidacion').val()
            });
        }else{
            let index = _destinos.findIndex(function(item){
                return item.id === idTransito;
            });

            _destinos[index] = {
                id: idTransito,
                etapaDestino: parseInt($('#etapaDestino').val()),
                etapaDestinoNombre: $('#etapaDestino option:selected').text(),
                namespaceValidacion:$('#namespaceValidacion').val(),
                claseValidacion:$('#claseValidacion').val(),
                metodoValidacion:$('#metodoValidacion').val()
            }
        }
        $('#modal-destinos').modal('hide');
        render();
    });


    $('#modal-destinos').on('show.bs.modal', function(event){
        if((typeof $(event.relatedTarget).data('id') != 'undefined'))
        {
            let idTransito = $(event.relatedTarget).data('id');
            let modelo = _destinos.find(function(item){
                return item.id == idTransito;
            })
            $('#IdTr').val(idTransito);
            $('#etapaDestino').val(modelo.etapaDestino)
            $('#namespaceValidacion').val(modelo.namespaceValidacion)
            $('#claseValidacion').val(modelo.claseValidacion)
            $('#metodoValidacion').val(modelo.metodoValidacion)
            render();
        }
    })

    $('#modal-destinos').on('hide.bs.modal', function(event){
        $('#IdTr').val('0');
        $('#etapaDestino').val('')
        $('#namespaceValidacion').val('Doitclick.Services.Workflow.Transition')
        $('#claseValidacion').val('WorkflowApproved')
        $('#metodoValidacion').val('Validar')
    })


    


    $('#frm-generico').on('submit', function (e) {
        e.preventDefault();
        var model = $(this).serializeFormJSON();
        model.destinos = _destinos;
        $(".submit-generico").html("Cargando...");


        console.log({e, model})
        $.ajax({
            type: "POST",
            url: "/WorkflowAdmin/GardarEtapa",
            data: JSON.stringify(model),
            contentType: "application/json; charset=utf-8" 
        }).done(function (data) {
            
            $.niftyNoty({
                type: "success",
                container : "floating",
                title : "Suceso Exitoso",
                message : "Los datos de la Etapa se han guardado correctamente",
                closeBtn : false,
                timer : 5000
            });
            
        }).fail(function (xhr, textStatus) {
            $.niftyNoty({
                type: "warning",
                container : "floating",
                title : "Suceso Erroneo",
                message : "Está mal: " + textStatus,
                closeBtn : false,
                timer : 5000
            });
        }).always(function () {
            $(".submit-generico").html("Guardar");
        });
        

        return false;
    });

});