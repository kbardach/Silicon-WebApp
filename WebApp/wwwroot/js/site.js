
/*============================= HAMBURGER MENU =================================*/


document.addEventListener("DOMContentLoaded", () => {
    let btnMenu = document.querySelector(".btn-menu");
    let nav = document.querySelector("nav");
    let container = document.querySelector(".container");
    let accountButtons = document.querySelector(".account-buttons");
    let profile = document.querySelector(".profile");

    function toggleMenu() {
        btnMenu.classList.toggle("active");
        btnMenu.classList.toggle("fixed");
        nav.classList.toggle("active");
        container.classList.toggle("menu-active");

        if (profile) {
            profile.classList.toggle("active");
        }

        if (accountButtons) {
            accountButtons.classList.toggle("active");
        }
    }

    btnMenu.addEventListener("click", toggleMenu);

    window.addEventListener("resize", () => {
        btnMenu.classList.remove("active");
        btnMenu.classList.remove("fixed");
        nav.classList.remove("active");
        container.classList.remove("menu-active");

        if (profile && profile.classList.contains("active")) {
            profile.classList.remove("active");
        }

        if (accountButtons && accountButtons.classList.contains("active")) {
            accountButtons.classList.remove("active");
        }
    });
});
/*==============================================================================*/



/*============================= FORM VALIDATION ================================*/


let forms = document.querySelectorAll("form");
if (forms.length > 0) { // La till denna för att se om det minst finns ett fält att fylla i (även för dark/light mode)även för dark/light mode
    let inputs = forms[0].querySelectorAll("input");

    inputs.forEach(input => {
        if (input.dataset.val === "true") {
            input.addEventListener("keyup", (e) => {
                switch (e.target.type) {
                    case "text":
                        textValidation(e, e.target.dataset.valMinlengthMin);
                        break;
                    case "email":
                        emailValidation(e, e.target.dataset.valRegExPattern);
                        break;
                    case "password":
                        passwordValidation(e);
                        break;
                }
            });
        }
    });
}


const handleValidationOutput = (isValid, e, text = "") => {
    let span = document.querySelector(`[data-valmsg-for="${e.target.name}"]`)

    if (isValid) {
        e.target.classList.remove("input-validation-error")
        span.classList.remove("field-validation-error")
        span.classList.add("field-validation-valid")
        span.innerHTML = ""

    } else {
        e.target.classList.add("input-validation-error")
        span.classList.add("field-validation-error")
        span.classList.remove("field-validation-valid")
        span.innerHTML = text
    }
}


const textValidation = (e, minLength = 1) => {
    if (e.target.value.length > 0)
        handleValidationOutput(e.target.value.length >= minLength, e, e.target.dataset.valMinlength)
    else
        handleValidationOutput(false, e, e.target.dataset.valRequired)
}


