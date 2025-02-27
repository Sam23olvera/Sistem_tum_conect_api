window.onload = function () {
    const estadoSeleccionado = document.getElementById("selEstado")?.dataset.selected;
    const municipioSeleccionado = document.getElementById("seleMuni")?.dataset.selected;
    const coloniaSeleccionada = document.getElementById("Colonia")?.dataset.selected;
    const FechNac = document.getElementById("FechNac")?.value;
    const CP = document.getElementById("CP")?.value;


    document.getElementById("loading-overlay").style.display = "none";

    if (estadoSeleccionado && estadoSeleccionado !== "0") {
        llenaMunicipio().then(() => {
            if (municipioSeleccionado && municipioSeleccionado !== "0") {
                document.getElementById("seleMuni").value = municipioSeleccionado;
                llenacolonia().then(() => {
                    if (coloniaSeleccionada && coloniaSeleccionada !== "0") {
                        document.getElementById("Colonia").value = coloniaSeleccionada;
                    }
                });
            }
        });
    }
    if (FechNac !== "0") {
        document.getElementById("loading-overlay").style.display = "none";
        mostrarEdad();
    }
    if (CP !== "") {
        buscarporcp();
    }
};

document.addEventListener("DOMContentLoaded", function () {
    var links = document.querySelectorAll(".carga");
    links.forEach(function (link) {
        link.addEventListener("click", function () {
            document.getElementById("loading-overlay").style.display = "block";
        });
    });
    const mensaje = document.getElementById('mensaje');
    const guarda = document.getElementById('guarda');
    const status = document.getElementById('status');
    const divguard = document.getElementById('divguard');
    const divarchivos = document.getElementById('divarchivos');
    if (mensaje?.value) toastr.error(mensaje.value);
    if (guarda?.value) toastr.success(guarda.value);
    if (status.value == 200) {
        let controles = divguard.querySelectorAll("input, select, textarea, button");
        controles.forEach(control => {
            if (!control.disabled) {
                control.disabled = true;  // Deshabilitar si está habilitado
            }
        });
    }
    if (status.value == 400) {
        let contr = divarchivos.querySelectorAll("checkbox,input ,table,button");
        contr.forEach(control => {
            if (!control.disabled) {
                control.disabled = true;  // Deshabilitar si está habilitado
            }
        });
    }
    const nombreInput = document.getElementById("Nombre");
    const apPaternoInput = document.getElementById("ApPaterno");
    const apMaternoInput = document.getElementById("ApMaterno");
    const fechaNacimientoInput = document.getElementById("FechNac");
    const curpInput = document.getElementById("CURP");
    const rfcInput = document.getElementById("RFC");
    const masculinocheck = document.getElementById("masculino");
    const femeninocheck = document.getElementById("femenino");
    const originario = document.getElementById("originario");
    document.getElementById("ConInfonavit").addEventListener("change", DesmarInfonavit);
    document.getElementById("ConFonacot").addEventListener("change", Fonacot);
    function generarCurprfc() {
        curpInput.value = '';
        rfcInput.value = '';
        const nombre = nombreInput.value.trim().toUpperCase();
        const apPat = apPaternoInput.value.trim().toUpperCase();
        const apMat = apMaternoInput.value.trim().toUpperCase();
        const fechana = fechaNacimientoInput.value;
        //const lug = originario.value;
        const lug = originario.options[originario.selectedIndex].getAttribute("data-entidad");

        let sex = masculinocheck.checked ? masculinocheck.value : femeninocheck.checked ? femeninocheck.value : "";

        if (nombre && apPat && apMat && fechana) {
            const fecha = fechana.split("-");
            const anio = fecha[0].slice(-2);
            const mes = fecha[1];
            const dia = fecha[2];

            function obtenerVocalInterna(palabra) {
                const vocales = "AEIOU";
                for (let i = 1; i < palabra.length; i++) {
                    if (vocales.includes(palabra[i])) {
                        return palabra[i];
                    }
                }
                return "X"; // Si no encuentra vocal, usa "X"
            }
            function obtenerConsonante(palabra) {
                const consonante = "BCDFGHJKLMNÑPQRSTVXZWY";
                for (let i = 1; i < palabra.length; i++) {
                    if (consonante.includes(palabra[i])) {
                        return palabra[i];
                    }
                }
                return "X";
            }

            const curp = `${apPat.charAt(0)}${obtenerVocalInterna(apPat)}${apMat.charAt(0)}${nombre.charAt(0)}${anio}${mes}${dia}${sex}${lug}${obtenerConsonante(apPat)}${obtenerConsonante(apMat)}${obtenerConsonante(nombre)}00`;

            const rfc = `${apPat.charAt(0)}${obtenerVocalInterna(apPat)}${apMat.charAt(0)}${nombre.charAt(0)}${anio}${mes}${dia}XX`;

            curpInput.value = curp;
            rfcInput.value = rfc;
        }
    }
    nombreInput.addEventListener("input", generarCurprfc);
    apPaternoInput.addEventListener("input", generarCurprfc);
    apMaternoInput.addEventListener("input", generarCurprfc);
    fechaNacimientoInput.addEventListener("change", generarCurprfc);
    masculinocheck.addEventListener("change", generarCurprfc);
    femeninocheck.addEventListener("change", generarCurprfc);
    originario.addEventListener("change", generarCurprfc);

    function DesmarInfonavit() {
        const FolInfonavit = document.getElementById("FolInfonavit");
        var ConInfonavit = document.getElementById("ConInfonavit");
        FolInfonavit.value = '';
        if (ConInfonavit.checked) {
            FolInfonavit.disabled = false;
        }
        else {
            FolInfonavit.disabled = true;
        }
    }
    function Fonacot() {
        var ConFonacot = document.getElementById("ConFonacot")
        const FolFonacot = document.getElementById("FolFonacot");
        FolFonacot.value = '';
        if (ConFonacot.checked) {
            FolFonacot.disabled = false;
        }
        else {
            FolFonacot.disabled = true;
        }
    }

});


