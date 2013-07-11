


$(document).ready(function () {

    $("[id$=btnHideSearch]").click(function () {
        $('#showButton').hide();
        window.print();
        window.open('/UsersView', 'UserView', null);
    });




});