let starindex;
let Border;
var x = window.matchMedia("(max-width: 1070px)")
myFunction(x) // Call listener function at run time
x.addListener(myFunction) // Attach listener function on state changes
function myFunction(x) {
    if (x.matches) { // If media query matches
        starindex = 2;
        Border = 2;
    }
    else {
        /*az 4 start */
        starindex = 4;
        Border = 4;
    }
}

showcrousel(starindex);

function ArrowLink(n) {
    showcrousel(starindex += n)

}

function showcrousel(n) {


    var crousel = document.getElementsByClassName("crousel-item");

    if (n < Border) { starindex = crousel.length }
    if (n > crousel.length) { starindex = Border }
    n = starindex;
    let end = n;
    let first = n - Border;
    for (let i = 0; i < crousel.length; i++) {
        crousel[i].style.display = "none";
    }

    for (let i = first; i < end; i++) {
        crousel[i].style.display = "block";

    }
}