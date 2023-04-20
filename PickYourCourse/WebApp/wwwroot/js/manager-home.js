
let el = document.getElementsByClassName("prof-btn");
for(let i = 0; i < el.length; i++) {
    el[i].addEventListener("click", function () {
        document.getElementById("pagination").style.display = "none";
        document.getElementById("btns").style.display = "flex";
    });
}
document.getElementById("cancel-prof").addEventListener("click", function () {
    document.getElementById("btns").style.display = "none";
    document.getElementById("pagination").style.display = "flex";
});

