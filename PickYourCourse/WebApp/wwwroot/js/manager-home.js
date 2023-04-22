let el = document.getElementsByClassName("prof-btn");
const selectedProfessor = {
    id: 0,
    email: "",
};
for(let i = 0; i < el.length; i++) {
    el[i].addEventListener("click", function (event) {
        selectedProfessor.id = +event.target.id;
        selectedProfessor.email = event.target.innerText;
        console.log(selectedProfessor)
        document.getElementById("pagination").style.display = "none";
        document.getElementById("btns").style.display = "flex";
    });
}
document.getElementById("cancel-prof").addEventListener("click", function () {
    document.getElementById("btns").style.display = "none";
    document.getElementById("pagination").style.display = "flex";
});

function professorAction(managersEmail, isApproval) {
    $.ajax({
        type: "POST",
        url: `ProfessorAction`,
        data: JSON.stringify({isApproval: isApproval, professorEmail: selectedProfessor.email, managerEmail: managersEmail}),
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        },
    }).done(function () {
        window.location.href = "ManagerHomePage?email=" + managersEmail;
    });
}
