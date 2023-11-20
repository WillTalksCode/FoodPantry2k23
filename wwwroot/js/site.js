﻿// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
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


/*$('input[name=SearchName]').keyup(function () {
    var SearchString = $("#SearchName").val();
    $.ajax({
        dataType: "Html",
        type: "POST",
        url: $(this).data('houseHoldUrl'),
        data: { SearchName: SearchString },
        success: function (a) {
            // Replace the div's content with the page method's return.
            //alert("success");
            $('#peopleSearchResults').html(a);
        },
        error: function (jqXHR, textStatus, errorThrown) {
            alert(errorThrown)
        }
    });

}); */

