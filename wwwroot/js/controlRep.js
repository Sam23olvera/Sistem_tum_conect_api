const ventanas = {};
window.onload = function () {
    document.getElementById("loading-overlay").style.display = "none";
    // Iniciar el contador de 10 minutos
    iniciarContador();
};
function ventanaEmergenteAbierta(nombre) {
    if (!ventanas[nombre] || ventanas[nombre].closed) {
        aviso(`La ventana "${nombre}" no está abierta.`);
        return false;
    }
    return true;
}
function cerrarVentana(nombre) {
    if (ventanaEmergenteAbierta(nombre)) {
        ventanas[nombre].close();
        delete ventanas[nombre]; // Eliminar la referencia de la ventana cerrada
        aviso(`Se ha cerrado la ventana "${nombre}".`);
    }
}

function iniciarContador() {
    const contadorDisplay = document.createElement("div");
    contadorDisplay.id = "contador";
    contadorDisplay.style.position = "fixed";
    contadorDisplay.style.bottom = "10px";
    contadorDisplay.style.right = "10px";
    contadorDisplay.style.backgroundColor = "rgba(0, 0, 0, 0.8)";
    contadorDisplay.style.color = "white";
    contadorDisplay.style.padding = "10px";
    contadorDisplay.style.borderRadius = "5px";
    contadorDisplay.style.zIndex = "9999";
    document.body.appendChild(contadorDisplay);

    let tiempoRestante = 10 * 60; // 10 minutos en segundos


    const interval = setInterval(() => {
        const minutos = Math.floor(tiempoRestante / 60);
        const segundos = tiempoRestante % 60;

        // Mostrar el contador en formato mm:ss
        contadorDisplay.textContent = `La página se recargará en ${minutos}:${segundos.toString().padStart(2, "0")}`;

        tiempoRestante--;

        if (tiempoRestante < 0) {
            clearInterval(interval);
            location.reload(); // Refrescar la página
        }
    }, 1000); // Ejecutar cada segundo
}

