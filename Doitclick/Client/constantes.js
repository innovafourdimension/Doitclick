const unidadesMedida = Array('Granel', 'Gramos', 'Kilogramos', 'Litros');
const tipoCliente=('Clinica','Doctor','Paciente');



$('#logout').on('click', function(){

    $.ajax({
        type: "GET",
        url: "/api/Auth/Logout",
        contentType: "application/json; charset=utf-8",
        success: function (data) { 
            localStorage.clear();
            location.href="/";
        },
        failure: function (errMsg) {
            console.log(errMsg)
        }
    });
    
});