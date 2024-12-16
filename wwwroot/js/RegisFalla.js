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

    var CheckViaje = document.getElementById('CheckViaje');
    var inCheckViaje = document.getElementById('inCheckViaje').value;

    var Opera = document.getElementById('Opera');
    var SelectOperacion = document.getElementById('SelectOperacion');
    var Ruta = document.getElementById('Ruta');
    var claveEmp = document.getElementById('cveEmp');
    var remolque1 = document.getElementById('remolque1');
    var remolque2 = document.getElementById('remolque2');
    var seleuni = document.getElementById('seleuni');
    ///extra
    var clavesFalAndComen = document.getElementById('clavesFalAndComen');
    var fallallantas = document.getElementById('fallallantas');


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
            if (CheckViaje.checked) {
                mostrarMapa(claveEmp.value, seleuni.value);
                muestraViaje(claveEmp.value, num[0]);
                document.getElementById('tabmosfal').querySelector("tbody").innerHTML = "";
                clavesFalAndComen.value = "";
                fallallantas.value = "";
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
    var clavesFalAndComen = document.getElementById('clavesFalAndComen');
    var fallallantas = document.getElementById('fallallantas');

    var CheckViaje = document.getElementById('CheckViaje');
    var inCheckViaje = document.getElementById('inCheckViaje');
    var Opera = document.getElementById('Opera');
    var SelectOperacion = document.getElementById('SelectOperacion');
    var Ruta = document.getElementById('Ruta');
    var remolque1 = document.getElementById('remolque1');
    var remolque2 = document.getElementById('remolque2');
    var seleuni = document.getElementById('seleuni');

    seleuni.addEventListener('change', function () {
        var CheckViaje = document.getElementById('CheckViaje');
        var numuni = seleuni.options[seleuni.selectedIndex].text;
        var num = numuni.split("|");
        var inCheckViaje = document.getElementById('inCheckViaje');
        if (CheckViaje.checked) {
            inCheckViaje.value = true;
            mostrarMapa(claveEmp.value, seleuni.value);
            muestraViaje(claveEmp.value, num[0]);
            document.getElementById('tabmosfal').querySelector("tbody").innerHTML = "";
            clavesFalAndComen.value = "";
            fallallantas.value = "";
        }
        else {
            inCheckViaje.value = false;
            mostrarMapa(claveEmp.value, seleuni.value);
            document.getElementById('tabmosfal').querySelector("tbody").innerHTML = "";
            clavesFalAndComen.value = "";
            fallallantas.value = "";
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
                if (CheckViaje.checked) {
                    mostrarMapa(claveEmp.value, seleuni.value);
                    muestraViaje(claveEmp.value, num[0]);
                    document.getElementById('tabmosfal').querySelector("tbody").innerHTML = "";
                    clavesFalAndComen.value = "";
                    fallallantas.value = "";
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

function agrgar() {
    // Obtén los elementos del formulario
    var AltaFalla = document.getElementById('AltaFalla');
    var fallasmuestra = document.getElementById('fallasmuestra').querySelector('tbody');
    var comentario = document.getElementById('Comentario').value;
    var tipoFalla = document.getElementById('seltip').options[document.getElementById('seltip').selectedIndex].text;
    var clasificacion = document.getElementById('selclasi').options[document.getElementById('selclasi').selectedIndex].text;
    var fallaEn = document.getElementById('selfalla').options[document.getElementById('selfalla').selectedIndex].text;
    var clavesFalAndComen = document.getElementById('clavesFalAndComen');
    var fallallantas = document.getElementById('fallallantas');
    var clavefal = document.getElementById('selfalla').value;
    var claveclasi = document.getElementById('selclasi').value;
    var clavetipo = document.getElementById('seltip').value;
    //remolque cvleequipo
    var remolque = document.getElementById('remolque');
    var selcveEquipo = document.getElementById('selcveEquipo').value;
    var verselcveEquipo = null;


    // Validar los campos
    if (!fallaEn || fallaEn === '[seleccionar]' ||
        !clasificacion || clasificacion === '[Seleccionar]' ||
        !tipoFalla || tipoFalla === '[Seleccionar]') {
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
    clavesFalAndComen.value = clavesFalAndComen.value + clavefal + '|' + selcveEquipo + '|' + claveclasi + '|' + clavetipo + '|' + comentario + '%';

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
        <td>${tipoFalla}</td>
        <td>${comentario}</td>
        <td>
            <button type="button" class="btn btn-outline-secondary" onclick="MostraEvid()">
                <img src="${evidenciasImgPath}" alt="Evidencias" />
            </button>
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
    document.getElementById('seltip').selectedIndex = 0;
    document.getElementById('selclasi').selectedIndex = 0;
    document.getElementById('selfalla').selectedIndex = 0;
    document.getElementById('NumDaLla').value = '';
    document.getElementById('llantasContainer').innerHTML = '';
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
    toastr.success('Falla eliminada correctamente.');

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
                PintaMapa(long, lati, LGPS, fechaGPs);
            }
        })
        .catch(error => {
            var map = document.getElementById('map');
            map.style.display = "block";
            console.log("Error:", error);
            toastr.error("Error al cargar el mapa");
            PintaMapa(long, lati, "", "");
        })
        .finally(() => {
            overlay.style.display = 'none'; // Ocultar el spinner
        });

}


function mostrafalla() {
    var AltaFalla = document.getElementById('AltaFalla');
    var Evidencia = document.getElementById('Evidencia');
    if (AltaFalla.style.display === "none") {
        AltaFalla.style.display = "block";
        Evidencia.style.display = "none";
    } else {
        AltaFalla.style.display = "none";
        Evidencia.style.display = "none";
    }
}
function loadCarouselImages() {

}
function MostraEvid() {
    var AltaFalla = document.getElementById('AltaFalla');
    var Evidencia = document.getElementById('Evidencia');
    if (Evidencia.style.display === "none") {
        Evidencia.style.display = "block";
        AltaFalla.style.display = "none";
    }
    else {
        Evidencia.style.display = "none";
        AltaFalla.style.display = "none";
    }
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
        var newLatLng = e.target.getLatLng(); // Obtener las nuevas coordenadas
        inputLatValue = newLatLng.lat;
        inputLngValue = newLatLng.lng;

        marker.setPopupContent(
            "Actualización de ubicación reportada:<br>" +
            "Latitud: " + inputLatValue.toFixed(6) + "<br>" +
            "Longitud: " + inputLngValue.toFixed(6)
        ).openPopup();
        actualizarTexto(inputLatValue, inputLngValue);
    });

}
function actualizarTexto(lat, lng) {
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
