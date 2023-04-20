document.getElementById("role").addEventListener("change", function () {
    console.log(document.getElementById("role").value);
    if(document.getElementById("role")?.value == 1 || document.getElementById("role")?.value == 2) {
        document.getElementById("year").style.display = "none";
        document.getElementById("third").style.justifyContent = "center"
    }
    else {
        document.getElementById("year").style.display = "block";
    }
});
