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


function PrintMapa() {

    var map = L.map('map').setView([21.6167060, -101.2252099], 6); // Cambia las coordenadas y zoom según tu preferencia

    // Agrega el mapa base de OpenStreetMap
    L.tileLayer('https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png', {
        attribution: '&copy; <a href="https://www.openstreetmap.org/copyright">OpenStreetMap</a> contributors'
    }).addTo(map);

    var datitos = document.getElementById('datosload').value;
    var jserial = JSON.parse(datitos);
    console.log(jserial);
    console.log(jserial.local);
    // Lista de ubicaciones con coordenadas (lat, lng) y nombre
    //var locations = [
    //    { name: "TUM TRANSPORTISTAS UNIDOS MEXICANOS CUAUTITLAN IZCALLI", lat: 19.61747213015414, lng: - 99.22406370789754 },
    //    { name: "TUM TRANSPORTISTAS UNIDOS MEXICANOS QUERÉTARO", lat: 20.570928711236398, lng: -100.30288772919846 }, // Ejemplo: Ciudad de México
    //    { name: "TUM TRANSPORTISTAS UNIDOS MEXICANOS GUADALAJARA", lat: 20.56577818262262, lng: - 103.26878318164584 }, // Ejemplo: Cancún
    //    { name: "TUM TRANSPORTISTAS UNIDOS MEXICANOS AGUASCALIENTES", lat: 21.76029500758191, lng: - 102.27903498636567 } // Ejemplo: Guadalajara
    //];
    //console.log(locations);
    // Añadir un marcador para cada ubicación
    locations.forEach(function (jserial.local) {
        //console.log(location);
        //console.log(location.lat);
        //console.log(location.lng);
        L.marker([jserial.local.lat, jserial.local.lng])
            .addTo(map)
            .bindPopup(`<b>${location.name}</b>`)
            .openPopup();
    });
}