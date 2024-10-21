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

    $('#FechaBaja').datetimepicker({
        //format: 'm/d/Y'
        format: 'Y/m/d'
    });

    // Define cuántas filas mostrar por página
    const rowsPerPage = 10;
    const table = document.getElementById("TabHeadCount");
    const tbody = table.querySelector("tbody");
    const rows = Array.from(tbody.querySelectorAll("tr"));
    const pagination = document.getElementById("pagination");
    // Función para mostrar las filas de la página actual
    function displayPage(page) {
        const start = (page - 1) * rowsPerPage;
        const end = start + rowsPerPage;

        // Ocultar todas las filas
        rows.forEach((row, index) => {
            row.style.display = (index >= start && index < end) ? "" : "none";
        });

        // Actualiza los botones de paginación
        updatePagination(page);
    }
    // Función para crear los botones de paginación
    function updatePagination(currentPage) {
        pagination.innerHTML = ""; // Limpiar contenido anterior
        const totalPages = Math.ceil(rows.length / rowsPerPage);

        // Botón de "Anterior"
        const prevButton = document.createElement("button");
        prevButton.textContent = "Anterior";
        prevButton.classList.add("btn", "btn-primary", "me-2");
        prevButton.disabled = currentPage === 1;
        prevButton.addEventListener("click", () => displayPage(currentPage - 1));
        pagination.appendChild(prevButton);

        // Botones de número de página
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

function clic(ClaveHeadCount,idus) {
    var check = document.getElementById('checkSustituible-' + ClaveHeadCount);
    //console.log(check.checked);
    var sus = check.checked;

    var url = new URL('https://webportal.tum.com.mx/wsstmdv/api/execspxor');
    var myHeaders = new Headers();
    myHeaders.append("Content-Type", "application/json");

    var raw = JSON.stringify({
        "data": {
            "bdCc": 6,
            "bdSch": "dbo",
            "bdSp": "SPUPD_HC_Sustituible"
        },
        "filter": [
            {
                "property": "claveHeadCount",
                "value": ClaveHeadCount
            },
            {
                "property": "Sustituible",
                "value": sus
            },
            {
                "property": "ClaveUsuario",
                "value": idus
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
            else if (obj.status == 200) {
                var guarda = document.getElementById('guarda').value;
                guarda = obj.success + ' , ' + obj.status + ' , ' + obj.message;
                if (guarda !== '') {
                    toastr.success(guarda);
                }
            }
            else {
                var mensaje = document.getElementById('mensaje').value;
                mensaje = obj;
                if (mensaje !== '') {
                    toastr.error(mensaje);
                }
            }
        })
        .catch(error => console.log("error", error));
}
function regresar(page) {
    const rowsPerPage = 10;
    const table = document.getElementById("TabHeadCount");
    const tbody = table.querySelector("tbody");
    const rows = Array.from(tbody.querySelectorAll("tr"));
    const pagination = document.getElementById("pagination");
    // Función para mostrar las filas de la página actual

    const start = (page - 1) * rowsPerPage;
    const end = start + rowsPerPage;

    // Ocultar todas las filas
    rows.forEach((row, index) => {
        row.style.display = (index >= start && index < end) ? "" : "none";
    });

    // Actualiza los botones de paginación
    updatePagination(page);
}

function filterBusca() {
    var input = document.getElementById("searchInput");
    var filter = input.value.toUpperCase();

    if (input.value == "") {
        regresar(1);
    }

    var table = document.getElementById("TabHeadCount");
    var tr = table.getElementsByTagName("tr");

    for (var i = 1; i < tr.length; i += 2) {  // i+=2 ya que tienes un tr seguido por un tr con detalles
        var tdArray = tr[i].getElementsByTagName("td");
        var rowMatch = false;

        // Verificar si alguna columna coincide con la búsqueda
        for (var j = 0; j < tdArray.length; j++) {
            var td = tdArray[j];
            if (td) {
                if (td.innerHTML.toUpperCase().indexOf(filter) > -1) {
                    rowMatch = true;
                    break;
                }
            }
        }

        // Verificar los datos ocultos en el div con id `datos-@cabeza.ClaveHeadCount`
        var clavehead = tr[i].querySelector('button').getAttribute('onclick').match(/'(\d+)'/)[1];
        var datosDiv = document.getElementById('datos-' + clavehead);
        if (datosDiv && datosDiv.innerText.toUpperCase().indexOf(filter) > -1) {
            rowMatch = true;
        }

        // Mostrar u ocultar las filas basadas en la coincidencia
        if (rowMatch) {
            tr[i].style.display = "";  // Mostrar la fila con los datos
            tr[i + 1].style.display = "";  // Mostrar la fila de detalles
        } else {
            tr[i].style.display = "none";  // Ocultar la fila con los datos
            tr[i + 1].style.display = "none";  // Ocultar la fila de detalles
        }
    }

}
function openModalBaja(claveHeadCount, ClaveEmpleado) {
    $('#editClaveHeadCountBaja').val(claveHeadCount);
    $('#editClaveEmpleado').val(ClaveEmpleado);
    llenaCombobaja();
    $('#editModalBaja').modal('show');
}
function openModalProm(claveHeadCount, ClaveEmpleado) {
    $('#editClaveHeadCountPromo').val(claveHeadCount);
    $('#editClaveEmpleadoProm').val(ClaveEmpleado);
    llenarComboPromo();
    $('#editModalPromo').modal('show');
}

function llenarComboPromo() {
    var url = new URL('https://webportal.tum.com.mx/wsstmdv/api/execspxor');
    var myHeaders = new Headers();
    myHeaders.append("Content-Type", "application/json");

    var raw = JSON.stringify({
        "data": {
            "bdCc": 6,
            "bdSch": "dbo",
            "bdSp": "SPQRY_Puestos"
        },
        "filter": []
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
            else if (obj.data.length != 0) {
                var options = "";
                const pues = obj.data[0].Puestos;
                for (let p of pues) {
                    options += '<option value="' + p.cvepuesto + '">' + p.DescPuesto + '</option>';
                }
                $("#selectPromo").html(options);
            }
            else {

            }
        })
        .catch(error => console.log("error", error));
}
function llenaCombobaja() {

    var url = new URL('https://webportal.tum.com.mx/wsstmdv/api/execspxor');
    var myHeaders = new Headers();
    myHeaders.append("Content-Type", "application/json");

    var raw = JSON.stringify({
        "data": {
            "bdCc": 6,
            "bdSch": "dbo",
            "bdSp": "SPQRY_CatCausaBaja"
        },
        "filter": []
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
            else if (obj.data.length != 0) {
                var options = "";
                const causa = obj.data[0].CausaBaja;
                for (let c of causa) {
                    options += '<option value="' + c.ClaveCausaBaja + '">' + c.Descripcion + '</option>';
                }
                $("#idCausa").html(options);
            }
            else {

            }
        })
        .catch(error => console.log("error", error));
}

function mostrar(clavehead) {
    var dat = document.getElementById('datos-' + clavehead);
    if (dat.style.display === "none") {
        dat.style.display = "block";
    } else {
        dat.style.display = "none";
    }
}

