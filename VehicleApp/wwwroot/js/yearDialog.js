// wwwroot/js/yearDialog.js
function openYearDialog(makeId, makeName) {
    document.getElementById("makeId").value = makeId;
    document.getElementById("makeName").textContent = makeName;
    document.getElementById("yearModal").style.display = "block";
}

function closeYearDialog() {
    document.getElementById("yearModal").style.display = "none";
}
