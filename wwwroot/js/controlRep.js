﻿window.onload = function () {
    document.getElementById("spinner-overlay").style.display = "none";
};

document.addEventListener("DOMContentLoaded", function () {
    var links = document.querySelectorAll(".carga");
    //var pagin = document.querySelectorAll(".page-link");
    links.forEach(function (link) {
        link.addEventListener("click", function () {
            document.getElementById("spinner-overlay").style.display = "block";
        });
    });

    //pagin.forEach(function (link) {
    //    link.addEventListener("click", function () {
    //        document.getElementById("spinner-overlay").style.display = "block";
    //    });
    //});

    var highlightRowId = localStorage.getItem('highlightRowId');
    if (highlightRowId) {
        var renglor = document.getElementById('renglon-'+highlightRowId);
        if (renglor) {
            renglor.classList.add('ResaltaRenglon');
            setTimeout(function () {
                renglor.classList.remove('ResaltaRenglon');
                localStorage.removeItem('highlightRowId');
            }, 5000);
        }
    }
    
    const rowsPerPage = 10;
    const table = document.getElementById("TabDatos");
    const tbody = table.querySelector("tbody");
    const rows = Array.from(tbody.querySelectorAll("tr"));
    const pagination = document.getElementById("pagination");

    function displayPage(page) {
        const start = (page - 1) * rowsPerPage;
        const end = start + rowsPerPage;

        rows.forEach((row, index) => {
            row.style.display = (index >= start && index < end) ? "" : "none";
        });

        updatePagination(page);
    }
    function updatePagination(currentPage) {
        pagination.innerHTML = ""; // Limpiar contenido anterior
        const totalPages = Math.ceil(rows.length / rowsPerPage);


        const prevButton = document.createElement("button");
        prevButton.textContent = "Anterior";
        prevButton.classList.add("btn", "btn-primary", "me-2");
        prevButton.disabled = currentPage === 1;
        prevButton.addEventListener("click", () => displayPage(currentPage - 1));
        pagination.appendChild(prevButton);

        for (let i = 1; i <= totalPages; i++) {
            const pageButton = document.createElement("button");
            pageButton.textContent = i;
            pageButton.classList.add("btn", "btn-secondary", "me-2");
            if (i === currentPage) {
                pageButton.classList.add("active");
            }
            pageButton.addEventListener("click", () => displayPage(i));
            pagination.appendChild(pageButton);
        }

        // Botón de "Siguiente"
        const nextButton = document.createElement("button");
        nextButton.textContent = "Siguiente";
        nextButton.classList.add("btn", "btn-primary", "me-2");
        nextButton.disabled = currentPage === totalPages;
        nextButton.addEventListener("click", () => displayPage(currentPage + 1));
        pagination.appendChild(nextButton);
    }

    // Inicializar la primera página
    displayPage(1);

});



//$(document).ready(function () {
//    var showModal = document.getElementById('showModal').value;
//    if (showModal === 'True') {
//        $('#myModal').modal('show'); // Mostrar el modal
//    }
//    else if (showModal === 'false') {
//        $('#myModal').hide();
//    }
//});

$(document).ready(function () {
    var ctx = $('#barChart').get(0).getContext('2d');

    function calculateTotal(estaId, estaUniId, totalId) {
        var estaValue = parseInt($(estaId).val()) || 0;
        var estaUniValue = parseInt($(estaUniId).val()) || 0;
        var totalValue = estaValue + estaUniValue;
        $(totalId).val(totalValue);
    }

    calculateTotal('#Esta-1', '#Esta-uni-1', '#Esta-Total-1');
    calculateTotal('#Esta-2', '#Esta-uni-2', '#Esta-Total-2');
    calculateTotal('#Esta-3', '#Esta-uni-3', '#Esta-Total-3');
    calculateTotal('#Esta-4', '#Esta-uni-4', '#Esta-Total-4');
    calculateTotal('#Esta-5', '#Esta-uni-5', '#Esta-Total-5');

    var barChart = new Chart(ctx, {
        type: 'polarArea',
        data: {
            labels: [
                $('#Name-Esta-1').val(),
                $('#Name-Esta-2').val(),
                $('#Name-Esta-3').val(),
                $('#Name-Esta-4').val(),
                $('#Name-Esta-5').val()
            ],
            datasets: [{
                label: 'Reporte Mensual',
                data: [
                    parseInt($('#Esta-Total-1').val()),
                    parseInt($('#Esta-Total-2').val()),
                    parseInt($('#Esta-Total-3').val()),
                    parseInt($('#Esta-Total-4').val()),
                    parseInt($('#Esta-Total-5').val())
                ],
                backgroundColor: [
                    'rgb(255, 99, 132)',
                    'rgb(75, 192, 192)',
                    'rgb(255, 205, 86)',
                    'rgb(201, 203, 207)',
                    'rgb(54, 162, 235)'
                ]
            }]
        },
        options: {
            scales: {
                yAxes: [{
                    ticks: {
                        beginAtZero: true
                    }
                }]
            }
        }
    });
});

