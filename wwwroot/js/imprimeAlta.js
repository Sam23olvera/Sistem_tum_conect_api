
document.addEventListener("DOMContentLoaded", function () {
    var links = document.querySelectorAll(".carga");
    links.forEach(function (link) {
        link.addEventListener("click", function () {
            document.getElementById("loading-overlay").style.display = "block";
        });
    });
    const mensaje = document.getElementById("mensaje");
    const guarda = document.getElementById("guarda");
    const status = document.getElementById("status");
    const divguard = document.getElementById("divguard");
    const divarchivos = document.getElementById("divarchivos");
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
    
});

function openModal(docId, Descrip) {
    document.getElementById("docId").innerText = Descrip;
    document.getElementById("modalLabel").innerText = Descrip;
    document.getElementById("idDoc").value = docId;
    var modal = new bootstrap.Modal(document.getElementById("uploadModal"));

    modal.show();
}
function imprimirPagina() {
    var meses = [
        "Enero", "Febrero", "Marzo",
        "Abril", "Mayo", "Junio", "Julio",
        "Agosto", "Septiembre", "Octubre",
        "Noviembre", "Diciembre"
    ]

    var date = new Date();
    var dia = date.getDate();
    var mes = date.getMonth();
    var yyy = date.getFullYear();
    var fecha_formateada = dia + ' de ' + meses[mes] + ' de ' + yyy;

    var Nombreinput = document.getElementById("Nombre").value;
    var ApMaternoinput = document.getElementById("ApMaterno").value;
    var ApPaternoinput = document.getElementById("ApPaterno").value;
    var Edadinput = document.getElementById("Edad").value;
    var originario = document.getElementById("txtoriginario").value;
    var nacionalidad = document.getElementById("txtSelPais").value;
    const masculinocheck = document.getElementById("masculino");
    const femeninocheck = document.getElementById("femenino");
    let sex = masculinocheck.checked ? masculinocheck.value : femeninocheck.checked ? femeninocheck.value : "";
    var letsex = "";
    if (sex == "H") {
        letsex = "MASCULINO";
    }
    else {
        letsex = "FEMENINO";
    }
    var CURPinput = document.getElementById("CURP").value;
    var RFCinput = document.getElementById("RFC").value;
    var NSSinput = document.getElementById("NSS").value;
    var Estado = document.getElementById("textselEstado").value;
    var Muni = document.getElementById("textseleMuni").value;
    var Colonia = document.getElementById("textColonia").value;
    var Calle = document.getElementById("Calle").value;
    var NumExterior = document.getElementById("NumExterior").value;
    var NumInterior = document.getElementById("NumInterior").value;
    var CP = document.getElementById("CP").value;
    var EstaCivil = document.getElementById("textEdoCivil").value;
    var Salario = document.getElementById("textselSal").value;
    var puesto = document.getElementById("textselePues").value;
    var repre = "C. DELGADO GONZALEZ INDIRA NEREY";
    var empresa = "";
    if (document.getElementById("cveEmp").value == 1) {
        empresa = "TUM TRANSPORTISTAS UNIDOS MEXICANOS DIVISION NORTE S.A. DE C.V ";
    }
    else {
        empresa = "TUM LOGISTICA Y SERVICIOS DEDICADOS, S.A. DE C.V.";
    }

    var mywindow = window.open("", "PRINT", "height=600,width=800");

    var htmlContent = `
<!DOCTYPE html>
<html lang="es">

<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>Contrato Individual de Trabajo</title>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/css/bootstrap.min.css" rel="stylesheet">
    <style>
        .contract {
            width: 21cm;
            min-height: 27.94cm;
            padding: 0.25cm;
            margin: auto;
            background: white;
            box-shadow: 0px 0px 10px rgba(0, 0, 0, 0.1);
        }

        h2, h5 {
            text-align: center;
        }

        p {
            text-align: justify;
        }

        ul, ol {
            text-align: justify;
        }

        @media print {
            body {
                background: none !important;
                margin: 0.25cm;
                padding: 0.25cm;
            }

            .contract {
                width: 100%;
                padding: 0.25cm;
                box-shadow: none;
                text-align: justify;
            }

            h2, h5 {
                text-align: center;
            }
        }
    </style>
</head>

<body class="bg-light">
    <div class="container align-content-center my-5">
        <div class="contract p-5">
            <h2>CONTRATO INDIVIDUAL DE TRABAJO POR TIEMPO INDETERMINADO</h2>
            <p>En CUAUTITLÁN IZCALLI, EDO. DE MÉXICO, el dia ${fecha_formateada.toUpperCase()} los que suscriben el presente Contrato,
            por una parte como Patrón <strong>${empresa.toUpperCase()}</strong>
            la cual está representada por el ${repre.toUpperCase()} en su carácter de Representante legal,
            y por la otra ${ApPaternoinput.toUpperCase()} ${ApMaternoinput.toUpperCase()} ${Nombreinput.toUpperCase()}
            </p>
            <p><strong>Es intención de las partes facilitar la interpretación de este pacto, por lo que hacen las
                    siguientes:</strong></p>

            <h5 class="text-center">DECLARACIONES</h5>
                <p>
                    <strong>I.- Declara el ${repre}</strong> que en lo sucesivo en
                    el cuerpo de este Contrato se denominará “Trabajador” que:
                </p>
                <ul>
                    <li>Es originario de: ${originario.toUpperCase()}</li>
                    <li>Nacionalidad: ${nacionalidad.toUpperCase()}</li>
                    <li>Edad: ${Edadinput}</li>
                    <li>Sexo: ${letsex.toUpperCase()}</li>
                    <li>Estado Civil: ${EstaCivil.toUpperCase()}</li>
                    <li>CURP: ${CURPinput.toUpperCase()}</li>
                    <li>Registro Federal de Contribuyentes: ${RFCinput.toUpperCase()}</li>
                    <li>Número de Seguridad Social: ${NSSinput}</li>
                    <li>Con domicilio en: ${Calle.toUpperCase()} ${NumExterior.toUpperCase()} ${NumInterior.toUpperCase()} ${CP.toUpperCase()} ${Colonia.toUpperCase()} ${Muni.toUpperCase()} ${Estado.toUpperCase()} </li>
                </ul>
                <p>
                    <strong>II.- Declara ${empresa.toUpperCase()}</strong>
                    que en lo sucesivo en el cuerpo de este Contrato se denominará “Patrón” que:
                </p>
                <ul>
                    <li><strong>Es una Sociedad mercantil, debidamente constituida de conformidad a las leyes
                            mexicanas.</strong></li>
                    <li><strong>Que tiene su domicilio en:</strong> CALLE Quetzal SN, Col. INDUSTRIAL SAN MARTIN OBISPO,
                        CUAUTITLÁN IZCALLI CP 54769.</li>
                    <li><strong>Que entre sus actividades están:</strong> Autotransporte de Carga General.</li>
                </ul>
                <h3>FUNCIONES Y OBLIGACIONES</h3>
                <p>Se describen de manera enunciativa más no limitativa las funciones y obligaciones de “EL
                    TRABAJADOR”</p>
                <ul>
                    <li>“EL TRABAJADOR” tiene la obligación de seguir rigurosamente las disposiciones del reglamento
                        interior de trabajo y apegarse a lo estipulado en éste, en la inteligencia de que cualquier
                        violación al reglamento será sancionado de acuerdo a los términos del mismo.</li>
                    <li>Tendrá la obligación de realizar las actividades necesarias para la exitosa consecución de los
                        objetivos para los cuales fue contratado.</li>
                    <li>Tendrá la obligación de aceptar los programas de Capacitación y medición en los que lo incluya
                        la empresa, dentro y fuera de las instalaciones.</li>
            </div>
            <div class="contract p-5">
                    <li>Tener bajo su responsabilidad el resguardo de los medios y herramientas de trabajo, por lo tanto
                        se le considerará como depositario de la misma. </li>
                    <li>Efectuar todas las actividades del área relacionadas con su puesto, indicadas por su superior o
                        jefe.</li>
                    <li>Portar en todo momento el uniforme de trabajo y/o equipo de seguridad en buen estado y limpio,
                        sin perjuicio del desgaste normal generado por el uso</li>
                    <li>Cumplir con las leyes de tránsito terrestre aplicables.</li>
                    <li>Seguir los procedimientos y protocolos de seguridad.</li>
                </ul>
                <p><strong>En virtud de lo declarado y para el logro de las finalidades que las partes proponen, el
                        Contrato Individual de Trabajo por Tiempo Indeterminado lo sujetan al tenor de las
                        siguientes:</strong></p>
            <h5>CLÁUSULAS</h5>
            <p>PRIMERA.- Los contratantes se reconocen expresamente la personalidad con que se ostentan, para
                todos los efectos legales a que haya lugar y convienen que en el cuerpo de este contrato en lo
                sucesivo se denominarán respectivamente Patrón y Trabajador.</p>
            <p>SEGUNDA.- El Patrón por su parte declara estar en uso de sus facultades legales teniendo la
                capacidad necesaria para celebrar el presente Contrato de Trabajo.</p>
            <p>TERCERA.- Este Contrato se celebra por tiempo indeterminado de conformidad con lo dispuesto por
                el artículo 35 de la Ley Federal del Trabajo y solo podrá modificarse, rescindirse o terminarse
                en los casos establecidos por dicho ordenamiento, con un periodo de prueba de treinta días, con
                fundamento en lo establecido por el artículo 39-A, 47 y 53 de la Ley Federal del Trabajo.</p>
            <p>CUARTA.- El Trabajador se obliga a prestar bajo la dirección, dependencia y subordinación del
                patrón, sus servicios personales consistentes en <strong>CONDUCTOR DOBLE OPERADOR.</strong></p>
            <p>QUINTA.- El Trabajador y el Patrón convienen en que a juicio de éste se podrá cambiar al
                trabajador de unidad asignada, siempre y cuando se le garanticen su categoría y salario.</p>
            <p>SEXTA.- El Trabajador se obliga a desempeñar sus funciones en el área Metropolitana de la Ciudad
                de México y/o en cualquier otro lugar que el Patrón le asigne en el interior de la República.
            </p>
            <p>SEPTIMA.- La duración de la jornada de trabajo se encuentra regulada como trabajo especial dada
                la naturaleza de la actividad que desempeña el Autotransporte, por lo que no existe jornada de
                trabajo específica, sino la necesaria para el viaje encomendado, no existiendo por consiguiente
                tiempo extra, salvo por causas ajenas al trabajador que prolongue o retarde el viaje, con
                fundamento en los artículos 5° fracción III, 60 y 62 de la Ley Federal del Trabajo.</p>
            <p>OCTAVA.- El salario o sueldo convenido, como retribución por los servicios a que en este contrato
                se refiere es el siguiente: ${Salario.toUpperCase()} mensuales, lo anterior con fundamento en los artículos 257
                y 258 de la Ley Federal del Trabajo.</p>
            <p>NOVENA- El día de descanso semanal o séptimos días, días festivos y descansos obligatorios, se
                encuentran cubiertos con el salario pactado en la cláusula anterior, en términos de lo dispuesto
                por los artículos 69 y 258 de la Ley Federal del Trabajo.</p>
            <p>DECIMA.- Se conviene expresamente en que la violación a cualquiera de las obligaciones
                consignadas en este contrato por el trabajador, es causal de rescisión del mismo sin
                responsabilidad para el Patrón, la relación de trabajo se encuentra regulada en el capítulo VI
                consistente en los trabajos especiales del Autotransporte de la Ley Federal del Trabajo.</p>
            <p>DECIMA PRIMERA- Se faculta expresamente al patrón para que determine el período de vacaciones y
                prima vacacional que deberá disfrutar el trabajador, de acuerdo con las necesidades de la
                empresa, lo anterior con fundamento en los artículos 76 y 80 de la Ley Federal del Trabajo.</p>
            <p>DECIMA SEGUNDA.- Se reconoce expresamente al trabajador una antigüedad de servicio en la Empresa
                a partir del día ${fecha_formateada.toUpperCase()}</p>
            <p>DECIMA TERCERA.- El Patrón cumpliendo con lo establecido en el artículo 25 fracción VIII de la
                Ley Federal del Trabajo, proporcionará al Trabajador los medios suficientes para Capacitarlo en
                su trabajo, si así lo requiere a efecto del mejor desempeño de sus labores.</p>
            <p>DECIMA CUARTA.- El Trabajador conviene a someterse a los reconocimientos médicos que
                periódicamente ordene el Patrón, en término de lo dispuesto en la Fracción X del Artículo 134 de
                la Ley Federal del Trabajo.</p>
            <p>DECIMA QUINTA.- El Trabajador declara conocer el Reglamento Interior de Trabajo que rige las
                relaciones con el Patrón, obligándose a acatarlo en todas y cada una de sus partes, conociendo
                las sanciones que el mismo establece para las diferentes violaciones.</p>
            <p>DECIMA SEXTA.- El Patrón pagará al Trabajador un aguinaldo anual equivalente a 15 días de
                salario, que se cubrirá antes del 20 de diciembre y en forma proporcional durante el año, lo
                anterior con fundamento en el articulo 87 de la Ley Federal de Trabajo.</p>
            <p>DECIMA SEPTIMA.- El Patrón se obliga a proporcionar capacitación y adiestramiento en los términos
                del Capítulo III Bis del Título Cuarto de la Ley Federal del Trabajo. Por su parte el Trabajador
                se compromete a sujetarse a los cursos de capacitación y adiestramiento instrumentados por la
                Comisión Mixta y aprobados por la Secretaría del Trabajo y Previsión Social, según lo disponen
                los artículos 153 A, 153 B, 153 I, 153 II y demás relativos y aplicables de la Ley Federal del
                Trabajo.</p>
            <p>DÉCIMA OCTAVA: Serán días de descanso obligatorio los que señala el Artículo 74 de la Ley Federal
                del Trabajo y los que ”LA EMPRESA” establezca, tomando en cuenta que “LA EMPRESA”, en caso de
                requerir que “EL TRABAJADOR” labore en los días de descanso obligatorios señalados en el
                artículo 74 de La Ley Federal del Trabajo al efecto se aplicará lo dispuesto por el Articulo 73
                de La Ley Federal del Trabajo, en el entendido de que la falta injustificada en cualquiera de
                estos días será considerado como falta de probidad, debido a las necesidades operativas,
                administrativas o comerciales de “LA EMPRESA”.</p>
        </div>
        <div class="contract p-5">
            <p>DECIMA NOVENA: En caso de fallecimiento o desaparición derivada de un acto delincuencial el
                trabajador haciendo uso del derecho que le concede la fracción X del artículo 25 de la Ley
                Federal del Trabajo en relación con el artículo 501 de la misma Ley, designa como beneficiarios
                a los C.:</p>
            <ol style="margin:0pt; padding-left:0pt">
                <li class="BodyText" style="margin-left:31.34pt; padding-left:4.66pt; font-family:Arial">
                    &#xad;<span style="background-color:#c0c0c0">___________________________________________con
                        parentesco, ________________</span>
                        <span style="font-family:Arial; background-color:#c0c0c0">del empleado, a quien en su caso se le
                    localizará en el teléfono móvil: ___________________</span>
                    <span style="font-family:Arial; background-color:#c0c0c0">2.____________________________________________,
                    con parentesco _______________</span>
                <span style="font-family:Arial; background-color:#c0c0c0">del empleado a quien en su caso se le
                    localizará en el teléfono móvil:___________________</span>
                    </li>
            </ol>
           
            <p>Personas a quienes autoriza expresamente para poder recibir el finiquito y prestaciones a las que haya
                tenido derecho.</p>
        
            <p>VIGESIMA: El Trabajador se obliga a dar por escrito cualquier cambio de domicilio en la inteligencia de
                que
                sí no lo hace se tendrá como tal el que se señala en este Contrato para todos los efectos legales.</p>
            <p>VIGESIMA PRIMERA: Ambas partes convienen en que para el caso de cualquier siniestro, robo o pérdida de la
                mercancía que fuese encomendado el servicio o encargo al trabajador, que fuera propiedad del cliente del
                patrón o de éste mismo, el trabajador acepta someterse a cualquier investigación o control de veracidad
                sobre los hechos ocurridos que le sea requerida por el patrón.</p>
            <p>Ambas partes convienen en que la negativa del trabajador a someterse a la práctica de investigación o
                control
                de veracidad del hecho por las empresas establecidas para tal efecto que designe el patrón, será causa
                de
                rescisión de la relación de trabajo sin responsabilidad para el patrón, considerando dicha negativa una
                falta de probidad del trabajador.</p>
            <p>Así mismo y como se estipula en el artículo 24 de la Ley Federal del Trabajo, se entrega un ejemplar del
                mismo y se acusa de recibido dicho contrato.</p>
            <p>Leído que fue por ambas partes este contrato ante los testigos que también lo firman e impuestos todos de
                su contenido y sabedores de los efectos legales; se firma de común acuerdo el día ${fecha_formateada}
            </p>
            <div class="row">
                <div class="col-6">
                    <p class="mt-5 text-center">__________________________________</p>
                    <p class="text-center">Firma del Patrón</p>  
                </div>
                <div class="col-6">
                    <p class="mt-5 text-center">__________________________________</p>
                    <p class="text-center">Firma del Trabajador</p>
                </div>
            </div>
        </div>
        <br/>
        <br/>
        <br/>
        <br/>
        <br/>
        <br/>
        <br/>
        <div class="contract p-5">
            <h2 class="text-center">ACUSE DE RECIBIDO</h2>
            <p>USTED PARTICIPARA COMO UN COLABORADOR DE LA COMPAÑÍA <strong>${empresa.toUpperCase()}</strong>
                EN LO SUCESIVO DENOMINADA EMPRESA, EN EL PUESTO ${puesto.toUpperCase()} A TRAVÉS DEL CUAL DESARROLLARÁ SUS ACTIVIDADES
                ASIGNADAS.</p>
            <p>USTED RECONOCE QUE PODRÁ TENER ACCESO A CONOCIMIENTOS, FORMULACIONES, PROCEDIMIENTOS, SECRETOS, PATENTES,
                ESTRATEGIAS, PROGRAMAS, PRODUCTOS Y OTRAS INFORMACIONES DE CARÁCTER CONFIDENCIAL ( EN LOS SUCESIVO
                INFORMACIÓN CONFIDENCIAL), DE LA CUAL PUEDEN SER PROPIETARIOS LA EMPRESA O UNO O MAS DE SUS CLIENTES Y
                QUE LA DIVULGACIÓN DE DICHA INFORMACIÓN PUEDE CAUSAR DAÑOS Y PERJUICIOS A SUS PROPIETARIOS.</p>
            <p>POR LO ANTERIOR, USTED PODRÁ TENER ACCESO A LA INFORMACIÓN CONFIDENCIAL EN RELACIÓN, O COMO RESULTADO DE
                SU PARTICIPACIÓN COMO COLABORADOR O MIEMBRO DE LA EMPRESA, Y PARA EL ÚNICO PROPÓSITO DE CUMPLIR CON SUS
                FUNCIONES COMO TAL; POR LO QUE SE COMPROMETE A MANTENER DICHA INFORMACIÓN SEA QUE LA HAYA ADQUIRIDO EN
                DOCUMENTOS, MEDIOS ELECTROMAGNÉTICOS O EN FORMA VERBAL, RESERVADA PARA EL USO INDISPENSABLES Y
                NECESARIOS PARA CUMPLIR CON SUS OBLIGACIONES Y FUNCIONES.</p>
            <p>ASÍ MISMO, USTED SE COMPROMETE A QUE NO OBSTANTE LA INFORMACIÓN CONFIDENCIAL PUEDE SER EVIDENTE PARA UN
                TÉCNICO EN LA MATERIA, A DARLE TRATO DE LA MAYOR CONFIDENCIABILIDAD A NO USARLA PARA SU BENEFICIO
                PERSONAL NI PARA EL BENEFICIO DE SU SUPERIORES, SOCIOS, EMPLEADOS REPRESENTANTE O CLIENTES Y NO DIVULGAR
                LA INFORMACIÓN CONFIDENCIAL POR NINGÚN MEDIO SIN LA AUTORIZACIÓN EXPRESA DE LA EMPRESA, Y HA NO HACERLA
                DEL CONOCIMIENTO DE PERSONA AJENAS A LA MISMA, ASÍ COMO EVITAR SU REPRODUCCIÓN O DIVULGACIÓN POR PARTE
                DE CUALQUIER DE TERCERA PERSONA. FUNDAMENTANDO LO ANTERIOR MANIFESTADO, AL ARTICULO 211 DEL CÓDIGO PENAL PARA EL DISTRITO FEDERAL Y PARA
                TODA LA REPÚBLICA EN MATERIA DE FUERO FEDERAL, SANCIÓN DE PENA PRIVATIVA Y HASTA LA SUSPENSIÓN DE SU
                PROFESIÓN EN SU CASO, CUANDO SE ESTE EN LOS SUPUESTO EN LOS PÁRRAFOS SEÑALADOS CON ANTELACIÓN.
                ASÍ MISMO Y COMO SE ESTIPULA EN EL ARTÍCULO 24 DE LA LEY FEDERAL DEL TRABAJO, SE ENTREGA UN EJEMPLAR DEL
                MISMO Y SE ACUSA DE RECIBIDO DICHO CONTRATO.</p>
            <p>LEÍDO QUE FUE POR AMBAS PARTES ESTE CONTRATO ANTE LOS TESTIGOS QUE TAMBIÉN LO FIRMAN E IMPUESTOS TODOS DE
                SU CONTENIDO Y SABEDORES DE LOS EFECTOS LEGALES; SE FIRMA DE COMÚN ACUERDO EL DÍA ${fecha_formateada}.</p>
             <div class="row">
                <div class="col-6">
                    <p class="mt-5 text-center">__________________________________</p>
                    <p class="text-center">Firma del Patrón</p>  
                </div>
                <div class="col-6">
                    <p class="mt-5 text-center">__________________________________</p>
                    <p class="text-center">Firma del Trabajador</p>
                </div>
            </div>
        </div>
        <div class="contract p-5">
            <h2>${empresa.toUpperCase()}</h2>
            <br/>
            <h2>P R E S E N T E</h2>
            <br/>
            <p>Por medio de la presente, hago de su conocimiento que con esta fecha y por así convenir a mis intereses,
                en uso de la fracción I del Artículo 53 de la Ley Federal del Trabajo, renuncio libre y voluntariamente
                a la relación de trabajo que con el puesto de ${puesto.toUpperCase()} que venía desempeñando en esta empresa, en
                la inteligencia de que así conviene a mis intereses.</p>
            <p>Quiero hacer constar que a la fecha he recibido todos y cada uno de los pagos <strong>${empresa.toUpperCase()}</strong> 
                por concepto de salarios ordinarios, salarios relativos a vacaciones, pago de prima vacacional, pago de 
                prima de antigüedad, séptimos días, descansos obligatorios, aguinaldo, horas extras reconociendo expresamente 
                no haberlas laborado, reparto de utilidades, previsión social que me correspondían, así como todo a 
                lo que tuve derecho conforme a la Ley de la materia.</p>
            <p>Asimismo, reconozco libremente que durante el tiempo que preste mis servicios en la empresa jamás sufrí
                riesgo de trabajo o enfermedad profesional alguna por lo que no me reservo acción o derecho alguno que
                intentar por cualquiera de los conceptos mencionados, ni por ningún otro, liberando a la empresa, a la
                firma de la presente renuncia, de cualquier obligación derivada de mi relación de trabajo otorgándole el
                más amplio finiquito que en derecho proceda, no reservándome acción o derecho alguno que intentar en
                contra de la misma o quien resulte ser su representante.</p>
            <p>Por último manifiesto libremente que de estimarlo necesario la compañía, me obligo a RATIFICAR, ante las
                autoridades del Trabajo lo manifestado libre y voluntariamente, toda vez que me han sido cubierto
                oportunamente todas y cada una de las prestaciones a las que se refiere la Ley.</p>
                <br/>
                <br/>
            <p class="text-center">ATENTAMENTE</p>
            <p class="mt-5 text-center">__________________________________</p>
        </div>
    </div>
    <script>
        mywindow.document.write(htmlContent);
        mywindow.document.close(); 
        mywindow.print();
    </script>
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/js/bootstrap.bundle.min.js"></script>
</body>

</html>
`;
    mywindow.document.write(htmlContent);
    mywindow.document.close();
    mywindow.print();
}

