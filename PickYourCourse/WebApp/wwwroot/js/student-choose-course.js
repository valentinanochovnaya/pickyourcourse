let el = document.getElementsByClassName("course-btn");

let selectedCourse = {
    id: 0,
    profName: "",
    description: "",
    minScore: "",
}

for(let i = 0; i < el.length; i++) {
    el[i].addEventListener("click", function (event) {
        console.log(event)
        console.log(event.target.getAttribute("description"))
        selectedCourse.id = +event.target.id;
    });
}