$(document).ready(function () {
    $('#FehTick').datetimepicker({
        //format: 'm/d/Y'
        format: 'Y/m/d H:i'
    });
    $('#TiempAsig').datetimepicker({
        format: 'Y/m/d H:i'
    });
    $('#datetimepicker').datetimepicker({
        format: 'm/d/Y H:i'
    });
    $('#FechEstima').datetimepicker({
        format: 'm/d/Y H:i'
    });
    $('#FehFin').datetimepicker({
        format: 'Y/m/d H:i',
        timepicker: false,
        maxDate: new Date(),
        onClose: function (selectedDate) {
            var endDate = new Date(selectedDate);
            endDate.setDate(endDate.getDate() - 31);

            $('#FehInicio').datetimepicker('setOptions', {
                maxDate: endDate
            });

            var formattedDate = endDate.getFullYear() + '/' +
                ('0' + (endDate.getMonth() + 1)).slice(-2) + '/' +
                ('0' + endDate.getDate()).slice(-2);

            $('#FehInicio').val(formattedDate);
        }
    });

    //$('#FehInicio').datetimepicker({
    //    format: 'Y/m/d',
    //    timepicker: false,
    //    onShow: function () {
    //        $(this).datetimepicker('hide');
    //    }
    //});
});

function mostrarModalOrdenes(cveEmp, NumTicket, TipoEquipo, idus, ClaveTipoTicket, ClaveEquipo) {
    document.getElementById('modalTitle').innerText = `Relacion de Ordenes con Tikcet - ${NumTicket}`;
    document.getElementById('TpEqui').innerText = `Tipo de Equipo: ${TipoEquipo}`;

    LLeModOrd(cveEmp, NumTicket, idus, ClaveTipoTicket, ClaveEquipo);

    $('#myModal').modal('show');
}

