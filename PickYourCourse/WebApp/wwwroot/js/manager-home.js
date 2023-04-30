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

document.getElementById('add-manager').addEventListener('click', function () {
    document.getElementById('add-manager-form').style.display = "flex";
    document.getElementById('add-manager').style.display = "none";
    document.getElementById('managers').style.display = "none";
    document.getElementById('pagination-managers').style.display = "none";

});

function addManager(managersEmail) {
    inputEmail = document.getElementById('email-input').value;
    console.log(inputEmail)
    $.ajax({
        type: "POST",
        url: `AddManager`,
        data: JSON.stringify({isApproval: false, professorEmail: inputEmail, managerEmail: managersEmail}),
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        },
        error: function(xhr) {
            if(xhr.status == 404) {
                var errorToast = Toastify({
                    text: "User not found",
                    duration: 3000,
                    callback: function(){
                        window.location.href = "ManagerHomePage?email=" + managersEmail;
                    }
                })
                console.log(errorToast)
                errorToast.showToast();
            }
        },
        success: function () {
            var successToast = Toastify({
                text: "Manager added",
                duration: 3000,
                callback: function(){
                    window.location.href = "ManagerHomePage?email=" + managersEmail;
                }
            })
            successToast.showToast();
        }
    });
}
