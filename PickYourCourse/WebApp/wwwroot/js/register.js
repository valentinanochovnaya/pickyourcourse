document.getElementById("role").addEventListener("change", function () {
    console.log('here')
    if(document.getElementById("role").value === "Professor") {
        document.getElementById("year").style.display = "none";
        document.getElementById("third").style.justifyContent = "center"
    }
    else {
        document.getElementById("year").style.display = "block";
    }
});
