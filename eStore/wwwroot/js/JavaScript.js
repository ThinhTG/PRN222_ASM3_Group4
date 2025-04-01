window.saveAsFile = (fileName, base64Data) => {
    const link = document.createElement("a");
    link.href = "data:application/vnd.openxmlformats-officedocument.spreadsheetml.sheet;base64," + base64Data;
    link.download = fileName;
    document.body.appendChild(link);
    link.click();
    document.body.removeChild(link);
}

if (document.getElementById('orderChart')) {
    var ctx = document.getElementById('orderChart').getContext('2d');
    new Chart(ctx, { /* chart configuration */ });
} else {
}