const emailValidation = (e) => {
    const regEx = /^[a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+@[a-zA-Z0-9](?:[a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?(?:\.[a-zA-Z0-9](?:[a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?)*\.[a-zA-Z]{2,}$/

    if (e.target.value.length > 0)
        handleValidationOutput(regEx.test(e.target.value), e, e.target.dataset.valRegex)
    else
        handleValidationOutput(false, e, e.target.dataset.valRequired)
}


const passwordValidation = (e) => {
    const regEx = /^(?=.*[0-9])(?=.*[a-z])(?=.*[A-Z])(?=.*[\W_])(?!.*\s).{8,}$/

    let compareTo = document.querySelector(e.target.dataset.valEqualtoOther)

    if (e.target.value.length > 0)
        handleValidationOutput(regEx.test(e.target.value), e, e.target.dataset.valRegex)
    else
        handleValidationOutput(false, e, e.target.dataset.valRequired)
}
/*========================================================================================*/



/*============================ SWITCH-LIGHT-DARK-MODE ====================================*/


document.addEventListener("DOMContentLoaded", () => {
    const savedTheme = localStorage.getItem("theme") || "";
    document.documentElement.setAttribute("data-theme", savedTheme);

    const toggleButton = document.getElementById("theme-switch-mode");
    toggleButton.checked = savedTheme === "dark";

    toggleButton.addEventListener("click", () => {
        const currentTheme = document.documentElement.getAttribute("data-theme");
        const newTheme = currentTheme === "dark" ? '' : "dark";
        document.documentElement.setAttribute("data-theme", newTheme);
        localStorage.setItem("theme", newTheme);

        toggleButton.checked = newTheme === "dark";
    });
});
/*========================================================================================*/



/*================================= DROP-DOWN-MENU =======================================*/


document.addEventListener("DOMContentLoaded", function () {
    select()
    search()
    handleProfileImageUpload()
})

function select() {
    let select = document.querySelector(".select");
    let selected = select.querySelector(".selected");
    let selectOptions = select.querySelector(".select-options");
    let hiddenInput = document.getElementById("serviceInput");
    let isContactPage = document.body.classList.contains('contact-page');

    selected.addEventListener("click", function () {
        selectOptions.style.display = (selectOptions.style.display === "block") ? "none" : "block";
    });

    let options = selectOptions.querySelectorAll(".option");
    options.forEach(function (option) {
        option.addEventListener("click", function () {
            selected.innerHTML = this.textContent;
            selectOptions.style.display = "none";
            let category = this.getAttribute("data-value");

            if (isContactPage) {

                hiddenInput.value = category;
            }
            else {

                updateCoursesByFilter(category);
            }
        });
    });
}


/*FUNGERAR PÅ CONTACT*/
//function select() {
//    let select = document.querySelector(".select");
//    let selected = select.querySelector(".selected");
//    let selectOptions = select.querySelector(".select-options");
//    let hiddenInput = document.querySelector('input[name="Service"]');

//    selected.addEventListener("click", function () {
//        selectOptions.style.display = (selectOptions.style.display === "block") ? "none" : "block";
//    });

//    let options = selectOptions.querySelectorAll(".option");
//    options.forEach(function (option) {
//        option.addEventListener("click", function () {
//            selected.innerHTML = this.textContent;
//            selectOptions.style.display = "none";
//            let category = this.getAttribute("data-value");
//            hiddenInput.value = category; 
//        });
//    });
//}

/*FUNGERAR PÅ Courses*/
//function select() {
//    try {
//        let select = document.querySelector(".select")
//        let selected = select.querySelector(".selected")
//        let selectOptions = select.querySelector(".select-options")

//        selected.addEventListener("click", function () {
//            selectOptions.style.display = (selectOptions.style.display === "block") ? "none" : "block"
//        })

//        let options = selectOptions.querySelectorAll(".option")
//        options.forEach(function (option) {
//            option.addEventListener("click", function () {
//                selected.innerHTML = this.textContent
//                selectOptions.style.display = "none"
//                let category = this.getAttribute("data-value")

//                options.forEach(function (option) {
//                    option.addEventListener("click", function () {
//                        selected.innerHTML = this.textContent;
//                        selectOptions.style.display = "none";
//                        let category = this.getAttribute("data-value");
//                        document.querySelector('input[name="Service"]').value = category;
//                    })
//                })

//                updateCoursesByFilter(category)
//            })
//        })

//    }
//    catch { }
//}

/*========================================================================================*/


/*================================= SEARCH_QUERY =========================================*/

function search() {
    try {

        document.querySelector("#searchQuery").addEventListener("keyup", function () {
            const searchValue = this.value
            const category = document.querySelector(".select .selected").getAttribute("data-value") || "all"
            const url = `/courses/courses?category=${encodeURIComponent(category)}&searchQuery=${encodeURIComponent(searchValue)}`

            fetch(url)
                .then(res => res.text())
                .then(data => {
                    const parser = new DOMParser()
                    const dom = parser.parseFromString(data, 'text/html')
                    document.querySelector(".items").innerHTML = dom.querySelector(".items").innerHTML

                    const pagination = dom.querySelector(".pagination") ? dom.querySelector(".pagination").innerHTML : ""
                    document.querySelector(".pagination").innerHTML = pagination

                })
        })

    }
    catch { }
}

/*========================================================================================*/


/*================================= UPDATE-COURSES-BY-CATEGORY ===========================*/

function updateCoursesByFilter(category) {
    const searchValue = document.getElementById("searchQuery").value
    const url = `/courses/courses?category=${encodeURIComponent(category)}&searchQuery=${encodeURIComponent(searchValue)}`

    fetch(url)
        .then(res => res.text())
        .then(data => {
            const parser = new DOMParser()
            const dom = parser.parseFromString(data, "text/html")
            document.querySelector(".items").innerHTML = dom.querySelector(".items").innerHTML

            const pagination = dom.querySelector(".pagination") ? dom.querySelector(".pagination").innerHTML : ""
            document.querySelector(".pagination").innerHTML = pagination

        })
}


/*========================================================================================*/


/*================================= UPLOAD-PROFILE-PIC ===================================*/

function handleProfileImageUpload() {

    try {

        let fileUploader = document.querySelector("#fileUploader")
        if (fileUploader != undefined) {
            fileUploader.addEventListener("change", function () {

                if (this.files.length > 0) {
                    this.form.submit()
                }

            })
        }

    }
    catch { }

}

/*========================================================================================*/