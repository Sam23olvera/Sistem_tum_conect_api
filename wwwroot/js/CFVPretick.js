window.onload = function () {
    document.getElementById("spinner-overlay").style.display = "none";
};
document.addEventListener("DOMContentLoaded", function () {
    var links = document.querySelectorAll(".carga");
    links.forEach(function (link) {
        link.addEventListener("click", function () {
            document.getElementById("spinner-overlay").style.display = "block";
        });
    });
});

$(document).ready(function () {
    var mensaje = document.getElementById('mensaje').value;
    var guarda = document.getElementById('guarda').value;
    if (mensaje !== '') {
        toastr.error(mensaje);
    }
    if (guarda !== '') {
        toastr.success(guarda);
    }
});
$(document).ready(function () {
    $('#FechCrePreTickIn').datetimepicker({
        //format: 'm/d/Y'
        format: 'Y/m/d'
    });
    $('#FechCrePreTickFin').datetimepicker({
        //format: 'm/d/Y'
        format: 'Y/m/d'
    });
    
});