document.addEventListener("DOMContentLoaded", function () {

    var lati = document.getElementById('Latitud').value;
    var long = document.getElementById('Longitud').value;

    var map = L.map('map').setView([lati,long ], 15);
    L.tileLayer('https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png', {
        attribution: '© OpenStreetMap contributors'
    }).addTo(map);

    L.marker([lati,Long]).addTo(map)
        .bindPopup("<b>Ubicación</b><br>Lat: ${Latitud}<br>Lon: ${Longitud}")
        .openPopup();
});