var modelo = {

    NombreServicio: '',
    DescripcionServicio: '',
    CodigoServicio: '',
    VManoObra: null,
    PorcentajeComision: null,
    materiales: []
};


function render(){

    //Limpierza de pantalla
    $("#listado > tbody").html("");
    //Enanche Formulario
    //$("#CodigoServicio").val(modelo.codigoServicio);
    //$("#NombreServicio").val(modelo.nombreServicio);

    modelo.materiales.forEach(function(material){
        var _trMaterial = $("<tr>").prop({id: material.guid });
        
        var _tdMaterial = $("<td>").append(material.nombre);
        _trMaterial.append(_tdMaterial);

        var _tdUM = $("<td>").append(material.unidadMedida);
        _trMaterial.append(_tdUM);

        var _tdCNT = $("<td>").append(material.cantidad);
        _trMaterial.append(_tdCNT);

        var _tdPrecio = $("<td>").append(material.precio.toMoney());
        _trMaterial.append(_tdPrecio);

        var _tdTotal = $("<td>").append((material.precio * material.cantidad).toMoney());
        _trMaterial.append(_tdTotal);

        var _tdAcciones = $("<td>").append($('<button>').text('Eliminar').addClass('btn btn-danger btn-sm del-service').prop({type: 'button'}));
        _trMaterial.append(_tdAcciones);

        $("#listado > tbody").append(_trMaterial);
    });
}

$(function () {



    $(document).on('click', '.del-service', function () {
        
        var eleGuid = $(this).closest('tr').prop("id");
        
        var idxEliminar = modelo.materiales.findIndex(function (element) {
            return element.guid === eleGuid;    
        });

        modelo.materiales.splice(idxEliminar, 1);
        render();
        //console.log({ idxEliminar, modelo });
    });
            
    $('.btn-guardar-material').on('click', function(){

        //console.log($('#material').val(), $("#cantidad").val())
        if ($('#material').val() != "" && $("#cantidad").val() != ""){
            const material = {
                materialId: $('#material').val(),
                cantidad: $("#cantidad").val(),
                nombre: $('#material option:selected').text(),
                stock: $('#material option:selected').data('stocka'),
                unidadMedida: $('#material option:selected').data('um'),
                precio: parseInt($('#material option:selected').data('valor')),
                valorTotal: parseInt($('#material option:selected').data('valor')) * parseInt($("#cantidad").val()),
                guid: uuid4()
            };


            $('#material').val("");
            $("#cantidad").val("");


            $.niftyNoty({
                type: 'info',
                container: "#mensajero-default",
                html: '<p class="alert-message">Material Agregado</p>',
                timer: 2000,
                closeBtn: false
            });
            modelo.materiales.push(material);
            render();
        }
        else 
        {
            $.niftyNoty({
                type: 'warning',
                container: "#mensajero-default",
                html: '<p class="alert-message">Debes rellenar los campos</p>',
                timer: 2000,
                closeBtn: false
            });
        }
    });


    $("#material").on("change", function(){
        var um = $('#material option:selected').data('um');
        $("#desc-unity").text(um);
    });


    $('#frm-ingreso-servicios').on("submit", function () {
        
        var mdl = $(this).serializeFormJSON();
        Object.assign(modelo, mdl);
        
        $.ajax({
            type: "POST",
            url: "/api/mantenedor/servicios",
            data: JSON.stringify(modelo),
            contentType: "application/json; charset=utf-8",
            success: function (data) { 
                $.niftyNoty({
                    type: "success",
                    container : "floating",
                    title : "Suceso Exitoso",
                    message : "Los datos del Servicio se han guardado correctamente",
                    closeBtn : true,
                    timer : 5000,
                    onHidden: function(){
                        location.reload();
                    }
                });
                
            },
            failure: function (errMsg) {

                $.niftyNoty({
                    type: "warning",
                    container : "floating",
                    title : "Suceso Warning",
                    message : "Errrorrrrr",
                    closeBtn : false,
                    timer : 5000
                });

                console.log(errMsg);
            }
        });

        return false;
    });

});

function uuid4() {
    function hex(s, b) {
        return s +
            (b >>> 4).toString(16) +  // high nibble
            (b & 0b1111).toString(16);   // low nibble
    }

    let r = crypto.getRandomValues(new Uint8Array(16));

    r[6] = r[6] >>> 4 | 0b01000000; // Set type 4: 0100
    r[8] = r[8] >>> 3 | 0b10000000; // Set variant: 100

    return r.slice(0, 4).reduce(hex, '') +
        r.slice(4, 6).reduce(hex, '-') +
        r.slice(6, 8).reduce(hex, '-') +
        r.slice(8, 10).reduce(hex, '-') +
        r.slice(10, 16).reduce(hex, '-');
}