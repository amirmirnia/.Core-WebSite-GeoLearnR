window.addEventListener("resize", checkSize);

function checkSize() {
    if (window.innerWidth < 800) {
        alert("لطفا وضعیت صفحه گوشی خود را به افقی تغییر دهید.");
        document.getElementById("#alertportrait").style.display = "block";


        window.addEventListener("orientationchange", checkOrientation);

        function checkOrientation() {
            if (window.orientation == -90 || window.orientation == 90) {
                // این خط باعث می شود که صفحه وب دوباره قابل استفاده شود
                document.getElementById("#alertportrait").style.display = "none";
            }
                /*else {*/
            //    alert("ggg");

            //    document.body.style.pointerEvents = "none";

            //}
            // Initial execution if needed
            checkOrientation();
        }
    } else {
        // این خط باعث می شود که صفحه وب دوباره قابل استفاده شود
        document.body.style.pointerEvents = "auto";
    }
}