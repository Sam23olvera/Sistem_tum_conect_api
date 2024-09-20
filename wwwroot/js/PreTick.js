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
    $('#FechCreTick').datetimepicker({
        //format: 'm/d/Y'
        format: 'Y/m/d'
    });
});
var selectedPretickets = [];
function checkpretick(ClavePreTicket) {
    
    var checkpre = document.getElementById('checkpre-' + ClavePreTicket)
    var SelClvTick = document.getElementById('SelClvTick');
    var preticketDetails = document.getElementById('preticketDetails-' + ClavePreTicket);
    checkpre.addEventListener('change', function () {
        if (checkpre.checked == true) {
            preticketDetails.style.display = "block";
            checkpre.checked;
            if (!selectedPretickets.includes(ClavePreTicket)) {
                selectedPretickets.push(ClavePreTicket);
            }
        }
        else if (checkpre.checked == false) {
            preticketDetails.style.display = "none";
            selectedPretickets = selectedPretickets.filter(item => item !== ClavePreTicket);
        }
        console.log(selectedPretickets);
        SelClvTick.value = selectedPretickets;
    });
}

function checkall() {
    var isChecked = $('#selectAll').is(':checked');
    var SelClvTick = document.getElementById('SelClvTick');
    $('.preticket-checkbox').each(function () {
        var ClavePreTicket = $(this).attr('id').split('-')[1]; 

        if (isChecked) {
            $(this).prop('checked', true);
            $('#preticketDetails-' + ClavePreTicket).css('display', 'block');

            if (!selectedPretickets.includes(ClavePreTicket)) {
                selectedPretickets.push(ClavePreTicket);
            }
        } else {
            $(this).prop('checked', false);
            $('#preticketDetails-' + ClavePreTicket).css('display', 'none');
            
            selectedPretickets = [];
        }
    });
    console.log(selectedPretickets);
    SelClvTick.value = selectedPretickets;
}

function filterBusca() {
    var input, filter, table, tr, td, i, txtValue, cards, cardText;
    input = document.getElementById("searchInput");
    filter = input.value.toUpperCase();

    // Filtrar en la tabla
    table = document.getElementById("TablePretickets");
    tr = table.getElementsByTagName("tr");

    for (i = 1; i < tr.length; i++) {  // Empieza en 1 para ignorar el encabezado
        td = tr[i].getElementsByTagName("td")[1]; // Asume que la columna 1 tiene el # Preticket
        if (td) {
            txtValue = td.textContent || td.innerText;
            if (txtValue.toUpperCase().indexOf(filter) > -1) {
                tr[i].style.display = ""; // Mostrar fila
            } else {
                tr[i].style.display = "none"; // Ocultar fila
            }
        }
    }

    // Filtrar en las tarjetas
    cards = document.getElementsByClassName("cartas");

    for (i = 0; i < cards.length; i++) {
        cardText = cards[i].getElementsByClassName("card-header")[0].innerText;
        if (cardText.toUpperCase().indexOf(filter) > -1) {
            cards[i].style.display = ""; // Mostrar tarjeta
        } else {
            cards[i].style.display = "none"; // Ocultar tarjeta
        }
    }
}

