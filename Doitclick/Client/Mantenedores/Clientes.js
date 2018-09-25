var $table = $('#table');

function initTable() {
    var token = localStorage.getItem('token')
    $table.bootstrapTable({
        height: getHeight(),
        ajaxOptions: {
            headers: {
                Authorization: "bearer " + token
            }
        },
        columns: [
            {
                title: 'Rut',
                field: 'rut',
                align: 'center',
                valign: 'middle'
            }, {
                title: 'Nombre',
                field: 'nombres',
                align: 'center',
                valign: 'middle',
                
            }, {
                title: 'Tipo Cliente',
                field: 'tipoCliente',
                align: 'center',
                valign: 'middle'
            }, {
                title: 'Personalidad',
                field: 'esPersonalidadJuridica',
                align: 'center',
                valign: 'middle',
               
            },  {
                title: 'Prevision',
                field: 'previsionSalud',
                align: 'center',
                valign: 'middle',
            }
        ]
    });
    // sometimes footer render error.
    setTimeout(function () {
        $table.bootstrapTable('resetView');
    }, 200);
    
    $table.on('expand-row.bs.table', function (e, index, row, $detail) {
        if (index % 2 == 1) {
            $detail.html('Loading from ajax request...');
            $.get('LICENSE', function (res) {
                $detail.html(res.replace(/\n/g, '<br>'));
            });
        }
    });
    $table.on('all.bs.table', function (e, name, args) {
        console.log(name, args);
    });
    
    $(window).resize(function () {
        $table.bootstrapTable('resetView', {
            height: getHeight()
        });
    });
}

function responseHandler(res) {
    $.each(res.rows, function (i, row) {
        row.state = $.inArray(row.id, selections) !== -1;
    });
    return res;
}

function detailFormatter(index, row) {
    var html = [];
    $.each(row, function (key, value) {
        html.push('<p><b>' + key + ':</b> ' + value + '</p>');
    });
    return html.join('');
}

function getHeight() {
    return $(window).height() - $('h1').outerHeight(true);
}
$(function(){
    $('.es-rut').mask('99.999.999-*');
})
$(function () {
  
    initTable();
    $('#frm-ingreso-datos-cliente').on("submit", function () {
        let model = $(this).serializeFormJSON();
        
        $.ajax({
            type: "POST",
            url: "/api/mantenedor/clientes/ingreso-datos-cliente",
            data: JSON.stringify(model),
            contentType: "application/json; charset=utf-8",
            dataType: "json"
            
        }).done(function (data) {


            $.niftyNoty({
                type: "success",
                container : "floating",
                title : "Suceso Exitoso",
                message : "Los datos del Cliente se han guardado correctamente",
                closeBtn : false,
                timer : 5000
            });

            this.reset();
            location.reload();
        }).fail(function (errMsg) {
            console.log(errMsg);
            this.reset();
        })
        $('#frm-ingreso-datos-cliente')[0].reset();
        return false;
    
    });
    


})