﻿
document.addEventListener("DOMContentLoaded", () => {
    // Cache de elementos comunes
    const mensaje = document.getElementById('mensaje');
    const guarda = document.getElementById('guarda');
    const estatus = document.getElementById('status');

    if (mensaje?.value) toastr.error(mensaje.value);
    if (guarda?.value) toastr.success(guarda.value);
    if (estatus.value === '200') {
        if (Notification) {
            if (Notification.permission !== "granted") {
                Notification.requestPermission()
            }
            if (mensaje.value !== '')
            {
                var title = "Mensaje"
                var extra = {
                    icon: "https://webportal.tum.com.mx/wsstest/imag/logo_difuminado.png",
                    body: mensaje.value
                }
                if (typeof noti2 != 'undefined') {
                    noti2.onclose = null;
                    noti2.close()
                }
                // Generamos la notificación
                noti2 = new Notification(title, extra);

                noti2.onclick = function () {
                    noti2.onclose = null;
                    document.getElementById('XITRUS_act_perm2').value = 'click'
                    setTimeout(function () { document.getElementById('XITRUS_act_perm2').value = '' }, 2000)
                }
                noti2.onclose = function () {
                    document.getElementById('XITRUS_act_perm2').value = 'close'
                    setTimeout(function () { document.getElementById('XITRUS_act_perm2').value = '' }, 2000)
                }
            }
            if (guarda.value !== '')
            {
                var title = "Mensaje"
                var extra = {
                    icon: "https://webportal.tum.com.mx/wsstest/imag/logo_difuminado.png",
                    body: guarda.value
                }
                if (typeof noti2 != 'undefined') {
                    noti2.onclose = null;
                    noti2.close()
                }
                // Generamos la notificación
                noti2 = new Notification(title, extra);

                noti2.onclick = function () {
                    noti2.onclose = null;
                    document.getElementById('XITRUS_act_perm2').value = 'click'
                    setTimeout(function () { document.getElementById('XITRUS_act_perm2').value = '' }, 2000)
                }
                noti2.onclose = function () {
                    document.getElementById('XITRUS_act_perm2').value = 'close'
                    setTimeout(function () { document.getElementById('XITRUS_act_perm2').value = '' }, 2000)
                }
            }
        }
    }

});

