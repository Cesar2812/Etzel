// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
<script>
function mostrarCamposRol() {
    const rol = document.getElementById("rol").value;

    document.getElementById("camposMipyme").style.display = (rol === "MIPYME") ? "block" : "none";
    document.getElementById("camposExperto").style.display = (rol === "Expert") ? "block" : "none";
}
</script>
function goToStep(step) {
    document.querySelectorAll(".form-step").forEach(el => el.classList.remove("active"));

    const rol = document.getElementById("rol").value;

    if (step === 1) {
        document.getElementById("step1").classList.add("active");
    } else if (step === 2) {
        if (rol === "MIPYME") {
            document.getElementById("step2-mipyme").classList.add("active");
        } else if (rol === "Expert") {
            document.getElementById("step2-expert").classList.add("active");
        } else {
            alert("Please select a role first.");
            document.getElementById("step1").classList.add("active");
        }
    }
}
