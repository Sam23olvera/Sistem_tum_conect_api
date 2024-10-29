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
    $('#fechaSolicitud').datetimepicker({
        format: 'Y/m/d'
    });
    $('#fechaRecibo').datetimepicker({
        format: 'Y/m/d'
    })
});

function fun() {
    var puesto = document.getElementById('nombrePuesto').value;
    var $select = document.getElementById('subdept');
    if (puesto != 0)
    {
        const jserial = JSON.parse(document.getElementById('jsonsub').value);
        $select.innerHTML = "";

        const defaultOption = document.createElement("option");
        defaultOption.value = 0;
        defaultOption.text = '[Selecciona]';
        // Agregar la opción por defecto al select
        $select.appendChild(defaultOption);
        for (var i = 0; i < jserial.length; i++) {
            if (puesto == jserial[i].clavePuesto) {
                //console.log(jserial.subDepto[i].claveSubDepartamento + ',' +  + ',' + jserial.subDepto[i].clavePuesto);

                const option = document.createElement("option");
                option.value = jserial[i].claveSubDepartamento;
                option.text = jserial[i].subdepto;

                // Agregar la opción al select
                $select.appendChild(option);
            }
        }        
    }
}

function selectDep()
{
    var sub = document.getElementById('subdept').value;
    var $set = document.getElementById('seledepa');
    if (sub != 0)
    {
        const jserial = JSON.parse(document.getElementById('jsondepto').value);
        $set.innerHTML = "";

        const defaultOption = document.createElement("option");
        defaultOption.value = 0;
        defaultOption.text = '[Selecciona]';

        $set.appendChild(defaultOption);

        for (var j = 0; j < jserial.length; j++)
        {
            //console.log(jserial.deptos[j].claveSubDepartamento + ',' + jserial.deptos[j].claveDepartamento + ',' + jserial.deptos[j].departamento);
            if (sub == jserial[j].claveSubDepartamento)
            {
                const option = document.createElement("option");
                option.value = jserial[j].claveDepartamento;
                option.text = jserial[j].departamento;

                $set.appendChild(option);
            }
        }
    }   
}

function selectlocal()
{
    var dep = document.getElementById('seledepa').value;
    var $localidad = document.getElementById('localidad');
    if (dep != 0)
    {
        const jserial = JSON.parse(document.getElementById('jsonlocal').value);
        $localidad.innerHTML = "";

        const defaultOption = document.createElement("option");
        defaultOption.value = 0;
        defaultOption.text = '[Selecciona]';

        $localidad.appendChild(defaultOption);

        for (var l = 0; l < jserial.length; l++) {
            if (dep == jserial[l].claveDepartamento)
            {
                const option = document.createElement("option");
                option.value = jserial[l].claveArea;
                option.text = jserial[l].localidad;
                $localidad.appendChild(option);
            }

        }

    }

}