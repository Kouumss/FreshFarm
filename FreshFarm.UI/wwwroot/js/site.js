// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.


// script.js



// TOASTER
function showToaster() {
    const toaster = document.getElementById("toaster");
    const loadingBarContainer = document.getElementById("loading-bar-container");
    const loadingBar = document.getElementById("loading-bar");
    
    toaster.style.display = "block"; // Affiche le toaster
    loadingBarContainer.style.display = "block"; // Affiche la barre de chargement

    let width = 100; // Largeur initiale de la barre
    const interval = setInterval(() => {
        width -= 20; // Réduit la largeur de 20% toutes les secondes
        loadingBar.style.width = width + "%";

        // Après 5 secondes (ou 5 réductions), arrête l'intervalle
        if (width <= 0) {
            clearInterval(interval);
            toaster.style.display = "none"; // Cache le toaster
        }
    }, 1000); // Réduit toutes les 1 seconde
}

// Appelez cette fonction lorsque vous souhaitez afficher le toaster
showToaster();