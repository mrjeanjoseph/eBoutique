const download = function(data) {
    const blob = new Blob([data], {type: 'text/csv'});
    const url = window.URL.createObjectURL(blob);
    const a = document.createElement('a');
    a.setAttribute('hidden', '');
    a.setAttribute('href', url);
    a.setAttribute('download', 'download.csv');
    document.body.appendChild(a);
    a.click();
    document.body.removeChild(a);
}

const objectToCsv = function(data){
    const csvRows = [];

    const headers = Object.keys(data[0]);
    csvRows.push(headers.join(','));

    // console.log(csvRows);
    for( const row of data ) {
        const values = headers.map(header => {
            const escaped = ('' + row[header]).replace(/"/g, '\\"')
            return `"${ escaped }"`;
        });
        csvRows.push(values.join(','));
    }
    // console.log(csvRows);
    return csvRows.join('\n');
}


const getReport = async function () {
    const jsonUrl = 'https://retoolapi.dev/dRneSC/data';
    const res = await fetch(jsonUrl);
    const json = await res.json();

    const data = json.map(row => ({
        id: row.id,
        DloKann: row.DloKann,
        Feybanann: row.Feybanann
    }));

    // console.log(json);
    const csvData = objectToCsv(data);
    // console.log(csvData);
    download(csvData);
};

(function () {
    const button = document.querySelector("#myButton");
    button.addEventListener("click", getReport);
})();