const clearTable = () => {
    const clavesFalAndComen = document.getElementById('clavesFalAndComen');
    const fallallantas = document.getElementById('fallallantas');

    document.getElementById('tabmosfal').querySelector("tbody").innerHTML = "";
    clavesFalAndComen.value = "";
    fallallantas.value = "";
};
$(document).ready(function () {

    var CheckViaje = document.getElementById('CheckViaje');
    var inCheckViaje = document.getElementById('inCheckViaje').value;

    var Opera = document.getElementById('Opera');
    var SelectOperacion = document.getElementById('SelectOperacion');
    var Ruta = document.getElementById('Ruta');
    var claveEmp = document.getElementById('cveEmp');
    var remolque1 = document.getElementById('remolque1');
    var remolque2 = document.getElementById('remolque2');
    var seleuni = document.getElementById('seleuni');
    var seleuniempresa = document.getElementById('seleuniempresa');

    if (inCheckViaje === "True") {

        CheckViaje.checked = true;

        inCheckViaje.value = true;
        if (seleuni.value != 0) {
            Opera.disabled = true;
            SelectOperacion.disabled = true;
            Ruta.disabled = true;
            remolque1.disabled = true;
            remolque2.disabled = true;
            var numuni = seleuni.options[seleuni.selectedIndex].text;
            var num = numuni.split("|");
            seleuniempresa.value = seleuni.value;
            claveEmp.value = seleuniempresa.options[seleuniempresa.selectedIndex].text;
            
            if (CheckViaje.checked) {
                mostrarMapa(claveEmp.value, seleuni.value);
                muestraViaje(claveEmp.value, num[0]);
                clearTable();
            }
        }
        else {
            Opera.disabled = true;
            Opera.value = 0;
            SelectOperacion.disabled = true;
            SelectOperacion.value = 0;
            Ruta.disabled = true;
            Ruta.value = "";
            remolque1.disabled = true;
            remolque1.value = 0;
            remolque2.disabled = true;
            remolque2.value = 0;
            seleuniempresa.value = 0;
            claveEmp.value = 0;
        }

    }
    else if (inCheckViaje == "False") {
        CheckViaje.checked = false;
        inCheckViaje.value = false;
        Opera.disabled = false;
        SelectOperacion.disabled = false;
        Ruta.disabled = false;
        remolque1.disabled = false;
        remolque2.disabled = false;
    }

});
document.addEventListener("DOMContentLoaded", function () {
    var TipoClas = document.getElementById('selclasi');
    var forllantas = document.getElementById('forllantas');
    TipoClas.addEventListener('change', function () {
        if (TipoClas.value == 2) {
            forllantas.style.display = "block";
        }
        else {
            forllantas.style.display = "none";
        }
    });
});
$(document).ready(function () {
    var seleuni = document.getElementById('seleuni');
    var inCheckViaje = document.getElementById('inCheckViaje');
    if (inCheckViaje.value == "") {
        seleuni.checked = false;
    }
    else if (inCheckViaje.value == true) {
        seleuni.checked = true;
    }
    else {
        seleuni.checked = false;
    }
});
$(document).ready(function () {
    var selfalla = document.getElementById('selfalla');
    var remolque1 = document.getElementById('remolque1');
    var remolque2 = document.getElementById('remolque2');
    var remolque = document.getElementById('remolque');
    var selcveEquipo = document.getElementById('selcveEquipo');
    selfalla.addEventListener('change', function () {
        if (selfalla.value == 2) {
            if (remolque1.value !== "0" && remolque2.value !== "0") {
                var textremolque1 = remolque1.options[remolque1.selectedIndex].text;
                var valueremolque1 = remolque1.value;
                var textremolque2 = remolque2.options[remolque2.selectedIndex].text;
                var valueremolque2 = remolque2.value;
                remolque.style.display = "block";
                selcveEquipo.innerHTML = "";


                // Opción por defecto
                var defaultOption = document.createElement("option");
                defaultOption.value = "0";
                defaultOption.text = "[Seleccionar]";
                selcveEquipo.appendChild(defaultOption);

                // Opción para remolque1
                var option1 = document.createElement("option");
                option1.value = valueremolque1;
                option1.text = textremolque1;
                selcveEquipo.appendChild(option1);

                // Opción para remolque2
                var option2 = document.createElement("option");
                option2.value = valueremolque2;
                option2.text = textremolque2;
                selcveEquipo.appendChild(option2);
            }
            else {
                remolque.style.display = "none";
                selcveEquipo.innerHTML = "";
            }
        }
        else {
            remolque.style.display = "none";
            selcveEquipo.innerHTML = "";
        }
    });

});
$(document).ready(function () {

    var seleuni = document.getElementById('seleuni');
    var claveEmp = document.getElementById('cveEmp');
    ///extra


    var CheckViaje = document.getElementById('CheckViaje');
    var inCheckViaje = document.getElementById('inCheckViaje');
    var Opera = document.getElementById('Opera');
    var SelectOperacion = document.getElementById('SelectOperacion');
    var Ruta = document.getElementById('Ruta');
    var remolque1 = document.getElementById('remolque1');
    var remolque2 = document.getElementById('remolque2');
    var seleuni = document.getElementById('seleuni');
    var seleuniempresa = document.getElementById('seleuniempresa');


    if (seleuni.value != 0) {
        var CheckViaje = document.getElementById('CheckViaje');
        var numuni = seleuni.options[seleuni.selectedIndex].text;
        var num = numuni.split("|");

        claveEmp.value = seleuniempresa.options[seleuniempresa.selectedIndex].text;

        var inCheckViaje = document.getElementById('inCheckViaje');

        if (CheckViaje.checked) {
            inCheckViaje.value = true;
            mostrarMapa(claveEmp.value, seleuni.value);
            muestraViaje(claveEmp.value, num[0]);
            clearTable();
        }
        else {

            inCheckViaje.value = false;
            mostrarMapa(claveEmp.value, seleuni.value);
            clearTable();
        }
    }
    seleuni.addEventListener('change', function () {
        var CheckViaje = document.getElementById('CheckViaje');
        var numuni = seleuni.options[seleuni.selectedIndex].text;
        var num = numuni.split("|");
        var inCheckViaje = document.getElementById('inCheckViaje');
        seleuniempresa.value = seleuni.value;
        claveEmp.value = seleuniempresa.options[seleuniempresa.selectedIndex].text;

        if (CheckViaje.checked) {
            inCheckViaje.value = true;
            mostrarMapa(claveEmp.value, seleuni.value);
            muestraViaje(claveEmp.value, num[0]);
            clearTable();
        }
        else {
            inCheckViaje.value = false;
            mostrarMapa(claveEmp.value, seleuni.value);
            clearTable();
        }
    });


    CheckViaje.addEventListener("change", function () {
        if (this.checked) {
            inCheckViaje.value = true;
            if (seleuni.value != 0) {
                Opera.disabled = true;
                SelectOperacion.disabled = true;
                Ruta.disabled = true;
                remolque1.disabled = true;
                remolque2.disabled = true;
                var numuni = seleuni.options[seleuni.selectedIndex].text;
                var num = numuni.split("|");
                claveEmp.value = seleuniempresa.options[seleuniempresa.selectedIndex].text;
                if (CheckViaje.checked) {
                    mostrarMapa(claveEmp.value, seleuni.value);
                    muestraViaje(claveEmp.value, num[0]);
                    clearTable();
                }
            }
            else {
                Opera.disabled = true;
                Opera.value = 0;
                SelectOperacion.disabled = true;
                SelectOperacion.value = 0;
                Ruta.disabled = true;
                Ruta.value = "";
                remolque1.disabled = true;
                remolque1.value = 0;
                remolque2.disabled = true;
                remolque2.value = 0;
                seleuniempresa.value = 0;
                claveEmp.value = 0;
            }
        }
        else {
            inCheckViaje.value = false;
            Opera.disabled = false;
            Opera.value = 0;
            SelectOperacion.disabled = false;
            SelectOperacion.value = 0;
            Ruta.disabled = false;
            Ruta.value = 0;
            remolque1.disabled = false;
            remolque1.value = 0;
            remolque2.disabled = false;
            remolque2.value = 0;
            seleuniempresa.value = 0;
            claveEmp.value = 0;
        }
    });


});