document.addEventListener("DOMContentLoaded", function () {
    if (Notification.permission !== "granted") {
        Notification.requestPermission()
    }
    const mensaje = document.getElementById('mensaje');
    const guarda = document.getElementById('guarda');

    var links = document.querySelectorAll(".carga");
    //var pagin = document.querySelectorAll(".page-link");
    links.forEach(function (link) {
        link.addEventListener("click", function () {
            document.getElementById("loading-overlay").style.display = "block";
        });
    });

    if (mensaje?.value) toastr.error(mensaje.value);
    if (guarda?.value) toastr.success(guarda.value);


    // Seleccionar todos los botones que tienen el atributo data-bs-target y NO están deshabilitados
    const botones = document.querySelectorAll('button[data-bs-target]:not([disabled])');
    botones.forEach(function (boton) {
        boton.addEventListener('click', async function () {
            const modalidEco = boton.getAttribute('data-bs-name');
            const modalId = boton.getAttribute('data-bs-target');
            const claveTicket = modalId.replace('#modalEco-', '').replace('#modalDetalle-', '');
            if (modalidEco === 'ECO') {
                const modalId = boton.getAttribute('data-bs-target');
                const claveTicket = modalId.replace('#modalEco-', ''); // Por ejemplo: "12345"
                var json = {
                    "data": {
                        "bdCc": 5,
                        "bdSch": "dbo",
                        "bdSp": "SPQRY_VerTicket"
                    },
                    "filter": [
                        {
                            "property": "ClaveTicket",
                            "value": claveTicket // Usamos la clave obtenida dinámicamente
                        }
                    ]
                };

                try {
                    const result = await consumir(json);
                    if (result.status == 200) {
                        if (result.data == null || result.data == 0) {
                            toastr.error(result.status + " No Se Encuentran Datos del Ticket " + claveTicket);
                            notificacion(result.status + " No Se Encuentran Datos del Ticket " + claveTicket, "TUM NOTIFICA");
                        }
                        else {
                            var inpNumOP = 'NumOP-' + claveTicket;
                            var inpTipoCarga = 'TipoCarga-' + claveTicket;
                            var inpTel_Operador = 'Tel_Operador-' + claveTicket;
                            var inpFolio = 'Folio-' + claveTicket;
                            var inNombreCliente = 'NombreCliente-' + claveTicket;
                            var tabFallasDeta = 'FallasDeta-' + claveTicket;
                            var nametabTabFallasDeta = 'TabFallasDeta-' + claveTicket;
                            var inpRemolques = 'Remolques-' + claveTicket;
                            var inpDoly = 'Doly-' + claveTicket;
                            var inpMostMap = 'MostMap-' + claveTicket;
                            var labtipoCarga = 'labTipoCarga-' + claveTicket;

                            const numop = document.getElementById(inpNumOP);
                            const TipoCarga = document.getElementById(inpTipoCarga);
                            const Tel_Operador = document.getElementById(inpTel_Operador);
                            const Folio = document.getElementById(inpFolio);
                            const NombreCliente = document.getElementById(inNombreCliente);
                            const Remolques = document.getElementById(inpRemolques);
                            const Doly = document.getElementById(inpDoly);
                            const labtip = document.getElementById(labtipoCarga);
                            const fallasdeta = document.getElementById(tabFallasDeta).querySelector('tbody');

                            numop.textContent = result.data[0].Ticket[0].NumEmpleado + ' | ' + result.data[0].Ticket[0].NombOperador;
                            TipoCarga.checked = result.data[0].Ticket[0].TipoCarga;
                            if (result.data[0].Ticket[0].TipoCarga == true) {
                                labtip.textContent = 'cargado';
                            }
                            else
                            {
                                labtip.textContent = 'vacio';
                            }
                            Tel_Operador.textContent = result.data[0].Ticket[0].Tel_Operador;
                            Folio.textContent = result.data[0].Ticket[0].Folio;
                            if (result.data[0].Ticket[0].Remolque1 === null && result.data[0].Ticket[0].Remolque2 === null) {
                                Remolques.textContent = '' + ' | ' + '';
                            }
                            else
                            {
                                Remolques.textContent = result.data[0].Ticket[0].Remolque1 + ' | ' + result.data[0].Ticket[0].Remolque2;
                            }
                            const prove = result.data[1].Provee; // Lista de proveedores
                            const proveString = encodeURIComponent(JSON.stringify(prove)); // Convertimos a JSON y codificamos
                            Doly.textContent = result.data[0].Ticket[0].NumDolly;
                            NombreCliente.textContent = result.data[0].Ticket[0].NombreCliente;
                            var botonmapa = `<strong> ECO: </strong> <br/> <button type="button" class="btn btn-outline-dark" onclick="mostrarubicacion(${claveTicket}, ${result.data[0].Ticket[0].ECO}, ${result.data[0].Ticket[0].Latitud}, ${result.data[0].Ticket[0].Longitud}, '${proveString}')"> <img src="https://webportal.tum.com.mx/wsstest/imag/gpsunidad.png"/> ${result.data[0].Ticket[0].ECO}  </button>`;
                            document.getElementById(inpMostMap).innerHTML = botonmapa;

                            document.getElementById(nametabTabFallasDeta).querySelector("tbody").innerHTML = "";
                            
                            const fallas = result.data[0].Ticket[0].Fallas;
                            for (let i = 0; i <= fallas.length; i++) {
                                var imag = ``;
                                if (fallas[i].IdTipoEq == 1) {
                                    imag = `<img src="https://webportal.tum.com.mx/wsstest/imag/Unidad.png" />`;
                                }
                                else if (fallas[i].IdTipoEq == 2) {
                                    imag = `<img src="https://webportal.tum.com.mx/wsstest/imag/remolque.png" />`;
                                }
                                else if (fallas[i].IdTipoEq == 3) {
                                    imag = `<img src="https://webportal.tum.com.mx/wsstest/imag/Dolly.png" />`;
                                }
                                else if (fallas[i].IdTipoEq == 3) {
                                    imag = `<i class="bi bi-car-front"></i>`;
                                }
                                var Referencia = fallas[i].Referencia;
                                if (fallas[i].DOT === null) {
                                    Referencia = Referencia + '' ;
                                }
                                else {
                                    Referencia = Referencia + '<br/>DOT: ' + fallas[i].DOT;
                                }
                                if (fallas[i].Marca === null) {
                                    Referencia = Referencia + '';
                                }
                                else {
                                    Referencia = Referencia + '<br/>Marca: ' + fallas[i].Marca;
                                }
                                if (fallas[i].Medida === null) {
                                    Referencia = Referencia + '';
                                }
                                else {
                                    Referencia = Referencia + '<br/>Medida: ' + fallas[i].Medida;
                                }
                                if (fallas[i].Posicion === null) {
                                    Referencia = Referencia + '';
                                }
                                else {
                                    Referencia = Referencia + '<br/>Posicion: ' + fallas[i].Posicion;
                                }
                                if (fallas[i].ECOLlanta === null) {
                                    Referencia = Referencia + '';
                                }
                                else {
                                    Referencia = Referencia + '<br/>ECO Llanta: ' + fallas[i].ECOLlanta;
                                }
                                Referencia = '<td>' + Referencia + '</td>';
                                var nuevafilas = document.createElement('tr');
                                nuevafilas.innerHTML = `
                                <td>
                                    ${fallas[i].FallaEn} 
                                    ${fallas[i].Remolque1 || ''}
                                    ${fallas[i].Remolque2 || ''}
                                    ${fallas[i].NumDolly || ''}
                                <br/>
                                    ${imag}
                                <br/>
                                    ${fallas[i].Equipo}
                                </td>
                                <td>
                                    ${fallas[i].Clasificacion}
                                </td>
                                ${Referencia}
                                <td>
                                    <button class="btn btn-light" onclick="MostrarTabProveedores('${claveTicket}')">
                                        <i class="bi bi-eye"></i>
                                    </button>
                                </td>
                                <td>
                                    <button type="button" class="btn btn-light btn-sm" onclick="subirevidencias(${claveTicket})">
                                        <i class="bi bi-paperclip"></i>Falla
                                    </button>
                                    <br/>
                                    <button type="button" class="btn btn-light btn-sm" onclick="subirevidencias(${claveTicket})">
                                       <i class="bi bi-paperclip"></i>Reparacion
                                    </button>
                                    <br/>
                                    <button type="button" class="btn btn-light btn-sm" onclick="subirevidencias(${claveTicket})">
                                       <i class="bi bi-paperclip"></i>Liberada
                                    </button>
                                </td>
                                <td>
                                    ${fallas[i].FolLlantas || ''}
                                </td>`;
                                fallasdeta.appendChild(nuevafilas);
                            }
                        }
                    }

                } catch (error) {
                    console.error("Error de RED:", error);
                }
            } else if (modalidEco === 'DETALLE') {

            }
        });
    });
});

