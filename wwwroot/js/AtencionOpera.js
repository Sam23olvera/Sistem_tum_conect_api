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


    const rowsPerPage = 10;
    const table = document.getElementById("tablaBuscar");
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

$(document).ready(function () {
    $('#FechaInicio').datetimepicker({
        //format: 'm/d/Y'
        format: 'Y/m/d H:i'
    });
    $('#FechaFin').datetimepicker({
        //format: 'm/d/Y'
        format: 'Y/m/d H:i'
    });
    $('#FechaReporte').datetimepicker({
        format: 'Y/m/d H:i'
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
    var alias = document.getElementById('selAli');
    var selunidad = document.getElementById('ClaveUnidad');

    selunidad.addEventListener('change', function () {
        alias.value = selunidad.value;
    });
    alias.addEventListener('change', function () {
        selunidad.value = alias.value;
    });

});

function mostrarselect() {
    var tiptick = document.getElementById('ClaveTipoTicket').value;
    var unidad1 = document.getElementById('unidad1');
    var unidad2 = document.getElementById('unidad2');
    var dolly = document.getElementById('dolly');
    var remolque = document.getElementById('remolque');
    if (tiptick == 0) {
        dolly.style.display = "none";
        unidad1.style.displayc = "none";
        unidad2.style.displayc = "none";
        remolque.style.display = "none";
    }
    else if (tiptick == 1) {
        unidad1.style.display = "block";
        unidad2.style.display = "block";
        dolly.style.display = "none";
        remolque.style.display = "none";
    }
    else if (tiptick == 2) {
        unidad1.style.display = "none";
        unidad2.style.display = "none";
        dolly.style.display = "none";
        remolque.style.display = "block";
    }
    else if (tiptick == 3) {
        unidad1.style.display = "none";
        unidad2.style.display = "none";
        dolly.style.display = "block";
        remolque.style.display = "none";
    }
}