document.getElementById('NumDaLla').addEventListener('input', function () {
    const numLlantas = parseInt(this.value) || 0; // Número de llantas (valor introducido)
    const container = document.getElementById('llantasContainer');
    // Limpiar los campos existentes
    container.innerHTML = '';

    // Generar dinámicamente los campos según el número de llantas
    for (let i = 1; i <= numLlantas; i++) {
        const llantaFields = `
            <div class="row">
                <div class="col-md-2">
                    <h6>Llanta ${i}</h6>
                </div>
                <div class="col-md-2">
                    <label for="Dot${i}" class="col-form-label">DOT:</label>
                    <input type="text" class="form-control form-control-sm" id="Dot${i}" name="Dot${i}" placeholder="#######" value="" />
                </div>
                <div class="col-md-2">
                    <label for="Marca${i}" class="col-form-label">Marca:</label>
                    <input type="text" class="form-control form-control-sm" id="Marca${i}" name="Marca${i}" placeholder="Marca" value="" />
                </div>
                <div class="col-md-2">
                    <label for="Medida${i}" class="col-form-label">Medida:</label>
                    <input type="text" class="form-control form-control-sm" id="Medida${i}" name="Medida${i}" placeholder="Medida" value="" />
                </div>
                <div class="col-md-2">
                    <label for="Posis${i}" class="col-form-label">Posición:</label>
                    <input type="number" class="form-control form-control-sm" id="Posis${i}" name="Posis${i}" placeholder="Posición:" value="" />
                </div>
                <div class="col-md-2">
                    <label for="Ecollant${i}" class="col-form-label">Eco LLantas:</label>
                    <input type="text" class="form-control form-control-sm" id="Ecollant${i}" name="Ecollant${i}" placeholder="Eco LLantas:" value="" />
                </div>
             </div>
            `;
        container.innerHTML += llantaFields; // Agregar los campos al contenedor
    }
});