async function consumir(jsonData) {
    const overlay = document.getElementById('loading-overlay');
    overlay.style.display = 'block'; // Mostrar el spinner

    const url = new URL('https://webportal.tum.com.mx/wsstmdv/api/execspxor');
    const myHeaders = new Headers();
    myHeaders.append("Content-Type", "application/json");

    const requestOptions = {
        method: "POST",
        headers: myHeaders,
        body: JSON.stringify(jsonData), // Convertimos el JSON al formato adecuado
        redirect: "follow"
    };

    try {
        // Realizamos el fetch y esperamos la respuesta
        const response = await fetch(url, requestOptions);

        // Verificamos si la respuesta es exitosa
        if (!response.ok) {
            throw new Error(`Error en la petición: ${response.status} - ${response.statusText} `);
        }

        // Parseamos la respuesta como JSON
        const result = await response.json();
        return result; // Devolvemos el resultado para su manejo posterior
    } catch (error) {
        console.error('Error en fetch:', error);
        throw error; // Propagamos el error para manejarlo fuera de la función
    } finally {
        const overlay = document.getElementById('loading-overlay');
        if (overlay) {
            overlay.style.display = 'none';
        }
    }
}

function mostrarubicacion(claveTicket, ECO, Latitud, Longitud, proveString) {
    var proveedores = JSON.parse(decodeURIComponent(proveString)); // Decodificamos el JSON recibido

    var ventana = window.open("", "", "menubar=no, scrollbars=no, width=850, height=400");
    var htmlContent = `
    <html>
    <head>
        <meta charset="UTF-8">
        <meta name="viewport" content="width=device-width, initial-scale=1.0">
        <title>Mapa-Eco-TUM</title>
        <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/css/bootstrap.min.css" rel="stylesheet">
        <link rel="stylesheet" href="https://unpkg.com/leaflet@1.9.4/dist/leaflet.css" />
        <style>
            body { font-family: Arial, sans-serif; padding: 20px; }
            #map { height: 400px; margin-top: 10px; }
            .info-box { font-size: 18px; margin-bottom: 10px; }
        </style>
    </head>
    <body>
        <div class="container">
            <div class="info-box">
                <strong>Numero de Ticket:</strong> ${claveTicket} <br>
                <strong>ECO:</strong> ${ECO}
            </div>
            <div id="map"></div>
        </div>
        <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/js/bootstrap.bundle.min.js"></script>
        <script src="https://unpkg.com/leaflet@1.9.4/dist/leaflet.js"></script>
        <script>
            window.onload = function() {
                var map = L.map('map').setView([${Latitud}, ${Longitud}], 10);

                L.tileLayer('https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png', {
                    attribution: '© OpenStreetMap contributors'
                }).addTo(map);

                var ecoIcon = L.icon({
                    iconUrl: 'https://webportal.tum.com.mx/wsstest/imag/Unidad.png',
                    iconSize: [20, 20]
                });

                L.marker([${Latitud}, ${Longitud}], { icon: ecoIcon }).addTo(map)
                    .bindPopup("<b>ECO: " + ${ECO} + "</b><br>Lat: " + ${Latitud} + "<br>Lon: " + ${Longitud})
                    .openPopup();

                var provIcon = L.icon({
                    iconUrl: 'https://webportal.tum.com.mx/wsstest/imag/herramientas.png',
                    iconSize: [20, 20]
                });

                var proveedores = ${JSON.stringify(proveedores)};

                proveedores.forEach(prov => {
                    L.marker([prov.latitud, prov.longitud], { icon: provIcon }).addTo(map)
                        .bindPopup("<b>Proveedor: " + prov.RazonSocial + "</b><br>Lat: " + prov.latitud + "<br>Lon: " + prov.longitud);
                });
            };
        </script>
    </body>
    </html>`;

    ventana.document.open();
    ventana.document.write(htmlContent);
    ventana.document.close();
}
function MostrarTabProveedores(ClaveTicket) {
    const nombre = `proveedores-${ClaveTicket}`;
    ventanas[nombre] = window.open("", "", "menubar=no, width=900, height=250");
    ventanas[nombre].document.write(`
           <html>
           <head>
               <meta charset="UTF-8">
               <meta name="viewport" content="width=device-width, initial-scale=1.0">
               <title>Proveedores-TUM</title>
               <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/css/bootstrap.min.css" rel="stylesheet">
               <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/bootstrap-icons/font/bootstrap-icons.css">
           </head>
           <body>
               <div class="container">
                   <div class="row align-content-center text-center">
                       <h1>Proveedores del ticket ${ClaveTicket}</h1>
                   </div>
                   <div class="row overflow-scroll">
                       <table class="table table-secondary table-sm table-bordered align-content-sm-center">
                           <thead>
                               <tr>
                                   <th class="text-center">Proveedor</th>
                                   <th class="text-center">Eta Proveedor</th>
                                   <th class="text-center">Costo MXN</th>
                                   <th class="text-center">Tipo</th>
                                   <th class="text-center">Fecha Pago</th>
                                   <th class="text-center">Forma de Pago</th>
                                   <th class="text-center">Fol Docto</th>
                                   <th class="text-center">Evidencias</th>
                               </tr>
                           </thead>
                           <tbody>
                               <tr>
                                   <td class="text-center">Proveedor</td>
                                   <td class="text-center">Eta Proveedor</td>
                                   <td class="text-center">Costo MXN</td>
                                   <td class="text-center">Tipo</td>
                                   <td class="text-center">Fecha Pago</td>
                                   <td class="text-center">Forma de Pago</td>
                                   <td class="text-center">Fol Docto</td>
                                   <td class="text-center">
                                       <button type="button" class="btn btn-light" onclick="subirevidencias(${ClaveTicket})">
                                       <i class="bi bi-paperclip"></i>
                                       </button>
                                   </td>
                               </tr>
                           </tbody>
                       </table>
                   </div>
               </div>
               <script src="js/controlRep.js" asp-append-version="true"></script>
               <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/js/bootstrap.bundle.min.js"></script>
           </body>
           </html>
       `);
}
function subirevidencias(ClaveTicket) {
    const nombre = `evidencias-${ClaveTicket}`;
    ventanas[nombre] = window.open("", "", "menubar=no, scrollbars=no, width=850, height=300");
    ventanas[nombre].document.write(`
        <html>
        <head>
            <meta charset="UTF-8">
            <meta name="viewport" content="width=device-width, initial-scale=1.0">
            <title>Subir Evidencias</title>
            <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/css/bootstrap.min.css" rel="stylesheet">
        </head>
        <body>
            <div class="container">
                <div class="row align-content-center text-center">
                    <h1>Evidencias del Proveedor del Ticket ${ClaveTicket}</h1>
                </div>
                <div class="row">
                    <div class="input-group mb-3">
                        <input type="file" class="form-control" id="inputGroupFile02">
                        <label class="input-group-text" for="inputGroupFile02">Subir</label>
                    </div>
                </div>
            </div>
            <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/js/bootstrap.bundle.min.js"></script>
        </body>
        </html>
    `);
};
async function enviarnotificacion(ClaveTicket) {
    const mensaje = document.getElementById('mensaje');
    const iduser = document.getElementById('IdUSER').value;

    var json = {
        "data": {
            "bdCc": 5,
            "bdSch": "dbo",
            "bdSp": "SPUPD_CRCNotifyFC"
        },
        "filter": [
            {
                "property": "ClaveTicket ",
                "value": ClaveTicket
            },
            {
                "property": "ClaveUser ",
                "value": iduser
            }
        ]
    };
    try {
        const obj = await consumir(json);  // Ahora sí se puede usar await

        if (obj.status == 200) {
            if (obj.message !== '') {
                notificacion(obj.message, "TUM NOTIFICA");
                location.reload();
            }
        } else {
            mensaje.value = obj.message;
            if (mensaje.value !== '') {
                toastr.error(mensaje.value);
                notificacion(obj.message, "TUM NOTIFICA ERROR");
            }
        }
    } catch (error) {
        console.error("Error al enviar la notificación:", error);
        toastr.error("Error al enviar la notificación.");
    }
}

function notificacion(body, title) {
    if (Notification) {
        if (Notification.permission !== "granted") {
            Notification.requestPermission()
        }

        if (typeof noti2 != 'undefined') {
            noti2.onclose = null;
            noti2.close()
        }

        var extra = {
            icon: "https://webportal.tum.com.mx/wsstest/imag/logo_difuminado.png",
            body: body
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