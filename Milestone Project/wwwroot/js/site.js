// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
$(function () {
    console.log("Page is ready");
    $(document).on("mousedown", ".game-button-image", function (e) {
        if (e.button == 2) {
            var id = $(this).val();
            console.log(id);
            flagButton(id, "/Minesweeper/FlagButton");
        }
    });
})

function flagButton(id, urlString)
{
    $.ajax(
        {
            datatype: 'json',
            method: 'POST',
            url: urlString,
            data: {
                "id": id
            },
            success: function (data) {
                console.log("Button number " + id + " was clicked");
                $("#" + id).html(data);
            },
            error: function (data) {
                console.log(data);
            }
        });
}
$(document).bind("contextmenu", function (e) {
    e.preventDefault();
});
