window.onload = function () {
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

    const rowsPerPage = 10;
    const table = document.getElementById("myTable");
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
document.getElementById('btnExcel').addEventListener('click', function (e) {
    e.preventDefault();
    var form = this.closest('form');

    document.getElementById("spinner-overlay").style.display = "block";

    fetch(form.action, {
        method: form.method,
        body: new URLSearchParams(new FormData(form))
    })
        .then(response => response.blob())
        .then(blob => {
            var link = document.createElement('a');
            link.href = window.URL.createObjectURL(blob);
            const d = new Date();
            var day = ("0" + d.getDate()).slice(-2) + '-' + ("0" + (d.getMonth() + 1)).slice(-2) + '-' + d.getFullYear() + ' ' + ("0" + d.getHours()).slice(-2) + ':' + ("0" + d.getMinutes()).slice(-2);
            link.download = 'Expo-' + day + '.xlsx';
            link.click();
            document.getElementById("spinner-overlay").style.display = "none";
        })
        .catch(error => {
            console.error('Error al descargar el archivo Excel:', error);
            document.getElementById("spinner-overlay").style.display = "none";
        });
});



$(document).ready(function () {
    $('#FehInicio').datetimepicker({
        format: 'Y/m/d'
    });
    $('#FehFin').datetimepicker({
        format: 'Y/m/d',
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
});
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
    var ClaveUNE = document.getElementById('ClaveUnidadNegocio');
    var cvtp = document.getElementById('cvtp');
    if (ClaveUNE.value != '') {
        llenar(ClaveUNE.value);
    }

    ClaveUNE.addEventListener('change', function () {
        if (ClaveUNE.value != '') {
            cvtp.value = "";
            llenar(ClaveUNE.value);
        }
    });
});

function filterBusca() {
    var input = document.getElementById("searchInput");
    var filter = input.value.toUpperCase();
    var cards = document.getElementById("cards");

    var table = document.getElementById("myTable");
    var tr = table.getElementsByTagName("tr");

    for (var i = 1; i < tr.length; i++) {
        var tdArray = tr[i].getElementsByTagName("td");
        var rowMatch = false;

        for (var j = 0; j < tdArray.length; j++) {
            var td = tdArray[j];
            if (td) {
                if (td.innerHTML.toUpperCase().indexOf(filter) > -1) {
                    rowMatch = true;
                    break;
                }
            }
        }

        if (rowMatch) {
            tr[i].style.display = "";
        } else {
            tr[i].style.display = "none";
        }
    }
}

function filterTable(columnIndex) {
    var input = document.getElementById("filter" + getColumnName(columnIndex));
    var filter = input.value.toUpperCase();
    var table = document.getElementById("myTable");
    var tr = table.getElementsByTagName("tr");

    for (var i = 1; i < tr.length; i++) {
        var td = tr[i].getElementsByTagName("td")[columnIndex];
        if (td) {
            var cellText = td.textContent || td.innerText;
            if (cellText.toUpperCase().indexOf(filter) > -1) {
                tr[i].style.display = "";
            } else {
                tr[i].style.display = "none";
            }
        }
    }
}
function filterEstatusTable(columnIndex) {
    var sele = document.getElementById("filterEstatus");
    var filter = sele.options[sele.selectedIndex].text;
    if (filter == '[Seleccionar]') {
        filter = '';
    }
    var table = document.getElementById("myTable");
    var tr = table.getElementsByTagName("tr");

    for (var i = 1; i < tr.length; i++) {
        var td = tr[i].getElementsByTagName("td")[columnIndex];
        if (td) {
            var cellText = td.textContent || td.innerText;
            if (cellText.indexOf(filter) > -1) {
                tr[i].style.display = "";
            } else {
                tr[i].style.display = "none";
            }
        }
    }
}
function getColumnName(index) {
    var columnNames = ["UNE", "TipoOperacion", "NumTicket", "Estatus"];
    return columnNames[index];
}
function llenar(ClaveUNE) {
    var url = new URL('https://webportal.tum.com.mx/wsstmdv/api/execspxor');
    var myHeaders = new Headers();
    myHeaders.append("Content-Type", "application/json");

    var raw = JSON.stringify({
        "data": {
            "bdCc": 5,
            "bdSch": "dbo",
            "bdSp": "SPQRY_CatalogoTipoOpxUNE"
        },
        "filter": [
            {
                "property": "ClaveUNE",
                "value": ClaveUNE
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
            var select = document.getElementById("ClaveTipoOperacion");
            select.innerHTML = "";

            var defaultOption = document.createElement("option");
            defaultOption.value = "0";
            defaultOption.textContent = "[Selecciona]";
            select.appendChild(defaultOption);

            if (obj.data == null) {
                toastr.error("Mensaje en null");
            } else {
                for (var i = 0; i < obj.data[0].TBCAT_TipoOp.length; i++) {
                    console.log(obj.data[0].TBCAT_TipoOp[i].ClaveTipoOperacion);
                    var option = document.createElement("option");
                    option.value = obj.data[0].TBCAT_TipoOp[i].ClaveTipoOperacion;
                    option.textContent = obj.data[0].TBCAT_TipoOp[i].Descripcion;
                    select.appendChild(option);
                }
                var cvtp = document.getElementById("cvtp");
                if (cvtp.value == "") {
                    select.value = "0";
                }
                else {
                    select.value = cvtp.value;
                }
            }
        })
        .catch(error => console.log("error", error));
}

function Btnexcel() {
    var excel = document.getElementById('excel');
    excel.value = "true";
}
function BtnConsultar() {
    var excel = document.getElementById('excel');
    excel.value = "false";
}
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
