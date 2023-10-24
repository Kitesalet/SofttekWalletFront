var token = localStorage.getItem('token');
var id = localStorage.getItem('id');

$(document).ready(function () {

    FillOriginDropdown();
    FillDestinationDropdown();
})

function FillDestinationDropdown() {
    $.ajax({
        url: `https://localhost:7243/api/accounts/${id}`,
        method: "GET",
        dataType: 'html',
        headers: {
            'Authorization': 'Bearer ' + token
        },
        success: function (response)
        {
            var select = $('#originAccountSelect');
            select.empty();

            var parsed = JSON.parse(response);
            parsed.data.forEach(function (item) {

                if (item.cbu) {
                    text ='Type - ' + item.type + ' | CBU - ' + item.cbu + ' | Balance - ' + item.balance
                } else {
                    text = 'Type - ' + item.type + ' | UUID - ' + item.uuid + ' | Balance - ' + item.balance
                }

                select.append($('<option>', {
                    value: item.id,
                    text: text
                }))
            }
            )
        }
    }
    )
}

var button = document.getElementById("submitButton");
button.addEventListener("click", TransferHandler)

function TransferHandler() {
    Swal.fire({
        title: 'You will make a transfer!',
        text: 'Do you want to proceed with the transfer?',
        icon: 'question',
        showCancelButton: true,
        confirmButtonText: 'Yes',
        cancelButtonText: 'No'
    }).then((result) => {
        if (result.isConfirmed) {
            var form = document.getElementById('transferForm');

            $.ajax({
                type: 'POST',
                url: 'BeginTransaction',
                data: $(form).serialize(),
                success: function (response) {
                    Swal.fire('Success', response, 'success');
                    setTimeout(function () {
                        location.reload();
                    }, 1000);
                },
                error: function (response) {
                    Swal.fire('Error', response.responseText, 'error');
                }
            });
        }
    });
}

function FillOriginDropdown() {
    $.ajax({
        url: `https://localhost:7243/api/accounts/${id}`,
        method: "GET",
        dataType: 'html',
        dataSrc: 'data',
        headers: { "Authorization": "Bearer " + token },
        success: function (response) {
            var select = $('#destinationAccountSelect');
            select.empty();

            var parsed = JSON.parse(response);
            parsed.data.forEach(function (item) {

                if (item.cbu) {
                    text = 'Type - ' + item.type + ' | CBU - ' + item.cbu + ' | Balance - ' + item.balance
                } else {
                    text = 'Type - ' + item.type + ' | UUID - ' + item.uuid + ' | Balance - ' + item.balance
                }

                select.append($('<option>', {
                    value: item.id,
                    text: text
                }))
            }
            )
        }
    }
    )
}