function agregar() {
    // Obtén los elementos del formulario
    var AltaFalla = document.getElementById('AltaFalla');
    var fallasmuestra = document.getElementById('fallasmuestra').querySelector('tbody');
    var comentario = document.getElementById('Comentario').value;
    //var tipoFalla = document.getElementById('seltip').options[document.getElementById('seltip').selectedIndex].text;
    var clasificacion = document.getElementById('selclasi').options[document.getElementById('selclasi').selectedIndex].text;
    var fallaEn = document.getElementById('selfalla').options[document.getElementById('selfalla').selectedIndex].text;
    var clavesFalAndComen = document.getElementById('clavesFalAndComen');
    var fallallantas = document.getElementById('fallallantas');
    var clavefal = document.getElementById('selfalla').value;
    var claveclasi = document.getElementById('selclasi').value;
    //var clavetipo = document.getElementById('seltip').value;
    //remolque cvleequipo
    var remolque = document.getElementById('remolque');
    var selcveEquipo = document.getElementById('selcveEquipo').value;
    var verselcveEquipo = null;

    var files = document.getElementById('Files').files;
    var evidencias = [];
    Array.from(files).forEach((file, index) => {
        // Generar URL de vista previa para cada archivo
        var fileUrl = URL.createObjectURL(file);
        evidencias.push({ name: file.name, url: fileUrl, type: file.type });
        //RutasArchivos.value = RutasArchivos.value + fileUrl + file.name + '|';
    });
    //AgrgarImagenes();

    // Validar los campos
    if (!fallaEn || fallaEn === '[seleccionar]' ||
        !clasificacion || clasificacion === '[Seleccionar]') {
        //|| !tipoFalla || tipoFalla === '[Seleccionar]') {
        toastr.error('Por favor, llena todos los campos antes de agregar.');
        return;
    }

    // Si el tipo de falla es "Llanta", agrega los datos de las llantas
    let llantasInfo = '';
    if (clasificacion === 'Llantas') {
        const numLlantas = parseInt(document.getElementById('NumDaLla').value) || 0;
        if (numLlantas === 0) {
            toastr.error('Por favor, especifica el número de llantas dañadas.');
            return;
        }

        for (let i = 1; i <= numLlantas; i++) {
            const dot = document.getElementById(`Dot${i}`).value || 'N/A';
            const marca = document.getElementById(`Marca${i}`).value || 'N/A';
            const medida = document.getElementById(`Medida${i}`).value || 'N/A';
            const posicion = document.getElementById(`Posis${i}`).value || '0';
            const Ecollant = document.getElementById(`Ecollant${i}`).value || 'N/A';

            llantasInfo += `<p>#Llanta : ${i}, DOT: ${dot}, Marca: ${marca},Medida: ${medida},Posición: ${posicion},Eco LLantas: ${Ecollant} </p>`;
            fallallantas.value = fallallantas.value + i + '|' + dot + '|' + marca + '|' + medida + '|' + posicion + '|' + Ecollant + '%';

        }
    }
    if (clavefal == 2) {
        var inCheckViaje = document.getElementById('inCheckViaje');
        //var selcveEquipo = document.getElementById('selcveEquipo');
        if (inCheckViaje.value == "true") {
            if (remolque1.value !== "0" && remolque2.value !== "0") {
                if (selcveEquipo == 0) {
                    toastr.error('Por favor, seleciona el remolque');
                    return;
                }
                else {
                    var f = document.getElementById('selcveEquipo').options[document.getElementById('selcveEquipo').selectedIndex].text;
                    verselcveEquipo = "Remolque: " + f;
                }
            }
            else if (selcveEquipo == '') {
                selcveEquipo = 0;
            }
        }
        else {
            if (selcveEquipo == '') {
                selcveEquipo = 0;
            }
        }
    }
    if (selcveEquipo == '') {
        selcveEquipo = 0;
    }
    //clavesFalAndComen.value = clavesFalAndComen.value + clavefal + '|' + selcveEquipo + '|' + claveclasi + '|' + clavetipo + '|' + comentario + '%';
    clavesFalAndComen.value = clavesFalAndComen.value + clavefal + '|' + selcveEquipo + '|' + claveclasi + '|' + comentario + '%';

    // Crea una nueva fila
    var nuevaFila = document.createElement('tr');
    nuevaFila.innerHTML = `
        <td>${fallaEn}</td>
        <td>
            <div class="container">
                <div class="row">
                    <div class="col-md-6">
                    ${clasificacion} 
                    </div>
                    <div class="col-md-6">
                    ${verselcveEquipo || ''}
                    ${llantasInfo || ''}
                    </div>
                </div>
            </div>
        </td>            
        <td>${comentario}</td>
        <td colspan="2">
            <button type="button" class="btn btn-outline-secondary" onclick="MostraEvid(this)">
                <img src="${evidenciasImgPath}" alt="Evidencias" />
            </button>
            <div class="evidencias-container" style="display: flex; overflow-x: auto; max-width: 200px;">
            ${evidencias.map(e => `
                <img src="${e.url}" class="img-thumbnail" alt="${e.name}" onclick="mostrarModal(this)" style="cursor: pointer; width: 100px; height: auto; margin-right: 5px;">
                `).join('')}
            </div>
        </td>
        <td>
            <button type="button" class="btn btn-outline-danger" onclick="eliminarFila(this)">
                <img src="${eliminarImgPath}" />
            </button>
        </td>
    `;

    // Agrega la nueva fila al tbody
    fallasmuestra.appendChild(nuevaFila);

    // Limpia los campos del formulario
    document.getElementById('Comentario').value = '';
    //document.getElementById('seltip').selectedIndex = 0;
    document.getElementById('selclasi').selectedIndex = 0;
    document.getElementById('selfalla').selectedIndex = 0;
    document.getElementById('NumDaLla').value = '';
    document.getElementById('llantasContainer').innerHTML = '';
    document.getElementById('Files').value = '';
    document.getElementById('forllantas').style.display = "none";
    AltaFalla.style.display = "none";
    remolque.style.display = "none";
    toastr.success('Falla agregada correctamente.');
}

