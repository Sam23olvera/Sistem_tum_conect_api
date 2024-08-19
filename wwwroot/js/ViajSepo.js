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

var ventana;

//Ejemplo de función para abrir una ventana en el navegador con un tamaño personalizado
//En este caso se abrirá una ventana en blanco
function abrirVentana() {
	if (!ventanaEmergenteAbierta()) {
		ventana = window.open("", "",
			"menubar=no, scrollbars=no, width=400, height=350");
		aviso("Ventana emergente abierta con tamaño 400x350.");
	}
	else {
		aviso("La ventana emergente ya está abierta. Ciérrela si quiere volver a abrirla.");
	}
}

//Imprime la página actual
function imprimirPagina() {
	window.print();
	aviso("Ha enviado a imprimir la página principal.");
}

//MOdificar el tamaño de la ventana
function modificarVentana() {
	if (ventanaEmergenteAbierta()) {
		ventana.resizeBy(640, 480);
		ventana.focus();
		aviso("Ha modificado el tamaño de la ventana emergente a 640x480.");
	}
}

//Muestra datos en la ventana creada
//Por ejemplo el tamaño (ancho y alto), el path, la url y el idioma del navegador
function mostrarDatosEnVentana() {
	if (ventanaEmergenteAbierta()) {
		var h = document.getElementById('Texto').value;
		ventana.document.write(''
			+ '<style> body { background-color: powderblue; } h1 { color: blue; } p { color: red; } prueba { background-color: blue; color: white; } </style>'
			+ '<body> '
			+ h + '<p style=\"color: blue;\">este es un dato del servidor </p>'
			+ '<a class=\"prueba\">Inicio</a>'
		+ '<p>Tamaño de la ventana: ' + screen.width + ' x ' + screen.height + '</p>'
		+ '<p>El Path de la página es: ' + window.location.pathname + '</p>'
		+ '<p>La URL de la página es: ' + window.location.href + '</p> '
		+ '<p>El idioma del navegador es: ' + navigator.language + '</p>'
		+ ' <p>Navegador: ' + navigator.userAgent + '</p> '
		+ ' <p id=\"textoModificar\">Ejemplo para modificar texto</p> '
		+ ' <button class= "btn btn-primary" onclick =\"{ window.close(); } \">Cerrar ventana</button> '
		+ ' </body>');
		aviso("Mostrados los datos en la ventana emergente.");
	}
}

//Cierra la ventana emergente
function cerrarVentana() {
	if (ventanaEmergenteAbierta()) {
		ventana.close();
		aviso("Ha cerrado la ventana emergente.");
	}
}

//Comprueba si se ha abierto la ventana emergente
//Devuelve true si se ha habierto, false si no se ha abierto
function ventanaEmergenteAbierta() {
	if ((typeof ventana === 'undefined') || (ventana.closed)) {
		aviso("No ha abierto la ventana.");
		return false;
	}
	else {
		return true;
	}
}

//Muestra un aviso en la página principal, modificando el objeto p con id "aviso"
function aviso(texto) {
	document.getElementById("aviso").innerHTML = "<b>" + texto + "</b>";
}

function modificarTextoVentanaSegunSelectorID() {
	if (ventanaEmergenteAbierta()) {
		ventana.document.getElementById('textoModificar').innerHTML =
			"Has pulsado en el botón 'Modificar datos en ventana' de la página principal";
	}
}


