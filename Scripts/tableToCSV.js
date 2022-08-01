
jQuery.fn.tableToCSV = function (caption) {

    var clean_text = function (text) {
        text = text.replace(/"/g, '""');
        return '"' + text + '"';
    };

    $(this).each(function () {
        var table = $(this);
        //var caption = $(this).find('caption').text();
        var title = [];
        var rows = [];

        $(this).find('tr').each(function () {
            var data = [];
            $(this).find('th').each(function () {
                var text = clean_text($(this).text());
                title.push(text);
            });
            $(this).find('td').each(function () {
                var text = clean_text($(this).text());
                data.push(text);
            });
            data = data.join(",");
            rows.push(data);
        });
        title = title.join(",");
        rows = rows.join("\n");

        var csv = title + rows;
        var uri = 'data:text/csv;charset=utf-8,' + encodeURIComponent(csv);
        var isIE = /*@cc_on!@*/false || !!document.documentMode;

        if (isIE) {
            csvData = csv;
            if (window.navigator.msSaveBlob) {
                var blob = new Blob([csvData], {
                    type: "text/csv;charset=utf8"
                });
                var titleIE;
                var ts = new Date().getTime();
                if (caption == "") {
                    titleIE = ts + ".csv";
                } else {
                    titleIE = caption + "-" + ts + ".csv";
                }
                navigator.msSaveBlob(blob, titleIE);
            }
        } //other browser
        else {
            //window.open("data:application/vnd.ms-excel," + encodeURIComponent(myTable));
            var download_link = document.createElement('a');
            //download_link.href = uri;
            csvDataOther = csv;
            xData = new Blob([csvDataOther], { type: 'text/csv' });
            var xUrl = URL.createObjectURL(xData);
            download_link.href = xUrl;
            var ts = new Date().getTime();
            if (caption == "") {
                download_link.download = ts + ".csv";
            } else {
                download_link.download = caption + "-" + ts + ".csv";
            }
            document.body.appendChild(download_link);
            download_link.click();
            document.body.removeChild(download_link);
        }

        /*var csv = title + rows;
        var uri = 'data:text/csv;charset=utf-8,' + encodeURIComponent(csv);
        var download_link = document.createElement('a');
        download_link.href = uri;
        var ts = new Date().getTime();
        if (caption == "") {
            download_link.download = ts + ".csv";
        } else {
            download_link.download = caption + "_" + ts + ".csv";
        }
        document.body.appendChild(download_link);
        download_link.click();
        document.body.removeChild(download_link);*/
    });

};