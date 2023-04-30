let el = document.getElementsByClassName("prof-btn");
const selectedCourse = {
    id: 0,
    Name: "",
    Description: ""
};

for(let i = 0; i < el.length; i++) {
    el[i].addEventListener("click", function (event) {
        selectedCourse.id = +event.target.id;
        selectedCourse.Name = event.target.innerText;
        selectedCourse.Description = event.target.getAttribute("description");
        
        document.getElementById("course_title").setAttribute("placeholder", selectedCourse.Name);
        document.getElementById("course_textarea").setAttribute("placeholder", selectedCourse.Description);

        document.getElementById("course_title").style.visibility = "visible";
        document.getElementById("course_textarea").style.visibility = "visible";

    });
}