//function openModal(docId, Descrip) {
//    document.getElementById("docId").innerText = Descrip;
//    document.getElementById("modalLabel").innerText = Descrip;
//    document.getElementById("idDoc").value = docId;
//    var modal = new bootstrap.Modal(document.getElementById('uploadModal'));

//    modal.show();
//}

function llenacp(event) {
    var selectedOption = event.target.options[event.target.selectedIndex];
    var CP = document.getElementById("CP");

    if (selectedOption.value !== "0") {
        CP.value = selectedOption.dataset.cp;
    } else {
        CP.value = "";
    }
}

async function mostrarEdad() {
    var fecha = document.getElementById("FechNac").value;
    if (fecha !== "") {
        var edad = calcularEdad(fecha);
        document.getElementById("Edad").value = edad;
    }
    document.getElementById("Edad").disabled = true;
}

function calcularEdad(fechaNacimiento) {
    if (!fechaNacimiento) return "";

    var hoy = new Date();
    var nacimiento = new Date(fechaNacimiento);
    var edad = hoy.getFullYear() - nacimiento.getFullYear();
    var mes = hoy.getMonth() - nacimiento.getMonth();
    var dia = hoy.getDate() - nacimiento.getDate();

    if (mes < 0 || (mes === 0 && dia < 0)) {
        edad--;
    }

    return edad;
}

