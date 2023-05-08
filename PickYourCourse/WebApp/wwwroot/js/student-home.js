function redirectToCourses(email) {
    $.ajax({
        type: "POST",
        url: `ChooseCourseGeneral`,
        data: JSON.stringify(email),
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        },
    }).done(function () {
        window.location.href = "ChooseCourseGeneral?email=" + email;
    });
}