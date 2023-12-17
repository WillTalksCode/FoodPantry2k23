// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

$('input[name=FirstOrLastName]').keyup(function () {
    var SearchString = $("#FirstOrLastName").val();
    $.ajax({
        dataType: "Html",
        type: "POST",
        url: $(this).data('peopleUrl'),
        data: { FirstOrLastName: SearchString },
        success: function (a) {
            // Replace the div's content with the page method's return.
            //alert("success");
            $('#peopleList').html(a);
        },
        error: function (jqXHR, textStatus, errorThrown) {
            alert(errorThrown)
        }
    });

});

function delay(fn, ms) {
    let timer = 0
    return function (...args) {
        clearTimeout(timer)
        timer = setTimeout(fn.bind(this, ...args), ms || 0)
    }
}






