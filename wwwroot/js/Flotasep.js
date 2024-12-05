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

if ('serviceWorker' in navigator && 'PushManager' in window) {
    navigator.serviceWorker.register('/sw.js')
        .then(function (registration) {
            console.log('Service Worker registrado:', registration);
        })
        .catch(function (error) {
            console.error('Error al registrar el Service Worker:', error);
        });
}
function prueba_notificacion2() {
    if (Notification) {
        if (Notification.permission !== "granted") {
            Notification.requestPermission()
        }

        if (typeof noti2 != 'undefined') {
            noti2.onclose = null;
            noti2.close()
        }

        var title = "Tum Flota"
        var extra = {
            icon: "https://webportal.tum.com.mx/wsstest/imag/logo_difuminado.png",
            body: "Prueba de Notificacion"
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
function requestNotificationPermission() {
    Notification.requestPermission().then(function (permission) {
        if (permission === 'granted') {
            console.log('Permiso para notificaciones concedido.');
        } else {
            console.error('Permiso para notificaciones denegado.');
        }
    });
}

function PrintMapa() {
    var map = L.map('map').setView([19.61747213015414, -99.22406370789754], 16);

    // Agrega el mapa base de OpenStreetMap
    L.tileLayer('https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png', {
        attribution: '&copy; <a href="https://www.openstreetmap.org/copyright">OpenStreetMap</a> contributors'
    }).addTo(map);

    var locations = JSON.parse(document.getElementById('datosload').value);

    var markers = [];

    locations.local.forEach(function (local, index) {
        let marker = L.marker([local.lat, local.lng], { draggable: true })
            .addTo(map)
            .bindPopup(`<b>${local.name}</b><br>Lat: ${local.lat}<br>Lng: ${local.lng}`)
            .openPopup();

        // Guardar el marcador en el arreglo
        markers.push(marker);

        // Manejar el evento 'dragend' para actualizar las coordenadas
        marker.on('dragend', function (e) {
            var newLatLng = e.target.getLatLng(); // Obtener las nuevas coordenadas
            local.lat = newLatLng.lat; // Actualizar las coordenadas en el objeto
            local.lng = newLatLng.lng;

            // Actualizar visualmente el popup con las nuevas coordenadas
            marker.bindPopup(`<b>${local.name}</b><br>Lat: ${newLatLng.lat}<br>Lng: ${newLatLng.lng}`).openPopup();

            // Actualizar el texto con las coordenadas
            actualizarTexto(locations.local);
        });
    });
    // Guardar las ubicaciones actualizadas en un atributo del mapa
    map.updatedLocations = locations;

    // Mostrar las coordenadas iniciales en el texto
    actualizarTexto(locations.local);
}

function actualizarTexto(locations) {
    const cambio = document.getElementById('texto');
    cambio.innerText = ""; // Limpiar el contenido previo

    // Iterar sobre las ubicaciones y mostrar las coordenadas actualizadas
    locations.forEach((local) => {
        cambio.innerText += `${local.name} -- ${local.lat} -- ${local.lng} --\n`;
    });
}
function guardarCoordenadas() {
    const mapElement = L.DomUtil.get('map'); // Obtener el mapa
    const actual = mapElement._leaflet_map.updatedLocations.local;

    console.log("Coordenadas guardadas:", actual);
    toastr.success("Coordenadas actualizadas y guardadas.");
}