// Función para eliminar una fila específica
function eliminarFila(boton) {
    var fila = boton.closest('tr');
    var indexFila = Array.from(fila.parentNode.children).indexOf(fila); // Índice de la fila

    var fallallantas = document.getElementById('fallallantas');
    var clavesFalAndComen = document.getElementById('clavesFalAndComen');

    var clavesArray = clavesFalAndComen.value.split('%');
    var llantasArray = fallallantas.value.split('%');

    if (indexFila < clavesArray.length) {
        clavesArray.splice(indexFila, 1); // Elimina la clave de esta fila
    }

    if (indexFila < llantasArray.length) {
        llantasArray.splice(indexFila, 1); // Elimina la info de llantas si aplica
    }
    clavesFalAndComen.value = clavesArray.join('%');
    fallallantas.value = llantasArray.join('%');

    // Eliminar la fila del DOM
    fila.remove();
    toastr.error('Falla eliminada correctamente.');
}
function MostraEvid(boton) {
    var evidenciasContainer = boton.nextElementSibling;
    if (evidenciasContainer.style.display === 'none') {
        evidenciasContainer.style.display = 'block';

    } else {
        evidenciasContainer.style.display = 'none';
    }
}
function mostrarModal(img) {
    const modal = document.getElementById("myModal");
    const modalImg = document.getElementById("img01");
    const captionText = document.getElementById("caption");

    modal.style.display = "block";
    modalImg.src = img.src;
    captionText.innerHTML = img.alt || "Sin descripción";
}

