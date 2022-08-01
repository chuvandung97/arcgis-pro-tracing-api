
function tableToPDF (caption) {
    var doc = new jsPDF('p', 'pt', 'a4');
    var res = doc.autoTableHtmlToJson($("#assets-data-table"));
    doc.text(caption, 40, 35);
    doc.setFontSize(20);
    doc.setTextColor(40);
    doc.setFontStyle('normal');
    doc.autoTable(res.columns, res.data, {
        startY: 60,
        tableWidth: 'auto',
        columnWidth: 'auto',
        theme: 'grid',
        //bodyStyles: {lineColor: [0, 0, 0]},
        styles: {
            font: 'courier',
            lineColor: [44, 62, 80],
            lineWidth: 0.75,
            overflow: 'linebreak'
        },
        headerStyles: {
            fillColor: [51, 122, 183],
            fontSize: 9
        },
        alternateRowStyles: {
            fillColor: [232, 233, 234]
        },
        margin: {
            top: 60
        }
    });
    doc.save();
};