async function llenacolonia() {
    const Colonia = document.getElementById('Colonia');

    Colonia.innerHTML = '<option value="0">[SELECCIONA]</option>';

    if (document.getElementById('selEstado').value == 0) {
        toastr.error("Selecciona un Estado.");
        return; // Salir de la función
    }
    if (document.getElementById('seleMuni').value == 0) {
        toastr.error("Selecciona un Municipio.");
        return; // Salir de la función
    }

    const json = {
        "data": {
            "bdCc": 2,
            "bdSch": "dbo",
            "bdSp": "SPQRY_CatColonia"
        },
        "filter": [
            {
                "property": "ClaveEstado",
                "value": document.getElementById('selEstado').value
            },
            {
                "property": "ClaveMunicipio",
                "value": document.getElementById('seleMuni').value
            }
        ]
    };

    try {
        const obj = await consumir(json);

        if (obj.status === 200 && obj.data && obj.data.length > 0) {
            const colonias = obj.data[0].TBCAT_Colonia;

            if (!Array.isArray(colonias) || colonias.length === 0) {
                console.warn("No hay colonias disponibles para el municipio seleccionado.");
                return;
            }
            colonias.forEach(colonia => {
                if (!colonia.ClaveColonia || !colonia.Descripcion) {
                    console.warn("Datos incompletos en la respuesta del API:", colonia);
                    return;
                }
                let option = document.createElement("option");
                option.value = colonia.ClaveColonia;
                option.textContent = colonia.Descripcion;
                option.dataset.cp = colonia.CP || '';
                Colonia.appendChild(option);
            });
        }
        else {
            console.warn("Error en el api debolvio datos vacios o invalidos");
        }
    }
    catch (error) {
        console.error("Error al consumir:", error);
    }
}
function formato(texto) {
    return texto.replace(/^(\d{4})-(\d{2})-(\d{2})$/g, '$3/$2/$1');
}
async function EjecutaValidarReingreso() {
    var curp = document.getElementById('tempCurp').textContent;
    var rfc = document.getElementById('tempRfc').textContent;
    var nss = document.getElementById('tempNss').textContent;
    var valiRein = document.getElementById('ValidaBTN');
    if (curp === '') {
        curp = null;
    }
    if (rfc === '') {
        rfc = null;
    }
    if (nss === '') {
        nss = null;
    }
    var Js = {
        "data": {
            "bdCc": 2,
            "bdSch": "dbo",
            "bdSp": "SPQRY_ValReingreso"
        },
        "filter": [
            {
                "property": "RFC",
                "value": rfc
            },
            {
                "property": "CURP",
                "value": curp
            },
            {
                "property": "NSS",
                "value": nss
            }
        ]
    };

    var modal = new bootstrap.Modal(document.getElementById('Reigreso'));
    var TabReingreso = document.getElementById('TabReingreso').querySelector('tbody');

    try {
        const obj = await consumir(Js);  // Ahora sí se puede usar await
        if (obj.status == 200) {
            const Regis = obj.data[0].ValidaReingreso;
            TabReingreso.innerHTML = '';
            for (let i = 0; i < Regis.length; i++) {
                let nuevaFila = document.createElement('tr'); // Crear una nueva fila en cada iteración

                nuevaFila.innerHTML = `
                <td>${Regis[i].Empresa}</td>
                <td>${Regis[i].Nombre}</td>
                <td>${Regis[i].NumEmpleado}</td> 
                <td>${Regis[i].Estatus}</td> 
                <td>${Regis[i].Puesto}</td> 
                <td>${formato(Regis[i].FechaIngreso)}</td> 
                <td><button type="button" class="btn btn-outline-light">Mostrar</button></td>
            `;

                TabReingreso.appendChild(nuevaFila); // Agregar la fila a la tabla
            }
            valiRein.value = 100;
            modal.show();
        }
        else if (obj.status == 400) {
            obj.message
            valiRein.value = 100;
            toastr.error(obj.message);
        }

    } catch (error) {
        console.error("Error al enviar la notificación:", error);
    }
}
async function ValidarReingreso() {

    const curpInput = document.getElementById("CURP");
    const rfcInput = document.getElementById("RFC");
    const NSSInput = document.getElementById("NSS");

    if (!curpInput.value && !rfcInput.value && !NSSInput.value) {
        toastr.error("Debes de ingresar por lo menos un CURP,RFC o NSS ");
        return;
    }

    var modal = new bootstrap.Modal(document.getElementById('ValRies'));
    document.getElementById('tempCurp').textContent = curpInput.value;
    document.getElementById('tempRfc').textContent = rfcInput.value;
    document.getElementById('tempNss').textContent = NSSInput.value;
    modal.show();
}

async function llenaMunicipio() {
    const seleMuni = document.getElementById('seleMuni');
    const Colonia = document.getElementById('Colonia');

    seleMuni.innerHTML = '<option value="0">[SELECCIONA]</option>';
    Colonia.innerHTML = '<option value="0">[SELECCIONA]</option>';

    if (document.getElementById('selEstado').value == 0) return;

    const json = {
        "data": {
            "bdCc": 2,
            "bdSch": "dbo",
            "bdSp": "SPQRY_CatMunicipio"
        },
        "filter": [
            {
                "property": "ClaveEstado",
                "value": document.getElementById('selEstado').value
            }
        ]
    };

    try {
        const obj = await consumir(json);

        if (obj.status === 200 && obj.data && obj.data.length > 0) {
            const Municipios = obj.data[0].TBCAT_Municipio;

            if (Array.isArray(Municipios) && Municipios.length > 0) {
                Municipios.forEach(municipio => {
                    let option = document.createElement("option");
                    option.value = municipio.ClaveMunicipio;
                    option.textContent = municipio.Descripcion;
                    seleMuni.appendChild(option);
                });
            }
            else {
                console.warn("No se encontraron municipios para el estado seleccionado.");
            }
        }
        else {
            console.warn("La respuesta de la API no contiene datos válidos.");
        }
    } catch (error) {
        console.error("Error al obtener municipios:", error);
    }
}


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