function cerrarModal() {
    const modal = document.getElementById("myModal");
    modal.style.display = "none";
}

function AgrgarImagenes() {
    var files = document.getElementById('Files').files;
    var formData = new FormData();

    Array.from(files).forEach((file) => {
        formData.append('files', file); // "files" debe coincidir con el nombre del parámetro en el backend
    });

    fetch('/api/upload', {
        method: 'POST',
        body: formData,
    })
        .then((response) => {
            if (response.ok) {
                toastr.success('Archivos subidos correctamente.');
            } else {
                toastr.error('Error al subir los archivos.');
            }
        })
        .catch((error) => {
            console.error('Error:', error);
            toastr.error('Ocurrió un error.');
        });
}
function validarArchivos(input) {
    const archivos = input.files;
    const maxArchivos = 5;
    const tiposPermitidos = ['image/png', 'image/jpeg', 'video/mp4'];
    const errores = [];

    if (archivos.length > maxArchivos) {
        errores.push(`Solo puedes subir un máximo de ${maxArchivos} archivos.`);
    }

    for (let i = 0; i < archivos.length; i++) {
        if (!tiposPermitidos.includes(archivos[i].type)) {
            errores.push(`El archivo "${archivos[i].name}" no es un tipo válido. Solo se aceptan imágenes .png , .jpg. y.mp4`);
        }
    }

    if (errores.length > 0) {
        toastr.error(errores.join('<br>'));
        input.value = ''; // Limpia los archivos seleccionados
        return;
    }
    toastr.success('Archivos válidos.');
}
function muestraViaje(claveem, numunidad) {
    var overlay = document.getElementById('loading-overlay');
    overlay.style.display = 'block'; // Mostrar el spinner

    const inpRuta = document.getElementById('Ruta');
    const Opera = document.getElementById('Opera');
    const remolque1 = document.getElementById('remolque1');
    const remolque2 = document.getElementById('remolque2');
    const SelectOperacion = document.getElementById('SelectOperacion');
    const claveviajetum = document.getElementById('ClvViajTum');

    inpRuta.value = "";
    remolque1.value = 0;
    remolque2.value = 0;
    Opera.value = 0;
    SelectOperacion.value = 0;

    const url = new URL('https://webportal.tum.com.mx/wsstmdv/api/execspxor');
    var myHeaders = new Headers();
    myHeaders.append("Content-Type", "application/json");

    var raw = JSON.stringify({
        "data": {
            "bdCc": 5,
            "bdSch": "dbo",
            "bdSp": "SPQRY_DataActualxUnidad"
        },
        "filter": [
            {
                "property": "NoUnidad",
                "value": numunidad
            },
            {
                "property": "ClaveEmpresa",
                "value": claveem
            }
        ]
    });

    var requestOptions = {
        method: "POST",
        headers: myHeaders,
        body: raw,
        redirect: "follow"
    };
    fetch(url, requestOptions)
        .then(response => response.text())
        .then(result => {
            const obj = JSON.parse(result);
            if (obj.data == null) {
                var mensaje = document.getElementById('mensaje').value;
                mensaje = obj;
                if (mensaje !== '') {
                    toastr.error(mensaje);
                }
            }
            else if (obj.data.length == 0) {
                toastr.error("No se encuentra viaje");
            }
            else {
                if (obj.data[0].DataUnidadActual[0].ClaveOperador == null) {
                    Opera.value = 0;
                }
                else {
                    Opera.value = obj.data[0].DataUnidadActual[0].ClaveOperador;
                }
                if (obj.data[0].DataUnidadActual[0].Ruta == null) {
                    inpRuta.value = "";
                }
                else {
                    inpRuta.value = obj.data[0].DataUnidadActual[0].Ruta;
                }
                if (obj.data[0].DataUnidadActual[0].CveRem1 == null) {
                    remolque1.value = 0;
                }
                else {
                    remolque1.value = obj.data[0].DataUnidadActual[0].CveRem1;
                }
                if (obj.data[0].DataUnidadActual[0].CveRem2 == null) {
                    remolque2.value = 0;
                }
                else {
                    remolque2.value = obj.data[0].DataUnidadActual[0].CveRem2;
                }
                if (obj.data[0].DataUnidadActual[0].ClaveTipoOperacion == null) {
                    SelectOperacion.value = 0;
                }
                else {
                    SelectOperacion.value = obj.data[0].DataUnidadActual[0].ClaveTipoOperacion;
                }
                if (obj.data[0].DataUnidadActual[0].ClaveViajeTum == null) {
                    claveviajetum.value = 0;
                }
                else {
                    claveviajetum.value = obj.data[0].DataUnidadActual[0].ClaveViajeTum;
                }
                toastr.success("Viaje " + obj.data[0].DataUnidadActual[0].Folio);
            }
        })
        .catch(error => {
            console.error("Error:", error);
            toastr.error("Error al cargar datos del viaje");
        })
        .finally(() => {
            overlay.style.display = 'none'; // Ocultar el spinner
        });

}
function mostrarMapa(claveem, unidad) {
    var overlay = document.getElementById('loading-overlay');
    overlay.style.display = 'block'; // Mostrar el spinner

    const ayudamap = document.getElementById('ayudamap');
    var url = new URL('https://webportal.tum.com.mx/wsstmdv/api/execspxor');
    var myHeaders = new Headers();
    myHeaders.append("Content-Type", "application/json");

    var raw = JSON.stringify({
        "data": {
            "bdCc": 5,
            "bdSch": "dbo",
            "bdSp": "SPQRY_UltimaPosicion_Unidad"
        },
        "filter": [
            {
                "property": "ClaveEmpresa",
                "value": claveem
            },
            {
                "property": "ClaveUnidad",
                "value": unidad
            }
        ]
    });
    var requestOptions = {
        method: "POST",
        headers: myHeaders,
        body: raw,
        redirect: "follow"
    };
    fetch(url, requestOptions)
        .then(response => response.text())
        .then(result => {
            const obj = JSON.parse(result);
            //console.log(obj);
            if (obj.data == null) {
                var mensaje = document.getElementById('mensaje').value;
                mensaje = obj;
                if (mensaje !== '') {
                    toastr.error(mensaje);
                }
            }
            else {
                var lati = obj.data[0].UltimaPosicion[0].Latitud;
                var long = obj.data[0].UltimaPosicion[0].Longitud;
                var LGPS = obj.data[0].UltimaPosicion[0].Position;
                var fechaGPs = obj.data[0].UltimaPosicion[0].SendTime;
                ayudamap.style.display = "block";
                PintaMapa(long, lati, LGPS, fechaGPs);
            }
        })
        .catch(error => {
            var map = document.getElementById('map');
            map.style.display = "block";
            console.log("Error:", error);
            PintaMapa(long, lati, "", "");
            ayudamap.style.display = "none";
            toastr.error("Error al cargar el mapa");
        })
        .finally(() => {
            overlay.style.display = 'none'; // Ocultar el spinner
        });

}


