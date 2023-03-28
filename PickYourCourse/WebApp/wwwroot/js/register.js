document.getElementById("role").addEventListener("change", function () {
    if(document.getElementById("role")?.value == 1) {
        document.getElementById("year").style.display = "none";
        document.getElementById("third").style.justifyContent = "center"
    }
    else {
        document.getElementById("year").style.display = "block";
    }
});
