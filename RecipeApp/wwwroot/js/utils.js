function createTable(data, tableClassName) {
    const table = $('<table></table>').addClass("table");

    const rowHead = $('<tr></tr>');

    for (const key in data[0]) {
        if (Object.hasOwnProperty.call(data[0], key)) {
            if (key.includes('image')) {
                const rowData = $('<th></th>').text("image");
                rowHead.append(rowData);
            } else {
                const rowData = $('<th></th>').text(key);
                rowHead.append(rowData);
            }
        }
    }
    table.append(rowHead);

    for (var i = 0; i < data.length; i++) {
        const row = $('<tr></tr>');

        for (const key in data[i]) {
            if (Object.hasOwnProperty.call(data[i], key)) {
                if (key.includes('image')) {
                    const image = $('<img>').attr('src', data[i][key]).attr("height", "100").attr("width", "100")
                    const rowData = $('<td></td>')
                    rowData.append(image)
                    row.append(rowData);
                } else {
                    const value = String(data[i][key]).substring(0, 20)
                    const rowData = $('<td></td>').text(String(data[i][key]).length >= 20 ? value + "..." : data[i][key]);
                    row.append(rowData);
                }

            }
        }
        const deleteButton = $('<button/>').attr("id", i).addClass('delete-button').text("Delete").click((e) => onDelete(e));
        const updateButton = $('<button/>').attr("id", i).addClass('update-button').text("Update").click((e) => onUpdate(e));
        row.append(deleteButton);
        row.append(updateButton);


        table.append(row);
    }

    if ($('table').length) {
        $(`#${tableClassName}-table tr:first`).after(row);
    }
    else {
        $(`#${tableClassName}-table`).append(table);
    }
}

function stringToSlug(str) {
    str = str.replace(/^\s+|\s+$/g, "");
    str = str.toLowerCase();

    var from = "åàáãäâèéëêìíïîòóöôùúüûñç·/_,:;";
    var to = "aaaaaaeeeeiiiioooouuuunc------";

    for (var i = 0, l = from.length; i < l; i++) {
        str = str.replace(new RegExp(from.charAt(i), "g"), to.charAt(i));
    }

    str = str
        .replace(/[^a-z0-9 -]/g, "")
        .replace(/\s+/g, "-")
        .replace(/-+/g, "-")
        .replace(/^-+/, "")
        .replace(/-+$/, "");

    return str;
}