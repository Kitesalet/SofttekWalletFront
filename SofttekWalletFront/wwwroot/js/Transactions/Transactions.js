var token = localStorage.getItem('token');

var id = localStorage.getItem('id');



$(document).ready(function () {

    $('#transferButton').click(function () {

        var confirmed = confirm("Do you want to complete the transfer?");

        if (confirmed == true) {
            $('form').submit();
        }
    })

})


