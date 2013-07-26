


$(document).ready(function () {

    $("[id$=btnHideSearch]").click(function () {
        $('#showButton').hide();
        //window.print();
        var ourUrl = $("#RedirectTo").val();
        window.location.href = ourUrl;
        //var x = '@Url.Action("Index", "UsersView")';
        //$.post('@Url.Action("Index", "UsersView")');
    });




});