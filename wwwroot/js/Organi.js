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
document.addEventListener("DOMContentLoaded", function () {
    var editForm = function () {
        this.nodeId = null;
    };

    editForm.prototype.init = function (obj) {
        var that = this;
        this.obj = obj;
        var orgchart = this.obj;
        this.editForm = document.getElementById("editForm");
        this.AreaInput = document.getElementById("Area");
        this.PlazasInput = document.getElementById("Plazas");
        this.cancelButton = document.getElementById("cancel");

        this.cancelButton.addEventListener("click", function () {
            that.hide();
        });
    };

    editForm.prototype.show = function (nodeId) {
        this.hide();
        this.nodeId = nodeId;

        var left = document.body.offsetWidth / 2 - 150;
        this.editForm.style.display = "block";
        this.editForm.style.left = left + "px";
        var node = this.obj.get(nodeId);
        this.AreaInput.textContent = node.area;
        this.PlazasInput.textContent = node.plazas;
    };

    editForm.prototype.hide = function () {
        this.editForm.style.display = "none";
    };

    OrgChart.templates.myTemplate = Object.assign({}, OrgChart.templates.olivia);
    OrgChart.templates.myTemplate.field_0 = '<text data-width="230" data-text-overflow="multiline" style="font-size: 18px;" fill="#000000" x="125" y="65" text-anchor="middle">{val}</text>';
    OrgChart.templates.myTemplate.field_1 = '<text data-width="230" data-text-overflow="ellipsis" style="font-size: 14px;" fill="#000000" x="125" y="25" text-anchor="middle">{val}</text>';


    let chart = new OrgChart(document.getElementById("tree"), {
        template: "myTemplate",
        filterBy: ["agencia", "departamento"],
        mouseScrool: OrgChart.none,
        layout: OrgChart.mixed,
        toolbar: {
            layout: true,
            zoom: true,
            fit: true,
            expandAll: true
        },
        nodeBinding: {
            field_0: "area",
            field_1: "plazas"
        },
        editUI: new editForm(),
        menu: {
            pdf: { text: "Export PDF" }
        },
        tags: {
            filter: {
                template: "dot"
            }
        }
    });
    var datitos = document.getElementById('datosload').value;
    var jserial = JSON.parse(datitos);
    chart.load(jserial);
});

function pdf(nodeId) {
    chart.exportPDF({ filename: "MyFileName.pdf", expandChildren: true, nodeId: nodeId });
}