function LLeModOrd(cveEmp, NumTicket, idus, ClaveTipoTicket, ClaveEquipo) {

    var url = new URL('https://webportal.tum.com.mx/wsstmdv/api/execspxor');
    var myHeaders = new Headers();
    myHeaders.append("Content-Type", "application/json");

    var raw = JSON.stringify({
        "data": {
            "bdCc": 5,
            "bdSch": "dbo",
            "bdSp": "SPQRY_OrdenesMtto"
        },
        "filter": [
            {
                "property": "ClaveEmpresa",
                "value": cveEmp
            },
            {
                "property": "ClaveTipoTicket",
                "value": ClaveTipoTicket
            },
            {
                "property": "ClaveTicket",
                "value": NumTicket
            },
            {
                "property": "ClaveEquipo",
                "value": ClaveEquipo
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
                $("#cuerpo").html("");
                document.getElementById('btnGuarOrd').disabled = true;
            }
            else if (obj.data.length == 0) {
                $("#cuerpo").html("");
                var mensaje = document.getElementById('mensaje').value;
                mensaje = 'No se encuentran Ordenes';
                if (mensaje !== '') {
                    toastr.error(mensaje);
                    document.getElementById('btnGuarOrd').disabled = true;
                }
            }
            else {
                $("#cuerpo").html("");
                document.getElementById('btnGuarOrd').disabled = false;
                for (let i = 0; i < obj.data[0]["OrdenesMtto"].length; i++) {
                    //console.log(obj.data[0]["OrdenesMtto"][i]["OrdenMtto"]);
                    const orden = obj.data[0]["OrdenesMtto"][i];
                    // Establece el estado del checkbox basado en "Seleccionar"
                    let isChecked = orden["Seleccionar"] ? ' checked' : '';
                    var tr = '<tr> '
                        + ' <td>' + obj.data[0]["OrdenesMtto"][i]["OrdenMtto"] + '</td>'
                        + ' <td>' + obj.data[0]["OrdenesMtto"][i]["ComentariosOrden"] + '</td>'
                        + ' <td>' + obj.data[0]["OrdenesMtto"][i]["Costo"] + '</td>'
                        + ' <td>' + obj.data[0]["OrdenesMtto"][i]["Equipo"] + '</td>'
                        + ' <td>' + obj.data[0]["OrdenesMtto"][i]["Proveedor"] + '</td>'
                        + ' <td>'
                        + '<input class=\"form-check-input\" type=\"checkbox\" id="chSelOrder" name="chSelOrder"' + isChecked + ' /> </td>'
                        + '<input type="hidden" value="' + NumTicket +'" id="NumTicketModOr" name="NumTicketModOr" />'
                        + '<input type="hidden" value="' + idus +'" id="idusModOr" name="idusModOr" />'
                        + '</tr>';
                    $("#cuerpo").append(tr)
                }
            }
        })
        .catch(error => console.log("error", error));
}

function GuardarOrden(cveEmp)
{
    const checkboxes = document.querySelectorAll('#cuerpo .form-check-input');
    let osTicketArray = [];

    var nutick = document.getElementById('NumTicketModOr').value;
    var idusModOr = document.getElementById('idusModOr').value;

    checkboxes.forEach((checkbox, index) => {
        if (checkbox.checked) {
            let row = checkbox.closest('tr');
            let orderID = row.querySelector('td:first-child').innerText;

            osTicketArray.push(
                {
                    "ClaveTicket": nutick,
                    "ClaveOrden": orderID,
                    "ClaveUsuario": idusModOr,
                    "Vinculado": true,
                    "CveEmpresa": cveEmp
                });
            // Guardar el ID del renglón para resaltarlo después
            localStorage.setItem('highlightRowId', nutick);
        }
        else
        {
            let row = checkbox.closest('tr');
            let orderID = row.querySelector('td:first-child').innerText;

            osTicketArray.push(
                {
                    "ClaveTicket": nutick,
                    "ClaveOrden": orderID,
                    "ClaveUsuario": idusModOr,
                    "Vinculado": false,
                    "CveEmpresa": cveEmp
                });
            localStorage.setItem('highlightRowId', nutick);
        }
    });
    var rawGuardar = JSON.stringify({ "OS_Ticket": osTicketArray });
    var enviojsonGuarda = JSON.stringify({
        "data": {
            "bdCc": 5,
            "bdSch": "dbo",
            "bdSp": "SPINS_RelTicketOrdMtto"
        },
        "filter": [
            {
                "property": "OrdenesRel",
                "value": rawGuardar.trim()
            }
        ]
    });
    GuardOrd(enviojsonGuarda);
    //setTimeout(function () { renglor.classList.remove('ResaltaRenglon'); }, 2000);
    //location.reload();    
    //var renglor = document.getElementById('renglon-' + nutick);
    //renglor.classList.add('ResaltaRenglon');
}
function GuardOrd(jsonGuarda) {

    var url = new URL('https://webportal.tum.com.mx/wsstmdv/api/execspxor');
    var myHeaders = new Headers();
    myHeaders.append("Content-Type", "application/json");
    var requestOptions = {
        method: "POST",
        headers: myHeaders,
        body: jsonGuarda,
        redirect: "follow"
    };

    fetch(url, requestOptions)
        .then(response => response.text())
        .then(result => {
            const obj = JSON.parse(result);
            //console.log(obj);
            if (obj.data == null || obj.data == 0) {
                var guarda = document.getElementById('guarda').value;
                guarda = obj.message;
                if (guarda !== '') {
                    toastr.success(guarda);
                }
                location.reload();
            }
        })
        .catch(error => console.log("error", error));
}

function cal(numTicket) {
    var Inp = "FechEstima-" + numTicket;
    var expand = document.getElementById(Inp);
    $(expand).datetimepicker({
        format: 'Y/m/d H:i'
    });
}
function calendario(numTicket) {
    var Tiem = "TiempAsig-" + numTicket;
    var TiempAsig = document.getElementById(Tiem);
    $(TiempAsig).datetimepicker({
        format: 'Y/m/d H:i'
    });
}
function caleEta(numTicket) {
    var Etafech = "Eta-" + numTicket;
    var Etafecha = document.getElementById(Etafech);
    $(Etafecha).datetimepicker({
        format: 'Y/m/d H:i'
    });
}

function llenarDiesel(numTick) {
    var nameChkDisel = 'ChkDisel-' + numTick;
    var txtnameCheckDisel = 'CheckDisel-' + numTick;
    var ChkDisel = document.getElementById(nameChkDisel);
    var txtCheckDisel = document.getElementById(txtnameCheckDisel);
    txtCheckDisel.value = ChkDisel.value;

    if (ChkDisel.checked == true) {
        txtCheckDisel.value = 1;
    }
    else if (ChkDisel.checked == false) {
        txtCheckDisel.value = 0;
    }
}

function llenarGrua(numTick) {
    var nameChkGrua = 'ChkGrua-' + numTick;
    var txtnameCheckGrua = 'CheckGrua-' + numTick;
    var ChkGrua = document.getElementById(nameChkGrua);
    var txtCheckGrua = document.getElementById(txtnameCheckGrua);
    if (ChkGrua.checked == true) {
        txtCheckGrua.value = 1;
    }
    else if (ChkGrua.checked == false) {
        txtCheckGrua.value = 0;
    }
}

function mostrarModal(numTicket) {
    var modal = document.getElementById('evidenciasModal-' + numTicket);
    $(modal).modal('show');
}
function CloseModal(numTicket) {
    var modal = document.getElementById('evidenciasModal-' + numTicket);
    $(modal).modal('hide');
}
$(document).ready(function () {
    $(".owl-carousel").owlCarousel({
        items: 1,
        merge: true,
        loop: true,
        margin: 10,
        autoplay: true,
        autoplayTimeout: 5000,
        autoplayHoverPause: true,
        center: true,
        video: true
    });
});

$(document).ready(function () {
    $('input[type="file"]').change(function () {
        const ticket = $(this).attr('id').split('-')[1];
        const carousel = $('#carousel-' + ticket);
        carousel.empty(); // Vacía el carousel antes de agregar nuevos elementos
        const filesArray = Array.from($(this)[0].files);
        filesArray.forEach((file) => {
            const reader = new FileReader();
            reader.readAsDataURL(file);
            reader.onload = (event) => {
                const src = event.target.result;
                // Verifica si es un video o una imagen
                if (file.type.startsWith('image')) {
                    carousel.append('<div class="item"><img src="' + src + '" alt="Imagen" style = "width: 40%; height: 80%; margin-right: 10px; margin-bottom: 10px;"></div>');
                } else if (file.type.startsWith('video')) {
                    carousel.append('<div class="item-video"><video autoplay controls style = "width: 40%; height: 40%; margin-right: 10px; margin-bottom: 10px;" ><source src="' + src + '" type="' + file.type + '">Tu navegador no soporta el elemento de video.</video></div>');
                }
            };
        });
        carousel.owlCarousel({
            items: 1,
            loop: true,
            margin: 10,
            autoplay: true,
            autoplayTimeout: 5000,
            autoplayHoverPause: true,
            center: true,
            video: true,
            responsive: {
                0: {
                    items: 1
                },
                600: {
                    items: 2
                },
                1000: {
                    items: 3
                }
            }
        });
    });
});

$(document).ready(function () {
    var checkIni = document.getElementById('checkIni');
    var checkIniValue = document.getElementById('checkIniValue').value;
    var checkFin = document.getElementById('checkFin');
    var checkFinValue = document.getElementById('checkFinValue').value;

    if (checkIniValue === 'true') {
        checkIni.checked = true;
    }
    else {
        checkIni.checked = false;
    }
    if (checkFinValue === 'true') {
        checkFin.checked = true;
    }
    else {
        checkFin.checked = false;
    }

});
function mostrarllantitas(numTicket) {
    var selectReclas_Asigna = "Reclas_Asigna-" + numTicket
    var Reclas_Asigna = document.getElementById(selectReclas_Asigna);
    var mostTab = '.llantitas-' + numTicket;

    if (Reclas_Asigna.value == 2) {
        $(mostTab).show();
        $('.headllantitas').show();
    }
    else {
        $(mostTab).hide();
        $('.headllantitas').hide();
    }
}

function asignarTicket(numTicket) {
    var selectId = "select-" + numTicket;
    var selectElement = document.getElementById(selectId);
    var valorSeleccionado = selectElement.value;
    var botonAsigna = document.getElementById("asigna-" + numTicket);
    var nuevaURL = botonAsigna.getAttribute("href").replace("asp-route-Asigna=", "asp-route-Asigna=" + valorSeleccionado);
    // Asignar la nueva URL al enlace del botón
    botonAsigna.setAttribute("href", nuevaURL);
}
function pinta() {

    var MenIni = document.getElementById('MenIni');
    var MenFal = document.getElementById('MenFal');
    var MenPorAsig = document.getElementById('MenPorAsig');
    var MenAsig = document.getElementById('MenAsig');
    var MenTipApoyo = document.getElementById('MenTipApoyo');
    var MenRep = document.getElementById('MenRep');
    var MenFin = document.getElementById('MenFin');
}

function cambio(numTicket) {
    var ch = "ChekAttPar-" + numTicket;
    var atp = "AttPar-" + numTicket;
    var ChekAttPar = document.getElementById(ch);
    var AttPar = document.getElementById(atp);
    ChekAttPar.addEventListener('change', function () {
        if (this.checked) {
            AttPar.value = ChekAttPar.checked;
        }
        else {
            AttPar.value = ChekAttPar.checked;
        }
    });
}
$(document).ready(function () {
    var checkIni = document.getElementById('checkIni');
    var checkIniValue = document.getElementById('checkIniValue');
    var checkFin = document.getElementById('checkFin');
    var checkFinValue = document.getElementById('checkFinValue');
    checkIni.addEventListener('change', function () {
        if (this.checked) {
            checkIniValue.value = true;
            checkFin.checked = false;
            checkFinValue.value = false;
        } else {
            checkIniValue.value = false;
            checkFin.checked = true;
            checkFinValue.value = true;
        }
    });

    checkFin.addEventListener('change', function () {
        if (this.checked) {
            checkFinValue.value = true;
            checkIni.checked = false;
            checkIniValue.value = false;
        } else {
            checkFinValue.value = false;
            checkIni.checked = true;
            checkIniValue.value = true;
        }
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
function mostrar(NumTicket) {
    var dat = document.getElementById('datos-' + NumTicket);
    if (dat.style.display === "none") {
        dat.style.display = "block";
    } else {
        dat.style.display = "none";
    }
}

function Exporta() {
    var Export = document.getElementById('ExportExcel');
    Export.value = "";
    Export.value = true;
}
function Buscar() {
    var Export = document.getElementById('ExportExcel');
    Export.value = "";
    Export.value = false;
}
function finalizar(cveEmp, ClavUsu, ClavCtRep) {
    var selTBCAT_TipoFalla = document.getElementById('selTBCAT_TipoFalla-' + ClavCtRep);
    var obsMat = document.getElementById('obsMat-' + ClavCtRep);
    var mensaje = document.getElementById('mensaje').value;
    var guarda = document.getElementById('guarda').value;
    var DesFalrel = document.getElementById('DesFalrel-' + ClavCtRep);
    var btnFinal = document.getElementById('btnFinal-' + ClavCtRep);
    var FechaFini = document.getElementById('FechaFini-' + ClavCtRep);

    if (selTBCAT_TipoFalla.value == '[Selecciona]') {
        mensaje = 'No selecciono una Falla';
        toastr.error(mensaje);
    }
    else if (obsMat.value === "") {
        mensaje = 'Describa la Falla';
        toastr.error(mensaje);
    }
    else {
        var url = new URL('https://webportal.tum.com.mx/wsstmdv/api/execspxor');
        var myHeaders = new Headers();
        myHeaders.append("Content-Type", "application/json");

        var raw = JSON.stringify({
            "data": {
                "bdCc": 5,
                "bdSch": "dbo",
                "bdSp": "SPUPD_FinRepFalla"
            },
            "filter": [
                {
                    "property": "ClaveEmpresa",
                    "value": cveEmp
                },
                {
                    "property": "ClaveTipFalla",
                    "value": selTBCAT_TipoFalla.value
                },
                {
                    "property": "ObsMantenimiento",
                    "value": obsMat.value
                },
                {
                    "property": "ClaveUser",
                    "value": ClavUsu
                },
                {
                    "property": "NumeroSolicitud",
                    "value": ClavCtRep
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
                    mensaje = obj.message;
                    if (mensaje !== '') {
                        toastr.error(mensaje);
                    }
                }
                else {
                    FechaFini.value = obj.data[0].Respuesta[0].FechaFinalizacion;
                    DesFalrel.disabled = true;
                    selTBCAT_TipoFalla.disabled = true;
                    btnFinal.disabled = true;
                    obsMat.disabled = true;
                    guarda = obj.message;
                    if (guarda !== '') {
                        toastr.success(guarda);
                    }
                }
            })
            .catch(error => console.log("error", error));
    }

}
