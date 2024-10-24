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


//function imprimirPagina() {
//    var form = document.getElementById("solicitude");
//    var ventanasoli = window.open(' ', 'Print-Window');
//    var html = '<!DOCTYPE html> <html>'
//        + '<head>'
//        + ' <title>Imprimir Requisición de Personal</title>'
//        + '  </head>'
//        + '  <body>'
//        + form.innerHTML
//        + '  </body>'
//        + '  </html>';
//    ventanasoli.document.write(html);
//    ventanasoli.document.close();
//    ventanasoli.print();
//    ventanasoli.close();
//}


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
        for (var i = 0; i < jserial.subDepto.length; i++) {
            if (puesto == jserial.subDepto[i].clavePuesto) {
                //console.log(jserial.subDepto[i].claveSubDepartamento + ',' +  + ',' + jserial.subDepto[i].clavePuesto);

                const option = document.createElement("option");
                option.value = jserial.subDepto[i].claveSubDepartamento;
                option.text = jserial.subDepto[i].subdepto;

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
        const jserial = JSON.parse(document.getElementById('jsonsub').value);
        $set.innerHTML = "";

        const defaultOption = document.createElement("option");
        defaultOption.value = 0;
        defaultOption.text = '[Selecciona]';

        $set.appendChild(defaultOption);

        for (var j = 0; j < jserial.deptos.length; j++)
        {
            //console.log(jserial.deptos[j].claveSubDepartamento + ',' + jserial.deptos[j].claveDepartamento + ',' + jserial.deptos[j].departamento);
            if (sub == jserial.deptos[j].claveSubDepartamento)
            {
                const option = document.createElement("option");
                option.value = jserial.deptos[j].claveDepartamento;
                option.text = jserial.deptos[j].departamento;

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
        const jserial = JSON.parse(document.getElementById('jsonsub').value);
        $localidad.innerHTML = "";

        const defaultOption = document.createElement("option");
        defaultOption.value = 0;
        defaultOption.text = '[Selecciona]';

        $localidad.appendChild(defaultOption);

        for (var l = 0; l < jserial.localidad.length; l++) {
            if (dep == jserial.localidad[l].claveDepartamento)
            {
                const option = document.createElement("option");
                option.value = jserial.localidad[l].claveArea;
                option.text = jserial.localidad[l].localidad;
                $localidad.appendChild(option);
            }

        }

    }

}