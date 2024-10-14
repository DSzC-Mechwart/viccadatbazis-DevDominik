// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
function like(id)
{

    var xhr = new XMLHttpRequest();
    xhr.withCredentials = true;

    xhr.addEventListener("readystatechange", function () {
        if (this.readyState === 4) {
            console.log(this.responseText);
        }
    });

    xhr.open("PATCH", "https://localhost:7193/api/Vicc/like/"+id);

    xhr.send();
}

function dislike(id) {

    var xhr = new XMLHttpRequest();
    xhr.withCredentials = true;

    xhr.addEventListener("readystatechange", function () {
        if (this.readyState === 4) {
            console.log(this.responseText);
            document.getElementById("tetszikDb").innerHTML = 
        }
    });

    xhr.open("PATCH", "https://localhost:7193/api/Vicc/dislike/" + id);

    xhr.send();
}