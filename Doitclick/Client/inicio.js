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
                title: 'Proceso',
                field: 'tarea.solicitud.proceso.nombre',
                align: 'center',
                valign: 'middle'
            }, {
                title: 'Ticket',
                field: 'tarea.solicitud.numeroTicket',
                align: 'center',
                valign: 'middle',
                formatter: function (value, row, index) {
                    console.log(row.tarea.etapa.nombreInterno);
                    return '<a href="/FlujoInterno/' + row.tarea.etapa.nombreInterno + '?ticket='+value+'" class="btn-link">' + value + '</a>';
                }
            }, {
                title: 'Tarea',
                field: 'tarea.etapa.nombre',
                align: 'center',
                valign: 'middle'
            }, {
                title: 'Paciente',
                field: 'cotizacion.cliente.nombres',
                align: 'center',
                valign: 'middle',
                formatter: function (value, row, index) {
                    return row.cotizacion.cliente.rut + ', ' + row.cotizacion.cliente.nombres;
                }
            },  {
                title: 'Iniciado el',
                field: 'tarea.fechaInicio',
                align: 'center',
                valign: 'middle',
                formatter: function (value, row, index) {
                    console.log(row);
                    return new Date(value).toLocaleString();
                }
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


$(function () {
    initTable();
})