function mostrafalla() {
    var AltaFalla = document.getElementById('AltaFalla');
    if (AltaFalla.style.display === "none") {
        AltaFalla.style.display = "block";
    } else {
        AltaFalla.style.display = "none";
    }
}
function loadCarouselImages() {

}

var map = null;
var marker = null;
function PintaMapa(inputLngValue, inputLatValue, DirGPS, feGPs) {
    const latin = document.getElementById('lat');
    const long = document.getElementById('long');
    const fecha = document.getElementById('fechaGPs');
    const DirGps = document.getElementById('DirPosGps');
    if (!map) {
        map = L.map('map').setView([inputLatValue, inputLngValue], 15);

        L.tileLayer('https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png', {
            attribution: '© OpenStreetMap contributors'
        }).addTo(map);
    }

    if (marker) {
        map.removeLayer(marker);
    }

    marker = L.marker([inputLatValue, inputLngValue], { draggable: true })
        .addTo(map)
        .bindPopup("Última ubicación reportada:<br>\n" + DirGPS + "<br>\nFecha Reportada:<br>\n" + feGPs)
        .openPopup();

    latin.value = "";
    long.value = "";
    fecha.value = "";
    DirGps.value = "";
    latin.value = inputLatValue.toFixed(6);
    long.value = inputLngValue.toFixed(6);
    fecha.value = feGPs;
    DirGps.value = DirGPS;

    toastr.success("Se creo el Mapa");
    marker.on('dragend', function (e) {
        var popup = e.target.getPopup();
        var newLatLng = e.target.getLatLng(); // Obtener las nuevas coordenadas
        inputLatValue = newLatLng.lat;
        inputLngValue = newLatLng.lng;

        // Llamar a Nominatim para obtener la dirección de las nuevas coordenadas
        const url = `https://nominatim.openstreetmap.org/reverse?format=jsonv2&lat=${inputLatValue}&lon=${inputLngValue}`;

        fetch(url)
            .then(response => response.json())
            .then(data => {
                if (data && data.display_name) {
                    // Actualizar el popup con la dirección
                    var newAddress = data.display_name; // Dirección obtenida de Nominatim
                    popup.setContent(
                        "Actualización de ubicación reportada:<br>" +
                        "Latitud: " + inputLatValue.toFixed(6) + "<br>" +
                        "Longitud: " + inputLngValue.toFixed(6) + "<br>" +
                        "Dirección: " + newAddress
                    ).openOn(map);
                    // Mostrar en consola para verificar
                    //console.log("Nueva Dirección:", newAddress);
                    actualizarTexto(inputLatValue, inputLngValue, newAddress);

                } else {
                    popup.setContent("No se pudo obtener la dirección.");
                    actualizarTexto(inputLatValue, inputLngValue, "No se pudo obtener la dirección.");
                }
            })
            .catch(error => {
                console.error("Error en la geocodificación inversa:", error);
                popup.setContent("Error al obtener la dirección.");
            });
        //marker.setPopupContent(
        //    "Actualización de ubicación reportada:<br>" +
        //    "Latitud: " + inputLatValue.toFixed(6) + "<br>" +
        //    "Longitud: " + inputLngValue.toFixed(6)
        //).openPopup();
        //actualizarTexto(inputLatValue, inputLngValue);
    });

}
function actualizarTexto(lat, lng, newAddress) {

    const cambio = document.getElementById('texto');
    const latin = document.getElementById('latNew');
    const long = document.getElementById('longNew');
    const fecha = document.getElementById('fechaGPsNew');
    const DirGps = document.getElementById('DirPosGpsNew');
    var n = new Date();

    cambio.innerText = ""; // Limpiar el contenido previo
    cambio.innerText = "Latitud: " + lat.toFixed(6) + " Longitud: " + lng.toFixed(6);
    latin.value = "";
    long.value = "";
    fecha.value = "";
    DirGps.value = "";
    latin.value = lat.toFixed(6);
    long.value = lng.toFixed(6);
    fecha.value = n.toISOString("yyyy/MM/dd HH:mm:ss");
    DirGps.value = newAddress;
    toastr.success("Se actualizan las Cordenadas");
}
function habilitarCamposDeshabilitados() {
    const campos = ['selorigen', 'Opera', 'SelectOperacion', 'Ruta', 'remolque1', 'remolque2'];

    campos.forEach(id => {
        const campo = document.getElementById(id);
        if (campo) {
            campo.disabled = false;
        }
    });
}