function Llenasal(event) {
    const overlay = document.getElementById('loading-overlay');
    overlay.style.display = 'block'; // Mostrar el spinner

    // Validar que event y event.target existan
    if (!event || !event.target) {
        console.error("Error: event o event.target es undefined.");
        overlay.style.display = 'none';
        return;
    }

    var selectChanged = event.target.id; // Saber qué select cambió
    var selectedOption = event.target.options[event.target.selectedIndex];

    // Validar que selectedOption exista
    if (!selectedOption) {
        console.error("Error: No se pudo obtener la opción seleccionada.");
        overlay.style.display = 'none';
        return;
    }

    var tipoOperacion = selectedOption.getAttribute("data-tipo-operacion");

    // Validar que tipoOperacion tenga un valor
    if (!tipoOperacion) {
        console.error("Error: data-tipo-operacion no encontrado en la opción seleccionada.");
        overlay.style.display = 'none';
        return;
    }

    var selSal = document.getElementById('selSal');
    var selTipOpera = document.getElementById('TipOpera');

    // Validar que los selects existan
    if (!selSal || !selTipOpera) {
        console.error("Error: No se encontró uno de los selects (selSal o TipOpera).");
        overlay.style.display = 'none';
        return;
    }

    let encontrado = false;

    if (selectChanged === "TipOpera") {
        // Si cambia TipOpera, buscamos el Salario correspondiente
        for (let i = 0; i < selSal.options.length; i++) {
            if (selSal.options[i].getAttribute("data-tipo-operacion") === tipoOperacion) {
                selSal.value = selSal.options[i].value; // Seleccionar salario
                encontrado = true;
                break;
            }
        }
    } else if (selectChanged === "selSal") {
        // Si cambia selSal, buscamos el Tipo de Operación correspondiente
        for (let i = 0; i < selTipOpera.options.length; i++) {
            if (selTipOpera.options[i].getAttribute("data-tipo-operacion") === tipoOperacion) {
                selTipOpera.value = selTipOpera.options[i].value; // Seleccionar tipo de operación
                encontrado = true;
                break;
            }
        }
    }

    if (!encontrado) {
        console.warn(`Advertencia: No se encontró una coincidencia para el tipo-operacion: ${tipoOperacion}`);
    }

    overlay.style.display = 'none'; // Ocultar el spinner
}

async function buscarporcp() {
    const SeleColonia = document.getElementById("Colonia");
    const CP = document.getElementById('CP').value;
    const Muni = document.getElementById('seleMuni');
    const Esta = document.getElementById('selEstado');

    SeleColonia.innerHTML = '<option value="0">[SELECCIONA]</option>';
    Muni.innerHTML = '<option value="0">[SELECCIONA]</option>';

    if (!CP) {
        console.warn("No se proporcionó un código postal.");
        return;
    }

    const JsoCP = {
        "data": {
            "bdCc": 2,
            "bdSch": "dbo",
            "bdSp": "SPQRY_BuscarporCP"
        },
        "filter": [
            {
                "property": "CP",
                "value": CP
            }
        ]
    };

    try {
        const obj = await consumir(JsoCP);

        if (obj.status === 200 && obj.data && obj.data.length > 0) {
            const datosCP = obj.data[0];
            const colonias = datosCP.TBCAT_Colonia || [];
            const municipios = datosCP.TBCAT_Municipio || [];
            const selects = datosCP.BuscaCodigoPostal || [];
            
            colonias.forEach(col => {
                if (!col.ClaveColonia || !col.Descripcion) {
                    console.warn("Datos incompletos de colonia:", col);
                    return;
                }
                let option = document.createElement("option");
                option.value = col.ClaveColonia;
                option.textContent = col.Descripcion;
                option.dataset.cp = col.CodigoPostal || '';
                SeleColonia.appendChild(option);
            });
            SeleColonia.value = "0";

            municipios.forEach(mun => {
                if (!mun.ClaveMunicipio || !mun.Descripcion) {
                    console.warn("Datos incompletos de municipio:", mun);
                    return;
                }
                let option = document.createElement("option");
                option.value = mun.ClaveMunicipio;
                option.textContent = mun.Descripcion;
                Muni.appendChild(option);
            });

            selects.forEach(sel => {
                if (!sel.ClaveEstado || !sel.ClaveMunicipio) {
                    console.warn("Datos incompletos de seleccionar", sel);
                    return;
                }
                Esta.value = sel.ClaveEstado;
                Muni.value = sel.ClaveMunicipio;
            });

            
        } else {
            console.warn("No se encontraron datos para el código postal ingresado.");
        }
    } catch (error) {
        console.error("Error al procesar la información:", error);
    }
}


function habilitarCamposDeshabilitados() {
    const campos = ['Edad'];

    campos.forEach(id => {
        const campo = document.getElementById(id);
        if (campo) {
            campo.disabled = false;
        }
    });
    document.getElementById("Edad").disabled = true;
}